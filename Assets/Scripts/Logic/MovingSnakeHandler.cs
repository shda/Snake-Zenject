using Zenject;

namespace Logic
{
    public class MovingSnakeHandler : IInitializable
    {
        private readonly ISnake _snake;

        public MovingSnakeHandler(
            ISnake snake,
            IInputSnake inputSnake )
        {
            _snake = snake;
            inputSnake.OnChangeDirection = OnChangeDirection;
        }
        
        private void OnChangeDirection(Direction direction)
        {
            _snake.ChangeMoveDirection(direction);
        }

        public void Initialize()
        {
            _snake.SetToStartPosition(new Point(10 , 18), new Point(6, 18));
            _snake.StartMoving();
        }
    }
}