// This file is auto-generated. Please do not change it.
using System.Runtime.CompilerServices;

using Eremex.AvaloniaUI.Controls.License;

namespace DemoCenter;
public class LicenseProvider
{
#pragma warning disable CA2255 // The 'ModuleInitializer' attribute should not be used in libraries
	[ModuleInitializer]
#pragma warning restore CA2255 // The 'ModuleInitializer' attribute should not be used in libraries
    public static void RegisterLicense()
	{
        ControlsLicenseManager.SetRuntimeLicenseOwner(new LicenseProvider(),"АО  ЭРЕМЕКС ", "49 89 73 9B C8 F5 94 2C 3F 13 87 B8 E6 27 F1 00 FA 6C 0B 41 6A A4 27 91 B9 99 1F D6 B5 83 ED 4B BE 61 0B 7C 4F 63 B5 B9");
	}
}