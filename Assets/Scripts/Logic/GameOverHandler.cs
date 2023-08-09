using Draw;
using Settings;
using UnityEngine;
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
            return  IsIntersectionToBorder(headPoint, _mapSettings.MapSize);
        }
        
        private static bool IsIntersectionToBorder(Point headPoint , Vector2Int mapSize)
        {
            var rect = Rect.MinMaxRect(1,1, mapSize.x - 1 , mapSize.y - 1);
            return !rect.Contains(new Vector2(headPoint.X, headPoint.Y));
        }

        public void Initialize()
        {
            
        }
    }
}