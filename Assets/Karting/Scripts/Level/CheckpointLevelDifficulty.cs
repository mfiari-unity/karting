
public class CheckpointLevelDifficulty : LevelDifficulty
{
    public override void setDifficulty(ObjectiveReachTargets objectiveReachTargets, LevelManager.GameLevel gameLevel, LevelManager.GameDifficulty gameDifficulty)
    {
        switch (gameLevel)
        {
            case LevelManager.GameLevel.OVAL:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        objectiveReachTargets.totalTimeInSecs = 30;
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        objectiveReachTargets.totalTimeInSecs = 300;
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        objectiveReachTargets.totalTimeInSecs = 10;
                        break;
                }
                break;
            case LevelManager.GameLevel.ADDITIONAL:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        objectiveReachTargets.totalTimeInSecs = 60;
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        objectiveReachTargets.totalTimeInSecs = 45;
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        objectiveReachTargets.totalTimeInSecs = 35;
                        break;
                }
                break;
            case LevelManager.GameLevel.COUNTRY:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        objectiveReachTargets.totalTimeInSecs = 50;
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        objectiveReachTargets.totalTimeInSecs = 35;
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        objectiveReachTargets.totalTimeInSecs = 25;
                        break;
                }
                break;
            case LevelManager.GameLevel.MOUNTAIN:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        objectiveReachTargets.totalTimeInSecs = 80;
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        objectiveReachTargets.totalTimeInSecs = 60;
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        objectiveReachTargets.totalTimeInSecs = 45;
                        break;
                }
                break;
            case LevelManager.GameLevel.WINDING:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        objectiveReachTargets.totalTimeInSecs = 40;
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        objectiveReachTargets.totalTimeInSecs = 30;
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        objectiveReachTargets.totalTimeInSecs = 20;
                        break;
                }
                break;
            default:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        objectiveReachTargets.totalTimeInSecs = 60;
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        objectiveReachTargets.totalTimeInSecs = 45;
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        objectiveReachTargets.totalTimeInSecs = 30;
                        break;
                }
                break;
        }
    }
}
