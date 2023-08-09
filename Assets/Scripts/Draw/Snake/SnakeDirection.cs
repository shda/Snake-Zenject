using System;
using System.Linq;
using Logic;
using UnityEngine;

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

        [SerializeField] private SpriteRenderer renderer;
        [SerializeField] private DirectionToImage[] directionToImages;

        public void SetDirection(Direction direction)
        {
            var findSprite= directionToImages.
                FirstOrDefault(x => x.bodyDirection == direction);
            renderer.sprite = findSprite.spriteBody;
        }
    }
}