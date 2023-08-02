using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Logic
{
    [ExecuteInEditMode]
    public class GameField : MonoBehaviour , IGameField
    {
        [SerializeField] private Vector2Int mapSize = new Vector2Int(20, 20);
        [SerializeField] private Transform gameFieldPrefab;

        private void Start()
        {
            gameFieldPrefab.gameObject.SetActive(false);
            CreateField();
        }

        [ContextMenu("Create field")]
        private void CreateField()
        {
            var children = transform.Cast<Transform>().ToArray();
            foreach (var child in children)
            {
                if (child != gameFieldPrefab)
                {
                    DestroyImmediate(child.gameObject);
                }
            }

            foreach (var point in GetPointBorder())
            {
                CreateItem(new Vector2Int(point.X, point.Y));
            }
        }

        private void CreateItem(Vector2Int pos)
        {
            var newItem = Instantiate(gameFieldPrefab ,  this.transform);
            newItem.position = new Vector3(pos.x, pos.y, 0);
            newItem.gameObject.SetActive(true);
        }
        
        public Point GetSize()
        {
            return new Point(mapSize.x , mapSize.y);
        }

        private IEnumerable<Point> GetPointBorder()
        {
            for (var x = 0; x < mapSize.x; x++)
            {
                yield return new Point(x, 0);
                yield return new Point(x, mapSize.y - 1);
            }
            
            for (var y = 0; y < mapSize.y; y++)
            {
                yield return new Point(0, y);
                yield return new Point(mapSize.x - 1, y);
            }
        }

        public bool IsIntersectionToBorder(Point headPoint)
        {
            foreach (var point in GetPointBorder())
            {
                if (headPoint == point)
                {
                    return true;
                }
            }

            return false;
        }
    }
}