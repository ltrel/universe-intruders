// LevelDesign.cs
// Created on: 2020-10-24
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    static partial class Game
    {
        // UI Elements
        public static Text ScoreText { get; set; }

        private static void LevelSetup()
        {
            // Clear everything from the menu
            Texts.Clear();
            Entities.Clear();
            Resources.MenuMusic.Stop();

            Resources.GameMusic.Play();
            Entity background = new Entity(Resources.Textures["menubackground"], windowView);
            background.Depth = 20;
            background.Initialize();
            BackgroundTile backgroundTile = new BackgroundTile(gameView, new Vector2f(0f, 0f));
            backgroundTile.Initialize();
            GameBorder leftBorder = new GameBorder(true);
            GameBorder rightBorder = new GameBorder(false);
            leftBorder.Initialize();
            rightBorder.Initialize();

            ScoreText = new Text($"SCORE: {Score.ToString("D4")}", Resources.Fonts["ibmbios"], 30);
            ScoreText.Position = new Vector2f(window.DefaultView.Size.X*0.75f + 32, 20);
            Texts.Add(ScoreText);

            Player player = new Player();
            Enemy enemy1 = new Enemy(new Vector2f(10f, 38));
            enemy1.Movements.Add(new Vector2f(138f, 0f));
            enemy1.Movements.Add(new Vector2f(0f, 10f));
            enemy1.Movements.Add(new Vector2f(-138f, 0f));
            enemy1.Movements.Add(new Vector2f(0f, 10f));
            enemy1.Initialize();
            Enemy enemy2 = new Enemy(new Vector2f(148f, 48f));
            enemy2.Movements.Add(new Vector2f(-138f, 0f));
            enemy2.Movements.Add(new Vector2f(0f, 10f));
            enemy2.Movements.Add(new Vector2f(138f, 0f));
            enemy2.Movements.Add(new Vector2f(0f, 10f));
            enemy2.Initialize();
            EnemyRandom enemy3 = new EnemyRandom(gameView.Center);
            enemy3.MinDistance = 5;
            enemy3.MinDistance = 50;
            enemy3.Initialize();
        }
        private static void MenuSetup()
        {
            Resources.MenuMusic.Play();
            Entity background = new Entity(Resources.Textures["menubackground"], windowView);
            background.Depth = 1;
            background.Initialize();
            Text title = new Text("Universe Intruders", Resources.Fonts["ibmbios"], 50);
            title.FillColor = Color.White;
            title.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - title.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.16f - title.GetLocalBounds().Height / 2);
            Texts.Add(title);
            Text highScore = new Text("High score: 0000", Resources.Fonts["ibmbios"], 32);
            highScore.FillColor = Color.White;
            highScore.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - highScore.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.5f - highScore.GetLocalBounds().Height / 2);
            Texts.Add(highScore);
            Text play = new Text("Press SPACE to start", Resources.Fonts["ibmbios"], 32);
            play.FillColor = Color.White;
            play.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - play.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.5f - play.GetLocalBounds().Height / 2 + play.CharacterSize * 3);
            Texts.Add(play);
            Text quit = new Text("Or press Q to quit", Resources.Fonts["ibmbios"], 32);
            quit.FillColor = Color.White;
            quit.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - quit.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.5f - quit.GetLocalBounds().Height / 2 + quit.CharacterSize * 6);
            Texts.Add(quit);
        }
    }
}