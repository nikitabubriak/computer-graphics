using ComputerGraphics.Shapes;
using System.Collections.Generic;

namespace ComputerGraphics
{
    interface IRenderer
    {
        public List<IShape> Shapes { get; }
        public Camera Camera { get; }
        public Vector Light { get; }

        public void AddShape(IShape shape);

        public void Render();
    }
}
