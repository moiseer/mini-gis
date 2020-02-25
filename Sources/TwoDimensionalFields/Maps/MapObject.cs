using System;
using TwoDimensionalFields.Drawing;
using TwoDimensionalFields.Drawing.Styling;
using TwoDimensionalFields.Searching;

namespace TwoDimensionalFields.Maps
{
    public abstract class MapObject : IMapObject, IDrawable, ISearchable<MapObject>, IStyled
    {
        protected MapObjectType objectType;
        private Bounds bounds;
        private Style style;

        public virtual Bounds Bounds => bounds ?? (bounds = GetBounds());
        public bool HasOwnStyle { get; set; }
        public MapObjectType ObjectType => objectType;
        public bool Selected { get; set; }

        public Style Style
        {
            get => style;
            set
            {
                HasOwnStyle = true;
                style = value;
            }
        }

        public void Draw(IDrawer drawer) => drawer.Draw(this);
        public MapObject Search(ISearcher<MapObject> searcher) => searcher.Search(this);
        protected abstract Bounds GetBounds();
    }
}
