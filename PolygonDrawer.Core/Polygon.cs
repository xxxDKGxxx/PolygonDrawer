using PolygonDrawer.Core.Edges;
using PolygonDrawer.Core.Edges.EdgeTypes;

namespace PolygonDrawer.Core
{

    public sealed class Polygon
    {
        public List<Point> Vertices { get; } = [];
        public List<Edge> Edges { get; } = [];
        public bool IsClosed { get; private set; } = false;

        public void AddVertex(Point vertex)
        {
            if (IsClosed)
            {
                return;
            }

            Vertices.Add(vertex);

            if (Vertices.Count <= 1)
            {
                return;
            }

            var lastVertex = Vertices[^2];
            var newEdge = new Edge(lastVertex, vertex);

            Edges.Add(newEdge);
        }

        public void RemoveVertex(Point vertex)
        {
            if (!IsClosed)
            {
                return;
            }

            if (!Vertices.Remove(vertex))
            {
                return;
            }

            var edgesToRemove = Edges.Where(e => e.Start == vertex || e.End == vertex);

            var neighboringPoints = edgesToRemove
                .Select(e => e.Start == vertex ? e.End : e.Start)
                .ToList();

            Edges.RemoveAll(e => edgesToRemove.Contains(e));

            if (neighboringPoints.Count == 2)
            {
                var newEdge = new Edge(neighboringPoints[0], neighboringPoints[1]);
                Edges.Add(newEdge);
            }
        }

        public void SplitEdge(Edge edge)
        {
            if (!Edges.Contains(edge))
            {
                return;
            }

            var newVertex = new Point((edge.Start.X + edge.End.X) / 2, (edge.Start.Y + edge.End.Y) / 2);

            RemoveEdge(edge);

            var edge1 = new Edge(edge.Start, newVertex);
            var edge2 = new Edge(newVertex, edge.End);

            Edges.Add(edge1);
            Edges.Add(edge2);
            Vertices.Add(newVertex);
        }

        public void Clear()
        {
            Vertices.Clear();
            Edges.Clear();
            IsClosed = false;
        }

        public void ClosePolygon()
        {
            if (Vertices.Count <= 2 || IsClosed)
            {
                return;
            }

            var firstVertex = Vertices[0];
            var lastVertex = Vertices[^1];
            var newEdge = new Edge(lastVertex, firstVertex);

            Edges.Add(newEdge);
            IsClosed = true;
        }

        public void ReplaceEdge(Edge oldEdge, Edge newEdge)
        {
            if (!Edges.Contains(oldEdge))
            {
                return;
            }

            if (newEdge is BezierEdge bezierEdge)
            {
                bezierEdge.ControlPoint1 = new Point(
                    (oldEdge.Start.X + oldEdge.End.X) / 2,
                    oldEdge.Start.Y);
                bezierEdge.ControlPoint2 = new Point(
                    (oldEdge.Start.X + oldEdge.End.X) / 2,
                    oldEdge.End.Y);
                Vertices.AddRange([bezierEdge.ControlPoint1, bezierEdge.ControlPoint2]);
            }

            RemoveEdge(oldEdge);
            Edges.Add(newEdge);
            ConstraintResolver.ResolveConstraints(this);
        }
        public void Translate(int dx, int dy)
        {
            foreach (var v in Vertices)
            {
                v.Translate(dx, dy);
            }
        }

        public void MovePoint(Point p, int newX, int newY)
        {
            if (!Vertices.Contains(p))
            {
                return;
            }

            p.X = newX;
            p.Y = newY;
            ConstraintResolver.ResolveConstraints(this, p);
        }

        public void ChangeLength(Edge e, int newLength)
        {
            if (!Edges.Contains(e) || e is not FixedLengthEdge fle)
            {
                return;
            }
            fle.FixedLength = newLength;
            ConstraintResolver.ResolveConstraints(this);
        }

        public Point? GetVertexNear(int x, int y, float radius = 10.0f)
        {
            return Vertices.FirstOrDefault(v => v.DistanceTo(x, y) < radius);
        }

        public Edge? GetEdgeByPoints(Point p1, Point p2)
        {
            return Edges.FirstOrDefault(e => (e.Start == p1 && e.End == p2) || (e.Start == p2 && e.End == p1));
        }
        public List<Edge> GetEdgesByPoint(Point p)
        {
            return [.. Edges.Where(e => e.Start == p || e.End == p)];
        }

        public List<Edge> GetEdgeNeighbors(Edge e)
        {
            var neighbors = new List<Edge>();
            foreach (var edge in Edges)
            {
                if (edge == e 
                    || (edge.Start != e.Start 
                        && edge.Start != e.End 
                        && edge.End != e.Start 
                        && edge.End != e.End))
                {
                    continue;
                }
                neighbors.Add(edge);
            }
            return neighbors;
        }

        public (int x, int y) GetCenter()
        {
            if (Vertices.Count == 0)
            {
                return (0, 0);
            }
            var sumX = Vertices.Sum(v => v.X);
            var sumY = Vertices.Sum(v => v.Y);
            return (sumX / Vertices.Count, sumY / Vertices.Count);
        }

        private void RemoveEdge(Edge e)
        {
            Edges.Remove(e);
        }
    }
}