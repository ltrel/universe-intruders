// GameBorder.cs
// Created on: 2020-10-19
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders {
    class GameBorder : Entity {
        public static List<Color> colors {get; private set; }

        public GameBorder(bool left) : base(Resources.Textures["borders"], Game.windowView) {
            if(left) {
                // Use the left half of the texture and place the border at the left edge
                TextureRect = new IntRect(0, 0, (int)Texture.Size.X/2, (int)Texture.Size.Y);
                Position = new Vector2f(Game.windowView.Size.X*0.25f-TextureRect.Width, 0);
            }
            else {
                // Use the right half of the texture and place the border at the right edge
                TextureRect = new IntRect((int)Texture.Size.X/2, 0, (int)Texture.Size.X/2, (int)Texture.Size.Y);
                Position = new Vector2f(Game.windowView.Size.X*0.75f, 0);
            }
            Color = Game.BorderColor;
        }

        static GameBorder() {
            colors = new List<Color>();
            // These are hex colors with the last byte being for opacity
            // Red
            colors.Add(new Color(0x922727ff));
            // Orange
            colors.Add(new Color(0x925927ff));
            // Yellow 
            colors.Add(new Color(0x928427ff));
            // Green 
            colors.Add(new Color(0x399227ff));
            // Cyan 
            colors.Add(new Color(0x279291ff));
            // Blue 
            colors.Add(new Color(0x273192ff));
            // Purple
            colors.Add(new Color(0x92278fff));
        }
    }
}