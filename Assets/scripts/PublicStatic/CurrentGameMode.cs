public class CurrentGameMode
{
    public enum GameMode { Normal, Tutorial };

    private static GameMode gameMode;

    public static void SetGameMode(GameMode _gameMode)
    {
        gameMode = _gameMode;
    }

    public static GameMode GetGameMode()
    {
        return gameMode;
    }

    public static bool IsInTutorialMode()
    {
        if (gameMode == GameMode.Tutorial)
            return true;
        else
            return false;
    }

    public static bool IsInNormalMode()
    {
        if (gameMode == GameMode.Normal)
            return true;
        else
            return false;
    }
}
