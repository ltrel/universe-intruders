// PlayerBullet.cs
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
    class PlayerBullet : Entity
    {
        public float MoveSpeed { get; set; }

        public PlayerBullet(Vector2f position) : base(Resources.Textures["playerbullet"], Game.gameView)
        {
            Depth = 1;
            MoveSpeed = 220f;
            Position = position;
            SetDefaultCollider();
            CollisionTag = CollisionTag.PlayerBullet;
        }
        public override void Update()
        {
            Position += new Vector2f(0, -MoveSpeed * Game.FrameTime);
            // If colliding with an enemy destroy the enemy and this bullet
            if (CollisionWithTag(CollisionTag.Enemy) is Enemy enemy)
            {
                // Only hit if the enemy has reached its start position
                if (enemy.ReachedStartPosition)
                {
                    Sound destroyedSound = new Sound(Resources.Sounds["destroyed"]);
                    Game.SoundManager.Play(destroyedSound);
                    enemy.EntityDestroyed = true;
                    this.EntityDestroyed = true;
                    Game.Score += 10;
                    Game.UpdateScoreText();
                }
            }
            // If bullet is off screen destroy it
            if (Position.Y + TextureRect.Height < 0)
                EntityDestroyed = true;
        }
    }
}
