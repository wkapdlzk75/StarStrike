using Newtonsoft.Json;
using System.IO;
using UnityEngine;

public class JsonTest : MonoBehaviour
{
    public TextAsset JsonFile;//Variable that contains json data
    private void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        var result = JsonConvert.DeserializeObject<TestJsonClass[]>(JsonFile.text);//convert to usable data
        foreach (var i in result)
        {
            Debug.Log($"Id : {i.Id}, Value : {i.Value}");//Print data;
        }
    }

    void SaveData(TestJsonClass[] data)
    {
        /*
         * -1 은 무제한 10000
        Application.targetFrameRate = 120;  // 120 이하로 고정
        Application.targetFrameRate = 60;  // 60 이하로 고정
        Application.targetFrameRate = 30;  // 30 이하로 고정
        */

        string jsonData = JsonConvert.SerializeObject(data, Formatting.Indented); // Convert data to json string with nice formatting
        string path = Path.Combine(Application.persistentDataPath, "output.json"); // Save to application's persistent data path
        File.WriteAllText(path, jsonData);
        Debug.Log($"Data saved to: {path}");
    }

    public void Save(TestJsonClass[] data)
    {
        SaveData(data);
    }
}