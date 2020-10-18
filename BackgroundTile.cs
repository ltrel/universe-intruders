// BackgroundTile.cs
// Created on: 2020-10-18
// Author: Leo Treloar

using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class BackgroundTile : Entity
    {
        public float moveSpeed { get; set; }

        public BackgroundTile(View targetView, Vector2f position) : base(Resources.Stars1, targetView)
        {
            Depth = 10;
            moveSpeed = 20f;
            Position = position;
            Initialize();
        }
        public BackgroundTile(View targetView) : base(Resources.Stars1, targetView)
        {
            Depth = 10;
            moveSpeed = 20f;
            Position = new Vector2f(0f, 0 - targetView.Size.Y);
            Initialize();
        }
        public override void Update()
        {
            Position += new Vector2f(0f, moveSpeed*Game.FrameTime);
        }
    }
}