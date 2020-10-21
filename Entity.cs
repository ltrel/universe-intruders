// Entity.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class Entity : Sprite
    {
        public View TargetView { get; protected set; }
        public IntRect CollisionRect { get; set; }
        public bool EntityDestroyed { get; protected set; }
        public int Depth { get; set; }

        public Entity(Texture texture, View targetView) : base(texture)
        {
            this.TargetView = targetView;
            EntityDestroyed = false;
            Depth = 0;
        }
        // The entity won't be rendered or run Update() until this is called
        public void Initialize()
        {
            Game.Entities.Add(this);
            // Sort the list so that entities with the highest depth values come first
            Game.Entities.Sort((e1, e2) => {return e2.Depth - e1.Depth;});
        }
        public virtual void Update() { }

        // Calculate the bounding box of the collider in global coordinates
        public FloatRect GetCollisionBounds() {
            FloatRect globalBounds = GetGlobalBounds();
            FloatRect collisionBounds = new FloatRect();
            collisionBounds.Top = globalBounds.Top + CollisionRect.Top;
            collisionBounds.Left = globalBounds.Left + CollisionRect.Left;
            collisionBounds.Width = CollisionRect.Width;
            collisionBounds.Height = CollisionRect.Height;
            return collisionBounds;
        }

        // Use the entire bounding rectangle of the sprite for collisions
        protected void SetDefaultCollider() { CollisionRect = (IntRect)GetLocalBounds(); }
    }
}