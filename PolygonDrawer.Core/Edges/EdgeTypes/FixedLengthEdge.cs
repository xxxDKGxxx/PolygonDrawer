using Newtonsoft.Json;
using PolygonDrawer.Core.Rendering;
using System.Numerics;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public sealed class FixedLengthEdge : Edge
    {
        public int FixedLength { get; set; }

        public FixedLengthEdge(Edge e) : this(e.Start, e.End)
        {
        }

        [JsonConstructor]
        public FixedLengthEdge(Point start, Point end) : base(start, end)
        {
            FixedLength = (int)Math.Round(Length);
        }

        public override bool ConstraintViolated()
        {
            return Math.Abs(Length - FixedLength) > CoreConstants.Eps;
        }

        public override void Accept(IEdgeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void FixConstraint(HashSet<Point> fixedPoints)
        {
            if (!ConstraintViolated())
            {
                return;
            }

            var dx = MathF.Abs(Start.X - End.X);
            var dy = MathF.Abs(Start.Y - End.Y);
            var sx = MathF.Sign(Start.X - End.X);
            var sy = MathF.Sign(Start.Y - End.Y);
            var scale = FixedLength / Length;

            var newDx = dx * scale;
            var newDy = dy * scale;
            var diffX = newDx - dx;
            var diffY = newDy - dy;

            if (!fixedPoints.Contains(Start) && !fixedPoints.Contains(End))
            {
                var halfDiffX = diffX / 2f;
                var halfDiffY = diffY / 2f;

                Start.X += sx * halfDiffX;
                Start.Y += sy * halfDiffY;

                End.X -= sx * halfDiffX;
                End.Y -= sy * halfDiffY;
            }
            else if (fixedPoints.Contains(Start))
            {
                End.X -= sx * diffX;
                End.Y -= sy * diffY;
            }
            else if (fixedPoints.Contains(End))
            {
                Start.X += sx * diffX;
                Start.Y += sy * diffY;
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
    }
}