using Vintagestory.API.Common;

namespace FasterLadderClimbing.Configuration
{
    static class ModConfig
    {
        private const string jsonConfig = "FasterLadderClimbing.json";
        private static FasterLadderClimbingConfig config;

        public static void ReadConfig(ICoreAPI api)
        {
            try
            {
                config = LoadConfig(api);

                if (config == null)
                {
                    GenerateConfig(api);
                    config = LoadConfig(api);
                }
                else
                {
                    GenerateConfig(api, config);
                }
            }
            catch
            {
                GenerateConfig(api);
                config = LoadConfig(api);
            }

            api.World.Config.SetDouble("ladder_climbing_speed_down_negative", config.ladder_climbing_speed_down_negative);
            api.World.Config.SetDouble("ladder_climbing_speed_down", config.ladder_climbing_speed_down);
            api.World.Config.SetDouble("ladder_climbing_speed_up", config.ladder_climbing_speed_up);
        }

        private static FasterLadderClimbingConfig LoadConfig(ICoreAPI api) => api.LoadModConfig<FasterLadderClimbingConfig>(jsonConfig);
        private static void GenerateConfig(ICoreAPI api) => api.StoreModConfig(new FasterLadderClimbingConfig(), jsonConfig);
        private static void GenerateConfig(ICoreAPI api, FasterLadderClimbingConfig previousConfig) => api.StoreModConfig(new FasterLadderClimbingConfig(previousConfig), jsonConfig);
    }
}