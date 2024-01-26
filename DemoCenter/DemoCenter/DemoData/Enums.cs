using DemoCenter.ViewModels;
using Eremex.AvaloniaUI.Controls.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCenter.DemoData
{
    public enum GraphicPosition
    {
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to Dimension.svg")]
        [Display(Description = "Set Graphics to Dimension")]
        Dimension,
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to Maximum.svg")]
        [Display(Description = "Set Graphics to Maximum")]
        Maximum,
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to Minimum.svg")]
        [Display(Description = "Set Graphics to Minimum")]
        Minimum,
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to Next Point.svg")]
        [Display(Description = "Set Graphics to Next Point")]
        NextPoint,
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to Peak.svg")]
        [Display(Description = "Set Graphics to Peak")]
        Peak,
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to Recess.svg")]
        [Display(Description = "Set Graphics to Recess")]
        Recess,
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to X.svg")]
        [Display(Description = "Set Graphics to X")]
        X,
        [Image($"avares://DemoCenter/Images/Group=SimOne, Icon=Graphics Set to Y.svg")]
        [Display(Description = "Set Graphics to Y")]
        Y
    }
    public enum GraphicView
    {
        [Image($"avares://DemoCenter/Images/Group=3D, Icon=View Back.svg")]
        [Display(Description = "Back View")]
        Back,
        [Image($"avares://DemoCenter/Images/Group=3D, Icon=View Front.svg")]
        [Display(Description = "Front View")]
        Front,
        [Image($"avares://DemoCenter/Images/Group=3D, Icon=View Top.svg")]
        [Display(Description = "Top View")]
        Top,
        [Image($"avares://DemoCenter/Images/Group=3D, Icon=View Bottom.svg")]
        [Display(Description = "Bottom View")]
        Bottom
    }
}
