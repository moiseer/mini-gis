using System;

namespace TwoDimensionalFields.MapObjects
{
    public class Node<T>
    {
        public Node(T x, T y)
        {
            X = x;
            Y = y;
        }

        public T X { get; set; }

        public T Y { get; set; }

        public void Deconstruct(out T x, out T y)
        {
            x = X;
            y = Y;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }
    }
}
