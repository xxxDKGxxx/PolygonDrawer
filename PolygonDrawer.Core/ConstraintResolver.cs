using System.Diagnostics;

namespace PolygonDrawer.Core
{
    internal sealed class ConstraintResolver
    {
        private const int iterNum = 3000;

        internal static void ResolveConstraints(Polygon polygon, Point? movedVert = null)
        {
            var verticies = polygon.Vertices.Except([movedVert])
                .ToList();
            var i = 0;
            for (; i < iterNum; i++)
            {
                if (!polygon.Edges.Any(e => e.ConstraintViolated()))
                {
                    return;
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

                    if (!connectedEdges.Any(e => e.ConstraintViolated()))
                    {
                        continue;
                    }

                    if (connectedEdges.All(e => e.ConstraintViolated()))
                    {
                        if (connectedEdges[0].CanFixByX(point) && connectedEdges[1].CanFixByY(point))
                        {
                            connectedEdges[0].FixByX(point);
                            connectedEdges[1].FixByY(point);
                        }
                        else if (connectedEdges[0].CanFixByY(point) && connectedEdges[1].CanFixByX(point))
                        {
                            connectedEdges[0].FixByY(point);
                            connectedEdges[1].FixByX(point);
                        }
                        else
                        {
                            connectedEdges[0].FixByXY(point);
                        }
                        continue;
                    }

                    var edgeToFix = connectedEdges.First(e => e.ConstraintViolated());
                    var otherEdge = connectedEdges.First(e => !e.ConstraintViolated());

                    if (edgeToFix.CanFixByX(point))
                    {
                        var originalX = point.X;
                        edgeToFix.FixByX(point);

                        if (otherEdge.ConstraintViolated() && !otherEdge.CanFixByY(point))
                        {
                            point.X = originalX;
                        }
                        else
                        {
                            otherEdge.FixByY(point);
                            continue;
                        }
                    }

                    if (edgeToFix.CanFixByY(point))
                    {
                        var originalY = point.Y;
                        edgeToFix.FixByY(point);
                        if (otherEdge.ConstraintViolated() && !otherEdge.CanFixByX(point))
                        {
                            point.Y = originalY;
                        }
                        else
                        {
                            otherEdge.FixByX(point);
                            continue;
                        }
                    }

                    edgeToFix.FixByXY(point);
                }
            }
            Debug.WriteLine($"Constraint resolver reached {i} iters");
        }
    }
}