using BPSum.Library.Blueprint;
using BPSum.Library.Data;
using BPSum.Library.Localization;
using BPSum.Library.World;
using System;
using System.Collections.Generic;
using System.IO;

namespace BPSum.Library
{
    public class SummaryCalculator
    {
        public Action<List<string>> HandleUnknownBlocks;

        DataLoader dataLoader;
        BlueprintLoader blueprintLoader;
        WorldLoader worldLoader;
        LocalizationLoader localizationLoader;
        string modsPath;
        Dictionary<string, ComponentDefinition> components = new Dictionary<string, ComponentDefinition>();
        Dictionary<string, CubeBlockDefinition> cubeBlocks = new Dictionary<string, CubeBlockDefinition>();
        Dictionary<string, string> localization = new Dictionary<string, string>();

        public SummaryCalculator(string gamePath = @"C:\Program Files (x86)\Steam\steamapps\common\SpaceEngineers", string modsPath = @"C:\Program Files (x86)\Steam\steamapps\workshop\content\244850")
        {
            dataLoader = new DataLoader(AddDefinitions);
            blueprintLoader = new BlueprintLoader();
            worldLoader = new WorldLoader();
            localizationLoader = new LocalizationLoader();
            this.modsPath = modsPath;
            string dataPath = Path.Combine(gamePath, "Content", "Data");
            dataLoader.LoadFile(Path.Combine(dataPath, "Components.sbc"));
            dataLoader.LoadFile(Path.Combine(dataPath, "CubeBlocks.sbc"));
            LoadLocalization(Path.Combine(dataPath, "Localization", "MyTexts.resx"));
        }

        public void LoadFile(string path)
        {
#if DEBUG
            Console.WriteLine($"Loading data from file {path}...");
#endif
            dataLoader.LoadFile(path);
        }

        public void LoadDir(string path)
        {
#if DEBUG
            Console.WriteLine($"Loading data from directory {path}...");
#endif
            dataLoader.LoadDir(path);
        }

        public void LoadMod(string path)
        {
            if (!Directory.Exists(path))
            {
                // Check if a mod id was supplied
                path = Path.Combine(modsPath, path);
                if (!Directory.Exists(path))
                {
                    throw new Exception($"Mod {path} not found");
                }
            }
#if DEBUG
            Console.WriteLine($"Loading mod {path}...");
#endif
            string dataPath = Path.Combine(path, "Data");
            if (Directory.Exists(dataPath))
            {
                LoadDir(dataPath);
                string localizationPath = Path.Combine(dataPath, "Localization", "MyTexts.resx");
                if (File.Exists(localizationPath))
                {
                    LoadLocalization(localizationPath);
                }
            }
#if DEBUG
            else
            {
                Console.WriteLine("Mod has no relevant data");
            }
#endif
        }

        public void LoadWorld(string path)
        {
#if DEBUG
            Console.WriteLine($"Loading mods from world {path}...");
#endif
            Mod[] mods = worldLoader.LoadFile(path);
            foreach (Mod mod in mods)
            {
                LoadMod(mod.Directory);
            }
        }

        public void LoadLocalization(string path)
        {
#if DEBUG
            Console.WriteLine($"Loading localization from {path}...");
#endif
            LocalizationData[] datas = localizationLoader.LoadFile(path);
            foreach (LocalizationData data in datas)
            {
                localization[data.Name] = data.Value;
            }
        }

        public void InitializeDefinitions()
        {
            // Add component references to each block and check that blocks don't contain invalid components
            foreach (CubeBlockDefinition block in cubeBlocks.Values)
            {
                foreach (CubeBlockComponent componentInfo in block.Components)
                {
                    if (!components.TryGetValue(componentInfo.Subtype, out componentInfo.Component))
                    {
                        throw new Exception($"Invalid component {componentInfo.Subtype} in block {block.Id.SubtypeId}");
                    }
                }
            }
            // Load proper name from localization for each component if exist, otherwise leave unchanged
            foreach (ComponentDefinition component in components.Values)
            {
                string name = component.DisplayName;
                if (!localization.TryGetValue(name, out component.DisplayName))
                {
                    component.DisplayName = name;
                }
            }
        }

        public Dictionary<ComponentDefinition, uint> Calculate(string path)
        {
            ShipBlueprint bp = blueprintLoader.LoadFile(path);
            Dictionary<ComponentDefinition, uint> dict = new Dictionary<ComponentDefinition, uint>();
            List<string> unknownBlocks = new List<string>();
            foreach (CubeGrid grid in bp.CubeGrids)
            {
                foreach (CubeBlock blockInfo in grid.CubeBlocks)
                {
                    if (cubeBlocks.TryGetValue(blockInfo.SubtypeName, out CubeBlockDefinition block))
                    {
                        foreach (CubeBlockComponent componentInfo in block.Components)
                        {
                            if (dict.TryGetValue(componentInfo.Component, out uint count))
                            {
                                dict[componentInfo.Component] = count + componentInfo.Count;
                            }
                            else
                            {
                                dict[componentInfo.Component] = componentInfo.Count;
                            }
                        }
                    }
                    else if (!unknownBlocks.Contains(blockInfo.SubtypeName))
                    {
                        unknownBlocks.Add(blockInfo.SubtypeName);
                    }
                }
            }
            if (unknownBlocks.Count != 0 && HandleUnknownBlocks != null)
            {
                HandleUnknownBlocks(unknownBlocks);
            }
            return dict;
        }

        internal void AddDefinitions(DataDefinitions definitions)
        {
            if (definitions.Components != null)
            {
#if DEBUG
                Console.WriteLine($"Found {definitions.Components.Length} components");
#endif
                AddComponents(definitions.Components);
            }
            if (definitions.CubeBlocks != null)
            {
                AddCubeBlocks(definitions.CubeBlocks);
#if DEBUG
                Console.WriteLine($"Found {definitions.CubeBlocks.Length} blocks");
#endif
            }
        }

        internal void AddComponents(ComponentDefinition[] componentsArray)
        {
            foreach (ComponentDefinition component in componentsArray)
            {
                components[component.Id.SubtypeId] = component;
            }
        }
        internal void AddCubeBlocks(CubeBlockDefinition[] cubeBlocksArray)
        {
            foreach (CubeBlockDefinition cubeBlock in cubeBlocksArray)
            {
                cubeBlocks[cubeBlock.Id.SubtypeId] = cubeBlock;
            }
        }
    }
}
