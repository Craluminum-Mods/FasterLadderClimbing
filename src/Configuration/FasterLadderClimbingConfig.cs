namespace FasterLadderClimbing.Configuration
{
    class FasterLadderClimbingConfig
    {
        public double ladder_climbing_speed_down_negative = -0.07;
        public double ladder_climbing_speed_down = 0.07;
        public double ladder_climbing_speed_up = 0.035;

        public FasterLadderClimbingConfig() { }

        public FasterLadderClimbingConfig(FasterLadderClimbingConfig previousConfig)
        {
            ladder_climbing_speed_down_negative = previousConfig.ladder_climbing_speed_down_negative;
            ladder_climbing_speed_down = previousConfig.ladder_climbing_speed_down;
            ladder_climbing_speed_up = previousConfig.ladder_climbing_speed_up;
        }
    }
}