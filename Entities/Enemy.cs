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
        public FloatRect Boundaries { get; set; }

        protected Vector2f startPosition { get; }
        public bool ReachedStartPosition { get; protected set; }
        protected float towardsStartSpeed { get; }

        public int ShootDelay { get; set; }
        protected int shootOffset;
        protected Clock shootClock;

        // Note: the CENTER of the sprite will end up at the start position
        public Enemy(Vector2f startPosition, bool moveToStart) : base(Resources.Textures["enemy"], Game.gameView)
        {
            ReachedStartPosition = false;
            CollisionTag = CollisionTag.Enemy;
            // Use top row of pixels for collision checking
            SetDefaultCollider();
            CollisionRect = new IntRect(0, 0, CollisionRect.Width, 1);
            MoveSpeed = 25f;
            towardsStartSpeed = 30f;
            Movements = new List<Vector2f>();
            movementIndex = 0;
            MoveDelay = 500;
            LoopMovements = true;
            Boundaries = new FloatRect(0, 0, TargetView.Size.X, TargetView.Size.Y + TextureRect.Height);
            ShootDelay = 800;

            // Offset the shooting of the enemy by a random number of milliseconds
            // so that all enemies will shoot at different times
            shootOffset = Game.Rand.Next(ShootDelay);

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
            Color = new Color(255, 255, 255, 60);
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

            // If time since last shot was greater than delay between last shot,
            // the starting offset has been passed, and the enemy is in the boundaries
            if (shootClock.ElapsedTime.AsMilliseconds() - shootOffset > ShootDelay &&
                Collision.IsPointInRect(Position, Boundaries))
                Shoot();

            // If starting position hasn't been reached yet, move towards it and do nothing else
            if (!ReachedStartPosition)
            {
                ReachedStartPosition = StepTowards(startPosition, towardsStartSpeed);
                if (ReachedStartPosition) Color = new Color(0xffffffff);
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

            // If a bit past the bottom of the screen trigger game over
            if (Position.Y - TextureRect.Height > TargetView.Size.Y)
                Game.GameOver = true;
        }

        // https://www.desmos.com/calculator/llcv91zhfi
        // Returns true if the destination has been reached
        protected bool StepTowards(Vector2f destination, float speed)
        {
            //if (destination == new Vector2f(0, 0)) return true;
            float realSpeed = speed * Game.FrameTime;
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
        // Using default speed
        protected bool StepTowards(Vector2f destination)
        {
            return StepTowards(destination, MoveSpeed);
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
            Game.SoundManager.Enqueue(shootSound);
            Vector2f position = Position + new Vector2f(TextureRect.Width / 2, 10);
            EnemyBullet bullet = new EnemyBullet(position);
            Game.EntityQueue.Enqueue(bullet);
            shootClock.Restart();
        }
    }
}
