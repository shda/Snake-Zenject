using System.Collections.Generic;
using System.Linq;
using Draw;

namespace Logic
{
    public class Snake : ISnake
    {
        private ISnakeDraw _snakeDraw;
        private LinkedList<BodyPart> _snakeBody = new();

        private Point _moveDirection = new Point(1, 0);
        private bool _isMoving;

        public Snake(ISnakeDraw snakeDraw , IStepSnakeTick stepSnakeTick)
        {
            _snakeDraw = snakeDraw;
            stepSnakeTick.StepSnake = StepSnake;
        }

        private void StepSnake()
        {
            var headHead = _snakeBody.First(x => 
                x.BodyType == BodyType.Head);
            
            Point lastPart = headHead.PointToMap;
            headHead.PointToMap += _moveDirection;
            
            foreach (var bodyPart in _snakeBody)
            {
                if (bodyPart.BodyType != BodyType.Head)
                {
                    (bodyPart.PointToMap, lastPart) = (lastPart, bodyPart.PointToMap);
                }
            }
            
            _snakeDraw.Draw(_snakeBody);
        }

        public void SetToStartPosition(Point headPoint, Point tailPoint)
        {
            var snake = SnakeUtils.MakeSnakeBodyParts(headPoint, tailPoint);
            _snakeDraw.Draw(snake);

            foreach (var bodyPart in snake)
            {
                _snakeBody.AddLast(bodyPart);
            }
        }

        public void StartMove()
        {
            _isMoving = true;
        }

        public void ChangeMoveDirection(Direction direction)
        {
            var dirVector2d = SnakeUtils.DirectionToVector2d(direction);
            _moveDirection = new Point(dirVector2d.x , dirVector2d.y) ;
        }
    }
}