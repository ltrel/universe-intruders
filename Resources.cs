// Resources.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using SFML.Graphics;
using System.Collections.Generic;

namespace UniverseIntruders
{
    static class Resources
    {
        public static Dictionary<string, Texture> Textures { get; }

        static Resources() {
            Textures = new Dictionary<string, Texture>();
            Textures.Add("player", new Texture("Resources\\Textures\\player.png"));
            Textures.Add("playerbullet", new Texture("Resources\\Textures\\playerbullet.png"));
            Textures.Add("stars1", new Texture("Resources\\Textures\\stars1.png"));
            Textures.Add("stars2", new Texture("Resources\\Textures\\stars2.png"));
            Textures.Add("stars3", new Texture("Resources\\Textures\\stars3.png"));
            Textures.Add("stars4", new Texture("Resources\\Textures\\stars4.png"));
            Textures.Add("borders", new Texture("Resources\\Textures\\borders.png"));
        }
    }
}