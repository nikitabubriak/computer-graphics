using System;
using System.IO;
using System.Collections.Generic;
using ComputerGraphics.Shapes;

namespace ComputerGraphics
{
    public class ScenePPM : IRenderer
    {
        public List<IShape> Shapes { get; }
        public Camera Camera { get; }
        public Vector Light { get; }
        public float[,] ScreenLightLevelMatrix { get; }
        public string OutputPath { get; }

        public ScenePPM(string outputPath, Camera camera, Vector light = null)
        {
            Shapes = new List<IShape>();
            Camera = camera;
            if (light != null)
                Light = light.Normalized() * -1;

            ScreenLightLevelMatrix = new float[Camera.ScreenWidth, Camera.ScreenHeight];
            OutputPath = outputPath;
        }

        public void AddShape(IShape shape)
        {
            Shapes.Add(shape);
        }

        private float GetScreenPointLightLevel(IShape shape, Point intersection)
        {
            float lightLevel;

            if (intersection == null)
                lightLevel = 0;
            else if (Light == null)
                lightLevel = 1;
            else
                lightLevel = Vector.Dot(Light, shape.GetIntersectionNormal(intersection));

            return lightLevel;
        }

        //Output image to .ppm file (Portable PixMap format) 
        public void Render()
        {
            SetScreenLightLevelMatrix();

            StreamWriter output = new StreamWriter(OutputPath);

            //file header
            //"P3" means this is a RGB color image in ASCII
            output.WriteLine("P3");
            //the width and height of the image in pixels
            output.WriteLine($"{Camera.ScreenWidth} {Camera.ScreenHeight}");
            //the maximum value for each color
            output.WriteLine("255");

            //image data: RGB triplets
            float lightLevel;
            Vector color;
            Vector white = new Vector(255, 255, 255);

            for (int y = Camera.ScreenHeight; y > 0; y--)
            {
                for (int x = 0; x < Camera.ScreenWidth; x++)
                {
                    lightLevel = Math.Abs(ScreenLightLevelMatrix[x, y - 1]);

                    color = white * lightLevel;
                    int r = (int)color.X;
                    int g = (int)color.Y;
                    int b = (int)color.Z;

                    //Console.WriteLine($"{r} {g} {b}");
                    output.WriteLine($"{r} {g} {b}");
                }
            }

            output.Close();
            Console.WriteLine($"Successfully created file {OutputPath}");
        }

        

        public void SetScreenLightLevelMatrix()
        {
            if (Shapes.Count == 0) return;

            Point rayOrigin = Camera.Position;

            for (int y = Camera.ScreenHeight; y > 0; y--)
            {
                for (int x = 0; x < Camera.ScreenWidth; x++)
                {
                    //look for the closest shape at this screenPoint

                    Point screenPoint = new Point(x, y, 0);
                    Vector rayDirection = (screenPoint - rayOrigin).Normalized();

                    IShape closestShape = null;
                    Point closestIntersection = null;

                    float minMag = float.MaxValue; //magnitude of a ray that hits closest shape

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
                                closestIntersection = intersection;
                            }
                        }
                    }
                    if (closestShape != null && closestIntersection != null)
                        ScreenLightLevelMatrix[x, y - 1] = 
                            GetScreenPointLightLevel(closestShape, closestIntersection);
                    else
                        ScreenLightLevelMatrix[x, y - 1] = 0;

                    //Shapes.Remove(closestShape);
                }
            }
        }

        //public void RenderFromLightLevelMatrixToConsole()
        //{
        //    SetScreenLightLevelMatrix();

        //    for (int y = Camera.ScreenHeight; y > 0; y--)
        //    {
        //        for (int x = 0; x < Camera.ScreenWidth; x++)
        //        {
        //            char symbol = ' ';
        //            float lightLevel = ScreenLightLevelMatrix[x, y - 1];
        //            switch (lightLevel)
        //            {
        //                case <= 0: symbol = ' '; break;
        //                case < 0.2f: symbol = '.'; break;
        //                case < 0.5f: symbol = '*'; break;
        //                case < 0.8f: symbol = 'O'; break;
        //                case >= 0.8f: symbol = '#'; break;
        //                default: symbol = ' '; break;
        //            }
        //            Console.Write(symbol);
        //            Console.Write(symbol);
        //        }
        //        Console.WriteLine();
        //    }
        //}
    }
}
