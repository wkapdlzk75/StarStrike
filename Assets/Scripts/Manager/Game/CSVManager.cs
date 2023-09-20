using System;
using System.Collections.Generic;
using UnityEngine;

// 이 클래스는 CSV 파일을 불러와 데이터를 저장하고 조회하는 기능을 제공합니다.

public class CSVManager : SSSingleton<CSVManager>
{
    Dictionary<string,int > m_Columns  = new Dictionary<string,int>();

    List<string[]> ItemArray = new List<string[]>();

    string[] m_column;

    public void MakeData(string fileName)
    {
        TextAsset myTextAsset = Resources.Load<TextAsset>(fileName);
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

    public virtual void Create()
    {

    }

    // 컬럼에 해당하는 인덱스를 반환
    int GetColumn(string column)
    {
        return m_Columns[column];
    }

    // id와 column에 해당하는 레코드를 가져옴
    string GetValue(int id,int column)
    {
        if (id == 0) return "";
        string[] ss= ItemArray[id - 1];
        string str= ss[column];
        return str;
    }

    /*public int GetId(string name)
    {
        GetColumn(Key);
        return;
    }*/

    public string GetItemString(int id, string column)
    {
        int col = GetColumn(column);
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
