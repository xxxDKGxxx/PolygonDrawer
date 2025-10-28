using Newtonsoft.Json;
using PolygonDrawer.Core.Edges;
using PolygonDrawer.Core.Rendering;
using System.Numerics;

namespace PolygonDrawer.Core
{
    [method: JsonConstructor]
    public sealed class Point(float x, float y, ContinuuityType pointType = ContinuuityType.G0) : IRenderable
    {
        public float X { get; set; } = x;
        public float Y { get; set; } = y;
        public int VertexNum { get; } = _globalVertexCounter++;
        public ContinuuityType Type { get; set; } = pointType;

        [JsonIgnore]
        public IRenderer? Renderer { get; set; } = null;

        private static int _globalVertexCounter = 0;

        public void SetRenderer(IRenderer renderer)
        {
            Renderer = renderer;
        }

        public float DistanceTo(float x, float y)
        {
            var dx = x - X;
            var dy = y - Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public void Translate(float dx, float dy)
        {
            X += dx;
            Y += dy;
        }

        public void Render()
        {
            Renderer?.DrawPoint(X, Y);
        }

        public bool ContinuuityViolated(Edge neighbor1, Edge neighbor2)
        {
            if (Type == ContinuuityType.G0)
            {
                return false;
            }

            var tangent1 = neighbor1.GetTangentAtEnd(this);
            var tangent2 = neighbor2.GetTangentAtEnd(this);

            var normalizedTangent1 = Vector2.Normalize(tangent1);
            var normalizedTangent2 = Vector2.Normalize(tangent2);


            var violated = false;

            var a1 = normalizedTangent1.X;
            var a2 = normalizedTangent1.Y;
            var b1 = normalizedTangent2.X;
            var b2 = normalizedTangent2.Y;

            if (a2 * b1 != 0 || a1 * b2 != 0)
            {
                var numerator = a1 * b2 == 0 ? a2 * b1 : a1 * b2;
                var denominator = numerator == a1 * b2 ? a2 * b1 : a1 * b2;

                violated = violated || MathF.Abs(a1 * b2 - a2 * b1) > 0.01f || Vector2.Dot(normalizedTangent1, normalizedTangent2) > -0.99f;
            }

            if (Type == ContinuuityType.C1)
            {
                var length1 = tangent1.LengthSquared();
                var length2 = tangent2.LengthSquared();

                violated = violated || MathF.Abs(length1 / length2 - 1) > 0.01f;
            }

            return violated;
        }

        public void FixContinuuityConstraint(Edge e1, Edge e2, HashSet<Point> fixedPoints)
        {
            if (Type == ContinuuityType.G0 || !ContinuuityViolated(e1, e2))
            {
                return;
            }

            var neighbor1 = e1;
            var neighbor2 = e2;

            var tangent1 = neighbor1.GetTangentAtEnd(this);
            var tangent2 = neighbor2.GetTangentAtEnd(this);

            if (Type == ContinuuityType.G1)
            {
                if (!neighbor1.AlignG1(tangent2, this, fixedPoints)
                    && !neighbor2.AlignG1(tangent1, this, fixedPoints))
                {
                    Type = ContinuuityType.G0;
                }

                return;
            }

            if (Type == ContinuuityType.C1)
            {
                if (!neighbor1.AlignC1(tangent2, this, fixedPoints)
                    && !neighbor2.AlignC1(tangent1, this, fixedPoints))
                {
                    Type = ContinuuityType.G0;
                }

                return;
            }
        }

        public override string ToString()
        {
            return $"Vertex ({X}, {Y}) {VertexNum}. {Type}";
        }
    }
}