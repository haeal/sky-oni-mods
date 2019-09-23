﻿using Harmony;
using System;
using System.Reflection;
using UnityEngine;
using static SkyLib.Logger;
using static SkyLib.OniUtils;

namespace RadiateHeat
{
    public class RadiatePatch
    {
        public static string ModName = "RadiateHeatInSpace";
        public static bool didStartUp_Building = false;

        public class PatchedBuilding
        {
            public Type type;
            public float emissivity = 0.9f;
            public float surface_area = 1f;

            public bool do_func(GameObject go)
            {
                var heat = go.AddOrGet<RadiatesHeat>();
                heat.emissivity = emissivity;
                heat.surface_area = surface_area;
                return true;
            }
        }

        public static class Mod_OnLoad
        {
            public static void OnLoad()
            {
                StartLogging(ModName);
            }

            public static void AttachHeatComponent(GameObject go, float emissivity, float surface_area)
            {
                var heat = go.AddOrGet<RadiatesHeat>();
                heat.emissivity = emissivity;
                heat.surface_area = surface_area;
            }
        }

        [HarmonyPatch(typeof(GeneratedBuildings))]
        [HarmonyPatch(nameof(GeneratedBuildings.LoadGeneratedBuildings))]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Path
        {
            public static void Prefix()
            {
                if (!didStartUp_Building)
                {
                    AddStatusItem($"{ModName}_RADIATING", "NAME", "Radiating {0}");
                    AddStatusItem($"{ModName}_RADIATING", "TOOLTIP", "This building is currently radiating heat at {0}.");
                    AddStatusItem($"{ModName}_NOTINSPACE", "NAME", "Not in space");
                    AddStatusItem($"{ModName}_NOTINSPACE", "TOOLTIP", "This building is not fully in space and is not radiating heat.");
                    didStartUp_Building = true;
                }
            }
        }

        [HarmonyPatch(typeof(AutoMinerConfig), "DoPostConfigureComplete")]
        public static class AutoMinerConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .2f, 3f);
            }
        }

        [HarmonyPatch(typeof(BatteryConfig), "DoPostConfigureComplete")]
        public static class BatteryConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .8f, 2f);
            }
        }

        [HarmonyPatch(typeof(BatteryMediumConfig), "DoPostConfigureComplete")]
        public static class BatteryMediumConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .8f, 4f);
            }
        }

        [HarmonyPatch(typeof(BatterySmartConfig), "DoPostConfigureComplete")]
        public static class BatterySmartConfigConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .7f, 4f);
            }
        }

        [HarmonyPatch(typeof(PowerTransformerSmallConfig), "DoPostConfigureComplete")]
        public static class PowerTransformerSmallConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .3f, 4f);
            }
        }

        [HarmonyPatch(typeof(PowerTransformerConfig), "DoPostConfigureComplete")]
        public static class PowerTransformerConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .2f, 6f);
            }
        }

        [HarmonyPatch(typeof(SolidTransferArmConfig), "DoPostConfigureComplete")]
        public static class SolidTransferArmConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .6f, 3f);
            }
        }

        [HarmonyPatch(typeof(ObjectDispenserConfig), "DoPostConfigureComplete")]
        public static class ObjectDispenserConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .2f, 2f);
            }
        }

        [HarmonyPatch(typeof(StorageLockerSmartConfig), "DoPostConfigureComplete")]
        public static class StorageLockerSmartConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .3f, 2f);
            }
        }

        [HarmonyPatch(typeof(SolidConduitInboxConfig), "DoPostConfigureComplete")]
        public static class SolidConduitInboxConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .4f, 2f);
            }
        }

        [HarmonyPatch(typeof(CheckpointConfig), "DoPostConfigureComplete")]
        public static class CheckpointConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .1f, 1f);
            }
        }

        [HarmonyPatch(typeof(GantryConfig), "DoPostConfigureComplete")]
        public static class GantryConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .1f, 12f);
            }
        }

        [HarmonyPatch(typeof(CeilingLightConfig), "DoPostConfigureComplete")]
        public static class CeilingLightConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .5f, .5f);
            }
        }

        [HarmonyPatch(typeof(FloorLampConfig), "DoPostConfigureComplete")]
        public static class FloorLampConfig_Patch
        {
            public static void Prefix(GameObject go)
            {
                Mod_OnLoad.AttachHeatComponent(go, .3f, 1.5f);
            }
        }
    }
}