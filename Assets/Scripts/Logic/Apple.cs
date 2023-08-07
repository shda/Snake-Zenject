using Draw;

namespace Logic
{
    public class Apple : IApple
    {
        private IAppleDraw _appleDraw;
        
        private Point _position;
        public Point Position 
        {
            get => _position;
            set
            {
                _position = value;
                _appleDraw.Draw(_position);
            } 
        }
        
        public Apple(IAppleDraw appleDraw)
        {
            _appleDraw = appleDraw;
        }
    }
}