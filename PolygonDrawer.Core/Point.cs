using PolygonDrawer.Core.Rendering;

namespace PolygonDrawer.Core
{
    public sealed class Point(int x, int y, ContinuuityType pointType = ContinuuityType.G0) : IRenderable
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;
        public int VertexNum { get; } = _globalVertexCounter++;
        public ContinuuityType Type { get; set; } = pointType;
        public IRenderer? Renderer { get; set; } = null;

        private static int _globalVertexCounter = 0;

        public float DistanceTo(int x, int y)
        {
            var dx = x - X;
            var dy = y - Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public void Translate(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public void Render()
        {
            Renderer?.DrawPoint(X, Y);
        }

        public override string ToString()
        {
            return $"Vertex {VertexNum}. {Type}";
        }
    }
}