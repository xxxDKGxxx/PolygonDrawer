using PolygonDrawer.Core.Edges;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public class BezierEdge(
        Point start,
        Point end) : Edge(start, end)
    {
        public Point? ControlPoint1 { get; set; }
        public Point? ControlPoint2 { get; set; }

        public BezierEdge(Edge e) : this(e.Start, e.End)
        {

        }

        public override SizeF GetTangentAtEnd(Point p)
        {
            if (ControlPoint1 is null || ControlPoint2 is null)
            {
                throw new InvalidOperationException("No control points provided");
            }

            return p switch
            {
                var u when u == Start => new SizeF(ControlPoint1.X - Start.X, ControlPoint1.Y - Start.Y),
                var v when v == End => new SizeF(ControlPoint2.X - End.X, ControlPoint2.Y - End.Y),
                _ => throw new InvalidOperationException("Point is not a part of the bezier edge")
            };
        }

        public override void Render()
        {
            if (ControlPoint1 == null || ControlPoint2 == null)
            {
                return;
            }

            Renderer?.DrawDashedLine(Start.X, Start.Y, ControlPoint1.X, ControlPoint1.Y);
            Renderer?.DrawDashedLine(ControlPoint1.X, ControlPoint1.Y,  ControlPoint2.X, ControlPoint2.Y);
            Renderer?.DrawDashedLine(ControlPoint2.X, ControlPoint2.Y, End.X, End.Y);

            Renderer?.DrawBezierCurve(
                Start.X,
                Start.Y,
                End.X,
                End.Y,
                ControlPoint1.X,
                ControlPoint1.Y,
                ControlPoint2.X,
                ControlPoint2.Y);
        }

    }
}
