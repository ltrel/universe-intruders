// NewGame.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders
{
    class NewGame
    {
        private RenderWindow window;
        private TestScene testScene;
        private SceneManager sceneManager;

        public NewGame()
        {
            VideoMode mode = new VideoMode(1280, 720);
            window = new RenderWindow(mode, "Testing");
            window.SetFramerateLimit(60);

            testScene = new TestScene();

            sceneManager = new SceneManager();
            int sceneId = sceneManager.Add(testScene);
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
