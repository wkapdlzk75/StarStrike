using System.Collections.Generic;
using UnityEngine;

public class GameManager : SSSingleton<GameManager>
{
    public int stage;       // 스테이지
    public int curScore;    // 점수
    public int highScore;   // 최대 점수
    public int inGameGold;

    public float masterVolume;
    public string language;

    public string userNickname;
    public string deviceType;

    public AudioSource audioSource;
    public bool isPlaying;
    public Player player;
    public const int initDamage = 100;
    public const int initMaxHp = 100;
    public int spawnCount = 0;

    public Dictionary<EResource, int> inventory = new Dictionary<EResource, int>();
    public Dictionary<EPlayerStatus, int> playerStatus = new Dictionary<EPlayerStatus, int>();

    public enum EPlayerStatus
    {
        maxHp,
        damage
    }

    public enum EResource
    {
        gold,
        boom
    }

    protected override void Awake()
    {
        base.Awake();
        SaveLoadManager.Load();
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        DeviceTypeChecker();
        stage = 1;
        userNickname = ES3.LoadString("Nickname", "");
    }
    void DeviceTypeChecker()
    {
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            deviceType = "Handheld";
        }
        else if (SystemInfo.deviceType == DeviceType.Desktop)
        {
            deviceType = "Desktop";
            //deviceType = "Handheld";
        }
    }


    public void PlaySound(AudioClip audioClip, float _volume = 1f)
    {
        audioSource.PlayOneShot(audioClip, _volume * masterVolume);
    }


    public int GetEnhanceCount(EPlayerStatus ePlayerStatus)
    {
        switch (ePlayerStatus)
        {
            case EPlayerStatus.maxHp:
                return (playerStatus[ePlayerStatus] - initMaxHp) / 1;   // 1씩 오를 경우 1로 나눔
            case EPlayerStatus.damage:
                return (playerStatus[ePlayerStatus] - initDamage) / 1;
            default:
                return 0;
        }
    }

    // 스탯 증가
    public void AddStatus(EPlayerStatus ePlayerStatus, int value)
    {
        if (!playerStatus.ContainsKey(ePlayerStatus))
        {
            playerStatus.Add(ePlayerStatus, 0);
        }
        playerStatus[ePlayerStatus] += value;
    }

    // 스탯 불러오기
    public int GetStatus(EPlayerStatus ePlayerStatus)
    {
        if (!playerStatus.ContainsKey(ePlayerStatus))
        {
            playerStatus.Add(ePlayerStatus, 0);
        }

        return playerStatus[ePlayerStatus];
    }

    // 재화 증가
    public void AddResource(EResource eResource, int amount)
    {
        if (!inventory.ContainsKey(eResource))
        {
            inventory.Add(eResource, 0);
        }
        inventory[eResource] += amount;
    }

    // 재화 감소
    public void RemoveResource(EResource eResource, int amount)
    {
        if (!inventory.ContainsKey(eResource))
        {
            inventory.Add(eResource, 0);
        }

        if (inventory[eResource] >= amount)
        {
            inventory[eResource] -= amount;
        }

    }

    // 재화 불러오기
    public int GetResourceAmount(EResource eResource)
    {
        if (!inventory.ContainsKey(eResource))
        {
            inventory.Add(eResource, 0);
        }

        return inventory[eResource];
    }

    // 게임시작
    public void GameStart()
    {
        isPlaying = true;
        inGameGold = 0;
    }

    // 점수 추가 및 UI 갱신
    public void AddScore(int _score)
    {
        curScore += _score;
        UIManagerGameScene.instance.UpdateScore();
    }

    // 게임 종료
    public void EndGame(bool game)
    {
        isPlaying = false;
        player.fireAble = false;
        spawnCount = 0;
        ObjectManager.Instance.AllPush();
        AddResource(EResource.gold, inGameGold);


        if (highScore < curScore)
            highScore = curScore;

        curScore = 0;
        SaveLoadManager.Save();
        UIManagerGameScene.instance.EndGame(game);
    }

}
