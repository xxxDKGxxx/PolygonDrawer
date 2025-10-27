using System.Diagnostics;
using System.Numerics;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public sealed class VerticalEdge(Point start, Point end) : Edge(start, end)
    {
        private const double Dampening = 1;
        public VerticalEdge(Edge e) : this(e.Start, e.End) { }
        public override bool CanFixByY(Point p)
        {
            return false;
        }

        public override void FixByX(Point p)
        {
            if (!ConstraintViolated())
            {
                return;
            }

            var otherp = Start == p ? End : Start;
            var dampedX = Lerp(p.X, otherp.X, Dampening);

            p.X = dampedX;
        }

        public override void FixByXY(Point p)
        {
            FixByX(p);
        }

        public override bool ConstraintViolated()
        {
            return Math.Abs(Start.X - End.X) > CoreConstants.Eps;
        }

        public override bool AlignG1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            return false;
        }

        public override bool AlignC1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            return false;
        }
    }
}