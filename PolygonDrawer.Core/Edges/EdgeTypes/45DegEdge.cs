using Newtonsoft.Json;
using PolygonDrawer.Core.Rendering;
using System.Numerics;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    [method: JsonConstructor]
    public sealed class Deg45Edge(Point start, Point end) : Edge(start, end)
    {
        public Deg45Edge(Edge e) : this(e.Start, e.End) { }

        public override bool ConstraintViolated()
        {
            return Math.Abs(Math.Abs(Start.X - End.X) - Math.Abs(Start.Y - End.Y)) > CoreConstants.Eps;
        }

        public override void FixConstraint(HashSet<Point> fixedPoints)
        {
            if (!ConstraintViolated())
            {
                return;
            }

            var dx = Start.X - End.X;
            var dy = Start.Y - End.Y;
            var sx = MathF.Sign(dx);
            var sy = MathF.Sign(dy);
            var diff = MathF.Abs(dx) - MathF.Abs(dy);
            var halfDiff = diff / 2f;

            if (!fixedPoints.Contains(Start) && !fixedPoints.Contains(End))
            {
                var quarterDiff = halfDiff / 2f;

                Start.X -= sx * quarterDiff;
                End.X += sx * quarterDiff;

                Start.Y += sy * quarterDiff;
                End.Y -= sy * quarterDiff;
            }
            else if (fixedPoints.Contains(Start))
            {
                End.X += sx * halfDiff;
                End.Y -= sy * halfDiff;
            }
            else if (fixedPoints.Contains(End))
            {
                Start.X -= sx * halfDiff;
                Start.Y += sy * halfDiff;
            }
        }

        public override bool AlignG1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            return false;
        }

        public override bool AlignC1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            return false;
        }

        public override void Accept(IEdgeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}