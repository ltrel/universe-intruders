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
        //const int WindowWidth = 1680;
        //const int WindowHeight = 1050;
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
        public static List<Text> Texts { get; }

        // Misc stuff
        public static Random Rand { get; private set; }
        public static Color BorderColor { get; set; }
        private static bool menu = true;
        private static bool debug = false;

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
            BorderColor = GameBorder.colors[0];
        }

        public static void Run()
        {
            MenuSetup();
            //LevelSetup();
            frameTimeClock = new Clock();
            // Game loop
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
        private static void RenderFrame()
        {
            window.Clear(Color.Black);
            window.SetView(windowView);
            foreach (Entity entity in Entities)
            {
                window.SetView(entity.TargetView);
                window.Draw(entity);

                // If the debug mode is on and the entity has a collider, show it with a rectangle
                if (debug && entity.CollisionRect != null) {
                RectangleShape collider = new RectangleShape();
                collider.Size = new Vector2f(entity.GetCollisionBounds().Width, entity.GetCollisionBounds().Height);
                collider.Position = new Vector2f(entity.GetCollisionBounds().Left, entity.GetCollisionBounds().Top);
                collider.FillColor = Color.Transparent;
                collider.OutlineThickness = 0.25f;
                collider.OutlineColor = Color.Cyan;
                window.Draw(collider);
                }
            }
            foreach (Text text in Texts) {
                window.SetView(window.DefaultView);
                window.Draw(text);
            }
            window.Display();
        }

        // Events
        private static void OnWindowClose(object sender, EventArgs eventArgs)
        {
            window.Close();
        }
        private static void OnKeyDown(object sender, KeyEventArgs eventArgs) {
            // If the menu is onscreen and space was pressed, start the game
            if(menu && eventArgs.Code == Keyboard.Key.Space) {
                // Wait for 500 milliseconds
                Clock delayClock = new Clock();
                const int delayTime = 500;
                while(delayClock.ElapsedTime.AsMilliseconds() < delayTime) {
                    window.DispatchEvents(); window.Display();
                }
                menu = false;
                LevelSetup();
            }
            // If the menu is onscreen and q was pressed, close the program
            if(menu && eventArgs.Code == Keyboard.Key.Q) {
                menu = false;
                window.Close();
            }
            // Toggle the debug view with f12
            if(eventArgs.Code == Keyboard.Key.F12) debug = !debug;
        }

        // Everything past this point is just placing objects and text
        private static void LevelSetup() {
            // Clear everything from the menu
            Texts.Clear();
            Entities.Clear();
            Entity background = new Entity(Resources.Textures["menubackground"], windowView);
            background.Depth = 20;
            background.Initialize();

            Player player = new Player(gameView);
            BackgroundTile backgroundTile = new BackgroundTile(gameView, new Vector2f(0f, 0f));
            backgroundTile.Initialize();
            GameBorder leftBorder = new GameBorder(true);
            GameBorder rightBorder = new GameBorder(false);
            leftBorder.Initialize();
            rightBorder.Initialize();
        }
        private static void MenuSetup() {
            Resources.MenuMusic.Play();
            Entity background = new Entity(Resources.Textures["menubackground"], windowView);
            background.Depth = 1;
            background.Initialize();
            Text title = new Text("Universe Intruders", Resources.Fonts["ibmbios"], 50);
            title.FillColor = Color.White;
            title.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - title.GetLocalBounds().Width/2,
                window.DefaultView.Size.Y * 0.16f - title.GetLocalBounds().Height/2);
            Texts.Add(title);
            Text highScore = new Text("High score: 0000", Resources.Fonts["ibmbios"], 32);
            highScore.FillColor = Color.White;
            highScore.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - highScore.GetLocalBounds().Width/2,
                window.DefaultView.Size.Y * 0.5f - highScore.GetLocalBounds().Height/2);
            Texts.Add(highScore);
            Text play = new Text("Press SPACE to start", Resources.Fonts["ibmbios"], 32);
            play.FillColor = Color.White;
            play.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - play.GetLocalBounds().Width/2,
                window.DefaultView.Size.Y * 0.5f - play.GetLocalBounds().Height/2 + play.CharacterSize*3);
            Texts.Add(play);
            Text quit = new Text("Or press Q to quit", Resources.Fonts["ibmbios"], 32);
            quit.FillColor = Color.White;
            quit.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - quit.GetLocalBounds().Width/2,
                window.DefaultView.Size.Y * 0.5f - quit.GetLocalBounds().Height/2 + quit.CharacterSize*6);
            Texts.Add(quit);
        }
    }
}