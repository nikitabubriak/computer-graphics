using ComputerGraphics.Shapes;
using NUnit.Framework;

namespace ComputerGraphics.Tests
{
    public class Tests
    {
        //TODO for lab1&2:

        //Sphere with intersection point
        //Sphere without intersection point

        //Closest intersection point

        //Triangle intersection point (Moller-Trumbore)

        [SetUp]
        public void Setup()
        {
            
        }

        Point rayOrigin = new Point(10, 10, -20);

        [Test]
        public void PlaneIntersectedByRay()
        {
            Plane plane = new Plane(
                center: new Point(10, 10, 0),
                normal: new Vector(1, 1, -0.2f));
            Point intersection = plane.CheckIntersection(rayOrigin, (new Point(10, 10, 0) - rayOrigin).Normalized());
            Assert.IsNotNull(intersection);
        }

        [Test]
        public void PlaneNotIntersectedByRay()
        {
            Plane plane = new Plane(
                center: new Point(10, 10, 0),
                normal: new Vector(1, 1, -0.2f));
            Point intersection = plane.CheckIntersection(rayOrigin, (new Point(15, 15, 0) - rayOrigin).Normalized());
            Assert.IsNull(intersection);
        }
    }
}