using System;
using System.Collections.Generic;
using System.Linq;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;
using TwoDimensionalFields.Maps;

namespace TwoDimensionalFields.Searching
{
    public class GridValueSearcher
    {
        private readonly Node<double> searchPoint;

        public GridValueSearcher(Node<double> searchPoint)
        {
            this.searchPoint = searchPoint;
        }

        public Bounds Bounds { get; set; }

        public static Func<Node<double>, double, int, double?> GetSearchingFunc(IrregularGrid grid, ValueCalculating calculatingType)
        {
            switch (calculatingType)
            {
                case ValueCalculating.ByRadius:
                    return grid.GetValueByRadius;
                case ValueCalculating.ByNodesCount:
                    return grid.GetValueByNodesCount;
                default:
                    throw new ArgumentOutOfRangeException(nameof(calculatingType), calculatingType, null);
            }
        }

        public Dictionary<Grid, double> Search(IMap map)
        {
            foreach (var grid in map.Layers.OfType<Grid>().Reverse())
            {
                var value = SearchGridValue(grid);
                if (value.HasValue)
                {
                    return new Dictionary<Grid, double> { { grid, value.Value } };
                }
            }

            return new Dictionary<Grid, double>();
        }

        public double? SearchGridValue(Grid grid)
        {
            switch (grid)
            {
                case IMapObject mapObject when Bounds != null && !Bounds.IntersectsWith(mapObject.Bounds):
                case ILayer layerObject when !layerObject.Visible:
                    return null;
                case RegularGrid regularGrid:
                    return SearchRegularGrid(regularGrid);
                case IrregularGrid irregularGrid:
                    return SearchIrregularGrid(irregularGrid);
                default:
                    throw new ArgumentException();
            }
        }

        private double? SearchIrregularGrid(IrregularGrid grid) => grid.GetValue(searchPoint.X, searchPoint.Y);
        private double? SearchRegularGrid(RegularGrid grid) => grid.GetValue(searchPoint.X, searchPoint.Y);
    }
}
