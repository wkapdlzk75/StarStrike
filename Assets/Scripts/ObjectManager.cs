using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ObjectManager : SSSingleton<ObjectManager>
{
    // 오브젝트의 이름을 키로 하고, 파일 경로와 오브젝트 풀을 값으로 갖는 딕셔너리
    private Dictionary<string, KeyValuePair<string, ObjectPool>> rangeObjectDict;

    // 파일 경로를 구성하기 위한 문자열 빌더
    private StringBuilder keyString;

    protected override void Awake()
    {
        base.Awake();
        rangeObjectDict = new Dictionary<string, KeyValuePair<string, ObjectPool>>();  // 딕셔너리 초기화
        keyString = new StringBuilder();    // 문자열 빌더 초기화
        Func();
    }

    void Func()
    {
        base.Func();
        Debug.Log("2");
    }

    // 지정된 오브젝트 이름에 대해 오브젝트 풀에서 게임 오브젝트를 가져와 주어진 액션을 실행합니다.
    public void GetRangedObject(string objectName, System.Action<GameObject> action)
    {
        // 딕셔너리에 키가 없으면 키와 값을 추가합니다.
        if (!rangeObjectDict.ContainsKey(objectName))
            this.PushKeyValue(objectName);

        // 파일 경로와 오브젝트 풀을 딕셔너리에서 가져옵니다.
        var filePath = rangeObjectDict[objectName].Key;
        var pool = rangeObjectDict[objectName].Value;

        // 오브젝트 풀에서 게임 오브젝트를 가져와 액션을 실행합니다.
        pool.VisibleObject(filePath, this.transform, action);
    }

    // 지정된 오브젝트 이름에 대해 오브젝트 풀에서 게임 오브젝트를 가져와 주어진 액션을 실행합니다.
    public GameObject GetRangedObject(string objectName)
    {
        // 딕셔너리에 키가 없으면 키와 값을 추가합니다.
        if (!rangeObjectDict.ContainsKey(objectName))
            this.PushKeyValue(objectName);

        // 파일 경로와 오브젝트 풀을 딕셔너리에서 가져옵니다.
        var filePath = rangeObjectDict[objectName].Key;
        var pool = rangeObjectDict[objectName].Value;

        // 오브젝트 풀에서 게임 오브젝트를 가져와 액션을 실행합니다.
        return pool.VisibleObject(filePath, this.transform);
    }

    // 게임 오브젝트를 오브젝트 풀에 푸시합니다. 스케일을 초기 상태로 재설정합니다.
    public void PushRangedObject(string objectName, GameObject gameObject)
    {
        if (!rangeObjectDict.ContainsKey(objectName))
            this.PushKeyValue(objectName);

        gameObject.transform.localScale = new UnityEngine.Vector3(1, 1, 1);
        rangeObjectDict[objectName].Value.InVisbileObject(gameObject);
    }

    // 지정된 이름 (objectName)을 키로 사용하여 GameObject를 관리하는 메서드
    public void PushRangedObject(string objectName, GameObject gameObject, UnityEngine.Vector3 scale)
    {
        // 해당 이름의 키가 존재하지 않으면 새로운 키/값 쌍을 추가하고, 
        // 제공된 GameObject의 크기를 조정한 후, 해당 객체를 "InvisibleObject" 상태로 설정합니다.
        if (!rangeObjectDict.ContainsKey(objectName))
            this.PushKeyValue(objectName);  // 키가 사전에 없는 경우, 새 키/값 쌍을 추가

        gameObject.transform.localScale = scale;    // GameObject의 스케일을 지정된 벡터로 설정
        rangeObjectDict[objectName].Value.InVisbileObject(gameObject);  // GameObject를 "InVisibleObject" 상태로 설정
    }

    // 현재 객체의 모든 자식 객체를 순회하며, 
    // 자식 객체가 활성 상태인 경우 비활성 상태로 설정하는 메서드입니다. 
    public void AllPush()
    {
        for (int i = 0; i < this.transform.childCount; i++) // 모든 자식 객체들을 순회
        {
            Transform child = this.transform.GetChild(i);   // i번째 자식 객체를 가져옴

            if (child.gameObject.activeSelf)    // 만약 자식 객체가 활성 상태인 경우
                child.gameObject.SetActive(false);  // 자식 객체를 비활성 상태로 설정
        }
    }

    // 오브젝트 이름과 관련된 파일 경로와 오브젝트 풀을 딕셔너리에 추가합니다.
    private void PushKeyValue(string objectName)
    {
        keyString.Clear();
        keyString.Append("Prefabs/");
        keyString.Append(objectName);

        rangeObjectDict.Add(objectName, new KeyValuePair<string, ObjectPool>(keyString.ToString(), new ObjectPool()));
    }
}
