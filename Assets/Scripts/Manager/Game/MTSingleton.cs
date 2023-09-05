/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSSingleton<T> : MonoBehaviour
{
    static private T _instance;
    public static T instance
    {
        get
        {
            return _instance;
        }

    }
    private void Awake()
    {
        _instance = GetComponent<T>();
    }

}*/


using UnityEngine;

public class MTSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                }
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
