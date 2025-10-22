namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public sealed class CircleEdge : Edge
    {
        public CircleEdge(Edge e) : base(e)
        {
        }

        public CircleEdge(Point start, Point end) : base(start, end)
        {
        }

        public override void Render()
        {
            var middleX = (Start.X + End.X) / 2;
            var middleY = (Start.Y + End.Y) / 2;
            var radius = (float)Math.Sqrt(Math.Pow(End.X - middleX, 2) + Math.Pow(End.Y - middleY, 2));

            Renderer?.DrawCircle(middleX, middleY, radius, Start.X, Start.Y, End.X, End.Y);
        }
    }
}
