using System;

namespace Logic
{
    public interface IInputSnake
    {
        public Action<Direction> OnChangeDirection { get; set; }
    }
}