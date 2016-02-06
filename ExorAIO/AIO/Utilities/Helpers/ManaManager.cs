using LeagueSharp;
using LeagueSharp.Common;

namespace ExorAIO.Utilities
{
    /// <summary>
    /// The Mana manager class.
    /// </summary>
    class ManaManager
    {
        /// <summary>
        /// The minimum mana needed to cast the Q Spell.
        /// </summary>
        public static int NeededQMana
        => 
            Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana") != null ?
                (int)Variables.Q.ManaCost +
                Variables.Menu.Item($"{Variables.MainMenuName}.qspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to cast the W Spell.
        /// </summary>
        public static int NeededWMana 
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana") != null ?
                (int)Variables.W.ManaCost +
                Variables.Menu.Item($"{Variables.MainMenuName}.wspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to cast the E Spell.
        /// </summary>
        public static int NeededEMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana") != null ?
                (int)Variables.E.ManaCost +
                Variables.Menu.Item($"{Variables.MainMenuName}.espell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to cast the R Spell.
        /// </summary>
        public static int NeededRMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.rspell.mana") != null ?
                (int)Variables.R.ManaCost +
                Variables.Menu.Item($"{Variables.MainMenuName}.rspell.mana").GetValue<Slider>().Value : 0;

        /// <summary>
        /// The minimum mana needed to stack the Tear Item.
        /// </summary>
        public static int NeededTearMana
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.misc.tearmana") != null ?
                (int)Variables.Q.ManaCost +
                Variables.Menu.Item($"{Variables.MainMenuName}.misc.tearmana").GetValue<Slider>().Value : 0;
    }
}
