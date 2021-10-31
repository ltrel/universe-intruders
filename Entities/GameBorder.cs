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
        private Sprite leftSprite { get; set; }
        private Sprite rightSprite { get; set; }
        private int colorIndex { get; set; } = 0;

        public GameBorder(Scene scene) : base(null, scene)
        {
            leftSprite = new Sprite(Resources.Textures["borders"]);
            leftSprite.TextureRect = new IntRect(
                0,
                0,
                (int)leftSprite.Texture.Size.X / 2,
                (int)leftSprite.Texture.Size.Y
            );

            rightSprite = new Sprite(Resources.Textures["borders"]);
            rightSprite.TextureRect = new IntRect(
                (int)rightSprite.Texture.Size.X / 2,
                0,
                (int)rightSprite.Texture.Size.X / 2,
                (int)rightSprite.Texture.Size.Y
            );
            leftSprite.Color = Colors[colorIndex];
            rightSprite.Color = Colors[colorIndex];
        }

        public override void Initialize()
        {
            leftSprite.Position = new Vector2f(
                TargetView.Size.X * 0.25f - leftSprite.TextureRect.Width, 0
            );
            rightSprite.Position = new Vector2f(
                TargetView.Size.X * 0.75f, 0
            );
        }

        public override void Draw()
        {
            View originalView = Scene.SceneManager.Window.GetView();
            Scene.SceneManager.Window.SetView(TargetView);
            Scene.SceneManager.Window.Draw(leftSprite);
            Scene.SceneManager.Window.Draw(rightSprite);
            Scene.SceneManager.Window.SetView(originalView);
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

        public void NextColor()
        {
            // Increase the index by one, looping back to the start if necessary
            colorIndex = (colorIndex + 1) % Colors.Count;
            // Set the border color to that
            leftSprite.Color = Colors[colorIndex];
            rightSprite.Color = Colors[colorIndex];
        }
    }
}
