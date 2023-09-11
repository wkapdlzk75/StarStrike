using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class CObjectPool
{
    Stack<GameObject> objectPool;   // NULL
    public CObjectPool()
    {
        // 변수 초기화를 위한 생성자
        objectPool = new Stack<GameObject>(); // 0
    }

    public GameObject VisibleObject(string filePath, Transform parent, System.Action<GameObject> action = null)
    {

        GameObject gameObject1 = Resources.Load<GameObject>(filePath);

        if (objectPool.Count <= 0)
        {
            var obj = GameObject.Instantiate(gameObject1, parent);
            if (action != null) action(obj);

            return obj;
        }

        else
        {
            GameObject gameObject;
            gameObject = objectPool.Pop();
            gameObject.transform.SetParent(parent);
            gameObject.transform.localScale = Vector3.one;

            var rect = gameObject.GetComponent<RectTransform>();
            if (rect != null)
            {
                rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, rect.anchoredPosition3D.y, 0f);
            }

            else
            {
                gameObject.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f);
            }
            gameObject.SetActive(true);
            if (action != null) action(gameObject);

            return gameObject;
        }
    }

    public GameObject VisibleObjectFirstSetPosition(string filePath, Transform parent, Vector3 position, System.Action<GameObject> action = null)
    {
        GameObject gameObject1 = Resources.Load<GameObject>(filePath);

        if (objectPool.Count <= 0)
        {
            var obj = GameObject.Instantiate(gameObject1, position, Quaternion.identity, parent);
            obj.transform.position = position;
            obj.SetActive(true);
            if (action != null) action(obj);

            return obj;
        }

        else
        {
            GameObject gameObject;
            gameObject = objectPool.Pop();
            gameObject.transform.SetParent(parent);
            gameObject.transform.position = position;
            gameObject.SetActive(true);
            if (action != null) action(gameObject);
            return gameObject;
        }

    }

    public void InVisbileObject(GameObject gameObject)
    {
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
        objectPool.Push(gameObject);
    }

    public void NotActiveFalsePush(GameObject gameObject)
    {
        gameObject.transform.rotation = Quaternion.identity;
        objectPool.Push(gameObject);
    }

    public void DestroyObjectes()
    {
        while (this.objectPool.Count > 0)
        {
            var obj = this.objectPool.Pop();
            Object.Destroy(obj);
        }
    }
}