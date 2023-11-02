using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Icons;

public class LocalizationManager : SSSingleton<LocalizationManager>
{
    private Dictionary<string, object> localizedText;
    private string currentLanguage;

    public void LoadLocalizedText(string language)
    {
        string path = "Language/" + language;
        localizedText = new Dictionary<string, object>();
        TextAsset jsonFile = Resources.Load<TextAsset>(path);

        if (jsonFile == null)
        {
            Debug.LogError("Cannot find file: " + language);
            return;
        }

        Dictionary<string, object> data = MiniJSON.Json.Deserialize(jsonFile.text) as Dictionary<string, object>;
        localizedText = data;
        currentLanguage = language;
    }

    public string GetLocalizedValue(string key)
    {
        string[] keys = key.Split('.');
        Dictionary<string, object> currentDict = localizedText;
        object currentValue = null;

        foreach (string k in keys)
        {
            if (currentDict.TryGetValue(k, out currentValue))
            {
                if (currentValue is Dictionary<string, object>)
                {
                    currentDict = (Dictionary<string, object>)currentValue;
                }
            }
            else
            {
                return "Not Found";
            }
        }

        return currentValue.ToString();
    }

    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }
}
