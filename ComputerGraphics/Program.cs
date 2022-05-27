using ComputerGraphics.Shapes;
using System.Numerics;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            //left-handed coordinate system

            Camera camera = new Camera(
                position:   new Point(10, 10, -20), 
                direction:  new Vector(0, 0, 1), 
                screenWidth:  20, 
                screenHeight: 20);

            Vector light = new Vector(-14, -10, 16);

            Scene scene = new Scene(camera, light);

            //Plane plane = new Plane(
            //    center: new Point(10, 10, 0),
            //    normal: new Vector(1, 1, -0.2f));
            //scene.AddShape(plane);

            Sphere sphere = new Sphere(
                center: new Point(10, 10, 0),
                radius: 8);
            //scene.AddShape(sphere);

            //Sphere sphereJr = new Sphere(
            //    center: new Point(10, 10, -5),
            //    radius: 4);
            //scene.AddShape(sphereJr);

            Triangle triangle = new Triangle(
                a: new Point(2, 5, -1),
                b: new Point(7, 16, -3),
                c: new Point(12, 5, -7));
            //scene.AddShape(triangle);


            //scene.Render(); //output the image to console
            //scene.RenderClosest();

            //Sphere shadowSphere = new Sphere(
            //    center: new Point(11, 11, 5),
            //    radius: 9);
            //Triangle shadowTriangle = new Triangle(
            //    a: new Point(0, 20, 0),
            //    b: new Point(10, 15, 10),
            //    c: new Point(10, 5, 0));
            //Camera shadowCamera = new Camera(
            //    position: new Point(0, 0, -20),
            //    direction: new Vector(0, 0, 1),
            //    screenWidth: 40,
            //    screenHeight: 40);
            //ppm.AddShape(shadowTriangle);
            //ppm.AddShape(shadowSphere);
            //ScenePPM ppm = new ScenePPM(inputPath, outputPath, shadowCamera, light);


            //ppm.RenderFromLightLevelMatrixToConsole();

            string inputPath = args[0].Substring(9);  //--source=cow.obj
            string outputPath = args[1].Substring(9); //--output=rendered.ppm



            camera = new Camera(
                position: new Point(0, 0, -1000),
                direction: new Vector(0, 0, 1),
                screenWidth: 1000,
                screenHeight: 1000);
            ScenePPM ppm = new ScenePPM(inputPath, outputPath, camera, light);
            //ppm.AddShape(sphere);


            //Matrix4x4 transformation = TransformationMatrix.CreateTransformationMatrix(
            //    //scale
            //    1f, 2f, 1f,
            //    //rotate
            //    30, 0, 30,
            //    //translate
            //    5, 0, 10);
            //IShape newTriangle = triangle.Transform(transformation);
            //ppm.AddShape(newTriangle);




            ppm.ReadObj(); //input triangles from .obj file to the shape list

            Matrix4x4 cowTransformation = TransformationMatrix.CreateTransformationMatrix(
                //scale
                1f, 2f, 1f,
                //rotate
                -15, 0, 0,
                //translate
                350, 400, 500);
            ppm.TransformShapes(cowTransformation); //transform triangles

            ppm.Render(); //output the image to .ppm file
            //ppm.RenderFromLightLevelMatrixToConsole();

        }
    }
}
