using Draw;
using Settings;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class EatAppleHandler : IInitializable
    {
        private readonly ISnake _snake;
        private readonly IApple _apple;
        private readonly IUiDraw _uiDraw;
        private readonly MapSettings _mapSettings;
        
        private int _score;
        
        public EatAppleHandler(
            ISnake snake,
            IApple apple,
            IUiDraw uiDraw,
            MapSettings mapSettings)
        {
            _uiDraw = uiDraw;
            _apple = apple;
            _snake = snake;
            _mapSettings = mapSettings;
            
            _snake.OnAfterStep += OnSnakeAfterStep;
        }

        private void OnSnakeAfterStep()
        {
            if (IsIntersectionToApple())
            {
                _score++;
                UpdateScore();
                
                SnakeUpSize();
                SetAppleToNewPosition();
            }
        }
        
        private void UpdateScore()
        {
            _uiDraw.SetScore(_score);
        }
        
        private void SnakeUpSize()
        {
            _snake.UpSize();
        }
        
        private bool IsIntersectionToApple()
        {
            return _snake.IsIntersection(_apple.GetPosition());
        }

        private void SetAppleToNewPosition()
        {
            int x;
            int y;

            var size = _mapSettings.MapSize;
            do
            {
                x = Random.Range(1, size.x - 1);
                y = Random.Range(1, size.y - 1);
            } while (_snake.IsIntersection(new Point(x, y)));

            _apple.SetPosition(new Point(x, y));
        }

        public void Initialize()
        {
            SetAppleToNewPosition();
            UpdateScore();
        }
    }
}