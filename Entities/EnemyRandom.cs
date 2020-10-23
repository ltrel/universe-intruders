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
                double angle = Game.Rand.NextDouble() * 360;
                float distance = (float)Game.Rand.NextDouble() * (MaxDistance - MinDistance) + MinDistance;
                Vector2f step = new Vector2f(
                    distance * (float)Math.Cos(angle),
                    distance * (float)Math.Sin(angle)
                );
                newDestination = Position + step;
            } while (!Collision.IsPointInRect(newDestination, movementBounds));
            currentDestination = newDestination;
        }
    }
}