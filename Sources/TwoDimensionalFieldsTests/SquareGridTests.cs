using System;
using TwoDimensionalFields.Grids;
using Xunit;

namespace TwoDimensionalFieldsTests
{
    public class SquareGridTests
    {
        [Fact]
        public void GetValue_Test()
        {
            var matrix = new double[,]
            {
                {1, 2, 3, 2},
                {2, 1, 2, 3},
                {3, 2, 3, 2},
                {2, 3, 2, 4},
            };
            var grid = new SquareGrid(matrix, 2)
            {
                Position = (2, 9)
            };

            var zCenter = grid.GetValue(3, 8);

            Assert.Equal(1.5, zCenter);
        }
    }
}