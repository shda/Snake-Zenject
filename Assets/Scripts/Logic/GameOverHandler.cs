using Draw;
using Settings;
using Zenject;

namespace Logic
{
    public class GameOverHandler : IInitializable
    {
        private readonly ISnake _snake;
        private readonly MapSettings _mapSettings;
        private readonly IUiDraw _uiDraw;

        public GameOverHandler(
            MapSettings mapSettings,
            ISnake snake,
            IUiDraw uiDraw)
        {
            _snake = snake;
            _mapSettings = mapSettings;
            _uiDraw = uiDraw;
            
            _snake.OnAfterStep += OnSnakeAfterStep;
        }

        private void GameOver()
        {
            _snake.StopMoving();
            _uiDraw.ShowGameOver();
        }

        private void OnSnakeAfterStep()
        {
            if (InIntersectionToBody() ||
                InIntersectionToBorder())
            {
                GameOver();
            }
        }
        
        private bool InIntersectionToBody()
        {
            return _snake.IsIntersectionToBody();
        }
        
        private bool InIntersectionToBorder()
        {
            var headPoint = _snake.GetPositionHead();
            return  SnakeUtils.IsIntersectionToBorder(headPoint, _mapSettings.MapSize);
        }

        public void Initialize()
        {
            
        }
    }
}