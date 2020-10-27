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

        // List of waves
        private static List<List<Enemy>> waveList = new List<List<Enemy>>();
        private static int currentWave = 0;

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

            ScoreText = new Text($"SCORE: {Score.ToString("D4")}", Resources.Fonts["ibmbios"], 34);
            ScoreText.Position = new Vector2f(window.DefaultView.Size.X * 0.75f + 32, 20);
            Texts.Add(ScoreText);

            Player player = new Player();
            
            // Start the first wave
            SpawnNextWave();
        }
        private static void MenuSetup()
        {
            Resources.MenuMusic.Play();
            Entity background = new Entity(Resources.Textures["menubackground"], windowView);
            background.Depth = 1;
            background.Initialize();
            Text title = new Text("Universe Intruders", Resources.Fonts["ibmbios"], 60);
            title.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - title.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.16f - title.GetLocalBounds().Height / 2);
            Texts.Add(title);
            Text highScore = new Text($"High Score: {GetHighScore().ToString("D4")}", Resources.Fonts["ibmbios"], 32);
            highScore.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - highScore.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.45f - highScore.GetLocalBounds().Height / 2);
            Texts.Add(highScore);
            Text play = new Text("Press SPACE to start", Resources.Fonts["ibmbios"], 32);
            play.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - play.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.45f - play.GetLocalBounds().Height / 2 + play.CharacterSize * 3);
            Texts.Add(play);
            Text quit = new Text("Or press Q to quit", Resources.Fonts["ibmbios"], 32);
            quit.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - quit.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.45f - quit.GetLocalBounds().Height / 2 + quit.CharacterSize * 6);
            Texts.Add(quit);
        }

        private static void GameOverScreenSetup()
        {
            // Clear everything from the game
            Texts.Clear();
            Entities.Clear();
            Resources.GameMusic.Stop();

            Resources.MenuMusic.Play();
            Entity background = new Entity(Resources.Textures["menubackground"], windowView);
            background.Depth = 1;
            background.Initialize();
            Text gameOver = new Text("GAME OVER", Resources.Fonts["ibmbios"], 70);
            gameOver.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - gameOver.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.25f - gameOver.GetLocalBounds().Height / 2);
            Texts.Add(gameOver);
            Text score = new Text($"Score: {Score.ToString("D4")}", Resources.Fonts["ibmbios"], 36);
            score.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - score.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.40f - score.GetLocalBounds().Height / 2);
            Texts.Add(score);
            Text highScore = new Text($"High Score: {GetHighScore().ToString("D4")}", Resources.Fonts["ibmbios"], 36);
            highScore.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - highScore.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.40f - highScore.GetLocalBounds().Height / 2 + highScore.CharacterSize * 3);
            Texts.Add(highScore);
            Text restart = new Text("Press SPACE to restart", Resources.Fonts["ibmbios"], 36);
            restart.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - restart.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.40f - restart.GetLocalBounds().Height / 2 + restart.CharacterSize * 6);
            Texts.Add(restart);
            Text quit = new Text("Or press Q to quit", Resources.Fonts["ibmbios"], 36);
            quit.Position = new Vector2f(
                window.DefaultView.Size.X / 2 - quit.GetLocalBounds().Width / 2,
                window.DefaultView.Size.Y * 0.40f - quit.GetLocalBounds().Height / 2 + quit.CharacterSize * 9);
            Texts.Add(quit);
        }

        private static void GenerateWaves()
        {
            {
                // Wave 1
                List<Enemy> wave1 = new List<Enemy>();
                Enemy enemy1 = new Enemy(new Vector2f(10f, 38), true);
                enemy1.Movements.Add(new Vector2f(138f, 0f));
                enemy1.Movements.Add(new Vector2f(0f, 10f));
                enemy1.Movements.Add(new Vector2f(-138f, 0f));
                enemy1.Movements.Add(new Vector2f(0f, 10f));
                wave1.Add(enemy1);
                Enemy enemy2 = new Enemy(new Vector2f(148f, 48f), true);
                enemy2.Movements.Add(new Vector2f(-138f, 0f));
                enemy2.Movements.Add(new Vector2f(0f, 10f));
                enemy2.Movements.Add(new Vector2f(138f, 0f));
                enemy2.Movements.Add(new Vector2f(0f, 10f));
                wave1.Add(enemy2);
                EnemyRandom enemy3 = new EnemyRandom(gameView.Center, true);
                enemy3.MinDistance = 5;
                enemy3.MinDistance = 50;
                wave1.Add(enemy3);
                waveList.Add(wave1);
            }
        }

        private static void SpawnNextWave() {
            foreach(Enemy enemy in waveList[currentWave++]) {
                enemy.Initialize();
            }
        }
    }
}