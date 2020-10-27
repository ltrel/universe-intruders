using System;
using SFML.System;

namespace UniverseIntruders
{
    class Timer
    {
        public bool StartCondition { get; set; }
        public bool TimerHasStarted { get; set; }
        private bool hasFinished;
        public int Length { get; }

        private Clock clock;

        public Timer(int length)
        {
            Length = length;
            StartCondition = false;
            TimerHasStarted = false;
            hasFinished = false;
            clock = new Clock();
        }

        public bool Tick()
        {
            if (StartCondition && !TimerHasStarted && !hasFinished)
            {
                clock.Restart();
                TimerHasStarted = true;
            }
            if (TimerHasStarted && clock.ElapsedTime.AsMilliseconds() >= Length)
            {
                TimerHasStarted = false;
                hasFinished = true;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            hasFinished = false;
        }
    }
}