using Avalonia.Media;
using DemoCenter.Helpers;
using static System.Net.Mime.MediaTypeNames;

namespace DemoCenter.DemoData
{
    public class ElementInfo
    {
        public string Name { get; init; }
        public ElementCategory Category { get; init; }
        public IImage Icon { get; init; }

        public ElementInfo(string name, ElementCategory category, IImage icon)
        {
            Name = name;
            Category = category;
            Icon = icon;
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
                new ElementInfo("Arsenide-gallium transistor", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___arsenid_gallievyj_polevoj_tranzistor), //"Group=Models, Icon=aktivnye komponenty - arsenid-gallievyj polevoj tranzistor"),
                new ElementInfo("N-Type bypolar transistor with base", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___bipoljarnyj_tranzistor_n_tipa_s_podlozhkoj),
                new ElementInfo("N-Type bypolar transistor", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___bipoljarnyj_tranzistor_n_tipa),
                new ElementInfo("P-Type bypolar transistor with base", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___bipoljarnyj_tranzistor_p_tipa_s_podlozhkoj),
                new ElementInfo("P-Type bypolar transistor", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___bipoljarnyj_tranzistor_p_tipa),

                new ElementInfo("MOS transistor (DN type)", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___mop_tranzistor_DN_tipa),
                new ElementInfo("MOS transistor (DP type)", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___mop_tranzistor_DP_tipa),
                new ElementInfo("MOS transistor (N type)", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___mop_tranzistor_N_tipa),
                new ElementInfo("MOS transistor (P type)", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___mop_tranzistor_P_tipa),

                new ElementInfo("Operational amplifier", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___operacionnyj_usilitel),
                new ElementInfo("Field-effect transistor (N type)", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___polevoj_tranzistor_n_tipa),
                new ElementInfo("Field-effect transistor (P type)", ElementCategory.Active, Eremex.AvaloniaUI.Icons.Models.Aktivnye_komponenty___polevoj_tranzistor_p_tipa),

                new ElementInfo("Functional voltage source", ElementCategory.IndependentSource, Eremex.AvaloniaUI.Icons.Models.Funkcionalnye_istochniki___funkcionalnyj_istochnik_naprjazhenija),
                new ElementInfo("Functional current source", ElementCategory.IndependentSource, Eremex.AvaloniaUI.Icons.Models.Funkcionalnye_istochniki___funkcionalnyj_istochnik_toka),

                new ElementInfo("Four-pole", ElementCategory.Multipole, Eremex.AvaloniaUI.Icons.Models.Mnogopolosniki___chetyrehpolosnik),
                new ElementInfo("Two-pole", ElementCategory.Multipole, Eremex.AvaloniaUI.Icons.Models.Mnogopolosniki___dvuhpolosnik),
                new ElementInfo("Six-pole", ElementCategory.Multipole, Eremex.AvaloniaUI.Icons.Models.Mnogopolosniki___shestipolosnik),
                new ElementInfo("Eight-pole", ElementCategory.Multipole,Eremex.AvaloniaUI.Icons.Models.Mnogopolosniki___vosmipolosnik),

                new ElementInfo("Battery", ElementCategory.IndependentSource, Eremex.AvaloniaUI.Icons.Models.Nezavisimye_istochniki___batareja),
                new ElementInfo("Voltage source", ElementCategory.IndependentSource, Eremex.AvaloniaUI.Icons.Models.Nezavisimye_istochniki___istochnik_naprjazhenija),
                new ElementInfo("Current source", ElementCategory.IndependentSource, Eremex.AvaloniaUI.Icons.Models.Nezavisimye_istochniki___istochnik_toka),

                new ElementInfo("Diod", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___diod),
                new ElementInfo("Long line", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___dlinnaja_linija),
                new ElementInfo("Transformator", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___dvuhobmotochnyj_transformator),
                new ElementInfo("Inductor", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___induktivnost),
                new ElementInfo("Capacitor", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___kondensator),
                new ElementInfo("Voltage-controlled switch", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___perekljuchatel_upravljaemyj_naprjazheniem),
                new ElementInfo("Current-controlled switch", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___perekljuchatel_upravljaemyj_tokom),
                new ElementInfo("Resistor", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___rezistor),
                new ElementInfo("Variable resistor", ElementCategory.Passive, Eremex.AvaloniaUI.Icons.Models.Passivnye_elementy___rezistor_potenciometr),

                new ElementInfo("Voltage-controlled voltage source", ElementCategory.ControlledSource, Eremex.AvaloniaUI.Icons.Models.Upravljaemye_istochniki___istochnik_naprjazhenija_upravljaemyj_naprjazheniem),
                new ElementInfo("Current-controlled voltage source", ElementCategory.ControlledSource, Eremex.AvaloniaUI.Icons.Models.Upravljaemye_istochniki___istochnik_naprjazhenija_upravljaemyj_tokom),
                new ElementInfo("Voltage-controlled current source", ElementCategory.ControlledSource, Eremex.AvaloniaUI.Icons.Models.Upravljaemye_istochniki___istochnik_toka_upravljaemyj_naprjazheniem),
                new ElementInfo("Current-controlled current source", ElementCategory.ControlledSource, Eremex.AvaloniaUI.Icons.Models.Upravljaemye_istochniki___istochnik_toka_upravljaemyj_tokom),
            };
        }
    }
}
