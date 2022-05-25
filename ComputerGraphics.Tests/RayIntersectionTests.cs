using ComputerGraphics.Shapes;
using NUnit.Framework;

namespace ComputerGraphics.Tests
{
    public class Tests
    {
        //TODO for lab2:

        //Triangle with intersection
        //Triangle without intersection

        Point rayOrigin; 
        Point screenPoint;
        Point intersection; 
        
        Plane plane;
        Sphere sphere;

        Camera camera;
        Scene scene;

        [SetUp]
        public void Setup()
        {
            camera = new Camera(
                position: new Point(10, 10, -20),
                direction: new Vector(0, 0, 1),
                screenWidth: 20,
                screenHeight: 20);
            scene = new Scene(camera);

            rayOrigin = camera.Position;
        }

        [Test]
        public void PlaneIntersectedByRay()
        {
            screenPoint = new Point(10, 10, 0);
            plane = new Plane(
                center: new Point(10, 10, 0),
                normal: new Vector(1, 1, -0.2f));

            intersection = plane.CheckIntersection(rayOrigin, 
                (screenPoint - rayOrigin).Normalized());

            Assert.IsNotNull(intersection);
        }

        [Test]
        public void PlaneNotIntersectedByRay()
        {
            screenPoint = new Point(15, 15, 0);
            plane = new Plane(
                center: new Point(10, 10, 0),
                normal: new Vector(1, 1, -0.2f));

            intersection = plane.CheckIntersection(rayOrigin, 
                (screenPoint - rayOrigin).Normalized());

            Assert.IsNull(intersection);
        }

        [Test]
        public void SphereIntersectedByRay()
        {
            screenPoint = new Point(10, 10, 0);
            sphere = new Sphere(
                center: new Point(10, 10, 0),
                radius: 4);

            intersection = sphere.CheckIntersection(rayOrigin, 
                (screenPoint - rayOrigin).Normalized());

            Assert.IsNotNull(intersection);
        }

        [Test]
        public void SphereNotIntersectedByRay()
        {
            screenPoint = new Point(15, 15, 0);
            sphere = new Sphere(
                center: new Point(10, 10, 0),
                radius: 4);

            intersection = sphere.CheckIntersection(rayOrigin, 
                (screenPoint - rayOrigin).Normalized());

            Assert.IsNull(intersection);
        }

        [Test]
        public void RenderClosestReturnsClosestShapeFromTwoSpheres()
        {
            Sphere sphere1 = new Sphere(
                center: new Point(10, 10, -5), //center is closer to camera by 5 units
                radius: 8);
            scene.AddShape(sphere1);

            Sphere sphere2 = new Sphere(
                center: new Point(10, 10, 0), 
                radius: 8);
            scene.AddShape(sphere2);

            IShape expectedClosest = sphere1;
            IShape actualClosest = scene.RenderClosest();

            Assert.AreSame(actualClosest, expectedClosest);
        }

        [Test]
        public void RenderClosestReturnsClosestShapeFromTwoSpheresAndOnePlane()
        {
            Sphere sphere1 = new Sphere(
                center: new Point(10, 10, 2),
                radius: 8);
            scene.AddShape(sphere1);

            Plane plane1 = new Plane(
                center: new Point(10, 10, 0), //not the closest center to camera,
                normal: new Vector(1, 1, -0.2f)); //but plane's left side is skewed closest to the camera
            scene.AddShape(plane1);

            Sphere sphere2 = new Sphere(
                center: new Point(10, 10, -4),
                radius: 8);
            scene.AddShape(sphere2);

            IShape expectedClosest = plane1;
            IShape actualClosest = scene.RenderClosest();

            Assert.AreSame(actualClosest, expectedClosest);
        }
    }
}