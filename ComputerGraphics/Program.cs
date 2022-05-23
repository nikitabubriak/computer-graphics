using ComputerGraphics.Shapes;

namespace ComputerGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            Camera camera = new Camera(
                position:   new Point(10, 10, -20), 
                direction:  new Vector(0, 0, 1), 
                screenWidth:  20, 
                screenHeight: 20);

            Scene scene = new Scene(camera);

            Plane plane = new Plane(
                center: new Point(10, 10, 0), 
                normal: new Vector(1, 1, -0.2f));

            scene.AddShape(plane);
            scene.Render();

        }
    }
}
