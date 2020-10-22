// Resources.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;

namespace UniverseIntruders
{
    static class Resources
    {
        public static Dictionary<string, Texture> Textures { get; }
        public static Dictionary<string, Font> Fonts { get; }

        public static Image AppIcon { get; }
        public static Music MenuMusic { get; }

        static Resources() {
            Textures = new Dictionary<string, Texture>();
            Textures.Add("player", new Texture("Resources\\Textures\\player.png"));
            Textures.Add("playerbullet", new Texture("Resources\\Textures\\playerbullet.png"));
            Textures.Add("enemy", new Texture("Resources\\Textures\\enemy.png"));
            Textures.Add("stars1", new Texture("Resources\\Textures\\stars1.png"));
            Textures.Add("stars2", new Texture("Resources\\Textures\\stars2.png"));
            Textures.Add("stars3", new Texture("Resources\\Textures\\stars3.png"));
            Textures.Add("stars4", new Texture("Resources\\Textures\\stars4.png"));
            Textures.Add("borders", new Texture("Resources\\Textures\\borders.png"));
            Textures.Add("menubackground", new Texture("Resources\\Textures\\menubackground.png"));

            Fonts = new Dictionary<string, Font>();
            Fonts.Add("ibmbios", new Font("Resources\\Fonts\\Ac437_IBM_BIOS.ttf"));

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