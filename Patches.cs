using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using Il2CppTLD.Gear;
using UnityEngine.Networking.Types;
using MelonLoader;

namespace TorchTweaker
{
    class Patches
    {
        [HarmonyPatch(typeof(Panel_FeedFire), nameof(Panel_FeedFire.Enable))]
        internal class Panel_FeedFire_Enable_UpdateTorches
        {
            private static void Postfix(Panel_FeedFire __instance)
            {
                __instance.m_MinNormalizedTorchCondition = Settings.settings.minCondition;
                __instance.m_MaxNormalizedTorchCondition = Settings.settings.maxCondition;
            }
        }

        [HarmonyPatch(typeof(Panel_ActionPicker), nameof(Panel_ActionPicker.Enable))]
        internal class Panel_ActionPicker_Enable_UpdateTorches
        {
            private static void Postfix(Panel_ActionPicker __instance)
            {
                InterfaceManager.GetPanel<Panel_FeedFire>().m_MinNormalizedTorchCondition = Settings.settings.minCondition;
                InterfaceManager.GetPanel<Panel_FeedFire>().m_MaxNormalizedTorchCondition = Settings.settings.maxCondition;
            }
        }

        [HarmonyPatch(typeof(TorchItem), nameof(TorchItem.CancelIgnite))]
        public class TorchItem_CancelIgnite_
        {
            public static void PostFix(TorchItem __instance)
            {
                MelonLogger.Msg("Cancel ignite");
            }
        }

        [HarmonyPatch(typeof(TorchItem), nameof(TorchItem.ExtinguishDelayed))]
        public class TorchItemPatch
        {
            public static bool Prefix(TorchItem __instance)
            {
                if (Settings.settings.disableTorch)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }

        [HarmonyPatch(typeof(InputManager), nameof(InputManager.ExecuteHolsterAction))]
        public class HolsterPatchTorch
        {
            public static bool Prefix(InputManager __instance)
            {
                if (Settings.settings.disableTorch && GameManager.GetPlayerManagerComponent().m_ItemInHands && GameManager.GetPlayerManagerComponent().m_ItemInHands.m_TorchItem)
                {
                    TorchItem holdingTorch = GameManager.GetPlayerManagerComponent().m_ItemInHands.m_TorchItem;

                    if (holdingTorch.IsBurning())
                    {
                        InterfaceManager.GetPanel<Panel_HUD>().StartItemProgressBar(holdingTorch.m_ExtinguishTime, Localization.Get("GAMEPLAY_Extinguishing"), holdingTorch.GetGearItem(), new Action(holdingTorch.ExtinguishAfterDelayStarted));
                        return false;
                    }
                }
                return true;
            }
        }


        [HarmonyPatch(typeof(KeroseneLampItem), nameof(KeroseneLampItem.Toggle))]
        public class LampItemPatch2
        {
            public static bool Prefix(KeroseneLampItem __instance)
            {
                if (__instance.m_On && Settings.settings.disableLantern)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
