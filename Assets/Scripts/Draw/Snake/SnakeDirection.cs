using System;
using System.Linq;
using Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Draw
{
    public class SnakeDirection : MonoBehaviour
    {
        [Serializable]
        public class DirectionToImage
        {
            public Direction bodyDirection;
            public Sprite spriteBody;
        }

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private DirectionToImage[] directionToImages;

        public void SetDirection(Direction direction)
        {
            var findSprite= directionToImages.
                FirstOrDefault(x => x.bodyDirection == direction);
            spriteRenderer.sprite = findSprite.spriteBody;
        }
    }
}