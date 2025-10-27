using System.Numerics;

namespace PolygonDrawer.Core.Edges.EdgeTypes
{
    public sealed class FixedLengthEdge : Edge
    {

        public int FixedLength { get; set; }

        private const float Dampening = 0.4f;

        public FixedLengthEdge(Edge e) : this(e.Start, e.End)
        {
        }

        public FixedLengthEdge(Point start, Point end) : base(start, end)
        {
            FixedLength = (int)Math.Round(Length);
        }

        public override bool ConstraintViolated()
        {
            return !(Math.Abs(Length / FixedLength - 1) < 0.01);
        }


        // results better when scaling, not fixing by single axis
        public override bool CanFixByX(Point p)
        {
            //var dy = End.Y - Start.Y;
            //return FixedLength * FixedLength - dy * dy >= 0;
            return false;
        }

        public override bool CanFixByY(Point p)
        {
            //var dx = End.X - Start.X;
            //return FixedLength * FixedLength - dx * dx >= 0;
            return false;
        }

        public override void FixByX(Point p)
        {
            if (!ConstraintViolated() || !CanFixByX(p))
            {
                return;
            }

            var otherp = Start == p ? End : Start;
            var dy = p.Y - otherp.Y;
            var sq = (double)FixedLength * FixedLength - dy * dy;

            if (sq < 0)
            {
                return;
            }

            var newDx = Math.Sqrt(sq);
            var dir = Math.Sign(p.X - otherp.X);

            if (dir == 0) dir = 1;

            var targetX = otherp.X + dir * newDx;
            var dampedX = Lerp(p.X, targetX, Dampening);

            p.X = ((float)Math.Round(dampedX));
        }

        public override void FixByY(Point p)
        {
            if (!ConstraintViolated() || !CanFixByY(p))
            {
                return;
            }

            var otherp = Start == p ? End : Start;
            var dx = p.X - otherp.X;
            var sq = (double)FixedLength * FixedLength - dx * dx;

            if (sq < 0)
            {
                return;
            }

            var newDy = Math.Sqrt(sq);
            var dir = Math.Sign(p.Y - otherp.Y);

            if (dir == 0) dir = 1;

            var targetY = otherp.Y + dir * newDy;
            var dampedY = Lerp(p.Y, targetY, Dampening);

            p.Y = ((float)Math.Round(dampedY));
        }

        public override void FixByXY(Point p)
        {
            if (!ConstraintViolated())
            {
                return;
            }

            var otherp = Start == p ? End : Start;

            var dx = p.X - otherp.X;
            var dy = p.Y - otherp.Y;
            var currentLength = Math.Sqrt(dx * dx + dy * dy);

            var scale = FixedLength / currentLength;

            var targetXDouble = otherp.X + dx * scale;
            var targetYDouble = otherp.Y + dy * scale;

            var dampedX = Lerp(p.X, targetXDouble, Dampening);
            var dampedY = Lerp(p.Y, targetYDouble, Dampening);

            p.X = ((float)Math.Round(dampedX));
            p.Y = ((float)Math.Round(dampedY));
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