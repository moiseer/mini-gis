using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Maps
{
    public class Map : IMap, IDrawable, IMapObject, ISearchable<IMapObject>
    {
        private readonly double maxMapScale = 1000;

        private double mapScale = 1;

        public Bounds Bounds
        {
            get { return CalcBounds(); }
        }

        public List<ILayer> Layers { get; } = new List<ILayer>();

        public (double X, double Y) MapCenter { get; set; } = (0, 0);

        public double MapScale
        {
            get { return mapScale; }
            set
            {
                if (mapScale < maxMapScale || value < mapScale)
                {
                    mapScale = value;
                }
            }
        }

        public bool Selected { get; set; } = false;

        public void Add(ILayer layer)
        {
            Layers.Add(layer);
        }

        public void Draw(IDrawer drawer)
        {
            drawer.Draw(this);
        }

        public void Remove(int index)
        {
            Layers.RemoveAt(index);
        }

        public void Remove(ILayer layer)
        {
            Layers.Remove(layer);
        }

        public void RemoveAll()
        {
            Layers.Clear();
        }

        public IMapObject Search(ISearcher<IMapObject> searcher)
        {
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                if (!(Layers[i] is ISearchable<IMapObject> searchable))
                {
                    continue;
                }

                var searchObj = searchable.Search(searcher);

                if (searchObj == null)
                {
                    continue;
                }

                return searchObj;
            }

            return null;
        }

        private Bounds CalcBounds()
        {
            return Layers
                .Where(mapObject => mapObject.Visible)
                .Aggregate(new Bounds(), (current, mapObject) => current + mapObject.Bounds);
        }
    }
}
