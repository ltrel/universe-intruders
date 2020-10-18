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
        private bool nextCreated = false;

        // This mess is because optional arguments don't work here,
        // the default value of position has to be calcaulted based on targetView
        public BackgroundTile(View targetView, Vector2f position) : base(Resources.Stars1, targetView)
        {
            SetValues(position);
        }
        public BackgroundTile(View targetView) : base(Resources.Stars1, targetView)
        {
            SetValues(new Vector2f(0f, 0 - targetView.Size.Y));
        }
        private void SetValues(Vector2f position)
        {
            Depth = 10;
            moveSpeed = 30f;
            Position = position;
            // Randomly choose one of the textures
            switch(Game.Rand.Next(1, 5)) {
                case 1:
                    Texture = Resources.Stars1;
                    break;
                case 2:
                    Texture = Resources.Stars2;
                    break;
                case 3:
                    Texture = Resources.Stars3;
                    break;
                case 4:
                    Texture = Resources.Stars4;
                    break;
            }
        }

        public override void Update()
        {
            Position += new Vector2f(0f, moveSpeed * Game.FrameTime);
            if (Position.Y >= 0 && !nextCreated)
            {
                BackgroundTile newTile = new BackgroundTile(TargetView);
                Game.EntityQueue.Enqueue(newTile);
                nextCreated = true;
            }
            if (Position.Y > TargetView.Size.Y)
            {
                EntityDestroyed = true;
            }
        }
    }
}