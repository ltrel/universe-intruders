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
        public Entity(Texture texture) : base(texture)
        {
        }
        public void Initialize()
        {
            Game.Entities.Add(this);
        }
        public virtual void Update() { }
    }
}