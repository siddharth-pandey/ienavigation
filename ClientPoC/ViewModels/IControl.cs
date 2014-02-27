using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientPoC.ViewModels
{
    public interface IControl
    {
        string Type { get; }
    }

    public interface IContainerControl
    {
        List<IControl> Controls { get; set; }
    }
}