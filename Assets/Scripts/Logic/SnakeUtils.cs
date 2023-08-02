using System;
using System.Collections.Generic;
using UnityEngine;

namespace Logic
{
    public static class SnakeUtils
    {
        public static Vector2Int DirectionToVector2d(Direction direction)
        {
            return direction switch
            {
                Direction.Up => new Vector2Int(0, 1),
                Direction.Down => new Vector2Int(0, -1),
                Direction.Left => new Vector2Int(-1, 0),
                Direction.Right => new Vector2Int(1, 0),
                _ => new Vector2Int(0, 0)
            };
        }

        public static List<BodyPart> MakeSnakeBodyParts(Point headPoint, Point tailPoint)
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
    }
}