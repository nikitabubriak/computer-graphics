using System;
using System.Collections.Generic;
using ComputerGraphics.Shapes;

namespace ComputerGraphics
{
    public class Scene
    {
        public List<IShape> Shapes { get; }
        public Camera Camera { get; }
        public Vector Light { get; }

        public Scene(Camera camera, Vector light)
        {
            Shapes = new List<IShape>();
            Camera = camera;
            Light = light.Normalized() * -1;
        }

        public void AddShape(IShape shape)
        {
            Shapes.Add(shape);
        }

        public void Render()
        {
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

                        //symbol = (intersection == null) ? ' ' : '#';

                        if (intersection == null) 
                                                symbol = ' ';
                        else
                        {
                            float lightLevel = Vector.Dot(Light, shape.GetIntersectionNormal(intersection));
                            switch (lightLevel)
                            {
                                case <= 0:      symbol = ' '; break;
                                case < 0.2f:    symbol = '.'; break;
                                case < 0.5f:    symbol = '*'; break;
                                case < 0.8f:    symbol = 'O'; break;
                                case >= 0.8f:   symbol = '#'; break;
                                default:        symbol = ' '; break;
                            }
                        }
                    }
                    Console.Write(symbol);
                    Console.Write(symbol); //compensate for symbol width in console
                }
                Console.WriteLine();
            }

        }
    }
}
