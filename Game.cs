// Game.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders {
    static class Game {
        const int WindowWidth = 1280;
        const int WindowHeight = 800;
        const int FPSLimit = 60;
        static VideoMode videoMode = new VideoMode(WindowWidth,WindowHeight);
        static RenderWindow window = new RenderWindow(videoMode,"Universe Intruders");
        public static void Run() {
            CreateEventHandlers();
            while(window.IsOpen) {
                window.DispatchEvents();
            }
            return;
        }
        private static void OnWindowClose(object sender, EventArgs eventArgs) {
            window.Close();
        }
        private static void CreateEventHandlers() {
            window.Closed += OnWindowClose;
        } 
    }
}