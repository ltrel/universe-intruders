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

        public Player(View targetView) : base(Resources.Player, targetView)
        {
            MoveSpeed = 50f;
            Position = targetView.Center - new Vector2f(TextureRect.Width / 2, TextureRect.Height / 2);
            Position = new Vector2f(Position.X, targetView.Size.Y - 10);
            Game.window.KeyPressed += OnKeyDown;
            Initialize();
        }
        public override void Update()
        {
            Vector2f moveVector = new Vector2f();
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
                moveVector += new Vector2f(-MoveSpeed*Game.FrameTime, 0);
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
                moveVector += new Vector2f(MoveSpeed*Game.FrameTime, 0);
            Position += moveVector;
        }
        private void OnKeyDown(object sender, SFML.Window.KeyEventArgs keyEventArgs)
        {

        }
    }
}