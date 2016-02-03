using LeagueSharp;
using LeagueSharp.Common;

namespace ExorLucian
{
    /// <summary>
    /// The Mana manager class.
    /// </summary>
    class ManaManager
    {
        /// <summary>
        /// Sets the minimum necessary mana to use the Q spell.
        /// </summary>
        public static int NeededQMana
        =>
           (int)Variables.Q.ManaCost +
            Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana to use the W spell.
        /// </summary>
        public static int NeededWMana 
        =>
            (int)Variables.W.ManaCost +
            Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana to use the E spell.
        /// </summary>
        public static int NeededEMana
        =>
            (int)Variables.E.ManaCost +
            Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana").GetValue<Slider>().Value;
    }
}
