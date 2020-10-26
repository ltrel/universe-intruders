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
        public static Music GameMusic { get; }

        static Resources() {
            string baseDir = Environment.CurrentDirectory;
            string resourceDir = Path.Join(baseDir, "Resources");

            // Move into the relevant directory
            Directory.SetCurrentDirectory(Path.Join(resourceDir, "Textures"));
            Textures = new Dictionary<string, Texture>();
            // Add everything to the dictionary
            Textures.Add("player", new Texture("player.png"));
            Textures.Add("enemy", new Texture("enemy.png"));
            Textures.Add("playerbullet", new Texture("playerbullet.png"));
            Textures.Add("enemybullet", new Texture("enemybullet.png"));
            Textures.Add("stars1", new Texture("stars1.png"));
            Textures.Add("stars2", new Texture("stars2.png"));
            Textures.Add("stars3", new Texture("stars3.png"));
            Textures.Add("stars4", new Texture("stars4.png"));
            Textures.Add("borders", new Texture("borders.png"));
            Textures.Add("menubackground", new Texture("menubackground.png"));

            // Repeat for everything else
            Directory.SetCurrentDirectory(Path.Join(resourceDir, "Fonts"));
            Fonts = new Dictionary<string, Font>();
            Fonts.Add("ibmbios", new Font("Ac437_IBM_BIOS.ttf"));

            Directory.SetCurrentDirectory(Path.Join(resourceDir, "Sounds"));
            Sounds = new Dictionary<string, SoundBuffer>();
            Sounds.Add("playershoot", new SoundBuffer("shoot.ogg"));
            Sounds.Add("enemyshoot", new SoundBuffer("enemyshoot.ogg"));
            Sounds.Add("destroyed", new SoundBuffer("destroyed.ogg"));

            // Set sound volume globally
            Listener.GlobalVolume = 20;

            Directory.SetCurrentDirectory(resourceDir);
            MenuMusic = new Music("Sounds\\menu.ogg");
            MenuMusic.Loop = true;
            GameMusic = new Music("Sounds\\music.ogg");
            GameMusic.Loop = true;
            AppIcon = new Image("icon.png");
            
            // Move back to the starting directory when finished
            Directory.SetCurrentDirectory(baseDir);
        }

        public static void SoundCleanup() {
            MenuMusic.Dispose();
            GameMusic.Dispose();
            foreach(KeyValuePair<string, SoundBuffer> sound in Sounds) {
                sound.Value.Dispose();
            }
        }
    }
}