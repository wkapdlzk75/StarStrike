using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoadManager
{
    public static bool loadEnd;
    /*
    //static string filePath = "C:/GitHub/StarStrike/Assets/TestSave.txt";
    static string filePath = "Assets/TestSave.txt";
    static StreamWriter streamWriter;
    static StreamReader streamReader;
    static FileStream fileStream;



    public static void Save(int score)
    {
        if (fileStream == null)
        {
            fileStream = new FileStream(filePath,FileMode.OpenOrCreate);
        }

        //if (!File.Exists(filePath))
        //{
        //     //파일 생성
        //    streamWriter = new StreamWriter(filePath);
        //}

        streamWriter = new StreamWriter(filePath);
        streamWriter.WriteLine(score);

        // 파일 닫기
        streamWriter.Flush();
        streamWriter.Close();

        Debug.Log("저장완료" + score);
    }

    public static string Load()
    {
        if (!File.Exists(filePath))
        {
            return null;
        }

        //if (fileStream == null)
        //{
        //    fileStream = new FileStream(filePath, FileMode.Open);
        //}

        streamReader = new StreamReader(filePath);

        string temp = streamReader.ReadToEnd();

        streamReader.Close();

        return temp;
    }*/


    // 저장

    public static void Save()
    {
        ES3.Save("highScore", GameManager.Instance.highScore);
        ES3.Save("gold", GameManager.Instance.GetResourceAmount(GameManager.EResource.gold));
        ES3.Save("boom", GameManager.Instance.GetResourceAmount(GameManager.EResource.boom));
        ES3.Save("maxHp", GameManager.Instance.GetStatus(GameManager.EPlayerStatus.maxHp));
        ES3.Save("damage", GameManager.Instance.GetStatus(GameManager.EPlayerStatus.damage));
    }


    // 로드
    public static void Load()
    {
        GameManager.Instance.highScore = ES3.Load<int>("highScore",0);
        GameManager.Instance.AddResource(GameManager.EResource.gold, ES3.Load<int>("gold",0)); // 신규유저의 경우 0골드 부터
        GameManager.Instance.AddResource(GameManager.EResource.boom, ES3.Load<int>("boom",3)); // 신규유저의 경우 3폭탄 부터
        loadEnd = true;
    }



    // 주기적 저장

}
