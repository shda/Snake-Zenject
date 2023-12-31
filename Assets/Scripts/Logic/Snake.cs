using System;
using System.Collections.Generic;
using System.Linq;
using Draw;

namespace Logic
{
    public class Snake : ISnake
    {
        public event Action OnAfterStep;
        
        private readonly ISnakeDraw _snakeDraw;
        private readonly LinkedList<BodyPart> _snakeBody = new();

        private Point _moveDirection = new(1, 0);
        private bool _isMoving;
        private bool _isUpSize;
        
        public Snake(
            ISnakeDraw snakeDraw , 
            IStepSnakeTick stepSnakeTick)
        {
            _snakeDraw = snakeDraw;
            stepSnakeTick.StepSnake = MoveShake;
        }

        private void MoveShake()
        {
            if(!_isMoving)
                return;
            
            var headHead = _snakeBody.First(x => 
                x.BodyType == BodyType.Head);
            
            var lastPart = headHead.PointToMap;
            headHead.PointToMap += _moveDirection;

            if (_isUpSize)
            {
                _isUpSize = false;

                var newPart = new BodyPart
                {
                    BodyType = BodyType.Body,
                    PointToMap = lastPart
                };
                
                _snakeBody.AddAfter(_snakeBody.First , newPart);
            }
            else
            {
                foreach (var bodyPart in _snakeBody)
                {
                    if (bodyPart.BodyType != BodyType.Head)
                    {
                        (bodyPart.PointToMap, lastPart) = (lastPart, bodyPart.PointToMap);
                    }
                }
            }

            _snakeDraw.Draw(_snakeBody);
            OnAfterStep?.Invoke();
        }

        public void SetToStartPosition(Point headPoint, Point tailPoint)
        {
            var snake = InitSnakeBodyParts(headPoint, tailPoint);
            _snakeDraw.Draw(snake);

            foreach (var bodyPart in snake)
            {
                _snakeBody.AddLast(bodyPart);
            }
        }

        private List<BodyPart> InitSnakeBodyParts(Point headPoint, Point tailPoint)
        {
            List<BodyPart> snake = new()
            {
                new BodyPart
                {
                    BodyType = BodyType.Head,
                    PointToMap = headPoint,
                }
            };

            var pointX = headPoint.X;
            var pointY = headPoint.Y;

            while (pointX != tailPoint.X ||
                   pointY != tailPoint.Y)
            {
                if (pointX > tailPoint.X)
                {
                    pointX--;
                }
                else if (pointX < tailPoint.X)
                {
                    pointX++;
                }

                if (pointY > tailPoint.Y)
                {
                    pointX--;
                }
                else if (pointY < tailPoint.Y)
                {
                    pointX++;
                }

                if (pointX != tailPoint.X ||
                    pointY != tailPoint.Y)
                {
                    snake.Add(new BodyPart()
                    {
                        BodyType = BodyType.Body,
                        PointToMap = new Point(pointX, pointY)
                    });
                }
            }

            snake.Add(new BodyPart
            {
                BodyType = BodyType.Tail,
                PointToMap = tailPoint
            });

            return snake;
        }

        public bool IsIntersection(Point point)
        {
            foreach (var bodyPart in _snakeBody)
            {
                if (bodyPart.PointToMap == point)
                {
                    return true;
                }
            }

            return false;
        }

        public void UpSize()
        {
            _isUpSize = true;
        }

        public bool IsIntersectionToBody()
        {
            foreach (var bodyPart in _snakeBody)
            {
                foreach (var bodyPartCheck in _snakeBody)
                {
                    if (bodyPart != bodyPartCheck &&
                        bodyPart.PointToMap == bodyPartCheck.PointToMap)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public Point GetPositionHead()
        {
            return _snakeBody.First.Value.PointToMap;
        }

        public void StartMoving()
        {
            _isMoving = true;
        }

        public void StopMoving()
        {
            _isMoving = false;
        }

        public void ChangeMoveDirection(Direction direction)
        {
            var dirVector2d = direction.ToVector2d();
            _moveDirection = new Point(dirVector2d.x , dirVector2d.y) ;
        }
    }
}