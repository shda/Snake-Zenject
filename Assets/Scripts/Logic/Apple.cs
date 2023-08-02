using Draw;

namespace Logic
{
    public class Apple : IApple
    {
        private IAppleDraw _appleDraw;
        private Point _positionOnMap;
        
        public Apple(IAppleDraw appleDraw)
        {
            _appleDraw = appleDraw;
        }

        public Point GetPosition()
        {
            return _positionOnMap;
        }

        public void SetPosition(Point point)
        {
            _positionOnMap = point;
            _appleDraw.Draw(point);
        }
    }
}