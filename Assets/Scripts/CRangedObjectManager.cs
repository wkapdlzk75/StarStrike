using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using Unity.VisualScripting;

using System.Numerics;

public class CRangedObjectManager : MTSingleton<CRangedObjectManager>
{
    BigInteger BigInteger1 = new BigInteger(10000000000000000000);
    BigInteger BigInteger2 = new BigInteger(10000000000000000000000000.0);

    void biginteger()
    {
        Debug.Log(BigInteger2);
        BigInteger BigInteger3 = BigInteger1 * BigInteger2;
        Debug.Log(BigInteger3);
    }
    

    private Dictionary<string, KeyValuePair<string, CObjectPool>> rangeObjectDict;
    private StringBuilder keyString;

    void Awake()
    {
        biginteger();
        
        rangeObjectDict = new Dictionary<string, KeyValuePair<string, CObjectPool>>();
        keyString = new StringBuilder();
    }

    public void GetRangedObject(string objectName, System.Action<GameObject> action)
    {
        if (!rangeObjectDict.ContainsKey(objectName))
        {
            this.PushKeyValue(objectName);
        }

        var filePath = rangeObjectDict[objectName].Key;
        var pool = rangeObjectDict[objectName].Value;

        pool.VisibleObject(filePath, this.transform, action);
    }

    public void PushRangedObject(string objectName, GameObject gameObject)
    {
        if (!rangeObjectDict.ContainsKey(objectName))
        {
            this.PushKeyValue(objectName);
        }

        gameObject.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        rangeObjectDict[objectName].Value.InVisbileObject(gameObject);
    }

    public void PushRangedObject(string objectName, GameObject gameObject, UnityEngine.Vector3 scale)
    {
        if (!rangeObjectDict.ContainsKey(objectName))
        {
            this.PushKeyValue(objectName);
        }

        gameObject.transform.localScale = scale;
        rangeObjectDict[objectName].Value.InVisbileObject(gameObject);
    }

    public void AllPush()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private void PushKeyValue(string objectName)
    {
        keyString.Clear();
        keyString.Append("Prefabs/");
        keyString.Append(objectName);

        rangeObjectDict.Add(objectName, new KeyValuePair<string, CObjectPool>(keyString.ToString(), new CObjectPool()));
    }
}