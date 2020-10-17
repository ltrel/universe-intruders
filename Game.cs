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
        const int WindowWidth = 1280;
        const int WindowHeight = 800;
        const int FPSLimit = 60;
        static Clock frameTimeClock;
        public static float FrameTime { get; private set; }
        static VideoMode videoMode = new VideoMode(WindowWidth, WindowHeight);
        static RenderWindow window = new RenderWindow(videoMode, "Universe Intruders");
        static View windowView = new View(new FloatRect(0f,0f,320f,200f));
        public static List<Entity> Entities { get; }
        static Game()
        {
            window = new RenderWindow(videoMode, "Universe Intruders");
            window.Closed += OnWindowClose;
            FrameTime = 1f / FPSLimit;
            Entities = new List<Entity>();
        }
        public static void Run()
        {
            frameTimeClock = new Clock();
            while (window.IsOpen)
            {
                FrameTime = frameTimeClock.ElapsedTime.AsSeconds();
                frameTimeClock.Restart();
                window.DispatchEvents();
                RenderFrame();
            }
            return;
        }
        private static void OnWindowClose(object sender, EventArgs eventArgs)
        {
            window.Close();
        }
        private static void RenderFrame() {
            window.Clear(Color.Black);
            window.SetView(windowView);
            foreach(Entity entity in Entities) {
                window.Draw(entity);
            }
            window.Display();
        }
    }
}