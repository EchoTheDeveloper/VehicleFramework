﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VehicleFramework.UpgradeTypes;

namespace VehicleFramework.Admin
{
    public static class Utils
    {
        public static Shader StoreShader(List<MeshRenderer> rends)
        {
            Shader m_ShaderMemory = null;
            foreach (var rend in rends) //go.GetComponentsInChildren<MeshRenderer>(true)
            {
                // skip some materials
                foreach (Material mat in rend.materials)
                {
                    if (mat.shader != null)
                    {
                        m_ShaderMemory = mat.shader;
                        break;
                    }
                }
            }
            return m_ShaderMemory;
        }
        public static void ListShadersInUse()
        {
            HashSet<string> shaderNames = new HashSet<string>();

            // Find all materials currently loaded in the game.
            Material[] materials = Resources.FindObjectsOfTypeAll<Material>();

            foreach (var material in materials)
            {
                if (material.shader != null)
                {
                    // Add the shader name to the set to ensure uniqueness.
                    shaderNames.Add(material.shader.name);
                }
            }

            // Now you have a unique list of shader names in use.
            foreach (var shaderName in shaderNames)
            {
                Debug.Log("Shader in use: " + shaderName);
            }
        }
        public static void ListShaderProperties()
        {
            Shader shader = Shader.Find("MarmosetUBER");
            for (int i = 0; i < shader.GetPropertyCount(); i++)
            {
                string propertyName = shader.GetPropertyName(i);
                Debug.Log($"Property {i}: {propertyName}, Type: {shader.GetPropertyType(i)}");
            }
        }
        public static void ApplyInteriorLighting()
        {
            //ListShadersInUse();
            //ListShaderProperties();
            //VehicleBuilder.ApplyShaders(this, shader4);
        }
        public static void LoadShader(ModVehicle mv, Shader shade)
        {
            VehicleBuilder.ApplyShaders(mv, shade);
        }
        public static bool IsAnAncestorTheCurrentMountedVehicle(Transform current)
        {
            if (current == null)
            {
                return false;
            }
            if (current.GetComponent<Vehicle>() != null)
            {
                return current.GetComponent<Vehicle>() == Player.main.GetVehicle();
            }
            return IsAnAncestorTheCurrentMountedVehicle(current.parent);
        }
        public static void RegisterDepthModules()
        {
            TechType depth1 = UpgradeRegistrar.RegisterUpgrade(new DepthModules.DepthModule1());

            var depthmodule2 = new DepthModules.DepthModule2();
            depthmodule2.ExtendRecipe(new Assets.Ingredient(depth1, 1));
            TechType depth2 = UpgradeRegistrar.RegisterUpgrade(depthmodule2);

            var depthmodule3 = new DepthModules.DepthModule3();
            depthmodule3.ExtendRecipe(new Assets.Ingredient(depth2, 1));
            TechType depth3 = UpgradeRegistrar.RegisterUpgrade(depthmodule3);
        }
        public static void EvaluateDepthModules(AddActionParams param)
        {
            // Iterate over all upgrade modules,
            // in order to determine our max depth module level
            int maxDepthModuleLevel = 0;
            List<string> upgradeSlots = new List<string>();
            param.mv.upgradesInput.equipment.GetSlots(VehicleBuilder.ModuleType, upgradeSlots);
            foreach (String slot in upgradeSlots)
            {
                InventoryItem upgrade = param.mv.upgradesInput.equipment.GetItemInSlot(slot);
                if (upgrade != null)
                {
                    //Logger.Log(slot + " : " + upgrade.item.name);
                    if (upgrade.item.name == "ModVehicleDepthModule1(Clone)")
                    {
                        if (maxDepthModuleLevel < 1)
                        {
                            maxDepthModuleLevel = 1;
                        }
                    }
                    else if (upgrade.item.name == "ModVehicleDepthModule2(Clone)")
                    {
                        if (maxDepthModuleLevel < 2)
                        {
                            maxDepthModuleLevel = 2;
                        }
                    }
                    else if (upgrade.item.name == "ModVehicleDepthModule3(Clone)")
                    {
                        if (maxDepthModuleLevel < 3)
                        {
                            maxDepthModuleLevel = 3;
                        }
                    }
                }
            }
            int extraDepthToAdd = 0;
            extraDepthToAdd = maxDepthModuleLevel > 0 ? extraDepthToAdd += param.mv.CrushDepthUpgrade1 : extraDepthToAdd;
            extraDepthToAdd = maxDepthModuleLevel > 1 ? extraDepthToAdd += param.mv.CrushDepthUpgrade2 : extraDepthToAdd;
            extraDepthToAdd = maxDepthModuleLevel > 2 ? extraDepthToAdd += param.mv.CrushDepthUpgrade3 : extraDepthToAdd;
            param.mv.GetComponent<CrushDamage>().SetExtraCrushDepth(extraDepthToAdd);
        }
        public static TechType GetTechTypeFromVehicleName(string name)
        {
            try
            {
                VehicleEntry ve = VehicleManager.vehicleTypes.Where(x => x.name.Contains(name)).First();
                return ve.techType;
            }
            catch
            {
                Logger.Error("GetTechTypeFromVehicleName Error. Could not find a vehicle by the name: " + name + ". Here are all vehicle names:");
                VehicleManager.vehicleTypes.ForEach(x => Logger.Log(x.name));
                return 0;
            }
        }
    }
}
