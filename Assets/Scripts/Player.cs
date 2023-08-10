using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool isTouchTop;
    public bool isTouchBottom;
    public bool isTouchRight;
    public bool isTouchLeft;
    void Update()
    {
        float moveHori = Input.GetAxisRaw("Horizontal");    // 좌우
        float moveVert = Input.GetAxisRaw("Vertical");      // 상하

        // 경계선 밖으로 나갈 경우
        if ((isTouchRight && moveHori == 1) || (isTouchLeft && moveHori == -1))
            moveHori = 0;
        if ((isTouchTop && moveVert == 1) || (isTouchBottom && moveVert == -1))
            moveVert = 0;

        Vector3 curPos = transform.position;                // 현재위치 가져옴
        Vector3 nextPos = new Vector3(moveHori, moveVert, 0) * speed * Time.deltaTime;

        transform.position = curPos + nextPos;              // 이동 반영
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 경계선 밖으로 나갈 경우
        if (collision.transform.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Top": isTouchTop = true; break;
                case "Bottom": isTouchBottom = true; break;
                case "Right": isTouchRight = true; break;
                case "Left": isTouchLeft = true; break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 경계선 안
        if (collision.transform.CompareTag("Border"))
        {
            switch (collision.gameObject.name)
            {
                case "Top": isTouchTop = false; break;
                case "Bottom": isTouchBottom = false; break;
                case "Right": isTouchRight = false; break;
                case "Left": isTouchLeft = false; break;
            }
        }
    }
}
