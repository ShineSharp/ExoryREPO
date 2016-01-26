using LeagueSharp;
using LeagueSharp.Common;

namespace NabbActivator
{
    /// <summary>
    /// The managers.
    /// </summary>
    class Managers
    {
        /// <summary>
        /// Sets the minimum necessary health percent to use a health potion.
        /// </summary>
        public static int MinHealthPercent
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.consumables.health").GetValue<Slider>().Value;

        /// <summary>
        /// Sets the minimum necessary mana percent to use a mana potion.
        /// </summary>
        public static int MinManaPercent
        =>
            Variables.Menu.Item($"{Variables.MainMenuName}.consumables.mana").GetValue<Slider>().Value;
    }
}
