using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Model
{
    public class HubLayout
    {
        private int _width;
        private int _height;
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }
        public String BackgroundColour { get; set; }
        public Int32 BorderThickness { get; set; }
        public String BorderColour { get; set; }
        public HubLayoutOrientation Orientation { get; set; }
        public List<HubLayout> RotationLayouts { get; set; }
        public List<ComponentData> Components { get; set; }
        public List<KeyDescription> ViewRoles { get; set; }
        public List<KeyDescription> ViewDeviceTypes { get; set; }
        public List<KeyDescription> ViewContexts { get; set; }

        public void ProcessComponents()
        {
            if (Components != null && Components.Count > 0)
            {
                foreach (var component in Components)
                {
                    
                }
            }
        }

        public int CalculateWidth(int dimension)
        {
            return CalculateLayout(dimension);
        }

        public int CalculateHeight(int dimension)
        {
            return CalculateLayout(dimension);
        }

        private int CalculateLayout(int dimension)
        {
            return (dimension * 100) + ((dimension - 1) * 5);
        }
    }
}