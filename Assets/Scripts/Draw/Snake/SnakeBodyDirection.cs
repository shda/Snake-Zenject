using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Draw
{
    public class SnakeBodyDirection : MonoBehaviour
    {
        [Serializable]
        public class DirectionToImage
        {
            public BodyDirection bodyDirection;
            public Sprite spriteBody;
        }

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private DirectionToImage[] directionToImages;
        
        
        public void SetSprite(BodyDirection bodyDirection)
        {
           var body = directionToImages.
               FirstOrDefault(x => x.bodyDirection == bodyDirection);
           spriteRenderer.sprite = body.spriteBody;
        }
    }
}