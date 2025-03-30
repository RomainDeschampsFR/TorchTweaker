using Il2CppNewtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModSettings;
using Il2Cpp;
using UnityEngine;

namespace TorchTweaker
{
    class TorchTweakerSettings : JsonModSettings
    {
        // PICK UP TORCH FROM FIRE SECTION
        [Section("PICK UP FROM FIRE")]
        [Name("MIN CONDITION")]
        [Description("The minimal condition of torches collected from fires. \nVanilla value : 0.20")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:0.00}")]
        public float minCondition = 0.10f;

        [Name("MAX CONDITION")]
        [Description("The maximal condition of torches collected from fires. \nVanilla value : 0.50")]
        [Slider(0f, 1f, 101, NumberFormat = "{0:0.00}")]
        public float maxCondition = 0.20f;

        // TORCH BURNING TIME SECTION
        [Section("TORCH BURNING TIME")]
        [Name("Burning time (min)")]
        [Description("Time taken for a 100 % torch to burn out. \nVanilla value : 90 min \nNeed a scene reload to be taken into account")]
        [Slider(60, 120)]
        public int burnTime = 60;

        // OTHER SECTION
        [Section("OTHER")]
        [Name("Lit from lantern button")]
        [Description("Button to ignite the torch from a lantern")]
        public KeyCode interactButton = KeyCode.Mouse2;

        [Name("Disable LMB : Torch")]
        [Description("Disable extinguishing of torch on mouseclick")]
        public bool disableTorch = true;

        [Name("Disable LMB : Lantern")]
        [Description("Disable extinguishing of lantern on mouseclick")]
        public bool disableLantern = true;

        protected override void OnConfirm()
        {
            base.OnConfirm();
        }
    }

    internal static class Settings
    {
        public static TorchTweakerSettings settings = new();

        public static void OnLoad()
        {
            settings.AddToModSettings("Torch Tweaker");
        }
    }
}

