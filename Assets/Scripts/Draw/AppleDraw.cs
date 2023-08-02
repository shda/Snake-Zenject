using Logic;
using UnityEngine;

namespace Draw
{
    public class AppleDraw : MonoBehaviour , IAppleDraw
    {
        public void Draw(Point point)
        {
            transform.position = point.GetVector3();
        }
    }
}