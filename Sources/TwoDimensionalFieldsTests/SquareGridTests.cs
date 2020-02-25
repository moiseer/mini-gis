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
                { 1, 2, 3, 2 },
                { 2, 3, 2, 4 },
            };
            var position = new Node<double>(2, 9);
            var grid = new SquareGrid(matrix, position, 2);

            double? zCenter = grid.GetValue(3, 6);

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
            var position = new Node<double>(2, 9);
            var grid = new SquareGrid(matrix, position, 2);

            double? zCenter = grid.GetValue(3, 6);

            Assert.Null(zCenter);
        }
    }
}
