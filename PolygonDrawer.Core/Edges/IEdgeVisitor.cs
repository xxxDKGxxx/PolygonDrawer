using PolygonDrawer.Core.Edges.EdgeTypes;

namespace PolygonDrawer.Core.Edges
{
    public interface IEdgeVisitor
    {
        void Visit(Edge edge);
        void Visit(Deg45Edge edge);
        void Visit(FixedLengthEdge edge);
        void Visit(VerticalEdge edge);
        void Visit(BezierEdge edge);
        void Visit(CircleEdge edge);
    }
}
