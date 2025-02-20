// This file is auto-generated. Please do not change it.
using System.Runtime.CompilerServices;

using Eremex.AvaloniaUI.Controls.License;

namespace DemoCenter.Desktop.UI.Tests;
public class LicenseProvider
{
#pragma warning disable CA2255 // The 'ModuleInitializer' attribute should not be used in libraries
	[ModuleInitializer]
#pragma warning restore CA2255 // The 'ModuleInitializer' attribute should not be used in libraries
    public static void RegisterLicense()
	{
        ControlsLicenseManager.SetRuntimeLicenseOwner(new LicenseProvider(),"АО  ЭРЕМЕКС ", "F5 1F 12 8C 3B 9F 55 5C 6D F4 4A CC 2F F2 25 4E 65 54 8E 74 40 AB 7D C8 FC 1C 92 0E AE FF 45 4A 52 76 B1 B6 A0 22 96 8D");
	}
}