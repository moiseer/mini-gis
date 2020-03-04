using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;
using TwoDimensionalFields.Searching;
using Point = System.Drawing.Point;

namespace MiniGis
{
    public partial class MapControl : UserControl
    {
        private const string HandCurPath = @"Resources\hand.cur";
        private const string MoveCurPath = @"Resources\dnd-move.cur";
        private const string ZoomInCurPath = @"Resources\zoom-in.cur";
        private const string ZoomOutCurPath = @"Resources\zoom-out.cur";

        private readonly Map map;
        private readonly int snap;

        private MapToolType activeTool;
        private Cursor handCur;
        private bool isMouseDown;
        private Point mouseDownPosition;
        private Cursor moveCur;
        private Cursor zoomInCur;
        private Cursor zoomOutCur;

        public MapControl()
        {
            SelectedObjects = new List<MapObject>();
            snap = 5;
            map = new Map();
            InitializeComponent();
            MouseWheel += Map_MouseWheel;
            LoadCursors();
        }

        public MapToolType ActiveTool
        {
            get => activeTool;
            set
            {
                activeTool = value;
                switch (activeTool)
                {
                    case MapToolType.Select:
                        Cursor = Cursors.Arrow;
                        break;
                    case MapToolType.Pan:
                        Cursor = handCur;
                        break;
                    case MapToolType.ZoomIn:
                        Cursor = zoomInCur;
                        break;
                    case MapToolType.ZoomOut:
                        Cursor = zoomOutCur;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public List<ILayer> Layers => map.Layers;
        public List<MapObject> SelectedObjects { get; }
        public void AddLayer(ILayer layer) => map.Layers.Add(layer);

        public void MoveLayerDown(ILayer layer)
        {
            if (!map.Layers.Contains(layer))
            {
                return;
            }

            if (layer == map.Layers.First())
            {
                return;
            }

            var index = map.Layers.IndexOf(layer);
            RemoveLayer(layer);
            InsertLayer(index - 1, layer);

            Invalidate();
        }

        public void MoveLayerUp(ILayer layer)
        {
            if (!map.Layers.Contains(layer))
            {
                return;
            }

            if (layer == map.Layers.Last())
            {
                return;
            }

            var index = map.Layers.IndexOf(layer);
            RemoveLayer(layer);
            InsertLayer(index + 1, layer);

            Invalidate();
        }

        public void RemoveAllLayer() => map.Layers.Clear();
        public void RemoveLayer(int index) => map.Layers.RemoveAt(index);
        public void RemoveLayer(ILayer layer) => map.Layers.Remove(layer);

        public Node<double> ScreenToMap(Point screenPoint)
        {
            var x = (screenPoint.X - Width / 2) / map.Scale + map.Center.X;
            var y = -(screenPoint.Y - Height / 2) / map.Scale + map.Center.Y;

            return new Node<double>(x, y);
        }

        public void ZoomAll()
        {
            var bounds = CalcBounds();
            if (!bounds.Valid)
            {
                return;
            }

            var w = (bounds.XMax - bounds.XMin) * map.Scale;
            var h = (bounds.YMax - bounds.YMin) * map.Scale;

            map.Center = new Node<double>((bounds.XMin + bounds.XMax) / 2, (bounds.YMin + bounds.YMax) / 2);

            if (w > snap && h > snap)
            {
                map.Scale *= Math.Min(Width / w, Height / h);
            }

            Invalidate();
        }

        public void ZoomLayers(IEnumerable<ILayer> layers)
        {
            if (layers == null || !layers.Any())
            {
                return;
            }

            var bounds = layers.Aggregate(new Bounds(), (current, layer) => current + layer.Bounds);

            if (!bounds.Valid)
            {
                return;
            }

            var w = (bounds.XMax - bounds.XMin) * map.Scale;
            var h = (bounds.YMax - bounds.YMin) * map.Scale;

            map.Center = new Node<double>((bounds.XMin + bounds.XMax) / 2, (bounds.YMin + bounds.YMax) / 2);

            if (Math.Abs(w) > 0.001 && Math.Abs(h) > 0.001)
            {
                map.Scale *= Math.Min(Width / w, Height / h);
            }

            Invalidate();
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        private static Cursor LoadCustomCursor(string path)
        {
            var hCurs = LoadCursorFromFile(path);
            if (hCurs == IntPtr.Zero)
            {
                throw new Win32Exception();
            }

            var curs = new Cursor(hCurs);

            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi?.SetValue(curs, true);
            return curs;
        }

        private Bounds CalcBounds()
        {
            return map.Layers
                .Where(layer => layer.Visible)
                .Aggregate(new Bounds(), (current, layer) => current + layer.Bounds);
        }

        private void ClearSelection()
        {
            foreach (var obj in SelectedObjects)
            {
                obj.Selected = false;
            }

            SelectedObjects.Clear();
        }

        private void InsertLayer(int index, ILayer layer) => map.Layers.Insert(index, layer);

        private void LoadCursors()
        {
            handCur = LoadCustomCursor(HandCurPath);
            moveCur = LoadCustomCursor(MoveCurPath);
            zoomInCur = LoadCustomCursor(ZoomInCurPath);
            zoomOutCur = LoadCustomCursor(ZoomOutCurPath);
        }

        private void Map_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            mouseDownPosition = e.Location;

            switch (ActiveTool)
            {
                case MapToolType.Select:
                    break;
                case MapToolType.Pan:
                    Cursor = moveCur;
                    break;
                case MapToolType.ZoomIn:
                    break;
                case MapToolType.ZoomOut:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isMouseDown)
            {
                return;
            }

            switch (ActiveTool)
            {
                case MapToolType.Select:
                    break;
                case MapToolType.Pan:
                    var dX = (e.X - mouseDownPosition.X) / map.Scale;
                    var dY = (e.Y - mouseDownPosition.Y) / map.Scale;

                    map.Center.X -= dX;
                    map.Center.Y += dY;

                    Invalidate();

                    mouseDownPosition = e.Location;

                    break;
                case MapToolType.ZoomIn:
                    var topLeft = new Point
                    {
                        X = mouseDownPosition.X < e.X ? mouseDownPosition.X : e.X,
                        Y = mouseDownPosition.Y < e.Y ? mouseDownPosition.Y : e.Y
                    };

                    var bottomRight = new Point
                    {
                        X = mouseDownPosition.X > e.X ? mouseDownPosition.X : e.X,
                        Y = mouseDownPosition.Y > e.Y ? mouseDownPosition.Y : e.Y
                    };

                    var g = CreateGraphics();
                    g.DrawRectangle(new Pen(Color.Blue, 2),
                        topLeft.X,
                        topLeft.Y,
                        bottomRight.X - topLeft.X,
                        bottomRight.Y - topLeft.Y);

                    Invalidate();

                    break;
                case MapToolType.ZoomOut:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Map_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            switch (ActiveTool)
            {
                case MapToolType.Select:
                    var dx = Math.Abs(mouseDownPosition.X - e.X);
                    var dy = Math.Abs(mouseDownPosition.Y - e.Y);

                    if (dx > snap && dy > snap)
                    {
                        break;
                    }

                    Node<double> searchPoint = ScreenToMap(e.Location);
                    var delta = snap / map.Scale;

                    if (ModifierKeys != Keys.Control)
                    {
                        ClearSelection();
                        Invalidate();
                    }

                    var result = map.Search(new MapObjectSearcher(searchPoint, delta));

                    if (result == null)
                    {
                        break;
                    }

                    result.Selected = true;
                    if (!SelectedObjects.Contains(result))
                    {
                        SelectedObjects.Add(result);
                    }

                    Invalidate();
                    break;
                case MapToolType.Pan:
                    Cursor = handCur;
                    break;
                case MapToolType.ZoomIn:
                    var x = (mouseDownPosition.X + e.X) / 2;
                    var y = (mouseDownPosition.Y + e.Y) / 2;
                    map.Center = ScreenToMap(new Point(x, y));

                    var w = Math.Abs(mouseDownPosition.X - e.X);
                    var h = Math.Abs(mouseDownPosition.Y - e.Y);

                    if (w <= snap && h <= snap)
                    {
                        map.Scale *= 1.5;
                    }

                    if (w <= snap && h > snap)
                    {
                        map.Scale *= (double)Height / h;
                    }

                    if (w > snap && h <= snap)
                    {
                        map.Scale *= (double)Width / w;
                    }

                    if (w > snap && h > snap)
                    {
                        map.Scale *= Math.Min(Width / w, Height / h);
                    }

                    Invalidate();
                    break;
                case MapToolType.ZoomOut:
                    map.Scale /= 1.5;
                    Invalidate();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                map.Scale *= 2;
            }
            else
            {
                map.Scale /= 2;
            }

            Invalidate();
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            var drawer = new GraphicsDrawer(e.Graphics);
            var bounds = GetBounds();

            drawer.SetParams(map.Center.X, map.Center.Y, map.Scale, Width, Height, bounds);

            map.Draw(drawer);
        }

        private void Map_Resize(object sender, EventArgs e) => Invalidate();

        private Bounds GetBounds()
        {
            (double x1, double y1) = ScreenToMap(new Point(0, 0));
            (double x2, double y2) = ScreenToMap(new Point(Width, Height));

            return new Bounds(x1, y1, x2, y2);
        }
    }
}
