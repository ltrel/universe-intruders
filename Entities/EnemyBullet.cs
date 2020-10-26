// EnemyBullet.cs
// Created on: 2020-10-26
// Author: Leo Treloar

// This file is almost identical to PlayerBullet.cs

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders {
    class EnemyBullet : Entity {
        public float MoveSpeed { get; set; }

        public EnemyBullet(Vector2f position) : base(Resources.Textures["enemybullet"], Game.gameView) {
            Depth = 1;
            MoveSpeed = 200f;
            Position = position;
            SetDefaultCollider();
            CollisionTag = CollisionTag.EnemyBullet;
        }
        public override void Update()
        {
            Position += new Vector2f(0, MoveSpeed*Game.FrameTime);
            // If colliding with the player destroy the player and this bullet
            if (CollisionWithTag(CollisionTag.Player) is Entity player)
            {
                Sound destroyedSound = new Sound(Resources.Sounds["destroyed"]);
                destroyedSound.Play();
                player.EntityDestroyed = true;
                this.EntityDestroyed = true;
            }
            // If bullet is off screen destroy it
            if (Position.Y < 0)
                EntityDestroyed = true;
        }
    }
}