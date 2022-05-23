

namespace ComputerGraphics
{
    public class Camera
    {
        public Point Position { get; }
        public Vector Direction { get; }
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        public Camera(Point position, Vector direction, int screenWidth, int screenHeight)
        {
            Position = position;
            Direction = direction.Normalized();

            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;
        }


    }
}
