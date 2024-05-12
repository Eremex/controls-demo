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
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/Dimension.svg")]
        [Display(Description = "Set Graphics to Dimension")]
        Dimension,
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/Maximum.svg")]
        [Display(Description = "Set Graphics to Maximum")]
        Maximum,
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/Minimum.svg")]
        [Display(Description = "Set Graphics to Minimum")]
        Minimum,
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/Point.svg")]
        [Display(Description = "Set Graphics to Next Point")]
        NextPoint,
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/Peak.svg")]
        [Display(Description = "Set Graphics to Peak")]
        Peak,
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/Recess.svg")]
        [Display(Description = "Set Graphics to Recess")]
        Recess,
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/X.svg")]
        [Display(Description = "Set Graphics to X")]
        X,
        [Image($"avares://Eremex.Avalonia.Icons/Graphics/Y.svg")]
        [Display(Description = "Set Graphics to Y")]
        Y
    }
    public enum GraphicView
    {
        [Image($"avares://Eremex.Avalonia.Icons/3D/View Back.svg")]
        [Display(Description = "Back View")]
        Back,
        [Image($"avares://Eremex.Avalonia.Icons/3D/View Front.svg")]
        [Display(Description = "Front View")]
        Front,
        [Image($"avares://Eremex.Avalonia.Icons/3D/View Top.svg")]
        [Display(Description = "Top View")]
        Top,
        [Image($"avares://Eremex.Avalonia.Icons/3D/View Bottom.svg")]
        [Display(Description = "Bottom View")]
        Bottom
    }
}
