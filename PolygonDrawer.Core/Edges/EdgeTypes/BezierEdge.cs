using Newtonsoft.Json;
using PolygonDrawer.Core.Rendering;
using System.Numerics;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public class BezierEdge : Edge
    {
        public Point? ControlPoint1 { get; set; }
        public Point? ControlPoint2 { get; set; }


        [JsonConstructor]
        public BezierEdge(Point start, Point end) : base(start, end)
        {
            start.Type = start.Type == ContinuuityType.G0 ? ContinuuityType.C1 : start.Type;
            end.Type = end.Type == ContinuuityType.G0 ? ContinuuityType.C1 : end.Type;
        }

        public BezierEdge(Edge e) : this(e.Start, e.End)
        {

        }

        public override Vector2 GetTangentAtEnd(Point p)
        {
            if (ControlPoint1 is null || ControlPoint2 is null)
            {
                throw new InvalidOperationException("No control points provided");
            }

            return p switch
            {
                var u when u == Start => 3 * new Vector2(ControlPoint1.X - Start.X, ControlPoint1.Y - Start.Y),
                var v when v == End => 3 * new Vector2(ControlPoint2.X - End.X, ControlPoint2.Y - End.Y),
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
            Renderer?.DrawDashedLine(ControlPoint1.X, ControlPoint1.Y, ControlPoint2.X, ControlPoint2.Y);
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

        public override bool AlignG1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            if (ControlPoint1 is null || ControlPoint2 is null)
            {
                throw new InvalidOperationException("No control points provided");
            }

            if (p == Start)
            {
                var length = (float)Math.Sqrt((ControlPoint1.X - Start.X) * (ControlPoint1.X - Start.X)
                       + (ControlPoint1.Y - Start.Y) * (ControlPoint1.Y - Start.Y));
                var unitTangent = Vector2.Normalize(tangent);

                if (fixedPoints.Contains(ControlPoint1))
                {
                    Start.X = ControlPoint1.X + unitTangent.X * length;
                    Start.Y = ControlPoint1.Y + unitTangent.Y * length;

                    return true;
                }

                ControlPoint1.X = Start.X - unitTangent.X * length;
                ControlPoint1.Y = Start.Y - unitTangent.Y * length;

                return true;
            }

            if (p == End)
            {
                var length = (float)Math.Sqrt((ControlPoint2.X - End.X) * (ControlPoint2.X - End.X)
                       + (ControlPoint2.Y - End.Y) * (ControlPoint2.Y - End.Y));
                var unitTangent = Vector2.Normalize(tangent);

                if (fixedPoints.Contains(ControlPoint2))
                {
                    End.X = ControlPoint2.X + unitTangent.X * length;
                    End.Y = ControlPoint2.Y + unitTangent.Y * length;

                    return true;
                }
                ControlPoint2.X = End.X - unitTangent.X * length;
                ControlPoint2.Y = End.Y - unitTangent.Y * length;

                return true;
            }

            return false;
        }

        public override bool AlignC1(Vector2 tangent, Point p, HashSet<Point> fixedPoints)
        {
            if (ControlPoint1 is null || ControlPoint2 is null)
            {
                throw new InvalidOperationException("No control points provided");
            }

            tangent /= 3;

            if (p == Start)
            {
                if (fixedPoints.Contains(ControlPoint1))
                {
                    Start.X = ControlPoint1.X + tangent.X;
                    Start.Y = ControlPoint1.Y + tangent.Y;

                    return true;
                }

                ControlPoint1.X = Start.X - tangent.X;
                ControlPoint1.Y = Start.Y - tangent.Y;

                return true;
            }

            if (p == End)
            {
                if (fixedPoints.Contains(ControlPoint2))
                {
                    End.X = ControlPoint2.X + tangent.X;
                    End.Y = ControlPoint2.Y + tangent.Y;

                    return true;
                }

                ControlPoint2.X = End.X - tangent.X;
                ControlPoint2.Y = End.Y - tangent.Y;

                return true;
            }

            return false;
        }

        public override List<Point> GetPoints()
        {
            return [Start, End, ControlPoint1, ControlPoint2];
        }

        public override void Accept(IEdgeVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}