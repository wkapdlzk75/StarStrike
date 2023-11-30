using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝에 보낼 수 있는 기능을 지원

// 인터페이스.   커서를 올려놓고 Ctrl + . 하면 구현해야 할 함수들을 자동으로 생성
public class Controller : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public RectTransform lever;
    public RectTransform joystick;

    public float leverRange;

    Vector2 inputDirection;
    bool isInput;

    public Player player;

    // 드래그 시작할 때
    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    // 드래그 중일 때
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    // 드래그 끝낼을 때
    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        //player.animator.SetInteger("Input", 0);
        player.MoveHandheld(Vector2.zero);
    }

    void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - joystick.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
        inputDirection.x = RoundTowardsZero(inputDirection.x);
        inputDirection.y = RoundTowardsZero(inputDirection.y);
        //Debug.Log($"Input Direction: {inputDirection}");
    }

    int RoundTowardsZero(float number)
    {
        if (number > 0)
            return (int)Math.Ceiling(number);
        else
            return (int)Math.Floor(number);
    }

    void Update()
    {
        if (isInput)
        {
            player.MoveHandheld(inputDirection);
        }
    }
}
