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
    static class Collision
    {
        public static bool IsPointInRect(Vector2f point, FloatRect rect)
        {
            return (point.X >= rect.Left &&
                point.X <= rect.Left + rect.Width &&
                point.Y >= rect.Top &&
                point.Y <= rect.Top + rect.Height);
        }
    }
}