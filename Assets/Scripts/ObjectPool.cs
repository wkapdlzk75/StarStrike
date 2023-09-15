using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    Stack<GameObject> objectPool;   // 오브젝트 풀을 관리하는 스택

    // 생성자에서 오브젝트 풀을 초기화합니다.
    public ObjectPool()
    {
        objectPool = new Stack<GameObject>();
    }

    // 지정된 파일 경로에서 게임 오브젝트를 로드하고,
    // 부모 Transform과 선택적으로 수행할 액션을 인자로 받습니다.
    public GameObject VisibleObject(string filePath, Transform parent, System.Action<GameObject> action = null)
    {
        // Resources 폴더에서 게임 오브젝트를 로드합니다.
        GameObject gameObject1 = Resources.Load<GameObject>(filePath);

        // 오브젝트 풀에 사용 가능한 객체가 없으면 새 인스턴스를 생성합니다.
        if (objectPool.Count <= 0)
        {
            var obj = GameObject.Instantiate(gameObject1, parent);
            if (action != null) action(obj);

            return obj;
        }
        // 오브젝트 풀에 사용 가능한 객체가 있으면 재사용합니다.
        else
        {
            GameObject gameObject;
            gameObject = objectPool.Pop();
            gameObject.transform.SetParent(parent);
            gameObject.transform.localScale = Vector3.one;

            var rect = gameObject.GetComponent<RectTransform>();

            if (rect != null)
                rect.anchoredPosition3D = new Vector3(rect.anchoredPosition3D.x, rect.anchoredPosition3D.y, 0f);
            else
                gameObject.transform.localPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f);

            // 게임 오브젝트를 활성화하고 액션을 실행합니다.
            gameObject.SetActive(true);
            if (action != null) action(gameObject);

            return gameObject;
        }
    }

    // 오브젝트 풀에서 게임 오브젝트를 가져오거나, 필요할 경우 새 게임 오브젝트를 생성합니다.
    // 또한, 제공된 액션을 게임 오브젝트에 적용합니다.
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

    // 게임 오브젝트를 비활성화하고 오브젝트 풀에 푸시합니다.
    public void InVisbileObject(GameObject gameObject)
    {
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.SetActive(false);
        objectPool.Push(gameObject);
    }

    // 비활성화되어 있는 오브젝트를 풀에 푸시합니다.
    public void NotActiveFalsePush(GameObject gameObject)
    {
        gameObject.transform.rotation = Quaternion.identity;
        objectPool.Push(gameObject);
    }

    // 오브젝트 풀에 있는 모든 오브젝트를 파괴합니다.
    public void DestroyObjectes()
    {
        while (this.objectPool.Count > 0)
        {
            var obj = this.objectPool.Pop();
            Object.Destroy(obj);
        }
    }
}
