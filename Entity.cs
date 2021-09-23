// Entity.cs
// Created on: 2020-10-14
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace UniverseIntruders
{
    class Entity : Sprite
    {
        public WindowEventTable EventHandlers { get; protected set; } = new WindowEventTable();
        public View TargetView { get; protected set; }
        public Scene Scene { get; }
        public IntRect CollisionRect { get; set; }
        public CollisionTag CollisionTag { get; set; }
        public int Depth { get; set; }

        public Entity(Texture texture, Scene scene) : base(texture)
        {
            this.TargetView = scene.Views["default"];
            Scene = scene;
            Depth = 0;
        }

        // This is called when the entity is added to an EntityManager
        public virtual void Initialize() { }
        public virtual void Update(float deltaTime) { }

        // Calculate the bounding box of the collider in global coordinates
        public FloatRect GetCollisionBounds()
        {
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

        // protected Entity CollisionWithTag(CollisionTag tag)
        // {
        //     List<Entity> entities = Game.Entities.FindAll(e => e.CollisionTag == tag);
        //     Entity collidingEntity = null;
        //     foreach (Entity entity in entities)
        //     {
        //         if (Collision.IsRectInRect(this.GetCollisionBounds(), entity.GetCollisionBounds()))
        //             collidingEntity = entity;
        //     }
        //     return collidingEntity;
        // }

        public void Draw(RenderWindow window)
        {
            View originalView = window.GetView();
            window.SetView(TargetView);
            window.Draw(this);
            window.SetView(originalView);
        }

        public void Destroy()
        {
            Scene.EntityManager.Remove(this);
        }
    }
}
