using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Maps
{
    public class Map : IMap, IDrawable, IMapObject, ISearchable<MapObject>
    {
        private readonly double maxMapScale;
        private double scale;

        public Map()
        {
            maxMapScale = 1000;
            scale = 1;
            Center = new Node<double>(0, 0);
            Layers = new List<ILayer>();
        }

        public Bounds Bounds => CalcBounds();
        public Node<double> Center { get; set; }
        public List<ILayer> Layers { get; }

        public double Scale
        {
            get => scale;
            set
            {
                if (scale < maxMapScale || value < scale)
                {
                    scale = value;
                }
            }
        }

        public bool Selected { get; set; }

        public void Add(ILayer layer) => Layers.Add(layer);
        public void Draw(IDrawer drawer) => drawer.Draw(this);
        public void Insert(int index, ILayer layer) => Layers.Insert(index, layer);
        public void Remove(int index) => Layers.RemoveAt(index);
        public void Remove(ILayer layer) => Layers.Remove(layer);
        public void RemoveAll() => Layers.Clear();

        public MapObject Search(ISearcher<MapObject> searcher)
        {
            for (int i = Layers.Count - 1; i >= 0; i--)
            {
                if (!(Layers[i] is ISearchable<MapObject> searchable))
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
