namespace NabbTracker
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;
    using SharpDX.Direct3D9;

    using Font = SharpDX.Direct3D9.Font;
    using Color = System.Drawing.Color; 

    /// <summary>
    /// The drawings class.
    /// </summary>
    public class Drawings
    {
        public static void Load()
        {
            Drawing.OnDraw += delegate
            {
                foreach (var pg in ObjectManager.Get<Obj_AI_Hero>()
                    .Where(
                        pg => 
                            pg.IsValid &&
                            pg.IsHPBarRendered &&
                            !pg.IsMe &&
                            (pg.IsEnemy && Variables.Menu.Item($"{Variables.MainMenuName}.enemies").GetValue<bool>() ||
                            pg.IsAlly && Variables.Menu.Item($"{Variables.MainMenuName}.allies").GetValue<bool>())))
                {
                    for (int Spell = 0; Spell < Variables.SpellSlots.Count(); Spell++)
                    {
                        Variables.SpellX = (int)pg.HPBarPosition.X + 10 + (Spell * 25);
                        Variables.SpellY = (int)pg.HPBarPosition.Y + 35;

                        var GetSpell = pg.Spellbook.GetSpell(Variables.SpellSlots[Spell]);
                        var GetSpellCD = GetSpell.CooldownExpires - Game.Time + 1;
                        var SpellCDString = string.Format("{0:0}", GetSpellCD);
                        
                        Variables.DisplayTextFont.DrawText(
                            null,
                            GetSpellCD > 0 ?
                            SpellCDString : Variables.SpellSlots[Spell].ToString(),
                            
                            Variables.SpellX,
                            Variables.SpellY,

                            GetSpell.Level < 1 ?
                                SharpDX.Color.Gray :

                            GetSpell.SData.ManaCostArray.MaxOrDefault((value) => value) > pg.Mana ?
                                SharpDX.Color.Cyan :

                            GetSpellCD > 0 && GetSpellCD <= 4 ?
                                SharpDX.Color.Red :

                            GetSpellCD > 4 ?
                                SharpDX.Color.Yellow :
                                SharpDX.Color.LightGreen
                        );

                        for (int DrawSpellLevel = 0; DrawSpellLevel <= GetSpell.Level - 1; DrawSpellLevel++)
                        {
                            Variables.SpellLevelX = Variables.SpellX + (DrawSpellLevel * 3) - 4;
                            Variables.SpellLevelY = Variables.SpellY + 4;
                            
                            Variables.DisplayTextFont.DrawText(
                                null,
                                ".",

                                Variables.SpellLevelX,
                                Variables.SpellLevelY,
                                
                                SharpDX.Color.White
                            );
                        }
                    }
                    
                    for (int SummonerSpell = 0; SummonerSpell < Variables.SummonerSpellSlots.Count(); SummonerSpell++)
                    {
                        Variables.SummonerSpellX = (int)pg.HPBarPosition.X + 10 + (SummonerSpell * 88);
                        Variables.SummonerSpellY = (int)pg.HPBarPosition.Y + 4;
                        
                        var GetSummonerSpell = pg.Spellbook.GetSpell(Variables.SummonerSpellSlots[SummonerSpell]);
                        var GetSummonerSpellCD = GetSummonerSpell.CooldownExpires - Game.Time + 1;
                        var SummonerSpellCDString = string.Format("{0:0}", GetSummonerSpellCD);
                        
                        switch (GetSummonerSpell.Name.ToLower())
                        {
                            case "summonerflash":        Variables.GetSummonerSpellName = "Flash";        break;
                            case "summonerdot":          Variables.GetSummonerSpellName = "Ignite";       break;
                            case "summonerheal":         Variables.GetSummonerSpellName = "Heal";         break;
                            case "summonerteleport":     Variables.GetSummonerSpellName = "Teleport";     break;
                            case "summonerexhaust":      Variables.GetSummonerSpellName = "Exhaust";      break;
                            case "summonerhaste":        Variables.GetSummonerSpellName = "Ghost";        break;
                            case "summonerbarrier":      Variables.GetSummonerSpellName = "Barrier";      break;
                            case "summonerboost":        Variables.GetSummonerSpellName = "Cleanse";      break;
                            case "summonermana":         Variables.GetSummonerSpellName = "Clarity";      break;
                            case "summonerclairvoyance": Variables.GetSummonerSpellName = "Clairvoyance"; break;
                            case "summonerodingarrison": Variables.GetSummonerSpellName = "Garrison";     break;
                            case "summonersnowball":     Variables.GetSummonerSpellName = "Mark";         break;
                            
                            default: Variables.GetSummonerSpellName = "Smite"; break;
                        }
                        
                        Variables.DisplayTextFont.DrawText(
                            null,
                            GetSummonerSpellCD > 0 ?
                                Variables.GetSummonerSpellName + ":" + SummonerSpellCDString :
                                Variables.GetSummonerSpellName + ": UP ",
                            
                            Variables.SummonerSpellX,
                            Variables.SummonerSpellY,
                            
                            GetSummonerSpellCD > 0 ?
                                SharpDX.Color.Red :
                                SharpDX.Color.Yellow
                        );
                    }
                }
            };
        }
    }
}
