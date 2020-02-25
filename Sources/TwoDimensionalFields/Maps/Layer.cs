using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Maps
{
    public class Layer : ILayer, IDrawable, ISearchable<MapObject>
    {
        public readonly List<MapObject> Objects;

        public Layer(string name)
        {
            Name = name;
            Objects = new List<MapObject>();
            Visible = true;
        }

        public Bounds Bounds => CalcBounds();
        public string Name { get; set; }
        public bool Selected { get; set; }
        public bool Visible { get; set; }

        public void Add(MapObject obj) => Objects.Add(obj);
        public void ClearSelection() => Objects.ForEach(obj => obj.Selected = false);
        public void Draw(IDrawer drawer) => drawer.Draw(this);
        public void Remove(int index) => Objects.RemoveAt(index);
        public void Remove(MapObject item) => Objects.Remove(item);
        public void RemoveAll() => Objects.Clear();
        public MapObject Search(ISearcher<MapObject> searcher) => searcher.Search(this);

        private Bounds CalcBounds()
        {
            return Objects.Aggregate(new Bounds(), (current, o) => current + o.Bounds);
        }
    }
}
