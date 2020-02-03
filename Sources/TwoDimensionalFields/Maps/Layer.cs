using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Maps
{
    public class Layer : ILayer, IDrawable, ISearchable<IMapObject>
    {
        public List<IMapObject> Objects = new List<IMapObject>();

        public Layer(string name)
        {
            Name = name;
            Visible = true;
        }

        public Bounds Bounds
        {
            get { return CalcBounds(); }
        }

        public string Name { get; set; }
        public bool Selected { get; set; }

        public bool Visible { get; set; }

        public void Add(IMapObject obj)
        {
            Objects.Add(obj);
        }

        /*public MapObject FindObject(Vertex searchPoint, double d)
        {
            if (Visible)
                for (int i = Objects.Count - 1; i >= 0; i--)
                {
                    if (Objects[i].IsIntersectsWithQuad(searchPoint, d))
                    {
                        return Objects[i];
                    }
                }
            return null;
        }*/

        public void ClearSelection()
        {
            foreach (var obj in Objects)
            {
                obj.Selected = false;
            }
        }

        public void Draw(IDrawer drawer)
        {
            drawer.Draw(this);
        }

        public void Remove(int index)
        {
            Objects.RemoveAt(index);
        }

        public void Remove(IMapObject item)
        {
            Objects.Remove(item);
        }

        public void RemoveAll()
        {
            Objects.Clear();
        }

        public IMapObject Search(ISearcher<IMapObject> searcher)
        {
            return searcher.Search(this);
        }

        private Bounds CalcBounds()
        {
            return Objects.Aggregate(new Bounds(), (current, o) => current + o.Bounds);
        }
    }
}
