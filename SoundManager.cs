// SoundManager.cs
// Created on: 2020-11-2
// Author: Leo Treloar

using System;
using SFML.Audio;
using System.Collections.Generic;

namespace UniverseIntruders
{
    class SoundManager : Queue<Sound>
    {
        private const int MaxSize = 256;
        private int capacity;
        public SoundManager(int queueSize) : base(queueSize)
        {
            if (queueSize > MaxSize)
                throw new ArgumentOutOfRangeException("queueSize", "No more than 256 sounds allowed");
            else
                capacity = queueSize;
        }
        public new void Enqueue(Sound sound)
        {
            // If there is room, just add the sound
            if (Count < capacity)
                base.Enqueue(sound);
            // If there isn't, delete the oldest sound first
            else
            {
                Dequeue().Dispose();
                base.Enqueue(sound);
            }
        }
    }
}
