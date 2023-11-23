using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 키보드, 마우스, 터치를 이벤트로 오브젝에 보낼 수 있는 기능을 지원

// 인터페이스.   커서를 올려놓고 Ctrl + . 하면 구현해야 할 함수들을 자동으로 생성
public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public RectTransform lever;
    RectTransform rectTransform;

    //[SerializeField, Range(10, 150)]
    public float leverRange;

    Vector2 inputDirection;
    bool isInput;

    //public TPSCharacterController controller;
    public Player player;
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

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
        //controller.Move(Vector2.zero);
    }

    void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDirection = inputVector / leverRange;
    }

    
    void InputControlVector()
    {
        // 조이스틱 입력 벡터값을 캐릭터에 전달
        //Debug.Log(inputDirection.x + " / " + inputDirection.y);
        //controller.Move(inputDirection);
        player.Move(inputDirection);
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }
}
