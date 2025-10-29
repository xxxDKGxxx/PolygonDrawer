using PolygonDrawer.Core.Edges;
using PolygonDrawer.Core.Edges.EdgeTypes;

namespace PolygonDrawer.EdgeVisitors;

internal class GdiEdgeLabelRenderer(Graphics graphics) : IEdgeVisitor
{
    private readonly Graphics _graphics = graphics;

    public void Visit(Edge edge)
    {

    }

    public void Visit(Deg45Edge edge)
    {
        DrawLabel(edge, "D");
    }

    public void Visit(FixedLengthEdge edge)
    {
        DrawLabel(edge, "F");
    }

    public void Visit(VerticalEdge edge)
    {
        DrawLabel(edge, "V");
    }

    public void Visit(BezierEdge edge)
    {

    }

    public void Visit(CircleEdge edge)
    {

    }

    private void DrawLabel(Edge edge, string label)
    {
        var start = edge.Start;
        var end = edge.End;

        var mid = new PointF(
            (start.X + end.X) / 2f,
            (start.Y + end.Y) / 2f);

        using var font = new Font("Arial", 10, FontStyle.Bold);
        var brush = Brushes.Black;

        _graphics.DrawString(label, font, brush, mid);
    }
}