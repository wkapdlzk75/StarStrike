using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class SaveLoadManager
{

    //static string filePath = "C:/GitHub/StarStrike/Assets/TestSave.txt";
    static string filePath = "Assets/TestSave.txt";
    static StreamWriter streamWriter;
    static StreamReader streamReader;
    static FileStream fileStream;



    public static void Save(int score)
    {
        /*if (fileStream == null)
        {
            fileStream = new FileStream(filePath,FileMode.OpenOrCreate);
        }*/

        /*if (!File.Exists(filePath))
        {
            // 파일 생성
            streamWriter = new StreamWriter(filePath);
        }*/

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

        /*if (fileStream == null)
        {
            fileStream = new FileStream(filePath, FileMode.Open);
        }*/

        streamReader = new StreamReader(filePath);

        string temp = streamReader.ReadToEnd();

        streamReader.Close();

        return temp;
    }
}
