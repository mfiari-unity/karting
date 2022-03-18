
public class CrashCourseLevelDifficulty : LevelDifficulty
{
    public override void setDifficulty(ObjectiveReachTargets objectiveReachTargets, LevelManager.GameLevel gameLevel, LevelManager.GameDifficulty gameDifficulty)
    {
        switch (gameLevel)
        {
            case LevelManager.GameLevel.OVAL:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        setObjectives(objectiveReachTargets, 45, 8);
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        setObjectives(objectiveReachTargets, 40, 10);
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        setObjectives(objectiveReachTargets, 30, 10);
                        break;
                }
                break;
            case LevelManager.GameLevel.ADDITIONAL:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        setObjectives(objectiveReachTargets, 60, 8);
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        setObjectives(objectiveReachTargets, 55, 10);
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        setObjectives(objectiveReachTargets, 45, 10);
                        break;
                }
                break;
            case LevelManager.GameLevel.COUNTRY:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        setObjectives(objectiveReachTargets, 45, 8);
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        setObjectives(objectiveReachTargets, 40, 10);
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        setObjectives(objectiveReachTargets, 35, 10);
                        break;
                }
                break;
            case LevelManager.GameLevel.MOUNTAIN:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        setObjectives(objectiveReachTargets, 60, 8);
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        setObjectives(objectiveReachTargets, 55, 10);
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        setObjectives(objectiveReachTargets, 50, 10);
                        break;
                }
                break;
            case LevelManager.GameLevel.WINDING:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        setObjectives(objectiveReachTargets, 45, 8);
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        setObjectives(objectiveReachTargets, 40, 10);
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        setObjectives(objectiveReachTargets, 35, 10);
                        break;
                }
                break;
            default:
                switch (gameDifficulty)
                {
                    case LevelManager.GameDifficulty.EASY:
                        setObjectives(objectiveReachTargets, 60, 8);
                        break;
                    case LevelManager.GameDifficulty.NORMAL:
                        setObjectives(objectiveReachTargets, 45, 10);
                        break;
                    case LevelManager.GameDifficulty.HARD:
                        setObjectives(objectiveReachTargets, 30, 10);
                        break;
                }
                break;
        }
    }

    private void setObjectives (ObjectiveReachTargets objectiveReachTargets, int timeInSeconds, int nbPickupToCollect)
    {
        objectiveReachTargets.totalTimeInSecs = timeInSeconds;
        if (nbPickupToCollect >= 10)
        {
            objectiveReachTargets.mustCollectAllPickups = true;
            objectiveReachTargets.title = "Knock down all Pins";
            objectiveReachTargets.displayMessage.message = "Knock down all Pins";
        } else
        {
            objectiveReachTargets.mustCollectAllPickups = false;
            objectiveReachTargets.pickupsToCompleteObjective = nbPickupToCollect;
            objectiveReachTargets.title = "Knock down " + nbPickupToCollect + " Pins";
            objectiveReachTargets.displayMessage.message = "Knock down " + nbPickupToCollect + " Pins";
        }
    }
}
