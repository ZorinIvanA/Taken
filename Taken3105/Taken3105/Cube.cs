namespace Taken3105
{
    public class Cube
    {
        public int Position { get; set; }
        public int? Value { get; set; }

        public int X => Position / 4;
        public int Y => Position % 4;
    }
}