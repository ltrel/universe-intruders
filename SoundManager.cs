// SoundManager.cs
// Created on: 2020-11-2
// Author: Leo Treloar

using System;
using SFML.Audio;
using System.Collections.Generic;

namespace UniverseIntruders
{
    class SoundManager
    {
        private Queue<Sound> soundQueue;

        private const int MaxSize = 256;
        private int capacity;

        public SoundManager(int queueSize)
        {
            if (queueSize > MaxSize)
                throw new ArgumentOutOfRangeException("queueSize", "No more than 256 sounds allowed");

            capacity = queueSize;
            soundQueue = new Queue<Sound>(queueSize);
        }

        public void Play(Sound sound)
        {
            // If there are already too many sounds, dequeue one
            if (soundQueue.Count >= capacity)
                soundQueue.Dequeue().Dispose();

            sound.Play();
            soundQueue.Enqueue(sound);
        }
    }
}
