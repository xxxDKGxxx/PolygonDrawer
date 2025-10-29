namespace PolygonDrawer.Renderers;

internal sealed class CustomRenderer : IRenderer, IGdiRenderer
{
    private Graphics? _graphics = null;
    private Brush? _pointBrush = null;

    private const float bezierD = 1 / 200f;

    public void DrawBezierCurve(
        float x1,
        float y1,
        float x2,
        float y2,
        float cp1x,
        float cp1y,
        float cp2x,
        float cp2y)
    {
        var v0 = new Vector2(x1, y1);
        var v1 = new Vector2(cp1x, cp1y);
        var v2 = new Vector2(cp2x, cp2y);
        var v3 = new Vector2(x2, y2);

        var a0 = v0;
        var a1 = 3 * (v1 - v0);
        var a2 = 3 * (v2 - 2 * v1 + v0);
        var a3 = v3 - 3 * v2 + 3 * v1 - v0;

        var p = a0;
        var dP = a3 * (float)Math.Pow(bezierD, 3) + a2 * (float)Math.Pow(bezierD, 2) + a1 * bezierD;
        var d2P = 6 * a3 * (float)Math.Pow(bezierD, 3) + 2 * a2 * (float)Math.Pow(bezierD, 2);
        var d3P = 6 * a3 * (float)Math.Pow(bezierD, 3);

        for (var t = 0f; t < 1; t += bezierD)
        {
            var nextP = p + dP;

            DrawLine((int)p[0], (int)p[1], (int)nextP[0], (int)nextP[1]);

            p = nextP;
            dP += d2P;
            d2P += d3P;
        }
    }

    public void DrawCircle(
        float middlex,
        float middley,
        float radius,
        float xfrom,
        float yfrom,
        float xto,
        float yto)
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

    public void DrawLine(float x1, float y1, float x2, float y2)
    {
        var dx = Math.Abs(x2 - x1);
        var dy = Math.Abs(y2 - y1);

        var sx = Math.Sign(x2 - x1);
        var sy = Math.Sign(y2 - y1);

        var steep = dy > dx;

        if (steep)
        {
            (x1, y1) = (y1, x1);
            (dx, dy) = (dy, dx);
            (sx, sy) = (sy, sx);
        }

        var d = 2 * dy - dx;
        var x = x1;
        var y = y1;

        for (var i = 0; i <= dx; i++)
        {
            if (steep)
            {
                PutPixel(y, x);
            }
            else
            {
                PutPixel(x, y);
            }

            if (d > 0)
            {
                y += sy;
                d -= 2 * dx;
            }

            d += 2 * dy;
            x += sx;
        }
    }

    public void DrawPoint(float x, float y)
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

    private void PutPixel(float x, float y)
    {
        _graphics?.FillRectangle(Brushes.Black, x, y, 1, 1);
    }
}