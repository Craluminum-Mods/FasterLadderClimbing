using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using FasterLadderClimbing.Configuration;
using HarmonyLib;
using Vintagestory.API.Common;
using Vintagestory.GameContent;

[assembly: ModInfo("Faster Ladder Climbing",
    Authors = new[] { "Craluminum2413" })]

namespace FasterLadderClimbing
{
    class Core : ModSystem
    {
        public const string HarmonyID = "craluminum2413.fasterladderclimbing";
        public static ICoreAPI _api;

        public override void Start(ICoreAPI api)
        {
            base.Start(api);
            _api = api;
            ModConfig.ReadConfig(api);
            new Harmony(HarmonyID).PatchAll(Assembly.GetExecutingAssembly());
            api.World.Logger.Event("started 'Faster Ladder Climbing' mod");
        }

        public override void Dispose()
        {
            new Harmony(HarmonyID).UnpatchAll();
            base.Dispose();
        }

        [HarmonyPatch]
        public static class EntityBehaviorControlledPhysicsPatch
        {
            [HarmonyTranspiler]
            [HarmonyPatch(typeof(EntityBehaviorControlledPhysics), nameof(EntityBehaviorControlledPhysics.DisplaceWithBlockCollision))]
            public static IEnumerable<CodeInstruction> Harmony_EntityBehaviorControlledPhysics_DisplaceWithBlockCollision_Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var ladder_climbing_speed_down_negative = _api.World.Config.GetDouble("ladder_climbing_speed_down_negative");
                var ladder_climbing_speed_down = _api.World.Config.GetDouble("ladder_climbing_speed_down");
                var ladder_climbing_speed_up = _api.World.Config.GetDouble("ladder_climbing_speed_up");

                bool found = false;
                var codes = new List<CodeInstruction>(instructions);

                for (int i = 0; i < codes.Count; i++)
                {
                    if (!found && codes[i].Is(OpCodes.Ldc_R8, -0.07))
                    {
                        codes[i].operand = ladder_climbing_speed_down_negative;
                        yield return codes[i];
                        continue;
                    }
                    if (!found && codes[i].Is(OpCodes.Ldc_R8, 0.07))
                    {
                        codes[i].operand = ladder_climbing_speed_down;
                        yield return codes[i];
                        continue;
                    }
                    if (!found && codes[i].Is(OpCodes.Ldc_R8, 0.035))
                    {
                        codes[i].operand = ladder_climbing_speed_up;
                        yield return codes[i];
                        found = true;
                        continue;
                    }
                    yield return codes[i];
                }
            }
        }
    }
}