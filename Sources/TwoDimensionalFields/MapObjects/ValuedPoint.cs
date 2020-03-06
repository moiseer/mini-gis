namespace TwoDimensionalFields.MapObjects
{
    public class ValuedPoint : Point
    {
        public ValuedPoint(Node3d<double> position) : base(position)
        {
            Value = position.Z;
        }

        public double Value { get; set; }
    }
}
