// Scene.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders
{
    abstract class Scene
    {
        public EventHandler<KeyEventArgs> keyEventHandler { get; }
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update(float deltaTime);
        public abstract void Draw(RenderWindow window);
    }
}
