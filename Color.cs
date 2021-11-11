namespace SharpEngine
{
    public struct Color
    {
       // ALL THE COLORS
        public static readonly Color Pink = new Color(1, 0.6f, 1, 1);
        public static readonly Color Aqua = new Color(0, 0.6f, 0.6f, 1);
        public static readonly Color Ocean = new Color(0, 0.2980392f, 0.6f, 1);

        public static readonly Color Red = new Color(1, 0, 0, 1);
        public static readonly Color Yellow = new Color(1, 1, 0, 1);
        public static readonly Color Blue = new Color(0, 0, 1, 1);






        public float r, g, b, a;

        public Color(float r, float g, float b, float a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
    }
}
