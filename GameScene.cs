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
        public GameScene(SceneManager sceneManager) : base(sceneManager)
        {
            Views.Add("gameView", new View(new FloatRect(0, 0, 158, 200)));
            Views["gameView"].Viewport = new FloatRect(0.25f, 0f, 0.5f, 1f);
            Views.Add("windowView", new View(new FloatRect(0, 0, 320, 200)));

            Player player = new Player(this);
            player.TargetView = Views["gameView"];
            EntityManager.Add(player);

            GameBorder borders = new GameBorder(this);
            borders.TargetView = Views["windowView"];
            EntityManager.Add(borders);
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

        public override void Draw()
        {
            SceneManager.Window.Clear(Color.Black);
            foreach (Entity entity in EntityManager.List())
            {
                entity.Draw();
            }
        }

        public void OnWindowClose(object sender, EventArgs eventArgs)
        {
            Environment.Exit(0);
        }
    }
}
