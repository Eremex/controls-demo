using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Avalonia.Media;
using Avalonia.Svg.Skia;
using ExCSS;
using System.Xml.Linq;
using System.Globalization;

namespace DemoCenter.DemoData
{
    public class ElementInfo
    {
        public string Name { get; init; }
        public ElementCategory Category { get; init; }
        public IImage Icon { get; init; }

        public ElementInfo(string name, ElementCategory category, string iconName)
        {
            Name = name;
            Category = category;
            Icon = SvgImageExtension.ProvideValue($"avares://DemoCenter/Images/{iconName}.svg", null!, null!);
        }
    }

    public enum ElementCategory
    {
        Active,
        Passive,
        IndependentSource,
        ControlledSource,
        Multipole
    }

    public static class ElementsSources
    {
        static List<ElementInfo> elements;
        public static List<ElementInfo> Elements
        {
            get
            {
                elements ??= GetElements();
                return elements;
            }
        }

        static List<ElementInfo> GetElements()
        {
            return new List<ElementInfo>()
            {
                new ElementInfo("Arsenide-gallium transistor", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - arsenid-gallievyj polevoj tranzistor"),
                new ElementInfo("N-Type bypolar transistor with base", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - bipoljarnyj tranzistor n-tipa s podlozhkoj"),
                new ElementInfo("N-Type bypolar transistor", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - bipoljarnyj tranzistor n-tipa"),
                new ElementInfo("P-Type bypolar transistor with base", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - bipoljarnyj tranzistor p-tipa s podlozhkoj"),
                new ElementInfo("P-Type bypolar transistor", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - bipoljarnyj tranzistor p-tipa"),

                new ElementInfo("MOS transistor (DN type)", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - mop tranzistor DN-tipa"),
                new ElementInfo("MOS transistor (DP type)", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - mop tranzistor DP-tipa"),
                new ElementInfo("MOS transistor (N type)", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - mop tranzistor N-tipa"),
                new ElementInfo("MOS transistor (P type)", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - mop tranzistor P-tipa"),

                new ElementInfo("Operational amplifier", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - operacionnyj usilitel"),
                new ElementInfo("Field-effect transistor (N type)", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - polevoj tranzistor n-tipa"),
                new ElementInfo("Field-effect transistor (P type)", ElementCategory.Active, "Group=Models, Icon=aktivnye komponenty - polevoj tranzistor p-tipa"),

                new ElementInfo("Functional voltage source", ElementCategory.IndependentSource, "Group=Models, Icon=funkcionalnye istochniki - funkcionalnyj istochnik naprjazhenija"),
                new ElementInfo("Functional current source", ElementCategory.IndependentSource, "Group=Models, Icon=funkcionalnye istochniki - funkcionalnyj istochnik toka"),

                new ElementInfo("Four-pole", ElementCategory.Multipole, "Group=Models, Icon=mnogopolosniki - chetyrehpolosnik"),
                new ElementInfo("Two-pole", ElementCategory.Multipole, "Group=Models, Icon=mnogopolosniki - dvuhpolosnik"),
                new ElementInfo("Six-pole", ElementCategory.Multipole, "Group=Models, Icon=mnogopolosniki - shestipolosnik"),
                new ElementInfo("Eight-pole", ElementCategory.Multipole, "Group=Models, Icon=mnogopolosniki - vosmipolosnik"),

                new ElementInfo("Battery", ElementCategory.IndependentSource, "Group=Models, Icon=nezavisimye istochniki - batareja"),
                new ElementInfo("Voltage source", ElementCategory.IndependentSource, "Group=Models, Icon=nezavisimye istochniki - istochnik naprjazhenija"),
                new ElementInfo("Current source", ElementCategory.IndependentSource, "Group=Models, Icon=nezavisimye istochniki - istochnik toka"),

                new ElementInfo("Diod", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - diod"),
                new ElementInfo("Long line", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - dlinnaja linija"),
                new ElementInfo("Transformator", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - dvuhobmotochnyj transformator"),
                new ElementInfo("Inductor", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - induktivnost"),
                new ElementInfo("Capacitor", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - kondensator"),
                new ElementInfo("Voltage-controlled switch", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - perekljuchatel upravljaemyj naprjazheniem"),
                new ElementInfo("Current-controlled switch", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - perekljuchatel upravljaemyj tokom"),
                new ElementInfo("Resistor", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - rezistor"),
                new ElementInfo("Variable resistor", ElementCategory.Passive, "Group=Models, Icon=passivnye elementy - rezistor-potenciometr"),

                new ElementInfo("Voltage-controlled voltage source", ElementCategory.ControlledSource, "Group=Models, Icon=upravljaemye istochniki - istochnik naprjazhenija upravljaemyj naprjazheniem"),
                new ElementInfo("Current-controlled voltage source", ElementCategory.ControlledSource, "Group=Models, Icon=upravljaemye istochniki - istochnik naprjazhenija upravljaemyj tokom"),
                new ElementInfo("Voltage-controlled current source", ElementCategory.ControlledSource, "Group=Models, Icon=upravljaemye istochniki - istochnik toka upravljaemyj naprjazheniem"),
                new ElementInfo("Current-controlled current source", ElementCategory.ControlledSource, "Group=Models, Icon=upravljaemye istochniki - istochnik toka upravljaemyj tokom"),
            };
        }
    }
}
