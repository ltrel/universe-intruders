// Collision.cs
// Created on: 2020-10-23
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    enum CollisionTag {
        None,
        Player,
        PlayerBullet,
        Enemy,
        EnemyBullet
    }

    static class Collision
    {
        public static bool IsPointInRect(Vector2f point, FloatRect rect)
        {
            return (point.X >= rect.Left &&
                point.X <= rect.Left + rect.Width &&
                point.Y >= rect.Top &&
                point.Y <= rect.Top + rect.Height);
        }

        // https://developer.mozilla.org/en-US/docs/Games/Techniques/2D_collision_detection
        public static bool IsRectInRect(FloatRect r1, FloatRect r2)
        {
            return (r1.Left <= r2.Left + r2.Width &&
                r1.Left + r1.Width >= r2.Left &&
                r1.Top <= r2.Top + r2.Height &&
                r1.Top + r1.Height >= r2.Top);
        }
    }
}