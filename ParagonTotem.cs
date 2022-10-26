using Assets.Scripts.Models;
using Assets.Scripts.Models.GenericBehaviors;
using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Abilities;
using Assets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Models.Towers.Filters;
using Assets.Scripts.Models.Towers.Projectiles;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.TowerFilters;
using Assets.Scripts.Models.Towers.Weapons;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Models.TowerSets;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display;
using Assets.Scripts.Utils;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using Harmony;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using ParagonTotem;
using UnhollowerBaseLib;
using BTD_Mod_Helper.Api.Data;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using Assets.Scripts.Unity.UI_New.Popups;
using System.IO;

[assembly: MelonInfo(typeof(ParagonTotem.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ParagonTotem
{
        public class Main : BloonsTD6Mod
        {
            public static string ModFolderPath = MelonHandler.ModsDirectory;
            public static string ModSettingsPath = MelonHandler.ModsDirectory + "\\BloonsTD6 Mod Helper\\Mod Settings\\ParagonTotemInShopSettings.txt";
            public static bool OPParagonTotem = false;
            public static bool Cheating;
            public static bool SettingsEdited = false;
            public override void OnMainMenu()
            {
                base.OnMainMenu();

                //cheats popup

                if (Cheating == true)
                {
                    PopupScreen.instance.ShowEventPopup(PopupScreen.Placement.menuCenter, "Cheat Mods Detected", "Cheat mods have been detected. To remove this message hold down ALT + F4 to close your game, then remove your cheat mods. If not then you will not be able to continue to the game. Have fun staring at this popup! :) :) :) :) :) :) :) :) :) :) :) :)", "Neither Does This", (Action)null, "This Does Nothing", (Action)null, Popup.TransitionAnim.AnimIndex, 38);
                }
                else
                {

                }

                //Menu messages

                if (SettingsEdited == true)
                {
                    if (OPParagonTotem == false)
                    {
                        MelonLogger.Msg(ConsoleColor.Magenta, "To switch to the regular version of the paragon totem, restart your game.");
                        PopupScreen.instance.ShowOkPopup("To switch to the regular version of the paragon totem, restart your game.");
                    }
                    else
                    {
                        MelonLogger.Msg(ConsoleColor.Magenta, "To switch to the OP version of the paragon totem, restart your game.");
                        PopupScreen.instance.ShowOkPopup("To switch to the OP version of the paragon totem, restart your game.");
                    }
                }
                else
                {
                    if (OPParagonTotem == false)
                    {
                        MelonLogger.Msg(ConsoleColor.Magenta, "The regular version of the paragon totem has been loaded.");
                        PopupScreen.instance.ShowOkPopup("The regular version of the paragon totem has been loaded.");
                    }
                    else
                    {
                        MelonLogger.Msg(ConsoleColor.Magenta, "The OP version of the paragon totem has been loaded.");
                        PopupScreen.instance.ShowOkPopup("The OP version of the paragon totem has been loaded.");
                    }
                }
            }
            public override void OnApplicationStart()
            {

                //cheats check

                if (ModContent.HasMod("BTD6 All Trophy Store Items Unlocker"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("BTD6 Boss Bloons In Sandbox"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("BTD6 Golden Bloon In Sandbox"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("BTD6 Infinite Monkey Knowledge"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("BTD6 Infinite Monkey Money"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("BTD6 Infinite Tower XP"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("Gurren Core"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("infinite_xp"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("NKHook6"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("NoAbilityCoolDown"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("UnlockAllMaps"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("UnlockDoubleCash"))
                {
                    Cheating = true;
                }
                if (ModContent.HasMod("xpfarming"))
                {
                    Cheating = true;
                }
                else
                {

                }

                //save data


                if (File.Exists(ModSettingsPath) == true)
                {
                    MelonLogger.Msg("Loading configs from mod settings file.");
                }
                else
                {
                    MelonLogger.Msg("Generating mod settings file.");
                    OPParagonTotem = false;
                    TextWriter tw = new StreamWriter(ModSettingsPath);
                    tw.WriteLine(OPParagonTotem);
                    tw.Close();
                    OPParagonTotem = false;
                }

                TextReader tr = new StreamReader(ModSettingsPath);
                OPParagonTotem = bool.Parse(tr.ReadLine());
                tr.Close();

                //console message

                MelonLogger.Msg(ConsoleColor.Magenta, "Paragon Totem Loaded!");
            }
            private static readonly ModSettingButton TurnOnOpTotem = new()
            {
                displayName = "Turn On OP/Regular Totem",
                action = () =>
                {
                    PopupScreen.instance.ShowOkPopup("Restart the game to apply changes.");
                    OPParagonTotem = true;
                    TextWriter tw = new StreamWriter(ModSettingsPath);
                    tw.WriteLine(OPParagonTotem);
                    tw.Close();
                    OPParagonTotem = true;
                    SettingsEdited = true;
                },
                buttonText = "Op",
            };
            private static readonly ModSettingButton TurnOnBalancedTotem = new()
            {
                displayName = "",
                action = () =>
                {
                    PopupScreen.instance.ShowOkPopup("Restart the game to apply changes.");
                    //PopupScreen.instance.ShowEventPopup(PopupScreen.Placement.menuCenter, "Restart Game", "For your changes to take place you must restart the game.", "Restart Game", (Action)null , "Cancel", (Action)null, Popup.TransitionAnim.AnimIndex, 38);
                    OPParagonTotem = false;
                    TextWriter tw = new StreamWriter(ModSettingsPath);
                    tw.WriteLine(OPParagonTotem);
                    tw.Close();
                    OPParagonTotem = false;
                    SettingsEdited = true;
                },
                buttonText = "Regular",
            };
            public class ParagonTotem : ModTower
            {
                public override string Name => nameof(ParagonTotem);

                public override string DisplayName => "Paragon Totem";

                public override string Description => "";

                public override string BaseTower => "DartMonkey";

                public override int Cost => 26000;

                public override int TopPathUpgrades => 0;

                public override int MiddlePathUpgrades => 0;

                public override int BottomPathUpgrades => 0;

                public override ParagonMode ParagonMode => ParagonMode.Base555;

                public override string TowerSet => "Support";

                public override void ModifyBaseTowerModel(TowerModel towerModel)
                {
                    towerModel.RemoveBehavior<AttackModel>();
                    towerModel.display = new PrefabReference() { guidRef = "2f4aba5a77592134cb19b35e844cea8a" };
                    towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "2f4aba5a77592134cb19b35e844cea8a" };
                    if(OPParagonTotem == false)
                    {
                        towerModel.AddBehavior(new ParagonSacrificeBonusModel("ParagonBonus", 2000));
                    }
                    else
                    {
                        towerModel.AddBehavior(new ParagonSacrificeBonusModel("ParagonBonus", 99999999999999));
                    }
                }

                public override string Icon => "ParagonPowerTotemPortrait";

                public override string Portrait => "ParagonPowerTotemPortrait";
            }
        }

    
}
