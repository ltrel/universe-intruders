// Game.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using System.IO;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    static partial class Game
    {
        // Graphics stuff
        //const int WindowWidth = 1280;
        //const int WindowHeight = 800;
        const int WindowWidth = 1440;
        const int WindowHeight = 900;
        const int FPSLimit = 90;
        const float MaxFrameTime = 1f;
        private static Clock frameTimeClock;
        // Private set means only this class can change the value
        public static float FrameTime { get; private set; }
        static VideoMode videoMode = new VideoMode(WindowWidth, WindowHeight);
        public static RenderWindow window { get; }
        // Using view with low resolution scales everything up so the small sprites work
        public static View windowView { get; private set; }
        public static View gameView { get; private set; }

        // Entity stuff
        public static List<Entity> Entities { get; }
        public static Queue<Entity> EntityQueue { get; }
        public static List<Text> Texts { get; }

        // Game stuff
        public static int Score { get; set; }
        private static int waveDelay = 5000;
        private static bool waitingForWave = false;
        private static Clock waveClock;
        public static bool GameOver { get; set; }
        private static bool waitingForGameOver = false;
        private static Clock timeSinceGameOver;
        private static int gameOverScreenDelay = 3000;

        // Misc stuff
        public static Random Rand { get; private set; }
        public static Color BorderColor { get; set; }
        private static bool menu = true;
        private static bool inGame;
        private static bool debug = false;
        private static string scoreFile = "highscore";

        static Game()
        {
            // Window
            window = new RenderWindow(videoMode, "Universe Intruders");
            window.SetIcon(Resources.AppIcon.Size.X, Resources.AppIcon.Size.Y, Resources.AppIcon.Pixels);
            window.SetFramerateLimit(FPSLimit);
            window.SetKeyRepeatEnabled(false);
            window.Closed += OnWindowClose;
            window.KeyPressed += OnKeyDown;
            // Views
            windowView = new View(new FloatRect(0f, 0f, 320f, 200f));
            gameView = new View(new FloatRect(0f, 0f, 158f, 200f));
            gameView.Viewport = new FloatRect(0.25f, 0f, 0.5f, 1f);
            // To start, assume the game is running at full frame rate
            FrameTime = 1f / FPSLimit;
            // Entities
            Entities = new List<Entity>();
            EntityQueue = new Queue<Entity>();
            Texts = new List<Text>();
            // Misc
            Rand = new Random();
            BorderColor = GameBorder.Colors[0];

            GameOver = false;
        }

        public static void Run()
        {
            MenuSetup();
            frameTimeClock = new Clock();
            waveClock = new Clock();
            timeSinceGameOver = new Clock();

            // Game loop
            while (window.IsOpen)
            {
                // Get the time since the last frame and limit it to the maximum
                // Comment this out if debugging
                FrameTime = Math.Clamp(frameTimeClock.ElapsedTime.AsSeconds(), 0f, MaxFrameTime);
                frameTimeClock.Restart();

                window.DispatchEvents();
                UpdateEntities();
                RenderFrame();
                GameLogic();
            }
            return;
        }

        private static void UpdateEntities()
        {
            // Run entity update functions
            foreach (Entity entity in Entities)
            {
                entity.Update();
            }
            // Add queued entities
            while (EntityQueue.Count > 0)
            {
                EntityQueue.Dequeue().Initialize();
            }
            // Remove destroyed entities
            Entities.RemoveAll(e => e.EntityDestroyed);
        }
        private static void RenderFrame()
        {
            window.Clear(Color.Black);
            window.SetView(windowView);
            foreach (Entity entity in Entities)
            {
                window.SetView(entity.TargetView);
                window.Draw(entity);

                // If the debug mode is on and the entity has a collider, show it with a rectangle
                if (debug && entity.CollisionRect != null)
                {
                    RectangleShape collider = new RectangleShape();
                    collider.Size = new Vector2f(entity.GetCollisionBounds().Width, entity.GetCollisionBounds().Height);
                    collider.Position = new Vector2f(entity.GetCollisionBounds().Left, entity.GetCollisionBounds().Top);
                    collider.FillColor = Color.Transparent;
                    collider.OutlineThickness = 0.25f;
                    collider.OutlineColor = Color.Cyan;
                    window.Draw(collider);
                }
            }
            // Use the window's default full resolution view for text rendering
            window.SetView(window.DefaultView);
            foreach (Text text in Texts)
            {
                window.Draw(text);
            }
            window.Display();
        }

        // Events
        private static void OnWindowClose(object sender, EventArgs eventArgs)
        {
            Resources.SoundCleanup();
            window.Close();
        }
        private static void OnKeyDown(object sender, KeyEventArgs eventArgs)
        {
            // If the menu is onscreen and space was pressed, start the game
            if (menu && eventArgs.Code == Keyboard.Key.Space)
            {
                // Wait for 500 milliseconds
                Clock delayClock = new Clock();
                const int delayTime = 500;
                while (delayClock.ElapsedTime.AsMilliseconds() < delayTime)
                {
                    window.DispatchEvents(); window.Display();
                }
                menu = false;
                inGame = true;
                LevelSetup();
            }
            // If the menu is onscreen and q was pressed, close the program
            if (menu && eventArgs.Code == Keyboard.Key.Q)
            {
                menu = false;
                Resources.SoundCleanup();
                window.Close();
            }
            // Toggle the debug view with f12
            if (eventArgs.Code == Keyboard.Key.F12) debug = !debug;
        }

        // Game logic
        public static void UpdateScoreText()
        {
            ScoreText.DisplayedString = $"SCORE: {Score.ToString("D4")}";
        }

        public static void GameLogic()
        {
            // If menu or game over screen is open skip this stuff
            if (inGame)
            {
                // If there are no enemies and not waiting for next wave
                if (Entities.FindIndex(e => e is Enemy) == -1 && !waitingForWave)
                {
                    waitingForWave = true;
                    waveClock.Restart();
                    GameBorder.NextColor();
                    Console.WriteLine("NEXT WAVE INBOUND");
                }
                else if (waitingForWave && waveClock.ElapsedTime.AsMilliseconds() > waveDelay)
                {
                    waitingForWave = false;
                    Console.WriteLine("STARTING NEXT WAVE");
                    EnemyRandom enemy3 = new EnemyRandom(gameView.Center, false);
                    enemy3.MinDistance = 5;
                    enemy3.MinDistance = 50;
                    enemy3.Initialize();
                }
            }

            // If the game is over and not already waiting for the game over screen
            if (GameOver && !waitingForGameOver)
            {
                timeSinceGameOver.Restart();
                waitingForGameOver = true;
                Console.WriteLine("GAME OVER");
            }

            // If waiting for the game over screen and enough time has passed
            if (waitingForGameOver && timeSinceGameOver.ElapsedTime.AsMilliseconds() > gameOverScreenDelay)
            {
                GameOver = false;
                waitingForGameOver = false;
                inGame = false;
                SaveScore();
                GameOverScreenSetup();
            }
        }

        // Scores are saved as binary files to make it slightly harder for people to edit them
        private static void SaveScore()
        {
            if (Score > GetHighScore())
            {
                BinaryWriter writer = new BinaryWriter(File.Open(scoreFile, FileMode.Create));
                writer.Write(Score);
                writer.Close();
            }
        }

        private static int GetHighScore()
        {
            if (!File.Exists(scoreFile)) return 0;
            BinaryReader reader = new BinaryReader(File.Open(scoreFile, FileMode.Open));
            int result = reader.ReadInt32();
            reader.Close();
            return result;
        }
    }
}