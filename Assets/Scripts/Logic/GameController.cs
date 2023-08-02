using Zenject;

namespace Logic
{
    public class GameController : IInitializable
    {
        private IApple _apple;
        private ISnake _snake;
        //private IInputSnake _inputSnake;
        
        public GameController(IApple apple , ISnake snake , IInputSnake inputSnake)
        {
            _apple = apple;
            _snake = snake;
           // _inputSnake = inputSnake;
            
           inputSnake.OnChangeDirection = OnChangeDirection;
        }

        private void OnChangeDirection(Direction direction)
        {
            _snake.ChangeMoveDirection(direction);
        }

        public void Initialize()
        {
            _snake.SetToStartPosition(new Point(3,0) , new Point(1 ,0 ));
            _snake.StartMove();
        }
    }
}
