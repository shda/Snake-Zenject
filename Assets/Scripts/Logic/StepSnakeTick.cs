using System;
using Settings;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class StepSnakeTick : ITickable , IStepSnakeTick
    {
        public Action StepSnake { get; set; }

        private float _currentTimer;
        
        private readonly SnakeSettings _settings;

        public StepSnakeTick(SnakeSettings settings)
        {
            _settings = settings;
        }

        public void Tick()
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer <= 0)
            {
                _currentTimer = _settings.DelayBetweenSteps;
                InvokeStep();
            }
        }

        public void InvokeStep()
        {
            StepSnake?.Invoke();
        }
    }
}