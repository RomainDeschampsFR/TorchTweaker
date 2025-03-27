using Il2Cpp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

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
    }
}
