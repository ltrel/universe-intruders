// PlayerBullet.cs
// Created on: 2020-10-19
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders {
    class PlayerBullet : Entity {
        public float MoveSpeed { get; set; }

        public PlayerBullet(Vector2f position) : base(Resources.Textures["playerbullet"], Game.gameView) {
            Depth = 1;
            MoveSpeed = 210f;
            Position = position;
            SetDefaultCollider();
            CollisionTag = CollisionTag.PlayerBullet;
        }
        public override void Update()
        {
            Position += new Vector2f(0, -MoveSpeed*Game.FrameTime);
            if(CollisionWithTag(CollisionTag.Enemy) != null)
                Console.WriteLine("Bullet is colliding with enemy");
            // If bullet is off screen destroy it
            if (Position.Y-TextureRect.Height > TargetView.Size.Y)
                EntityDestroyed = true;
        }
    }
}