namespace PolygonDrawer.Renderers
{
    internal interface IGdiRenderer
    {
        void SetGraphics(Graphics graphics);
        void SetPointBrush(Brush brush);
    }
}
