namespace PolygonDrawer.Core.Edges.EdgeTypes;

[method: JsonConstructor]
public sealed class VerticalEdge(Point start, Point end) : Edge(start, end)
{
    public VerticalEdge(Edge e) : this(e.Start, e.End) { }

    public override void Accept(IEdgeVisitor visitor)
    {
        visitor.Visit(this);
    }

    public override void FixConstraint(HashSet<Point> fixedPoints)
    {
        var diffX = Start.X - End.X;

        if (!fixedPoints.Contains(Start) && !fixedPoints.Contains(End))
        {
            var halfDiffX = diffX / 2f;
            Start.X -= halfDiffX;
            End.X += halfDiffX;
        }
        else if (fixedPoints.Contains(Start))
        {
            End.X += diffX;
        }
        else if (fixedPoints.Contains(End))
        {
            Start.X -= diffX;
        }
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