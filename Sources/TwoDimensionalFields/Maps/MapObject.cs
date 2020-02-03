using System;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Maps
{
    public abstract class MapObject : IMapObject, IDrawable, ISearchable<MapObject>
    {
        protected Bounds bounds;
        protected MapObjectType objectType;

        /*private Pen SelectionPen = new Pen(Color.Blue, 2);
        private Pen pen;
        public Pen Pen
        {
            get
            {
                if (pen == null) pen = new Pen(Color.Black);
                return pen;
            }
            set { pen = value; }
        }*/

        /*private Brush brush;
        public Brush Brush
        {
            get
            {
                if (brush == null) brush = new SolidBrush(Color.Gray);
                return brush;
            }
            set { brush = value; }
        }*/

        /*private Symbol symbol;
        public Symbol Symbol
        {
            get
            {
                if (symbol == null) symbol = new Symbol();
                return symbol;
            }
            set { symbol = value; }
        }*/

        public virtual Bounds Bounds
        {
            get { return bounds ?? GetBounds(); }
        }

        public MapObjectType ObjectType
        {
            get { return objectType; }
        }

        public MapObject Search(ISearcher<MapObject> searcher)
        {
            return searcher.Search(this);
        }

        public bool Selected { get; set; }

        /*public Pen GetCurrentPen()
        {
            if (Selected) return SelectionPen;
            if (UseOwnStyle) return Pen;
            else return Layer.Pen;
        }

        public Brush GetCurrentBrush()
        {
            if (UseOwnStyle) return Brush;
            else return Layer.Brush;
        }

        public Symbol GetCurrentSymbol()
        {
            if (UseOwnStyle) return Symbol;
            else return Layer.Symbol;
        }*/

        /*private bool selected = false;
        public bool Selected
        {
            get {return selected; }
            set
            {
                if (value)
                {
                    SelectionPen.Color = Layer.Map.SelectoinColor;
                    if (UseOwnStyle) SelectionPen.DashStyle = Pen.DashStyle;
                }
                selected = value;
            }
        }*/

        /*internal abstract bool IsIntersectsWithQuad(Vertex searchPoint, double d);

        internal static bool IsSegmentIntersectsWithQuad(Vertex begin, Vertex end, Vertex searchPoint, double d)
        {
            var sp_xmin = searchPoint.X - d;
            var sp_xmax = searchPoint.X + d;
            var sp_ymin = searchPoint.Y - d;
            var sp_ymax = searchPoint.Y + d;

            bool beginInside = (begin.X > sp_xmin) && (begin.Y > sp_ymin) && (begin.X < sp_xmax) && (begin.Y < sp_ymax);
            bool endInside = (end.X > sp_xmin) && (end.Y > sp_ymin) && (end.X < sp_xmax) && (end.Y < sp_ymax);

            if (beginInside || endInside) return true;
            Vertex point1 = new Vertex(sp_xmin, sp_ymax);
            Vertex point2 = new Vertex(sp_xmax, sp_ymax);
            if (IsSegmentsIntersect(begin, end, point1, point2)) return true;
            Vertex point3 = new Vertex(sp_xmax, sp_ymin);
            if (IsSegmentsIntersect(begin, end, point2, point3)) return true;
            Vertex point4 = new Vertex(sp_xmin, sp_ymin);
            if (IsSegmentsIntersect(begin, end, point3, point4)) return true;
            if (IsSegmentsIntersect(begin, end, point4, point1)) return true;
            return false;
        }

        internal static bool IsSegmentsIntersect(Vertex beginA, Vertex endA, Vertex beginB, Vertex endB)
        {
            double v1 = vector_mult(endB.X - beginB.X, endB.Y - beginB.Y, beginA.X - beginB.X, beginA.Y - beginB.Y);
            double v2 = vector_mult(endB.X - beginB.X, endB.Y - beginB.Y, endA.X - beginB.X, endA.Y - beginB.Y);
            double v3 = vector_mult(endA.X - beginA.X, endA.Y - beginA.Y, beginB.X - beginA.X, beginB.Y - beginA.Y);
            double v4 = vector_mult(endA.X - beginA.X, endA.Y - beginA.Y, endB.X - beginA.X, endB.Y - beginA.Y);
            if ((v1 * v2) < 0 && (v3 * v4) < 0)
                return true;
            return false;
        }

        private static double vector_mult(double ax, double ay, double bx, double by) //векторное произведение
        {
            return ax * by - bx * ay;
        }*/

        public void Draw(IDrawer drawer)
        {
            drawer.Draw(this);
        }

        protected abstract Bounds GetBounds();
    }
}
