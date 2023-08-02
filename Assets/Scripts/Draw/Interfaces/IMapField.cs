namespace Logic
{
    public interface IMapField
    {
        Point GetSize();
        bool IsIntersectionToBorder(Point headPoint);
    }
}