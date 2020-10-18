// Game.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    static class Game
    {
        // Graphics stuff
        const int WindowWidth = 1280;
        const int WindowHeight = 800;
        const int FPSLimit = 60;
        static Clock frameTimeClock;
        // Private set means only this class change the value
        public static float FrameTime { get; private set; }
        static VideoMode videoMode = new VideoMode(WindowWidth, WindowHeight);
        public static RenderWindow window { get; }
        // Using view with low resolution scales everything up so the small sprites work
        static View windowView = new View(new FloatRect(0f, 0f, 320f, 200f));
        static View gameView = new View(new FloatRect(0f, 0f, 158f, 200f));

        // Entity stuff
        public static List<Entity> Entities { get; }
        public static Queue<Entity> EntityQueue { get; }

        // Misc stuff
        public static Random Rand { get; private set; }

        static Game()
        {
            window = new RenderWindow(videoMode, "Universe Intruders");
            window.SetFramerateLimit(FPSLimit);
            window.Closed += OnWindowClose;
            gameView.Viewport = new FloatRect(0.25f, 0f, 0.5f, 1f);
            // To start, assume the game is running at full frame rate
            FrameTime = 1f / FPSLimit;
            Entities = new List<Entity>();
            EntityQueue = new Queue<Entity>();
            Rand = new Random();
            Player player = new Player(gameView);
            BackgroundTile backgroundTile = new BackgroundTile(gameView, new Vector2f(0f, 0f));
            backgroundTile.Initialize();
        }
        public static void Run()
        {
            frameTimeClock = new Clock();
            while (window.IsOpen)
            {
                // Get the time since the last frame
                FrameTime = frameTimeClock.ElapsedTime.AsSeconds();
                frameTimeClock.Restart();

                window.DispatchEvents();
                AddQueuedEntities();
                ClearDestroyedEntities();
                UpdateEntities();
                RenderFrame();
            }
            return;
        }
        private static void OnWindowClose(object sender, EventArgs eventArgs)
        {
            window.Close();
        }
        private static void RenderFrame()
        {
            window.Clear(Color.Black);
            window.SetView(windowView);
            foreach (Entity entity in Entities)
            {
                window.SetView(entity.TargetView);
                window.Draw(entity);
            }
            window.Display();
        }
        private static void UpdateEntities()
        {
            foreach (Entity entity in Entities)
            {
                entity.Update();
            }
        }
        private static void AddQueuedEntities()
        {
            foreach (Entity entity in EntityQueue) {
                entity.Initialize();
            }
            EntityQueue.Clear();
        }
        private static void ClearDestroyedEntities() {
            Entities.RemoveAll(e => e.EntityDestroyed);
        }
    }
}