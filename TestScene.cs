// TestScene.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders
{
    class TestScene : Scene
    {
        private CircleShape circle = new CircleShape(10);
        private float speed = 100;
        private Vector2f moveVector;

        public TestScene()
        {
            circle.FillColor = new Color(255, 255, 0);
            circle.Position = new Vector2f(20, 20);
        }

        public override void OnEnter()
        {
            EventHandlers.KeyPressed += OnKeyDown;
            EventHandlers.Closed += OnWindowClose;
        }

        public override void OnExit()
        {
        }

        public override void Update(float deltaTime)
        {
            circle.Position += moveVector * deltaTime;
        }

        public override void Draw(RenderWindow window)
        {
            window.Clear(Color.Black);
            window.Draw(circle);
        }

        public void OnKeyDown(object sender, KeyEventArgs eventArgs)
        {
            switch (eventArgs.Code)
            {
                case Keyboard.Key.Left:
                    moveVector = new Vector2f(-speed, 0);
                    break;
                case Keyboard.Key.Right:
                    moveVector = new Vector2f(speed, 0);
                    break;
                case Keyboard.Key.Up:
                    moveVector = new Vector2f(0, -speed);
                    break;
                case Keyboard.Key.Down:
                    moveVector = new Vector2f(0, speed);
                    break;
            }
        }

        public void OnWindowClose(object sender, EventArgs eventArgs)
        {
            Environment.Exit(0);
        }
    }
}
