using System;

namespace TwoDimensionalFields.Grids
{
    public class Progress
    {
        public Progress(int maxValue)
        {
            MaxValue = maxValue;
        }

        public event Action<int> ProgressChanged;
        public int MaxValue { get;}
        public void SetValue(int value) => ProgressChanged?.Invoke(value);
    }
}
