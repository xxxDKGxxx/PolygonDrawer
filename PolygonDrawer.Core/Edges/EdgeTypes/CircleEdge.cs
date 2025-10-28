using Newtonsoft.Json;
using PolygonDrawer.Core.Rendering;
using System.Numerics;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public sealed class CircleEdge : Edge
    {
        private float _middleXDelta = 0;
        private float _middleYDelta = 0;
        private float MiddleX => (Start.X + End.X) / 2 + _middleXDelta;
        private float MiddleY => (Start.Y + End.Y) / 2 + _middleYDelta;
        private float Radius => (float)Math.Sqrt(Math.Pow(End.X - MiddleX, 2)
            + Math.Pow(End.Y - MiddleY, 2));
        private Vector2? _lastTangent = null;

        public CircleEdge(Edge e) : this(e.Start, e.End)
        {

        }

        [JsonConstructor]
        public CircleEdge(Point start, Point end) : base(start, end)
        {
            start.Type = start.Type == ContinuuityType.C1
                ? ContinuuityType.G0
                : start.Type;

            end.Type = end.Type == ContinuuityType.C1
                    || start.Type == ContinuuityType.G1
                ? ContinuuityType.G0
                : end.Type;
        }

        public override void Render()
        {
            Renderer?.DrawCircle(MiddleX, MiddleY, Radius, Start.X, Start.Y, End.X, End.Y);
        }

        public override Vector2 GetTangentAtEnd(Point p)
        {
            if (Start.Type == ContinuuityType.C1)
            {
                Start.Type = ContinuuityType.G1;
            }

            if (End.Type == ContinuuityType.C1)
            {
                if (Start.Type == ContinuuityType.G1)
                {
                    End.Type = ContinuuityType.G0;
                }
                else
                {
                    End.Type = ContinuuityType.G1;
                }
            }

            if (float.IsInfinity(_middleXDelta)
                || float.IsInfinity(_middleYDelta)
                || float.IsNaN(_middleXDelta)
                || float.IsNaN(_middleYDelta))
            {
                ResetMiddle();
            }

            if (p == Start)
            {
                var baseAngle = (float)Math.Atan2(Start.Y - MiddleY, Start.X - MiddleX);
                var angleDiff = Math.Atan2(End.Y - MiddleY, End.X - MiddleX) - baseAngle;

                if (angleDiff < 0)
                {
                    angleDiff += 2 * Math.PI;
                }

                return Radius * new Vector2(
                    (float)(-Math.Sin(baseAngle) * angleDiff),
                    (float)(Math.Cos(baseAngle) * angleDiff)
                );
            }

            if (p == End)
            {
                var baseAngle = (float)Math.Atan2(End.Y - MiddleY, End.X - MiddleX);
                var angleDiff = Math.Atan2(Start.Y - MiddleY, Start.X - MiddleX) - baseAngle;

                if (angleDiff > 0)
                {
                    angleDiff -= 2 * Math.PI;
                }

                return Radius * new Vector2(
                    (float)(-Math.Sin(baseAngle) * angleDiff),
                    (float)(Math.Cos(baseAngle) * angleDiff)
                );
            }

            throw new InvalidOperationException("Point is not a part of the circle edge");
        }

        public override bool AlignG1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            _lastTangent = new Vector2(tangent.X, tangent.Y);

            tangent *= -1;

            var perpendicularTangent = new Vector2(-tangent.Y, tangent.X);
            var middleX = (Start.X + End.X) / 2;
            var middleY = (Start.Y + End.Y) / 2;
            var middleToP = new Vector2(p.X - middleX, p.Y - middleY);
            var middleTangent = new Vector2(-middleToP.Y, middleToP.X);

            var middleTangentCoeff = (middleY * perpendicularTangent.X
                    - p.Y * perpendicularTangent.X
                    - middleX * perpendicularTangent.Y
                    + p.X * perpendicularTangent.Y)
                / (perpendicularTangent.Y * middleTangent.X - middleTangent.Y * perpendicularTangent.X);

            _middleXDelta = middleTangentCoeff * middleTangent.X;
            _middleYDelta = middleTangentCoeff * middleTangent.Y;

            return true;
        }

        public override bool ConstraintViolated()
        {
            var middleToStart = MathF.Pow(Start.X - MiddleX, 2) + MathF.Pow(Start.Y - MiddleY, 2);
            var middleToEnd = MathF.Pow(End.X - MiddleX, 2) + MathF.Pow(End.Y - MiddleY, 2);

            return MathF.Abs(middleToStart - middleToEnd) > CoreConstants.Eps;
        }

        public override void Accept(IEdgeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void FixConstraint(HashSet<Point> fixedPoints)
        {
            if (_lastTangent is null)
            {
                return;
            }

            if (Start.Type != ContinuuityType.G0)
            {
                AlignG1(_lastTangent.Value, Start, []);
            }

            if (End.Type == ContinuuityType.G0)
            {
                return;
            }

            AlignG1(_lastTangent.Value, End, []);
        }

        public void ResetMiddle()
        {
            _middleXDelta = 0;
            _middleYDelta = 0;
        }
    }
}