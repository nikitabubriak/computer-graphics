using System;
using System.Collections.Generic;
using ComputerGraphics.Shapes;

namespace ComputerGraphics
{
    public class Scene : IRenderer
    {
        public List<IShape> Shapes { get; }
        public Camera Camera { get; }
        public Vector Light { get; }

        public Scene(Camera camera, Vector light = null)
        {
            Shapes = new List<IShape>();
            Camera = camera;
            if (light != null)
                Light = light.Normalized() * -1;
        }

        public void AddShape(IShape shape)
        {
            Shapes.Add(shape);
        }

        private void SetScreenPointSymbol(ref char symbol, IShape shape, Point intersection)
        {
            if (Light == null)
                symbol = (intersection == null) ? ' ' : '#';
            else
            {
                if (intersection == null)
                    symbol = ' ';
                else
                {
                    float lightLevel = Vector.Dot(Light, shape.GetIntersectionNormal(intersection));
                    switch (lightLevel)
                    {
                        case <= 0: symbol = ' '; break;
                        case < 0.2f: symbol = '.'; break;
                        case < 0.5f: symbol = '*'; break;
                        case < 0.8f: symbol = 'O'; break;
                        case >= 0.8f: symbol = '#'; break;
                        default: symbol = ' '; break;
                    }
                }
            }
        }

        //Output image to console
        public void Render()
        {
            if (Shapes.Count == 0) return;

            Point rayOrigin = Camera.Position;
            char symbol = ' ';

            for (int y = Camera.ScreenHeight; y > 0; y--)
            {
                for (int x = 0; x < Camera.ScreenWidth; x++)
                {
                    Point screenPoint = new Point(x, y, 0);
                    Vector rayDirection = (screenPoint - rayOrigin).Normalized();

                    foreach (IShape shape in Shapes)
                    {
                        Point intersection = shape.CheckIntersection(rayOrigin, rayDirection);
                        SetScreenPointSymbol(ref symbol, shape, intersection);
                    }
                    Console.Write(symbol); //print symbol for the current screenPoint
                    Console.Write(symbol); //compensate for symbol width in console
                }
                Console.WriteLine();
            }
        }

        //Output closest shape image to console
        public IShape RenderClosest()
        {
            if (Shapes.Count == 0) return null;

            Point rayOrigin = Camera.Position;
            char symbol = ' ';

            float minMag = float.MaxValue; //magnitude of a ray that hits closest shape
            IShape closestShape = null;

            //look for the closest shape
            for (int y = Camera.ScreenHeight; y > 0; y--)
            {
                for (int x = 0; x < Camera.ScreenWidth; x++)
                {
                    Point screenPoint = new Point(x, y, 0);
                    Vector rayDirection = (screenPoint - rayOrigin).Normalized();

                    foreach (IShape shape in Shapes)
                    {
                        Point intersection = shape.CheckIntersection(rayOrigin, rayDirection);
                        if (intersection != null)
                        {
                            float mag = (intersection - rayOrigin).Magnitude;
                            if (mag < minMag)
                            {
                                minMag = mag;
                                closestShape = shape;
                            }
                        }
                    }
                }
            }
            //render closest shape
            for (int y = Camera.ScreenHeight; y > 0; y--)
            {
                for (int x = 0; x < Camera.ScreenWidth; x++)
                {
                    Point screenPoint = new Point(x, y, 0);
                    Vector rayDirection = (screenPoint - rayOrigin).Normalized();

                    if (closestShape != null)
                    {
                        Point intersection = closestShape.CheckIntersection(rayOrigin, rayDirection);
                        SetScreenPointSymbol(ref symbol, closestShape, intersection);
                    }
                    else break;
                    Console.Write(symbol); //print symbol for the current screenPoint
                    Console.Write(symbol); //compensate for symbol width in console
                }
                Console.WriteLine();
            }

            return closestShape;
        }
    }
}
