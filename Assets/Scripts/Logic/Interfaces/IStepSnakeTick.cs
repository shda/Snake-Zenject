using System;

namespace Logic
{
    public interface IStepSnakeTick
    {
        Action StepSnake { get; set; }
    }
}