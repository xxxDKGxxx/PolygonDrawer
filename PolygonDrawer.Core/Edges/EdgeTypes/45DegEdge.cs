using System.Numerics;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public sealed class Deg45Edge : Edge
    {
        private const double Dampening = 1;
        public Deg45Edge(Edge e) : base(e) { }

        public Deg45Edge(Point start, Point end) : base(start, end) { }

        public override bool ConstraintViolated()
        {
            return Math.Abs(Math.Abs(Start.X - End.X) - Math.Abs(Start.Y - End.Y)) > CoreConstants.Eps;
        }

        public override void FixByX(Point p)
        {
            if (!ConstraintViolated())
            {
                return;
            }

            var otherp = Start == p ? End : Start;
            var dy = Math.Abs(otherp.Y - p.Y);

            var newX = otherp.X > p.X ? otherp.X - dy : otherp.X + dy;
            var dampedX = Lerp(p.X, newX, Dampening);

            p.X = dampedX;
        }

        public override void FixByY(Point p)
        {
            if (!ConstraintViolated())
            {
                return;
            }

            var otherp = Start == p ? End : Start;
            var dx = Math.Abs(otherp.X - p.X);

            var newY = otherp.Y > p.Y ? otherp.Y - dx : otherp.Y + dx;
            var dampedY = Lerp(p.Y, newY, Dampening);

            p.Y = dampedY;
        }

        public override void FixByXY(Point p)
        {
            if (!ConstraintViolated())
            {
                return;
            }

            var otherp = Start == p ? End : Start;

            var dx = Math.Abs(otherp.X - p.X);
            var dy = Math.Abs(otherp.Y - p.Y);

            var newX = otherp.X > p.X ? otherp.X - dy : otherp.X + dy;
            var newY = otherp.Y > p.Y ? otherp.Y - dx : otherp.Y + dx;

            if (Math.Abs(newX - p.X) < Math.Abs(newY - p.Y))
            {
                var dampedX = Lerp(p.X, newX, Dampening);
                p.X = dampedX;
            }
            else
            {
                var dampedY = Lerp(p.Y, newY, Dampening);
                p.Y = dampedY;
            }
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
}