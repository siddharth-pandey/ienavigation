using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.Models
{
    public class HubDetails
    {
        private int _layoutHeight;
        private int _layoutWidth;
        public string HubId { get; set; }
        public new int LayoutWidth
        {
            //get { return _layoutWidth; }
            //set { _layoutWidth = CalculateWidth(value); }
            get; set; }

        public new int LayoutHeight
        {
            //get { return _layoutHeight; }
            //set { _layoutHeight = CalculateHeight(value); }
            get; set; }
        public int XTile { get; set; }
        public int YTile { get; set; }
        public bool CanEdit { get; set; }
        public bool ShowEdit { get; set; }

        private int CalculateWidth(int dimension)
        {
            return CalculateLayout(dimension);
        }

        private int CalculateHeight(int dimension)
        {
            return CalculateLayout(dimension);
        }

        private int CalculateLayout(int dimension)
        {
            return (dimension * 100) + ((dimension - 1) * 5);
        }

        public new int LayoutWidthTemp
        {
            get { return CalculateWidth(LayoutWidth); }
            set { _layoutWidth = CalculateWidth(LayoutWidth); }
        }

        public new int LayoutHeightTemp
        {
            get { return CalculateHeight(LayoutHeight); }
            set { _layoutHeight = CalculateHeight(LayoutHeight); }
        }
    }
}