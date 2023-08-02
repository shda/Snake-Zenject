using System;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class StepSnakeTick : ITickable , IStepSnakeTick
    {
        public Action StepSnake { get; set; }

        private float _currentTime;
        private float _delayOneStep = 0.1f;
        
        public void Tick()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                _currentTime = _delayOneStep;
                StepSnake?.Invoke();
            }
        }
    }
}