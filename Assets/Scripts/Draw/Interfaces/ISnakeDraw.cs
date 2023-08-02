using System.Collections.Generic;
using Logic;

namespace Draw
{
    public interface ISnakeDraw
    {
        void Draw(IEnumerable<BodyPart> body);
    }
}