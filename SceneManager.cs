// SceneManager.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders {
  class SceneManager {
    private Dictionary<int, Scene> scenes;
    private Scene currentScene;
    private int nextSceneId = 0;

    public SceneManager() {
      scenes = new Dictionary<int, Scene>();
    }

    public int Add(Scene scene) {
      scenes.Add(nextSceneId, scene);
      return nextSceneId++;
    }

    public void Remove(int sceneId) {
      if(currentScene == scenes[sceneId]) currentScene = null;
      scenes.Remove(sceneId);
    }

    public void Enter(int sceneId) {
      if(currentScene != null) {
        currentScene.OnExit();
      }
      currentScene = scenes[sceneId];
      currentScene.OnEnter();
    }

    public void Update(float deltaTime) {
      if (currentScene == null) return;
      currentScene.Update(deltaTime);
    }

    public void Draw(RenderWindow window) {
      if (currentScene == null) return;
      currentScene.Draw(window);
    }
  }
}
