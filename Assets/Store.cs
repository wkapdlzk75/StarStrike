using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public void OpenStore()
    {
        gameObject.SetActive(true);
    }

    public void CloseStore()
    {
        gameObject.SetActive(false);
    }

    //속도 공격력 등등 업그레이드 기능 만들기 
}
