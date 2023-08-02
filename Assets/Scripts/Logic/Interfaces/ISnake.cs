
using System;

namespace Logic
{
    public interface ISnake
    {
        Action OnAfterStep { get; set; }
        void StartMoving();
        void StopMoving();
        void ChangeMoveDirection(Direction direction);
        void SetToStartPosition(Point headPoint, Point tailPoint);

        bool IsIntersection(Point point);
        void UpSize();
        bool IsIntersectionToBody();
        Point GetPositionHead();
    }
}