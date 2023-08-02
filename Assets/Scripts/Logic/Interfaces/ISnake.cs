namespace Logic
{
    public interface ISnake
    {
        void StartMove();
        void ChangeMoveDirection(Direction direction);
        void SetToStartPosition(Point headPoint, Point tailPoint);
    }
}