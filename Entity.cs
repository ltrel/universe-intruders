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
        public int Depth { get; set; }

        public Entity(Texture texture, View targetView) : base(texture)
        {
            Depth = 0;
            this.TargetView = targetView;
        }
        // The entity won't be rendered or run Update() until this is called
        public void Initialize()
        {
            Game.Entities.Add(this);
            Game.Entities.Sort((e1, e2) => {return e2.Depth - e1.Depth;});
        }
        public virtual void Update() { }
    }
}