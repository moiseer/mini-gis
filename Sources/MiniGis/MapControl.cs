using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace MiniGis
{
    public partial class MapControl : UserControl
    {
        private const string HandCur = @"Resources\hand.cur";
        private const string ZoomInCur = @"Resources\zoom-in.cur";
        private const string ZoomOutCur = @"Resources\zoom-out.cur";
        private const string MoveCur =@"Resources\dnd-move.cur";
        
        public List<Layer> Layers { get; } = new List<Layer>();
        private readonly int snap = 3;
        public Color SelectionColor { get; set; } = Color.Blue;
        private double mapScale = 1;
        public double MapScale
        {
            get { return mapScale; }
            set
            {
                if ((mapScale < 1000) || (value < mapScale))
                    mapScale = value;
                Invalidate();
            }
        }

        private bool isMouseDown;
        private System.Drawing.Point mouseDownPosition;
        private MapToolType activeTool;
        public MapToolType ActiveTool
        {
            get { return activeTool; }
            set
            {
                activeTool = value;
                switch (activeTool)
                {
                    case MapToolType.Select:
                        Cursor = Cursors.Arrow;
                        break;
                    case MapToolType.Pan:
                        Cursor = LoadCustomCursor(HandCur);
                        break;
                    case MapToolType.ZoomIn:
                        Cursor = LoadCustomCursor(ZoomInCur);
                        break;
                    case MapToolType.ZoomOut:
                        Cursor = LoadCustomCursor(ZoomOutCur);
                        break;
                }
            }
        }

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

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        public MapControl()
        {
            InitializeComponent();
            MouseWheel += Map_MouseWheel;
        }

        public Node<double> Center { get; set; } = new Node<double>(0, 0);

        public new Bounds Bounds
        {
            get
            {
                return CalcBounds();
            }
        }

        public List<MapObject> SelectedObjects { get; } = new List<MapObject>();

        public Bounds CalcBounds()
        {
            return Layers
                .Where(layer => layer.Visible)
                .Aggregate(new Bounds(), (current, layer) => current + layer.Bounds);
        }

        public void AddLayer(Layer layer)
        {
            Layers.Add(layer);
        }

        public void RemoveLayer(int index)
        {
            Layers.RemoveAt(index);
        }

        public void RemoveLayer(Layer layer)
        {
            Layers.Remove(layer);
        }

        public void RemoveAllLayer()
        {
            Layers.Clear();
        }

        private void Map_Paint(object sender, PaintEventArgs e)
        {
            var drawer = new GraphicsDrawer(e.Graphics, Center.X, Center.Y, MapScale, Width, Height);
            foreach (var layer in Layers.Where(layer => layer.Visible))
            {
                layer.Draw(drawer);
            }
        }

        public System.Drawing.Point MapToScreen(Node<double> mapPoint)
        {

            var x = (int) ((mapPoint.X - Center.X) * MapScale + Width / 2.0);
            var y = (int) (-(mapPoint.Y - Center.Y) * MapScale + Height / 2.0);
            
            return new System.Drawing.Point(x, y);
        }

        public Node<double> ScreenToMap(System.Drawing.Point screenPoint)
        {
            var x = (screenPoint.X - Width / 2) / MapScale + Center.X;
            var y = -(screenPoint.Y - Height / 2) / MapScale + Center.Y;
         
            return new Node<double>(x, y);
        }

        private void Map_Resize(object sender, EventArgs e)
        {
            Invalidate();
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
                    Cursor = LoadCustomCursor(MoveCur);
                    break;
                case MapToolType.ZoomIn:
                    break;
                case MapToolType.ZoomOut:
                    break;
            }
        }

        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            switch (ActiveTool)
            {
                case MapToolType.Select:
                    break;
                case MapToolType.Pan:
                    if (!isMouseDown) return;
                    var dX = (e.X - mouseDownPosition.X) / MapScale;
                    var dY = (e.Y - mouseDownPosition.Y) / MapScale;
                    Center.X -= dX;
                    Center.Y += dY;
                    Invalidate();
                    mouseDownPosition = e.Location;
                    break;
                case MapToolType.ZoomIn:
                    if (!isMouseDown) return;
                    var topLeft = new System.Drawing.Point
                    {
                        X = mouseDownPosition.X < e.X ? mouseDownPosition.X : e.X,
                        Y = mouseDownPosition.Y < e.Y ? mouseDownPosition.Y : e.Y
                    };
                    var bottomRight = new System.Drawing.Point
                    {
                        X = mouseDownPosition.X > e.X ? mouseDownPosition.X : e.X,
                        Y = mouseDownPosition.Y > e.Y ? mouseDownPosition.Y : e.Y
                    };
                    var g = CreateGraphics();
                    g.DrawRectangle(new Pen(Color.Blue, 2), topLeft.X, topLeft.Y,
                    bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
                    Invalidate();
                    break;
                case MapToolType.ZoomOut:
                    break;
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

                    if (dx > snap && dy > snap) break;
                    var searchPoint = ScreenToMap(e.Location);
                    var d = snap / MapScale;

                    if (ModifierKeys != Keys.Control)
                    {
                        ClearSelection();
                        Invalidate();
                    }
                    var result = FindObject(searchPoint, d);

                    if (result == null) break;
                    result.Selected = true;
                    if(!SelectedObjects.Contains(result))
                        SelectedObjects.Add(result);

                    Invalidate();
                    break;
                case MapToolType.Pan:
                    Cursor = LoadCustomCursor(HandCur);
                    break;
                case MapToolType.ZoomIn:
                    var x = (mouseDownPosition.X + e.X) / 2;
                    var y = (mouseDownPosition.Y + e.Y) / 2;
                    Center = ScreenToMap(new System.Drawing.Point(x, y));

                    var w = Math.Abs(mouseDownPosition.X - e.X);
                    var h = Math.Abs(mouseDownPosition.Y - e.Y);

                    if (w <= snap && h <= snap) MapScale *= 1.5;
                    if (w <= snap && h > snap) MapScale *= (double) Height / h;
                    if (w > snap && h <= snap) MapScale *= (double) Width / w;
                    if (w > snap && h > snap) MapScale *= Math.Min(Width / w, Height / h);

                    Invalidate();
                    break;
                case MapToolType.ZoomOut:
                    MapScale /= 1.5;
                    break;
            }
        }

        public MapObject FindObject(Node<double> searchPoint, double d)
        {
            MapObject result = null;
            /*for (int i = Layers.Count - 1; i >= 0; i--)
            {
                MapObject searchObj = Layers[i].FindObject(searchPoint, d);
                if (searchObj != null)
                {
                    result = searchObj;
                    break;
                }
            }*/
            return result;
        }

        private void Map_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                MapScale *= 2;
            else
                MapScale /= 2;
        }

        public void ZoomAll()
        {
            if (!Bounds.Valid) return;
            var w = (Bounds.XMax - Bounds.XMin) * MapScale;
            var h = (Bounds.YMax - Bounds.YMin) * MapScale;
            Center = new Node<double>((Bounds.XMin + Bounds.XMax) / 2, (Bounds.YMin + Bounds.YMax) / 2);
            if (!(w <= snap || h <= snap))
                mapScale *= Math.Min(Width / w, Height / h);
            Invalidate();
        }

        public void ZoomLayers(List<Layer> layers)
        {
            if (layers == null || layers.Count == 0)
            {
                return;
            }
            
            var bounds = layers.Aggregate(new Bounds(), (current, layer) => current + layer.Bounds);
            
            if (!bounds.Valid)
            {
                return;
            }
            
            var w = (bounds.XMax - bounds.XMin) * MapScale;
            var h = (bounds.YMax - bounds.YMin) * MapScale;
            
            Center = new Node<double>((bounds.XMin + bounds.XMax) / 2, (bounds.YMin + bounds.YMax) / 2);
            
            if (!(Math.Abs(w) < 0.001 || Math.Abs(h) < 0.001))
                mapScale *= Math.Min(Width / w, Height / h);
            
            Invalidate();
        }

        public void MoveLayerUp(Layer layer)
        {
            if (!Layers.Contains(layer))
            {
                return;
            }
            
            if (layer == Layers.Last())
            {
                return;
            }
            
            var index = Layers.IndexOf(layer);
            Layers.Remove(layer);
            Layers.Insert(index + 1, layer);
            
            Invalidate();
        }
        
        public void MoveLayerDown(Layer layer)
        {
            if (!Layers.Contains(layer))
            {
                return;
            }

            if (layer == Layers.First())
            {
                return;
            }
            
            var index = Layers.IndexOf(layer);
            Layers.Remove(layer);
            Layers.Insert(index - 1, layer);
            
            Invalidate();
        }

        public void ClearSelection()
        {
            foreach (var obj in SelectedObjects)
                obj.Selected = false;
            SelectedObjects.Clear();
        }

    }
}