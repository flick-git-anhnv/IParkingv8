namespace Kztek.Object
{
    public class DesignPoint
    {
        private Vector3 position;
        private double thickness;

        public double Thickness { get => thickness; set => thickness = value; }
        public Vector3 Position { get => position; set => position = value; }

        public DesignPoint()
        {
            Position = Vector3.Zero;
            thickness = 0;
        }

        public DesignPoint(Vector3 position)
        {
            Position = position;
            Thickness = 0;
        }
    }
}
