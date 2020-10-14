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
        static VideoMode videoMode = new VideoMode(WindowWidth, WindowHeight);
        static RenderWindow window = new RenderWindow(videoMode, "Universe Intruders");
        public static List<Entity> Entities { get; }
        static Game()
        {
            window.Closed += OnWindowClose;
            Entities = new List<Entity>();
        }
        public static void Run()
        {
            while (window.IsOpen)
            {
                window.DispatchEvents();
            }
            return;
        }
        private static void OnWindowClose(object sender, EventArgs eventArgs)
        {
            window.Close();
        }
    }
}