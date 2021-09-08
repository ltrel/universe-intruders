// Timer.cs
// Created on: 2020-10-27
// Author: Leo Treloar

using System;
using SFML.System;

namespace UniverseIntruders
{
    class Timer
    {
        private enum TimerState {
            Idle,
            Running,
            Finished
        }
        private TimerState state = TimerState.Idle;
        public int Duration { get; }

        private Clock clock;

        public Timer(int duration)
        {
            Duration = duration;
            clock = new Clock();
        }

        public bool Tick() {
            switch (state) {
                case TimerState.Idle:
                    return false;

                case TimerState.Running:
                    if (clock.ElapsedTime.AsMilliseconds() >= Duration) {
                        state = TimerState.Finished;
                        return true;
                    }
                    return false;

                case TimerState.Finished:
                    return false;

                default:
                    throw new Exception("Invalid state");
            }
        }

        public void SetRunning(bool startRunning) {
            if (state == TimerState.Idle && startRunning) {
                clock.Restart();
                state = TimerState.Running;
            }
        } 

        public void Reset() {
            state = TimerState.Idle;
        }
    }
}
