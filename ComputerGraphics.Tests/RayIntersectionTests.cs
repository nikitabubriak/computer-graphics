using ComputerGraphics.Shapes;
using NUnit.Framework;

namespace ComputerGraphics.Tests
{
    public class Tests
    {
        //TODO for lab1&2:

        //Closest intersection point
        //Triangle with intersection
        //Triangle without intersection

        Point rayOrigin; 
        Point screenPoint;
        Point intersection; 
        
        Plane plane;
        Sphere sphere;


        [SetUp]
        public void Setup()
        {
            rayOrigin = new Point(10, 10, -20); //camera position

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
    }
}