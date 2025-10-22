namespace PolygonDrawer.Core.Rendering
{
    public interface IRenderer
    {
        void DrawBezierCurve(int x1, int y1, int x2, int y2, int cp1x, int cp1y, int cp2x, int cp2y);
        void DrawCircle(int middlex, int middley, float radius, int xfrom, int yfrom, int xto, int yto);
        void DrawLine(int x1, int y1, int x2, int y2);
        void DrawDashedLine(float x1, float y1, float x2, float y2);
        void DrawPoint(int x, int y);
    }
}