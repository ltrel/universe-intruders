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

        public static List<Entity> Entities { get; }

        static Game()
        {
            window = new RenderWindow(videoMode, "Universe Intruders");
            window.SetFramerateLimit(FPSLimit);
            window.Closed += OnWindowClose;
            // To start, assume the game is running at full frame rate
            FrameTime = 1f / FPSLimit;
            Entities = new List<Entity>();
            Player player = new Player(windowView);
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
    }
}