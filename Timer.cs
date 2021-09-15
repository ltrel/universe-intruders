// Timer.cs
// Created on: 2020-10-27
// Author: Leo Treloar

using System;
using SFML.System;

namespace UniverseIntruders
{
    public enum TimerState
    {
        Running,
        Finished
    }

    class Timer
    {
        public TimerState State { get; private set; }
        public int Duration { get; }
        private Clock clock;
        private Action callback;

        public Timer(int duration, Action callback)
        {
            Duration = duration;
            clock = new Clock();
            State = TimerState.Running;
            this.callback = callback;
        }

        public void Poll()
        {
            if (State == TimerState.Running && clock.ElapsedTime.AsMilliseconds() >= Duration)
            {
                callback();
                State = TimerState.Finished;
            }
        }
    }
}
