namespace PolygonDrawer.Core.Rendering
{
    public interface IRenderer
    {
        void DrawBezierCurve(
            float x1,
            float y1,
            float x2,
            float y2,
            float cp1x,
            float cp1y,
            float cp2x,
            float cp2y);

        void DrawCircle(
            float middlex,
            float middley,
            float radius,
            float xfrom,
            float yfrom,
            float xto,
            float yto);

        void DrawLine(float x1, float y1, float x2, float y2);
        void DrawDashedLine(float x1, float y1, float x2, float y2);
        void DrawPoint(float x, float y);
    }
}