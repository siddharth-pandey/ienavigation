using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using ClientPoC.Model;
using ClientPoC.Models;
using Newtonsoft.Json;

namespace ClientPoC.ViewModels.StandardControls
{
    public class SingleLineTextBoxViewModel : ComponentData, IControl
    {
        public SingleLineTextBoxViewModel()
        {
            Height = 30;
            Width = 100;
        }

        //public SingleLineTextBoxViewModel(ComponentData component)
        //{
        //    var data = JsonConvert.DeserializeObject<SingleLineTextBoxViewModel>(component.PropertyString);
            
        //    TextValue = String.IsNullOrEmpty(data.TextValue) ? "NA" : data.TextValue;
        //    TextColour = data.TextColour;
        //    TextLength = data.TextLength;
        //    PlaceholderText = data.PlaceholderText;
        //    FontWeightValue = data.FontWeightValue;
        //    SizeOfFont = data.SizeOfFont;
        //    Alignment = data.Alignment;
        //    HAlignment = data.HAlignment;
        //    VAlignment = data.VAlignment;
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
                        TextValue = dv.Value;
                    }
                }
            }
        }

        public override void FeedDefaultData(ComponentData componentData)
        {
            base.FeedDefaultData(componentData);

            var data = JsonConvert.DeserializeObject<SingleLineTextBoxViewModel>(componentData.PropertyString);

            TextValue = data.TextValue;
            TextColour = data.TextColour;
            TextLength = data.TextLength;
            PlaceholderText = data.PlaceholderText;
            FontWeightValue = data.FontWeightValue;
            SizeOfFont = data.SizeOfFont;
            Alignment = data.Alignment;
            HAlignment = data.HAlignment;
            VAlignment = data.VAlignment;
        }

        [DefaultValue(20)]
        public string MinimumHeight { get; set; }
        
        [DefaultValue(20)]
        public string MinimumWidth { get; set; }

        public string TextValue { get; set; }
        public string TextColour { get; set; }
        
        [DefaultValue(50)]
        public string TextLength { get; set; }

        public string PlaceholderText { get; set; }
        
        [DefaultValue(2)]
        public string FontWeightValue { get; set; }

        [DefaultValue(12)]
        public string SizeOfFont { get; set; }

        [DefaultValue("Left")]
        public string Alignment { get; set; }

        [DefaultValue("Left")]
        public string HAlignment { get; set; }

        [DefaultValue("Center")]
        public string VAlignment { get; set; }


        public string Type
        {
            get { return "SingleLineTextBox"; }
        }
    }
}