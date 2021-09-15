// TestScene.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using System.Collections.Generic;
using SFML.System;
using SFML.Graphics;
using SFML.Window;

namespace UniverseIntruders {
  class TestScene : Scene {
    private CircleShape circle = new CircleShape(10);

    public TestScene() {
      circle.FillColor = new Color(255, 255, 0);
      circle.Position = new Vector2f(20, 20);
    }

    public override void OnEnter()
    {
    }

    public override void OnExit()
    {
    }

    public override void Update(float deltaTime) {
      circle.Position += new Vector2f(60 * deltaTime, 33 * deltaTime);
    }
    public override void Draw(RenderWindow window) {
      window.Clear(Color.Black);
      window.Draw(circle);
    }
  }
}
