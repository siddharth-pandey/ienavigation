using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClientPoC.Model;
using ClientPoC.Models;

namespace ClientPoC.ViewModels
{
    public class HubLayoutViewModel : HubLayout
    {
        public HubLayoutViewModel()
        {
        }
        public string Type
        {
            get { return "HubLayout"; }
        }

        public string SummaryViewReference { get; set; }
        private int _width;
        private int _height;
        public new int ViewWidth
        {
            get { return _width; }
            set { _width = CalculateWidth(value); }
        }
        public new int ViewHeight
        {
            get { return _height; }
            set { _height = CalculateHeight(value); }
        }

        public string HubSchemaId { get; set; }
        public string HubDataId { get; set; }
        public string SummaryViewId { get; set; }
        public string DetailViewId { get; set; }
        public string EditViewId { get; set; }
        public List<DataValue> DataValues { get; set; }

    }
}