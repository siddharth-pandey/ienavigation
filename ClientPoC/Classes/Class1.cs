using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientPoC.Classes
{
    public class ControlsModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            ValueProviderResult type = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + ".Type");
            object model = Activator.CreateInstance("ClientPoC", "ClientPoC.ViewModels.StandardControls." + type.AttemptedValue + "ViewModel").Unwrap();
            bindingContext.ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType());
            return model;
        }
    }
}