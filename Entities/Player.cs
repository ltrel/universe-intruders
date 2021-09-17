// Player.cs
// Created on: 2020-10-15
// Author: Leo Treloar

using System;
using System.Linq;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class Player : Entity
    {
        public float MoveSpeed { get; set; }

        private Clock shootClock;
        private int shootTime = 400;

        public Player(View view, Scene scene) : base(Resources.Textures["player"], view, scene)
        {
            CollisionTag = CollisionTag.Player;
            Depth = 0;
            MoveSpeed = 60f;
            // Set position to the horizontal center of the screen near the bottom
            Position = TargetView.Center - new Vector2f(TextureRect.Width / 2, TextureRect.Height / 2);
            Position = new Vector2f(Position.X, TargetView.Size.Y - 10);
            EventHandlers.KeyPressed += OnKeyDown;
            // Just use the bottom two rows of pixels for collision detection
            SetDefaultCollider();
            CollisionRect = new IntRect(CollisionRect.Left, CollisionRect.Top + 3, CollisionRect.Width, 2);
            Initialize();
        }
        public override void Initialize()
        {
            shootClock = new Clock();
            base.Initialize();
        }
        public override void Update(float deltaTime)
        {
            // Player movement
            Vector2f moveVector = new Vector2f();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                moveVector += new Vector2f(-MoveSpeed * deltaTime, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                moveVector += new Vector2f(MoveSpeed * deltaTime, 0);
            Position += moveVector;
            // Confine player to game view
            if (Position.X < 0)
                Position = new Vector2f(0, Position.Y);
            if (Position.X + TextureRect.Width > TargetView.Size.X)
                Position = new Vector2f(TargetView.Size.X - TextureRect.Width, Position.Y);
        }
        private void OnKeyDown(object sender, SFML.Window.KeyEventArgs keyEventArgs)
        {
            switch (keyEventArgs.Code)
            {
                // Player shooting
                case Keyboard.Key.Space:
                    if (shootClock.ElapsedTime.AsMilliseconds() >= shootTime)
                    {
                        Sound shootSound = new Sound(Resources.Sounds["playershoot"]);
                        Scene.SoundManager.Play(shootSound);
                        Vector2f position = Position + new Vector2f(TextureRect.Width / 2, -10);
                        PlayerBullet bullet = new PlayerBullet(position, TargetView, Scene);
                        Scene.EntityManager.Add(bullet);
                        shootClock.Restart();
                    }
                    break;
                case Keyboard.Key.Backslash:
                    Destroy();
                    break;
            }
        }
    }
}
