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
        protected View targetView;

        public Entity(Texture texture, View targetView) : base(texture)
        {
            this.targetView = targetView;
        }
        // The entity won't be rendered or run Update() until this is called
        public void Initialize()
        {
            Game.Entities.Add(this);
        }
        public virtual void Update() { }
    }
}