// Player.cs
// Created on: 2020-10-15
// Author: Leo Treloar

using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class Player : Entity
    {
        public float MoveSpeed { get; set; }

        public Player(View targetView) : base(Resources.Textures["player"], targetView)
        {
            Depth = 0;
            MoveSpeed = 60f;
            // Set position to the horizontal center of the screen near the bottom
            Position = TargetView.Center - new Vector2f(TextureRect.Width / 2, TextureRect.Height / 2);
            Position = new Vector2f(Position.X, TargetView.Size.Y - 10);
            Game.window.KeyPressed += OnKeyDown;
            // Just use the bottom two rows of pixels for collision detection
            SetDefaultCollider();
            CollisionRect = new IntRect(CollisionRect.Left, CollisionRect.Top+3, CollisionRect.Width, 2);
            Initialize();
        }
        public override void Update()
        {
            // Player movement
            Vector2f moveVector = new Vector2f();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                moveVector += new Vector2f(-MoveSpeed * Game.FrameTime, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                moveVector += new Vector2f(MoveSpeed * Game.FrameTime, 0);
            Position += moveVector;
            // Confine player to game view
            if (Position.X < 0)
                Position = new Vector2f(0, Position.Y);
            if (Position.X + TextureRect.Width > TargetView.Size.X)
                Position = new Vector2f(TargetView.Size.X - TextureRect.Width, Position.Y);
        }
        private void OnKeyDown(object sender, SFML.Window.KeyEventArgs keyEventArgs)
        {
            switch(keyEventArgs.Code) {
                // Player shooting
                case Keyboard.Key.Space:
                    Vector2f position = Position + new Vector2f(TextureRect.Width/2, -10);
                    PlayerBullet bullet = new PlayerBullet(position);
                    Game.EntityQueue.Enqueue(bullet);
                    break;
            }
        }
    }
}