using System.Linq;
using Settings;
using UnityEngine;
using Zenject;

namespace Logic
{
    public class MapDraw : MonoBehaviour
    {
        [SerializeField] private Transform gameFieldPrefab;

        private MapSettings _mapSettings;
        private void Start()
        {
            gameFieldPrefab.gameObject.SetActive(false);
            CreateField();
        }

        [Inject]
        public void Construct(MapSettings mapSettings)
        {
            _mapSettings = mapSettings;
        }
        
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

            foreach (var point in SnakeUtils.PointsBorder(_mapSettings.MapSize))
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
            return new Point(_mapSettings.MapSize.x , _mapSettings.MapSize.y);
        }
    }
}