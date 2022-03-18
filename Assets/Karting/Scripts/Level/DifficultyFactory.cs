
public class DifficultyFactory
{
    public static LevelDifficulty GetLevelDifficulty (LevelManager.GameMode gameMode)
    {
        switch (gameMode)
        {
            case LevelManager.GameMode.CHECKPOINT:
                return new CheckpointLevelDifficulty();
            case LevelManager.GameMode.CRASH_COURSE:
                return new CrashCourseLevelDifficulty();
            default:
                return new CheckpointLevelDifficulty();
        }
    }
}
