using Draw;

namespace Logic
{
    public class Apple : IApple
    {
        private IAppleDraw _appleDraw;
        
        public Apple(IAppleDraw appleDraw)
        {
            _appleDraw = appleDraw;
        }
    }
}