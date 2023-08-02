namespace Logic
{
    public interface IGameField
    {
        Point GetSize();
        bool IsIntersectionToBorder(Point headPoint);
    }
}