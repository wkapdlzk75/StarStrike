using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVManager : MTSingleton<CSVManager>
{
       
    //public TextAsset m_kCsv;
    struct ITEMSTRUCT
    {
        public int id;
        public string name;
        public int value;
    }

    //List<ITEMSTRUCT> m_kItem = new List<ITEMSTRUCT>();
    //Dictionary<int, ITEMSTRUCT> m_kItem  = new Dictionary<int, ITEMSTRUCT>();

    Dictionary<string,int > m_Columns  = new Dictionary<string,int>();

    List<string[]> ItemArray = new List<string[]>();

    string[] m_column;// = record[0].Split(",");
    public void Create()
    {
        TextAsset myTextAsset = Resources.Load<TextAsset>("test.txt");
        if (myTextAsset != null)
        {
            string[] record = myTextAsset.text.Split("\n");
            m_column= record[0].Split(",");
            for(int i = 0; i < m_column.Length;i++ )
            {
                m_Columns[m_column[i]] = i;
            }
            for (int i = 1; i < record.Length; i++)
            {
                string[] a = record[i].Split(",");
                ItemArray.Add(a);
            }
            Debug.Log(myTextAsset.text);
        }
        else
        {
            Debug.LogError("TextAsset not found!");
        }

    }

    int GetComumn(string column)
    {
        return m_Columns[column];
    }
    string GetValue(int id,int col)
    {
        return ItemArray[id][col];   
    }
    public string GetItemString(int id, string column)
    {
        int col = GetComumn(column);
        string value = GetValue(id, col);
        return value;

    }
    public int GetItemInt(int id, string column)
    {
        string str = GetItemString(id, column);

        return Convert.ToInt32(str);
    }
    public float GetItemFloat(int id, string column)
    {
        string str = GetItemString(id, column);
        float value;
        float.TryParse(str, out value);
        return value;
    }

}


