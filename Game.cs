// Game.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders
{
    class Game
    {
        private RenderWindow window;
        private GameScene gameScene;
        private SceneManager sceneManager;

        public Game()
        {
            VideoMode mode = new VideoMode(1440, 900);
            window = new RenderWindow(mode, "Testing");
            window.SetFramerateLimit(60);
            window.SetKeyRepeatEnabled(false);

            gameScene = new GameScene();

            // Create a scene manager and give it access to the window's events
            sceneManager = new SceneManager();
            window.KeyPressed += sceneManager.EventHandlers.KeyPressed;
            window.Closed += sceneManager.EventHandlers.Closed;

            int sceneId = sceneManager.Add(gameScene);
            sceneManager.Enter(sceneId);
        }

        public void Run()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
                sceneManager.Update(1f / 60);
                sceneManager.Draw(window);
                window.Display();
            }
        }
    }
}
