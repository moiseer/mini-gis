using System;

namespace TwoDimensionalFields.MapObjects
{
    public class Node3d<T> : Node<T>
    {
        public Node3d(T x, T y, T z) : base(x, y)
        {
            Z = z;
        }

        public T Z { get; set; }

        public void Deconstruct(out T x, out T y, out T z)
        {
            x = X;
            y = Y;
            z = Z;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
        }
    }
}
