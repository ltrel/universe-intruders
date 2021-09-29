// GameScene.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders
{
    class GameScene : Scene
    {
        public GameScene()
        {
            DefaultView = new View(new FloatRect(0,0, 320, 200));
            Player player = new Player(this);
            EntityManager.Add(player);
        }

        public override void OnEnter()
        {
            EventHandlers.Closed += OnWindowClose;
        }

        public override void OnExit()
        {
        }

        public override void Update(float deltaTime)
        {
            foreach (Entity entity in EntityManager.List())
            {
                entity.Update(deltaTime);
            }
        }

        public override void Draw(RenderWindow window)
        {
            window.Clear(Color.Black);
            foreach (Entity entity in EntityManager.List())
            {
                entity.Draw(window);
            }
        }

        public void OnWindowClose(object sender, EventArgs eventArgs)
        {
            Environment.Exit(0);
        }
    }
}
