using UnityEngine;

namespace Logic
{
    public static class DirectionExtension
    {
        public static Vector2Int ToVector2d(this Direction direction)
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
    }
}