using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using ClientPoC.Model;
using ClientPoC.Models;
using Glimpse.Core.ClientScript;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientPoC.ViewModels.StandardControls
{
    public class ImageBoxViewModel : ComponentData, IControl
    {
        public ImageBoxViewModel()
        {
            //MinimumHeight = 10.0;
            //MinimumWidth = 10.0;
            //Width = 150;
            //Height = 200;
            VContentAlignment = "Center";
            HContentAlignment = "left";
            Alignment = "left";
        }

        //public ImageBoxViewModel(ComponentData component)
        //{
        //    FeedData(component.PropertyString);
        //}

        private void FeedData(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                var obj = JsonConvert.DeserializeObject<ImageBoxViewModel>(value);
                var imgSrc = String.Format("data:image/png;base64,{0}", obj.ImageData);

                ImageData = imgSrc;
                StretchType = obj.StretchType;
                IsEditable = obj.IsEditable;
                AllowImageFromCamera = obj.AllowImageFromCamera;
                AllowImageFromFile = obj.AllowImageFromFile;
                ImageName = obj.ImageName;
                FileType = obj.FileType;
                SizeOfFont = obj.SizeOfFont;
                Alignment = obj.Alignment;
                HContentAlignment = obj.HContentAlignment;
                VContentAlignment = obj.VContentAlignment;
            }
        }

        public override void FeedData(IEnumerable<DataValue> dataValues)
        {
            if (dataValues != null && dataValues.Any())
            {
                foreach (var dv in dataValues)
                {
                    if (dv.Path.Equals(Path))
                    {
                        FeedData(dv.Value);
                    }
                }
            }
        }

        public override void FeedDefaultData(ComponentData componentData)
        {
            base.FeedDefaultData(componentData);
            FeedData(componentData.PropertyString);
        }
        public new string Type
        {
            get { return "ImageBox"; }
        }

        public string ImageData { get; set; }
        public string StretchType { get; set; }
        public string IsEditable { get; set; }
        public string AllowImageFromCamera { get; set; }
        public string AllowImageFromFile { get; set; }
        public string ImageName { get; set; }
        public string FileType { get; set; }
        public string Alignment { get; set; }
        public string HContentAlignment { get; set; }
        public string VContentAlignment { get; set; }

        public string SizeOfFont { get; set; }
        [DefaultValue(10.0)]
        public string MinimumHeight { get; set; }
        [DefaultValue(10.0)]
        public string MinimumWidth { get; set; }
        //[DefaultValue(200.0)]
        //public double Height { get; set; }
        //[DefaultValue(150.0)]
        //public double Width { get; set; }

        public Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            var ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(ms, true);
            return image;
        }
    }
}