﻿using System;
using TUNING;
using UnityEngine;

namespace Drain
{
    class DrainConfig : IBuildingConfig
    {
        public const string Id = "Drain";
        public const string DisplayName = "Drain";
        public const string Description = "";
        public static string Effect = $"Slowly drains liquids into a pipe.";
        public static float[] MASS = BUILDINGS.CONSTRUCTION_MASS_KG.TIER2;

        public override BuildingDef CreateBuildingDef()
        {
            var def = BuildingTemplates.CreateBuildingDef(
                id: Id,
                width: 1,
                height: 1,
                anim: "drain_kanim",
                hitpoints: BUILDINGS.HITPOINTS.TIER1,
                construction_time: 30f,
                construction_mass: MASS,
                construction_materials: MATERIALS.ALL_METALS,
                melting_point: 1600f,
                build_location_rule: BuildLocationRule.Tile,
                decor: BUILDINGS.DECOR.PENALTY.TIER0,
                noise: NOISE_POLLUTION.NONE
            );
            def.UseStructureTemperature = false;
            def.Floodable = false;
            def.AudioCategory = "Metal";
            def.Overheatable = false;
            def.Entombable = false;
            def.IsFoundation = true;
            def.EnergyConsumptionWhenActive = 0f;
            def.ExhaustKilowattsWhenActive = 0f;
            def.SelfHeatKilowattsWhenActive = 0f;
            def.UtilityOutputOffset = new CellOffset(0, 0);
            def.OutputConduitType = ConduitType.Liquid;
            def.ViewMode = OverlayModes.LiquidConduits.ID;
            def.PermittedRotations = PermittedRotations.Unrotatable;
            def.SceneLayer = Grid.SceneLayer.TileMain;
            def.AudioSize = "small";
            BuildingTemplates.CreateFoundationTileDef(def);
            return def;
        }

        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            GeneratedBuildings.MakeBuildingAlwaysOperational(go);
            // varioius configs stolen from meshtile
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
            go.AddOrGet<SimCellOccupier>().doReplaceElement = false;
            go.AddOrGet<TileTemperature>();
            go.AddOrGet<BuildingHP>().destroyOnDamaged = true;
            // where you add the state machine, i think
            go.AddOrGet<Drain>();
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            GeneratedBuildings.RemoveLoopingSounds(go);
            // MeshTile stuff
            go.GetComponent<KPrefabID>().AddTag(GameTags.FloorTiles, false);
            go.AddComponent<SimTemperatureTransfer>();
            go.AddComponent<ZoneTile>();
            // Pump stuff
            go.AddOrGet<Storage>().capacityKg = 1f;
            ElementConsumer elementConsumer = go.AddOrGet<ElementConsumer>();
            elementConsumer.configuration = ElementConsumer.Configuration.AllLiquid;
            elementConsumer.consumptionRate = 0.1f;
            elementConsumer.storeOnConsume = true;
            elementConsumer.showInStatusPanel = false;
            elementConsumer.consumptionRadius = 1;
            ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
            conduitDispenser.conduitType = ConduitType.Liquid;
            conduitDispenser.alwaysDispense = true;
            conduitDispenser.elementFilter = (SimHashes[])null;
            BuildingTemplates.DoPostConfigure(go);
            // add anim
            go.GetComponent<KBatchedAnimController>().initialAnim = "built";
        }
    }
}