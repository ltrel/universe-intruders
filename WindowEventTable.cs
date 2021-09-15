// WindowEventTable.cs
// Created on: 2021-09-15
// Author: Leo Treloar

using System;
using SFML.Window;

namespace UniverseIntruders
{
    class WindowEventTable
    {
        public EventHandler<KeyEventArgs> KeyPressed { get; set; }
        public EventHandler Closed { get; set; }

        public WindowEventTable()
        {
            KeyPressed = delegate { };
            Closed = delegate { };
        }
    }
}
