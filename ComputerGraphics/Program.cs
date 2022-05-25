using ComputerGraphics.Shapes;

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
            scene.AddShape(sphere);

            //Sphere sphereJr = new Sphere(
            //    center: new Point(10, 10, -5),
            //    radius: 4);
            //scene.AddShape(sphereJr);

            scene.Render(); //output the image to console
            //scene.RenderClosest();



            string outputPath = "rendered.ppm";
            ScenePPM ppm = new ScenePPM(outputPath, camera, light);

            ppm.AddShape(sphere);
            ppm.Render(); //output the image to .ppm file

        }
    }
}
