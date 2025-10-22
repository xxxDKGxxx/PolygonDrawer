using PolygonDrawer.Core.Rendering;
using System.Drawing.Drawing2D;

namespace PolygonDrawer.Renderers
{
    internal sealed class GdiRenderer() : IRenderer, IGdiRenderer
    {
        private Graphics? _graphics;
        private Brush? _pointBrush;

        public void SetGraphics(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void SetPointBrush(Brush brush)
        {
            _pointBrush = brush;
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            _graphics?.DrawLine(Pens.Black, x1, y1, x2, y2);
        }

        public void DrawPoint(int x, int y)
        {
            if(_pointBrush is null)
            {
                return;
            }

            _graphics?.FillEllipse(_pointBrush, x - 3, y - 3, 6, 6);
        }

        public void DrawBezierCurve(int x1, int y1, int x2, int y2, int cp1x, int cp1y, int cp2x, int cp2y)
        {
            _graphics?.DrawBezier(Pens.Black, x1, y1, cp1x, cp1y, cp2x, cp2y, x2, y2);
        }

        public void DrawCircle(int middlex, int middley, float radius, int xfrom, int yfrom, int xto, int yto)
        {
            var rect = new RectangleF(
                middlex - radius,
                middley - radius,
                radius * 2,
                radius * 2);

            var startAngle = (float)(Math.Atan2(yfrom - middley, xfrom - middlex) * 180 / Math.PI);
            var endAngle = (float)(Math.Atan2(yto - middley, xto - middlex) * 180 / Math.PI);
            var sweepAngle = endAngle - startAngle;

            if (sweepAngle <= 0)
            {
                sweepAngle += 360;
            }

            _graphics?.DrawArc(Pens.Black, rect, startAngle, sweepAngle);
        }

        public void DrawDashedLine(float x1, float y1, float x2, float y2)
        {
            var pen = new Pen(Color.Black)
            {
                DashStyle = DashStyle.Dash
            };

            _graphics?.DrawLine(pen, x1, y1, x2, y2);
        }
    }
}