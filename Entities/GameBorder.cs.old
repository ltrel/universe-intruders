// GameBorder.cs
// Created on: 2020-10-19
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class GameBorder : Entity
    {
        public static List<Color> Colors { get; private set; }

        public GameBorder(bool left) : base(Resources.Textures["borders"], Game.windowView)
        {
            if (left)
            {
                // Use the left half of the texture and place the border at the left edge
                TextureRect = new IntRect(0, 0, (int)Texture.Size.X / 2, (int)Texture.Size.Y);
                Position = new Vector2f(Game.windowView.Size.X * 0.25f - TextureRect.Width, 0);
            }
            else
            {
                // Use the right half of the texture and place the border at the right edge
                TextureRect = new IntRect((int)Texture.Size.X / 2, 0, (int)Texture.Size.X / 2, (int)Texture.Size.Y);
                Position = new Vector2f(Game.windowView.Size.X * 0.75f, 0);
            }
            Color = Game.BorderColor;
        }

        public override void Update()
        {
            Color = Game.BorderColor;
        }

        static GameBorder()
        {
            Colors = new List<Color>();
            // These are hex colors with the last byte being for opacity
            // Red
            Colors.Add(new Color(0x922727ff));
            // Orange
            Colors.Add(new Color(0x925927ff));
            // Yellow 
            Colors.Add(new Color(0x928427ff));
            // Green 
            Colors.Add(new Color(0x399227ff));
            // Cyan 
            Colors.Add(new Color(0x279291ff));
            // Blue 
            Colors.Add(new Color(0x273192ff));
            // Purple
            Colors.Add(new Color(0x92278fff));
        }

        public static void NextColor()
        {
            // Get the list index of the current color
            int index = Colors.FindIndex(c => c == Game.BorderColor);
            // Increase the index by one, looping back to the start if neccesary
            index = (index + 1) % Colors.Count;
            // Set the border color to that
            Game.BorderColor = Colors[index];
        }
    }
}