namespace ComponentsLibrary.Map
{
    public class Marker
    {
        public string Description { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public bool ShowPopup { get; set; }
        public bool IsNegative { get; set; }
        public double RadiusMeters { get; set; }
        public double Certainty { get; set; }
    }
}
