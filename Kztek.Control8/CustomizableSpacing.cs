using System.ComponentModel;

namespace Kztek.Control8
{

    [TypeConverter(typeof(ExpandableObjectConverter))]
    [Description("You can choose which edges will be included when styling the border radius")]
    public class CustomizableSpacing
    {
        internal Control Parent;

        public int Top { get; set; }
        public int Bottom { get; set; }
        public int Left { get; set; }
        public int Right { get; set; }


        public CustomizableSpacing(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public CustomizableSpacing()
        {
        }

        public override string ToString()
        {
            return Top + "," + Bottom + "," + Left + "," + Right;
        }

        public static CustomizableSpacing? TryParse(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }
            List<string> arrays = input.Split(",").ToList();
            if (arrays.Count < 4 || arrays.Count > 4)
            {
                return null;
            }

            int.TryParse(arrays[0], out int top);
            int.TryParse(arrays[1], out int bottom);
            int.TryParse(arrays[2], out int left);
            int.TryParse(arrays[3], out int right);

            return new CustomizableSpacing(top, bottom, left, right);
        }
    }
}
