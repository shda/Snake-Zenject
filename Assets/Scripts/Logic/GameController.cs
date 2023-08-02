using Draw;
using Zenject;
using Random = UnityEngine.Random;

namespace Logic
{
    public class GameController : IInitializable
    {
        private readonly IApple _apple;
        private readonly ISnake _snake;
        private readonly IUiDraw _uiDraw;
        private readonly IMapField _mapField;

        private int _score;

        public GameController(
            IApple apple,
            ISnake snake,
            IMapField mapField,
            IInputSnake inputSnake ,
            IUiDraw uiDraw)
        {
            _apple = apple;
            _snake = snake;
            _uiDraw = uiDraw;
            _mapField = mapField;

            _snake.OnAfterStep = OnSnakeAfterStep;
            inputSnake.OnChangeDirection = OnChangeDirection;
        }

        private void OnSnakeAfterStep()
        {
            if (InIntersectionToBody() ||
                InIntersectionToBorder())
            {
                GameOver();
                return;
            }
            
            if (IsIntersectionToApple())
            {
                _score++;
                UpdateScore();
                
                SnakeUpSize();
                SetAppleToNewPosition();
            }
        }

        private bool InIntersectionToBorder()
        {
            var headPoint = _snake.GetPositionHead();
            return _mapField.IsIntersectionToBorder(headPoint);
        }

        private void SnakeUpSize()
        {
            _snake.UpSize();
        }

        private bool InIntersectionToBody()
        {
            return _snake.IsIntersectionToBody();
        }

        private void GameOver()
        {
            _snake.StopMoving();
            _uiDraw.ShowGameOver();
        }

        private bool IsIntersectionToApple()
        {
            return _snake.IsIntersection(_apple.GetPosition());
        }

        private void OnChangeDirection(Direction direction)
        {
            _snake.ChangeMoveDirection(direction);
        }

        public void Initialize()
        {
            _snake.SetToStartPosition(new Point(10 , 18), new Point(9, 18));
            _snake.StartMoving();

            SetAppleToNewPosition();
            UpdateScore();
        }

        private void UpdateScore()
        {
            _uiDraw.SetScore(_score);
        }

        private void SetAppleToNewPosition()
        {
            int x;
            int y;

            var size = _mapField.GetSize();
            do
            {
                x = Random.Range(1, size.X - 1);
                y = Random.Range(1, size.Y - 1);
            } while (_snake.IsIntersection(new Point(x, y)));

            _apple.SetPosition(new Point(x, y));
        }
    }
}