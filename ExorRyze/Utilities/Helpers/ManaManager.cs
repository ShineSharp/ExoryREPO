using LeagueSharp;
using LeagueSharp.Common;

namespace ExorRyze
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
            (int)((Variables.Q.ManaCost / ObjectManager.Player.MaxMana) * 100) +
            Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana to use the W spell.
        /// </summary>
        public static int NeededWMana 
        =>
            (int)((Variables.W.ManaCost / ObjectManager.Player.MaxMana) * 100) +
            Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana to use the E spell.
        /// </summary>
        public static int NeededEMana
        =>
            (int)((Variables.E.ManaCost / ObjectManager.Player.MaxMana) * 100) +
            Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana").GetValue<Slider>().Value;

        /// <summary>
        /// The minimum mana needed to stack the Tear Item.
        /// </summary>
        public static int NeededTearMana
        =>
            (int)((Variables.Q.ManaCost / ObjectManager.Player.MaxMana) * 100) +
            Variables.Menu.Item($"{Variables.MainMenuName}.misc.tearmana").GetValue<Slider>().Value;
    }
}
