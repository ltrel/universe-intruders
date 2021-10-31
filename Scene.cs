// Scene.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders
{
    abstract class Scene
    {
        public SceneManager SceneManager { get; }
        public WindowEventTable EventHandlers { get; protected set; } = new WindowEventTable();
        public EntityManager EntityManager { get; protected set; }
        public SoundManager SoundManager { get; protected set; } = new SoundManager(128);
        public Dictionary<string, View> Views { get; protected set; } = new Dictionary<string, View>();

        public Scene(SceneManager sceneManager)
        {
            SceneManager = sceneManager;
            EntityManager = new EntityManager(EventHandlers);
        }

        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void Update(float deltaTime);
        public abstract void Draw();
    }
}
