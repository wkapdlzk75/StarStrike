using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    ProductBox[] productBox;

    private void Start()
    {
        Refresh();
        // productBox[0].enhanceButton.onClick.AddListener(() => UpgradeDamage());
        // productBox[1].enhanceButton.onClick.AddListener(() => UpgradeHp());
    }

    // UI 갱신
    void Refresh()
    {
        if (productBox == null || productBox.Length == 0)
        {
            productBox = GetComponentsInChildren<ProductBox>();
        }

        var box = productBox[0];
        box.mainText.text = "공격력 강화 " + GameManager.Instance.GetEnhanceCount(GameManager.EPlayerStatus.damage) + "레벨";
        box.enhanceText.text = "업그레이드<br>100 골드"; //+ 100.ToString();

        box = productBox[1];
        box.mainText.text = "최대 체력 강화 " + GameManager.Instance.GetEnhanceCount(GameManager.EPlayerStatus.maxHp) + "레벨";
        box.enhanceText.text = "업그레이드<br>100 골드"; //+ 100.ToString();
    }

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
        if (GameManager.Instance.GetResourceAmount(GameManager.EResource.gold) >= 100)
        {
            GameManager.Instance.AddStatus(GameManager.EPlayerStatus.damage, 1);
            GameManager.Instance.RemoveResource(GameManager.EResource.gold, 100);
            UIManagerLobbyScene.Instance.UpdateGoods();
            Refresh();
            SaveLoadManager.Save();
            //SaveLoadManager.SaveGold();
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void UpgradeHp()
    {
        if (GameManager.Instance.GetResourceAmount(GameManager.EResource.gold) >= 100)
        {
            GameManager.Instance.AddStatus(GameManager.EPlayerStatus.maxHp, 1);
            GameManager.Instance.RemoveResource(GameManager.EResource.gold, 100);
            UIManagerLobbyScene.Instance.UpdateGoods();
            Refresh();
            SaveLoadManager.Save();
            //SaveLoadManager.SaveGold();
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }
}
