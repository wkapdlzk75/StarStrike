public static class SaveLoadManager
{
    public static bool loadEnd;

    // 저장
    public static void Save()
    {
        ES3File eS3File = new ES3File();
        eS3File.Save("highScore", GameManager.Instance.highScore);
        eS3File.Save("wholeVolume", GameManager.Instance.masterVolume);
        eS3File.Save("language", GameManager.Instance.language);
        eS3File.Save("gold", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
        eS3File.Save("boom", GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
        eS3File.Save("maxHp", GameManager.Instance.GetStatus(GameManager.EPlayerStatus.maxHp));
        eS3File.Save("damage", GameManager.Instance.GetStatus(GameManager.EPlayerStatus.damage));
   
        eS3File.Sync();
    }

    public static void SaveGold()
    {
        ES3.Save("gold", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
    }


    // 로드
    public static void Load()
    {
        GameManager.Instance.highScore = ES3.Load<int>("highScore",0);
        GameManager.Instance.masterVolume = ES3.Load<float>("wholeVolume", 1f);
        GameManager.Instance.language = ES3.LoadString("language","eng");
        GameManager.Instance.AddResource(GameManager.EResource.gold, ES3.Load<int>("gold",0)); // 신규유저의 경우 0골드 부터
        GameManager.Instance.AddResource(GameManager.EResource.boom, ES3.Load<int>("boom",3)); // 신규유저의 경우 3폭탄 부터
        GameManager.Instance.AddStatus(GameManager.EPlayerStatus.damage, ES3.Load<int>("damage", GameManager.initDamage));
        GameManager.Instance.AddStatus(GameManager.EPlayerStatus.maxHp, ES3.Load<int>("maxHp", GameManager.initMaxHp));

        loadEnd = true;
    }

}
