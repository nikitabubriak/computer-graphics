using System;
using System.IO;
using System.Collections.Generic;
using ComputerGraphics.Shapes;
using System.Globalization;
using System.Diagnostics;

namespace ComputerGraphics
{
    public class ScenePPM : IRenderer
    {
        public List<IShape> Shapes { get; }
        public Camera Camera { get; }
        public Vector Light { get; }
        public float[,] ScreenLightLevelMatrix { get; }
        public string InputPath { get; }
        public string OutputPath { get; }

        public ScenePPM(string inputPath, string outputPath, Camera camera, Vector light = null)
        {
            Shapes = new List<IShape>();
            Camera = camera;
            if (light != null)
                Light = light.Normalized() * -1;

            ScreenLightLevelMatrix = new float[Camera.ScreenWidth, Camera.ScreenHeight];
            InputPath = inputPath;
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
            Console.WriteLine("Creating Screen LightLevel Matrix...");
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            SetScreenLightLevelMatrix();

            stopwatch.Stop();
            double time = stopwatch.Elapsed.TotalSeconds;
            Console.WriteLine($"Successfully created Screen LightLevel Matrix in {time} seconds");

            Console.WriteLine($"Creating file {OutputPath} ...");
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
                    {
                        //check point for shadow
                        Point ShadowIntersection = null;
                        foreach (IShape shape in Shapes)
                        {
                            if (shape == closestShape) continue;
                            Point ShadowRayOrigin = closestIntersection;
                            Vector ShadowRayDirection = Light * -1;

                            ShadowIntersection = shape.CheckIntersection(ShadowRayOrigin, ShadowRayDirection);
                        }

                        if (ShadowIntersection != null)
                        {
                            ScreenLightLevelMatrix[x, y - 1] = 0.1f;
                        }
                        else ScreenLightLevelMatrix[x, y - 1] =
                           GetScreenPointLightLevel(closestShape, closestIntersection);
                    }
                    else ScreenLightLevelMatrix[x, y - 1] = 0;
                }
            }
        }

        public void ReadObj()
        {
            StreamReader input = new StreamReader(InputPath);
            NumberFormatInfo format = CultureInfo.InvariantCulture.NumberFormat;

            //triangle vertices
            List<Point> v = new List<Point>();
            
            string line;

            while ((line = input.ReadLine()) != null)
            {
                if (line == "")
                    continue;

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                switch (parts[0])
                {
                    case "#":
                        break;
                    case "g":
                        break;
                    case "v":
                        {
                            float x = float.Parse(parts[1], format);
                            float y = float.Parse(parts[2], format);
                            float z = float.Parse(parts[3], format);

                            v.Add(new Point(x, y, z));
                            break;
                        }
                    case "vt":
                        break;
                    case "vn":
                        break;
                    case "f":
                        {
                            Point[] abc = new Point[3];

                            for (int i = 1; i < parts.Length; i++)
                            {
                                string[] indexes = parts[i].Split("/");
                                abc[i - 1] = v[int.Parse(indexes[0]) - 1];
                            }

                            AddShape(new Triangle(abc[0], abc[1], abc[2]));
                            break;
                        }
                    default: 
                        break;
                }
            }

            input.Close();
            Console.WriteLine($"Successfully read file {InputPath}");
        }

        public void RenderFromLightLevelMatrixToConsole()
        {
            SetScreenLightLevelMatrix();

            for (int y = Camera.ScreenHeight; y > 0; y--)
            {
                for (int x = 0; x < Camera.ScreenWidth; x++)
                {
                    char symbol = ' ';
                    float lightLevel = ScreenLightLevelMatrix[x, y - 1];
                    switch (lightLevel)
                    {
                        case <= 0: symbol = ' '; break;
                        case < 0.2f: symbol = '.'; break;
                        case < 0.5f: symbol = '*'; break;
                        case < 0.8f: symbol = 'O'; break;
                        case >= 0.8f: symbol = '#'; break;
                        default: symbol = ' '; break;
                    }
                    Console.Write(symbol);
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }
    }
}
