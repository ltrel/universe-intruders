// Enemy.cs
// Created on: 2020-10-21
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class Enemy : Entity
    {
        public float MoveSpeed { get; set; }

        public List<Vector2f> Movements { get; protected set; }
        public bool LoopMovements { get; set; }
        protected int movementIndex { get; set; }
        protected Vector2f currentDestination { get; set; }
        public int MoveDelay { get; set; }
        protected Clock moveClock;
        protected bool isMoving = true;

        protected Vector2f startPosition { get; }
        protected bool reachedStartPosition = false;

        public int shootDelay { get; set; }
        protected int shootOffset;
        protected Clock shootClock;

        // Note: the CENTER of the sprite will end up at the start position
        public Enemy(Vector2f startPosition, bool moveToStart) : base(Resources.Textures["enemy"], Game.gameView)
        {
            CollisionTag = CollisionTag.Enemy;
            // Use top row of pixels for collision checking
            SetDefaultCollider();
            CollisionRect = new IntRect(0, 0, CollisionRect.Width, 1);
            MoveSpeed = 25f;
            Movements = new List<Vector2f>();
            movementIndex = 0;
            MoveDelay = 500;
            LoopMovements = true;
            shootDelay = 700;

            // Offset the shooting of the enemy by a random number of milliseconds
            // so that all enemies will shoot at different times
            shootOffset = Game.Rand.Next(shootDelay);

            // Offset slightly for center of sprite
            startPosition -= new Vector2f(TextureRect.Width / 2, TextureRect.Height / 2);
            this.startPosition = startPosition;

            // Actually spawn the entity off-screen, either to the left or right depending on what's closer
            if (!moveToStart)
                Position = startPosition;
            else if (startPosition.X > TargetView.Size.X / 2)
            {
                Position = new Vector2f(startPosition.X + TargetView.Size.X / 2 + 20, startPosition.Y);
            }
            else
            {
                Position = new Vector2f(startPosition.X - TargetView.Size.X / 2 - 20, startPosition.Y);
            }
        }

        public override void Initialize()
        {
            // If there are movements in the list,
            // calculate where the first one will leave the enemy
            if (Movements.Count > 0)
            {
                currentDestination = startPosition + Movements[movementIndex];
            }
            else currentDestination = startPosition;
            moveClock = new Clock();
            shootClock = new Clock();
            base.Initialize();
        }

        public override void Update()
        {

            // If time since last shot was greater than delay between last shot
            // and the starting offset has been passed
            if(shootClock.ElapsedTime.AsMilliseconds() - shootOffset > shootDelay )
                Shoot();

            // If starting position hasn't been reached yet, move towards it and do nothing else
            if (!reachedStartPosition)
            {
                reachedStartPosition = StepTowards(startPosition);
                return;
            }

            // Take a step towards the current destination, if already there
            // run everything inside the if statement
            if (isMoving)
            {
                if (StepTowards(currentDestination))
                {
                    isMoving = false;
                    moveClock.Restart();
                    SetNextDestination();
                }
            }
            // If not moving and past delay between movements, start moving again
            else if (moveClock.ElapsedTime.AsMilliseconds() >= MoveDelay) isMoving = true;
        }

        // https://www.desmos.com/calculator/llcv91zhfi
        // Returns true if the destination has been reached
        protected bool StepTowards(Vector2f destination)
        {
            //if (destination == new Vector2f(0, 0)) return true;
            float realSpeed = MoveSpeed * Game.FrameTime;
            float distance = (float)Math.Sqrt(
                (destination.X - Position.X) * (destination.X - Position.X) +
                (destination.Y - Position.Y) * (destination.Y - Position.Y)
            );
            if (distance <= realSpeed)
            {
                Position = destination;
            }
            else
            {
                Vector2f step = new Vector2f(
                    (destination.X - Position.X) / distance * realSpeed,
                    (destination.Y - Position.Y) / distance * realSpeed
                );
                Position += step;
            }

            return Position == destination;
        }

        protected virtual void SetNextDestination()
        {
            // If there is another movement left in the list, set the destination to that
            if (movementIndex < Movements.Count - 1)
                currentDestination = Position + Movements[++movementIndex];
            // If there isn't another movement but looping is on, go back to the first movement
            else if (LoopMovements && Movements.Count > 0)
            {
                movementIndex = 0;
                currentDestination = Position + Movements[0];
            }
            // If loop is off, the enemy will remain at it's last destination
            // and the two if statements above will continue
            // wasting cpu instructions for all eternity
        }

        protected virtual void Shoot()
        {
            Sound shootSound = new Sound(Resources.Sounds["enemyshoot"]);
            shootSound.Play();
            Vector2f position = Position + new Vector2f(TextureRect.Width/2, 10);
            EnemyBullet bullet = new EnemyBullet(position);
            Game.EntityQueue.Enqueue(bullet);
            shootClock.Restart();
        }
    }
}