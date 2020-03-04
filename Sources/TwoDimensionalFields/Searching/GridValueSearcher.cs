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

        public static double? SearchIrregularGrid(IrregularGrid grid, Node<double> searchPoint, double delta, int pow, IrregularGrid.ValueCalculating calculatingType)
        {
            switch (calculatingType)
            {
                case IrregularGrid.ValueCalculating.ByRadius:
                    return grid.GetValueByRadius(searchPoint, delta, pow);
                case IrregularGrid.ValueCalculating.ByNodesCount:
                    return grid.GetValueByNodesCount(searchPoint, Convert.ToInt32(delta), pow);
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
