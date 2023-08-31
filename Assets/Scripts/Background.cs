using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class Background : MonoBehaviour
{
    public float speed;         // 배경 이동 속도
    public int startIndex;      // 시작
    public int endIndex;        // 끝
    public Transform[] sprites; // 배경 스프라이트

    float viewHeight;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        Move();
        Scrolling();
    }

    // 아래로 이동
    void Move()
    {

        Vector3 currentPos = transform.position;
        Vector3 nextPos = Vector3.down * speed * Time.deltaTime;
        transform.position = currentPos + nextPos;
    }

    // 스프라이트 이동
    void Scrolling()
    {
        if (sprites[endIndex].position.y < -viewHeight)
        {
            // 스프라이트 재사용
            Vector3 backSpirtePos = sprites[startIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpirtePos + Vector3.up * viewHeight;

            // 인덱스 변경
            int startIndexSave = startIndex;
            startIndex = endIndex;
            endIndex = (startIndexSave - 1 == -1) ? sprites.Length - 1 : --startIndexSave;
        }
    }
}
