using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using ParagonTotem;
using BTD_Mod_Helper.Api.Data;
using BTD_Mod_Helper.Api.ModOptions;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using BTD_Mod_Helper.Api.Helpers;
using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppNinjaKiwi.Common.ResourceUtils;

[assembly: MelonInfo(typeof(ParagonTotem.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace ParagonTotem
{
    public class Main : BloonsTD6Mod
        {
            public static bool Cheating;

        
        public override void OnNewGameModel(GameModel gameModel, Il2CppSystem.Collections.Generic.List<ModModel> mods)
        {
            gameModel.GetTowerFromId("ParagonTotem-ParagonTotem").cost = CostHelper.CostForDifficulty(Settings.TotemCost, mods);

            foreach (var towerModel in gameModel.towers)
            {
                if (towerModel.name == ModContent.TowerID<ParagonTotem>())
                {
                    if (Settings.OPParagonTotem == true)
                    {
                        towerModel.GetBehavior<ParagonSacrificeBonusModel>().bonus = 9999999999999999999;
                    }
                    else
                    {
                        towerModel.GetBehavior<ParagonSacrificeBonusModel>().bonus = 2000;
                    }
                }
            }
        }
        public override void OnMainMenu()
            {

                //Menu messages

                    if (Settings.OPParagonTotem == false)
                    {
                        MelonLogger.Msg(System.ConsoleColor.Magenta, "The regular version of the paragon totem has been loaded.");
                if (Settings.TogglePopup == true)
                {
PopupScreen.instance.ShowOkPopup("The regular version of the paragon totem has been loaded.");
                }
                        
                    }
                    else
                    {
                   
                        MelonLogger.Msg(System.ConsoleColor.Magenta, "The OP version of the paragon totem has been loaded.");
                if (Settings.TogglePopup == true)
                {
PopupScreen.instance.ShowOkPopup("The OP version of the paragon totem has been loaded.");
                }
                        
                    }
                
            }
            public override void OnApplicationStart()
            {
                MelonLogger.Msg(System.ConsoleColor.Magenta, "Paragon Totem Loaded!");
            }
            
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

                public override TowerSet TowerSet => TowerSet.Support;

                public override void ModifyBaseTowerModel(TowerModel towerModel)
                {
                    towerModel.RemoveBehavior<AttackModel>();
                    towerModel.display = new PrefabReference() { guidRef = "2f4aba5a77592134cb19b35e844cea8a" };
                    towerModel.GetBehavior<DisplayModel>().display = new PrefabReference() { guidRef = "2f4aba5a77592134cb19b35e844cea8a" };
                    towerModel.AddBehavior(new ParagonSacrificeBonusModel("ParagonBonus", 2000));
                }

                public override string Icon => "ParagonPowerTotemPortrait";

                public override string Portrait => "ParagonPowerTotemPortrait";
            }
        }
        public class Settings : ModSettings
        {
            public static readonly ModSettingBool OPParagonTotem = new(false)
            {
            displayName = "OP Mode of the Paragon Totem",
            description = "Toggles the OP mode of the paragon totem",
            button = true,
            };
            public static readonly ModSettingBool TogglePopup = new(true)
            {
            displayName = "Toggle Popup",
            description = "Toggles the popup on main menu that tells you what version of the paragon totem you are on",
            button = true,
            };
            public static readonly ModSettingInt TotemCost = new(26000)
            {
            displayName = "Paragon Totem Cost",
            min = 0
            };
    }
}
