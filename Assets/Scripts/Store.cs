using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    public void UpgradeDamage()
    {
        GameManager.Instance.AddStatus(GameManager.EPlayerStatus.damage, 1);
        GameManager.Instance.RemoveResource(GameManager.EResource.gold, 100);
    }

    public void UpgradeHp()
    {
        GameManager.Instance.AddStatus(GameManager.EPlayerStatus.maxHp, 1);
        GameManager.Instance.RemoveResource(GameManager.EResource.gold, 100);
    }
}
