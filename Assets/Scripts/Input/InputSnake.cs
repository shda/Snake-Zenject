using System;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class InputSnake : ITickable , IInputSnake
    {
        public Action<Direction> OnChangeDirection { get; set; }
        
        private readonly KeyByAction[] _actions;
        
        public InputSnake()
        {
            _actions = new[]
            {
                new KeyByAction
                {
                    KeyCodes = new []{KeyCode.W , KeyCode.UpArrow},
                    OnAction = () => ChangeDirection(Direction.Up)
                },
                new KeyByAction
                {
                    KeyCodes = new []{KeyCode.S , KeyCode.DownArrow},
                    OnAction = () => ChangeDirection(Direction.Down)
                },
                new KeyByAction
                {
                    KeyCodes = new []{KeyCode.A , KeyCode.LeftArrow},
                    OnAction = () => ChangeDirection(Direction.Left)
                }
                ,new KeyByAction
                {
                    KeyCodes = new []{KeyCode.D , KeyCode.RightArrow},
                    OnAction = () => ChangeDirection(Direction.Right)
                }
            };
        }
        
        public void Tick()
        {
            foreach (var keyByAction in _actions)
            {
                foreach (var keyCode in keyByAction.KeyCodes)
                {
                    if (Input.GetKeyDown(keyCode))
                    {
                        keyByAction.OnAction?.Invoke();
                        return;
                    }
                }
            }
        }

        private void ChangeDirection(Direction direction)
        {
            OnChangeDirection?.Invoke(direction);
        }
    }
}