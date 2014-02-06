using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlappyBird.Performance
{
    public interface IEntityFactory
    {
        object CreateNew();
        object CreateNew(FlatRedBall.Graphics.Layer layer);
    }
}
