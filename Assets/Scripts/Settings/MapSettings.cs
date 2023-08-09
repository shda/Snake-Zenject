using System;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class MapSettings
    {
        private Vector2Int mapSize = new(30,30);
        public Vector2Int MapSize => mapSize;

        public MapSettings(Vector2Int mapSize)
        {
            this.mapSize = mapSize;
        }

        public MapSettings()
        {
            
        }
    }
}