using System;
using System.Collections.Generic;
using ComputerGraphics.Shapes;

namespace ComputerGraphics
{
    public class Scene
    {
        public List<IShape> Shapes { get; }
        public Camera Camera { get; }

        public Scene(Camera camera)
        {
            Shapes = new List<IShape>();
            Camera = camera;
        }

        public void AddShape(IShape shape)
        {
            Shapes.Add(shape);
        }

        public void Render()
        {
            Point rayOrigin = Camera.Position;
            for (int y = Camera.ScreenHeight; y > 0; y--)
            {
                for (int x = 0; x < Camera.ScreenWidth; x++)
                {
                    Point screenPoint = new Point(x, y, 0);
                    Vector rayDirection = (screenPoint - rayOrigin).Normalized();

                    foreach (IShape shape in Shapes)
                    {
                        Point intersection = shape.CheckIntersection(rayOrigin, rayDirection);
                        var symbol = (intersection == null) ? ' ' : '#';
                        Console.Write(symbol);
                    }
                }
                Console.WriteLine();
            }

        }
    }
}
