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
        public static View windowView { get; private set; }
        public static View gameView { get; private set; }

        // Entity stuff
        public static List<Entity> Entities { get; }
        public static Queue<Entity> EntityQueue { get; }

        // Misc stuff
        public static Random Rand { get; private set; }
        public static Color BorderColor { get; set; }

        static Game()
        {
            // Window
            window = new RenderWindow(videoMode, "Universe Intruders");
            window.SetFramerateLimit(FPSLimit);
            window.Closed += OnWindowClose;
            // Views
            windowView = new View(new FloatRect(0f, 0f, 320f, 200f));
            gameView = new View(new FloatRect(0f, 0f, 158f, 200f));
            gameView.Viewport = new FloatRect(0.25f, 0f, 0.5f, 1f);
            // To start, assume the game is running at full frame rate
            FrameTime = 1f / FPSLimit;
            // Entities
            Entities = new List<Entity>();
            EntityQueue = new Queue<Entity>();
            // Misc
            Rand = new Random();
            BorderColor = GameBorder.colors[0];
            // Level
            Player player = new Player(gameView);
            BackgroundTile backgroundTile = new BackgroundTile(gameView, new Vector2f(0f, 0f));
            backgroundTile.Initialize();
            GameBorder leftBorder = new GameBorder(true);
            GameBorder rightBorder = new GameBorder(false);
            leftBorder.Initialize();
            rightBorder.Initialize();
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
            // Run entity update functions
            foreach (Entity entity in Entities)
            {
                entity.Update();
            }
            // Add queued entities
            while (EntityQueue.Count > 0) {
                EntityQueue.Dequeue().Initialize();
            }
            // Remove destroyed entities
            Entities.RemoveAll(e => e.EntityDestroyed);
        }
    }
}