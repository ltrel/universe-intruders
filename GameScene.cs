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
        private View view = new View(new FloatRect(0, 0, 320, 200));
        private float fps;

        public GameScene()
        {
            Player player = new Player(view, this);
            EntityManager.AddEntity(player);
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
            foreach (Entity entity in EntityManager.ListEntities())
            {
                entity.Update(deltaTime);
            }
            fps = 1f / deltaTime;
            Console.WriteLine(fps);
        }

        public override void Draw(RenderWindow window)
        {
            window.Clear(Color.Black);
            foreach (Entity entity in EntityManager.ListEntities())
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
