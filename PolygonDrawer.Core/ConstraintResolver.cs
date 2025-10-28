using System.Diagnostics;

namespace PolygonDrawer.Core
{
    internal sealed record ResolverState(IDictionary<Point, (float, float)> State);

    internal sealed class ConstraintResolver
    {
        private const int iterNum = 200000;

        internal static void ResolveConstraints(
            Polygon polygon,
            Point? movedVert = null,
            (float, float)? movedVertOldPos = null)
        {
            var stateDict = polygon.Vertices.ToDictionary(
                p => p,
                p => (p.X, p.Y));

            if (movedVert is not null
                && movedVertOldPos is not null)
            {
                stateDict[movedVert] = movedVertOldPos.Value;
            }

            var polygonState = new ResolverState(stateDict);
            var verticies = polygon.Vertices.ToArray();

            var i = 0;
            var random = new Random();
            var fixedPoints = new HashSet<Point>();

            if (movedVert is not null)
            {
                fixedPoints.Add(movedVert);
            }

            for (; i < iterNum; i++)
            {
                if (!polygon.Edges.Any(e => e.ConstraintViolated()) && !polygon.Vertices.Any(v =>
                {
                    var connectedEdges = polygon.GetEdgesByPoint(v);
                    return connectedEdges.Count == 2
                        && v.ContinuuityViolated(connectedEdges[0], connectedEdges[1]);
                }))
                {
                    return;
                }

                foreach(var edge in polygon.Edges)
                {
                    edge.FixConstraint(fixedPoints);
                }

                foreach (var point in verticies)
                {
                    if (point is null)
                    {
                        continue;
                    }

                    var connectedEdges = polygon.GetEdgesByPoint(point);

                    if (connectedEdges.Count != 2)
                    {
                        continue;
                    }

                    var randIdx = random.Next(0, 2);

                    point.FixContinuuityConstraint(
                        connectedEdges[randIdx],
                        connectedEdges[1 - randIdx],
                        fixedPoints);
                }
            }
            Debug.WriteLine($"{DateTime.Now} Constraint resolver reached {i} iters");

            if (movedVert is null || movedVertOldPos is null)
            {
                return;
            }

            var dx = movedVert.X - movedVertOldPos.Value.Item1;
            var dy = movedVert.Y - movedVertOldPos.Value.Item2;

            RestoreOldState(polygonState, polygon);

            polygon.Translate(dx, dy);
        }

        private static void RestoreOldState(ResolverState resolverState, Polygon polygon)
        {
            foreach (var vertex in polygon.Vertices)
            {
                if (!resolverState.State.TryGetValue(vertex, out var state))
                {
                    continue;
                }

                (vertex.X, vertex.Y) = state;
            }
        }
    }
}