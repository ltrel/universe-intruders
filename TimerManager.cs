// TimerManager.cs
// Created on: 2021-09-11
// Author: Leo Treloar

using System;
using System.Collections.Generic;

namespace UniverseIntruders {
  class TimerManager {
    private List<Timer> timers = new List<Timer>();

    public void Add(Timer timer) {
      timers.Add(timer);
    }

    public void Poll() {
      foreach(Timer timer in timers) {
        timer.Poll();
      }

      // Clear finished timers
      timers.RemoveAll((timer) => timer.State == TimerState.Finished);
    }
  }
}
