using System;
using TwoDimensionalFields.Grids;
using TwoDimensionalFields.MapObjects;
using Xunit;

namespace TwoDimensionalFieldsTests
{
    public class SquareGridTests
    {
        [Fact]
        public void GetValue_GetExistingValue_ValueEqualExpected()
        {
            var matrix = new double?[,]
            {
                { 1, 2, 3, 2 },
                { 2, 1, 2, 3 },
                { 3, 2, 3, 2 },
                { 2, 3, 2, 4 },
            };
            var grid = new SquareGrid(matrix, 2)
            {
                Position = new Node<double>(2, 9)
            };

            double? zCenter = grid.GetValue(3, 8);

            Assert.Equal(1.5, zCenter);
        }

        [Fact]
        public void GetValue_GetNotExistingValue_ValueEqualNull()
        {
            var matrix = new double?[,]
            {
                { 1, 2, 3, 2 },
                { 2, null, 2, 3 },
                { 3, 2, 3, 2 },
                { 2, 3, 2, 4 },
            };
            var grid = new SquareGrid(matrix, 2)
            {
                Position = new Node<double>(2, 9)
            };

            double? zCenter = grid.GetValue(3, 8);

            Assert.Null(zCenter);
        }
    }
}
