// EnemyRandom.cs
// Created on: 2020-10-22
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class EnemyRandom : Enemy
    {
        public FloatRect movementBounds { get; set; }
        public float MinDistance { get; set; }
        public float MaxDistance { get; set; }

        public EnemyRandom(Vector2f startPosition) : base(startPosition)
        {
            movementBounds = new FloatRect(0, 0, TargetView.Size.X, TargetView.Size.Y-30);
        }

        protected override void SetNextDestination()
        {
            Vector2f newDestination;
            do
            {
                // Get a random angle and random distance between the minimum and maximum
                double angle = Game.Rand.NextDouble() * 360;
                float distance = (float)Game.Rand.NextDouble() * (MaxDistance - MinDistance) + MinDistance;
                // Convert the angle and distance to cartesian coordinates
                Vector2f step = new Vector2f(
                    distance * (float)Math.Cos(angle),
                    distance * (float)Math.Sin(angle)
                );
                // Add this onto the enemy's current position
                newDestination = Position + step;
                // If the destination is outside of the bounds, try again
            } while (!Collision.IsPointInRect(newDestination, movementBounds));
            currentDestination = newDestination;
        }
    }
}