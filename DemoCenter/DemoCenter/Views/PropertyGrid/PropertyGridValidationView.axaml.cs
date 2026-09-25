using Avalonia.Controls;

using DemoCenter.DemoData;
using Eremex.AvaloniaUI.Controls.PropertyGrid;

namespace DemoCenter.Views
{
    public partial class PropertyGridValidationView : UserControl
    {
        const int FeetPerCabin = 20;

        public PropertyGridValidationView()
        {
            InitializeComponent();
        }

        void PropertyGridControl_ValidateRowValue(object sender, ValidateRowValueEventArgs e)
        {
            var yacht = (YachtValidationInfo)propertyGrid.SelectedObject;

            if (e.FieldName == nameof(YachtValidationInfo.RefitYear) && checkBoxRefitLaunchingYear.IsChecked == true)
            {
                if (e.Value is int refitYear && refitYear < yacht.LaunchingYear)
                    e.ErrorContent = "Custom Validation: Refit Year cannot be less than Launching Year";
            }
            else if (e.FieldName == nameof(YachtValidationInfo.LaunchingYear) && checkBoxRefitLaunchingYear.IsChecked == true)
            {
                if (e.Value is int launchingYear && launchingYear >= yacht.RefitYear)
                    e.ErrorContent = "Custom Validation: Launching Year cannot be more than Refit Year";
            }
            else if (e.FieldName == nameof(YachtValidationInfo.NumberOfCabins) && checkBoxCabins.IsChecked == true)
            {
                if (e.Value is int cabins && cabins > yacht.Length / FeetPerCabin)
                    e.ErrorContent = $"A {yacht.Length:0} ft hull takes at most {yacht.Length / FeetPerCabin:0} cabins";
            }
        }
    }
}
