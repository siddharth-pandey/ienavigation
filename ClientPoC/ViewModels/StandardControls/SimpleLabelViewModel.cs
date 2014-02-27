using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using ClientPoC.Model;
using ClientPoC.Models;
using Newtonsoft.Json;

namespace ClientPoC.ViewModels.StandardControls
{
    public class SimpleLabelViewModel : ComponentData, IControl
    {
        public SimpleLabelViewModel()
        {

        }

        //public SimpleLabelViewModel(ComponentData component)
        //{
        //    var data = JsonConvert.DeserializeObject<SimpleLabelViewModel>(component.PropertyString);
        //    TextValue = String.IsNullOrEmpty(data.TextValue) ? "NA" : data.TextValue;
        //    TextColour = data.TextColour;
        //    FontWeightValue = data.FontWeightValue;
        //    SizeOfFont = data.SizeOfFont;
        //    Path = component.Path;
        //}

        public override void FeedData(IEnumerable<DataValue> dataValues)
        {
            if (dataValues != null && dataValues.Any())
            {
                foreach (var dv in dataValues)
                {
                    if (dv.Path.Equals(Path))
                    {
                        if (!string.IsNullOrEmpty(dv.Value))
                        {
                            TextValue = dv.Value;
                        }
                    }
                }
            }
        }

        public override void FeedDefaultData(ComponentData componentData)
        {
            base.FeedDefaultData(componentData);

            var data = JsonConvert.DeserializeObject<SimpleLabelViewModel>(componentData.PropertyString);
            TextValue = String.IsNullOrEmpty(data.TextValue) ? "NA" : data.TextValue;
            TextColour = data.TextColour;
            FontWeightValue = data.FontWeightValue;
            SizeOfFont = data.SizeOfFont;
        }

        [DefaultValue("Label")]
        public string TextValue { get; set; }

        [DefaultValue("#000000")]
        public string TextColour { get; set; }

        [DefaultValue(false)]
        public string IsItalic { get; set; }

        public string LinkedComponent { get; set; }

        [DefaultValue(15)]
        public string SizeOfFont { get; set; }

        [DefaultValue(2)]
        public string FontWeightValue { get; set; }

        public string Type
        {
            get { return "SimpleLabel"; }
        }
    }
}