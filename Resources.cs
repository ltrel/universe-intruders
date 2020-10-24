// Resources.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using System.IO;
using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;

namespace UniverseIntruders
{
    static class Resources
    {
        public static Dictionary<string, Texture> Textures { get; }
        public static Dictionary<string, Font> Fonts { get; }
        public static Dictionary<string, SoundBuffer> Sounds { get; }

        public static Image AppIcon { get; }
        public static Music MenuMusic { get; }

        static Resources() {
            string baseDir = Environment.CurrentDirectory;
            string resourceDir = Path.Join(baseDir, "Resources");

            Directory.SetCurrentDirectory(Path.Join(resourceDir, "Textures"));
            Textures = new Dictionary<string, Texture>();
            Textures.Add("player", new Texture("player.png"));
            Textures.Add("playerbullet", new Texture("playerbullet.png"));
            Textures.Add("enemy", new Texture("enemy.png"));
            Textures.Add("stars1", new Texture("stars1.png"));
            Textures.Add("stars2", new Texture("stars2.png"));
            Textures.Add("stars3", new Texture("stars3.png"));
            Textures.Add("stars4", new Texture("stars4.png"));
            Textures.Add("borders", new Texture("borders.png"));
            Textures.Add("menubackground", new Texture("menubackground.png"));

            Directory.SetCurrentDirectory(Path.Join(resourceDir, "Fonts"));
            Fonts = new Dictionary<string, Font>();
            Fonts.Add("ibmbios", new Font("Ac437_IBM_BIOS.ttf"));

            Directory.SetCurrentDirectory(Path.Join(resourceDir, "Sounds"));
            Sounds = new Dictionary<string, SoundBuffer>();
            Sounds.Add("playershoot", new SoundBuffer("shoot.ogg"));

            Directory.SetCurrentDirectory(baseDir);
            AppIcon = new Image("Resources\\icon.png");
            MenuMusic = new Music("Resources\\Sounds\\menu.ogg");
            MenuMusic.Volume = 10;
            MenuMusic.Loop = true;
        }

        public static void SoundCleanup() {
            MenuMusic.Dispose();
        }
    }
}