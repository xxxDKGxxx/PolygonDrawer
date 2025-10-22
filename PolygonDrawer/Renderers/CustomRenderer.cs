using PolygonDrawer.Core.Rendering;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace PolygonDrawer.Renderers
{
    internal sealed class CustomRenderer : IRenderer, IGdiRenderer
    {
        private Graphics? _graphics = null;
        private Brush? _pointBrush = null;

        public void DrawBezierCurve(int x1, int y1, int x2, int y2, int cp1x, int cp1y, int cp2x, int cp2y)
        {
            
        }

        public void DrawCircle(int middlex, int middley, float radius, int xfrom, int yfrom, int xto, int yto)
        {
            throw new NotImplementedException();
        }

        public void DrawDashedLine(float x1, float y1, float x2, float y2)
        {
            var pen = new Pen(Color.Black)
            {
                DashStyle = DashStyle.Dash
            };

            _graphics?.DrawLine(pen, x1, y1, x2, y2);
        }

        public void DrawLine(int x1, int y1, int x2, int y2)
        {
            var dx = Math.Abs(x2 - x1);
            var dy = Math.Abs(y2 - y1);

            var sx = Math.Sign(x2 - x1);
            var sy = Math.Sign(y2 - y1);

            var x = x1;
            var y = y1;
            var xend = x2;
            var putPixel = () => PutPixel(x, y);

            if (dy > dx)
            {
                (x, y) = (y, x);
                (sx, sy) = (sy, sx);
                (dx, dy) = (dy, dx);
                xend = y2;
                putPixel = () => PutPixel(y, x);
            }

            var d0 = 2 * dx - dy;
            var d = d0;

            while (x != xend)
            {
                if (d > 0)
                {
                    y += sy;
                    d -= 2 * dx;
                }
                d += 2 * dy;
                x += sx;
                putPixel();
            }
        }

        public void DrawPoint(int x, int y)
        {
            _graphics?.FillEllipse(_pointBrush ?? Brushes.Black, x - 3, y - 3, 6, 6);
        }

        public void SetGraphics(Graphics graphics)
        {
            _graphics = graphics;
        }

        public void SetPointBrush(Brush brush)
        {
            _pointBrush = brush;
        }

        private void PutPixel(int x, int y)
        {
            _graphics?.FillRectangle(Brushes.Black, x, y, 1, 1);
        }
    }
}