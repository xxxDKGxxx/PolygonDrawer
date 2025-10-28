using Newtonsoft.Json;
using PolygonDrawer.Core.Rendering;
using System.Numerics;

namespace PolygonDrawer.Core.Edges
{
    public class Edge : IRenderable
    {
        public Point Start { get; }
        public Point End { get; }

        [JsonIgnore]
        public IRenderer? Renderer { get; set; } = null;

        public float Length
        {
            get
            {
                var dx = End.X - Start.X;
                var dy = End.Y - Start.Y;
                return (float)Math.Sqrt(dx * dx + dy * dy);
            }
        }

        private static int _globalEdgeCounter = 0;

        private readonly int _edgeNum = _globalEdgeCounter++;
        [JsonConstructor]
        public Edge(Point start, Point end)
        {
            Start = start;
            End = end;

            Start.Type = ContinuuityType.G0;
            End.Type = ContinuuityType.G0;
        }

        public Edge(Edge e) : this(e.Start, e.End) { }

        public void SetRenderer(IRenderer renderer)
        {
            Renderer = renderer;
        }

        public virtual void FixConstraint(HashSet<Point> fixedPoints)
        {

        }

        public virtual bool ConstraintViolated()
        {
            return false;
        }

        public virtual void Render()
        {
            Renderer?.DrawLine(Start.X, Start.Y, End.X, End.Y);
        }

        public virtual Vector2 GetTangentAtEnd(Point p)
        {
            return p switch
            {
                var u when u == Start => new Vector2(End.X - Start.X, End.Y - Start.Y),
                var v when v == End => new Vector2(Start.X - End.X, Start.Y - End.Y),
                _ => throw new InvalidOperationException("Point is not part of the edge")
            };
        }

        public virtual bool AlignG1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            var otherp = Start == p ? End : Start;

            if (fixedPoints.Contains(otherp))
            {
                return false;
            }

            var length = Length;
            var unitTangent = Vector2.Normalize(tangent);
            var newX = p.X - unitTangent.X * length;
            var newY = p.Y - unitTangent.Y * length;

            otherp.X = newX;
            otherp.Y = newY;

            return true;
        }

        public virtual bool AlignC1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            var otherp = Start == p ? End : Start;

            if (fixedPoints.Contains(otherp))
            {
                return false;
            }

            var newX = p.X - tangent.X;
            var newY = p.Y - tangent.Y;

            otherp.X = newX;
            otherp.Y = newY;

            return true;
        }

        public virtual List<Point> GetPoints()
        {
            return [Start, End];
        }

        public virtual void Accept(IEdgeVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return $"Edge {_edgeNum}: {Start} -> {End}.";
        }

        protected static float Lerp(double from, double to, double t)
        {
            return (float)(from + (to - from) * Math.Clamp(t, 0.0, 1.0));
        }

    }
}