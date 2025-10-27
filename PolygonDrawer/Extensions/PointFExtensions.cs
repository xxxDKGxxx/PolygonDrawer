namespace PolygonDrawer.Extensions
{
    internal static class PointFExtensions
    {
        public static PointF Substract(this PointF p1, PointF p2)
        {
            return new PointF((p1.X - p2.X), (p1.Y - p2.Y));
        }
    }
}