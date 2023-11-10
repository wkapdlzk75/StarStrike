using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    string max;
    string hp;
    string enhance;
    string damage;
    string upgrade;
    string gold;
    string level;

    ProductBox[] productBox;

    private void OnEnable()
    {
        Refresh();
        // productBox[0].enhanceButton.onClick.AddListener(() => UpgradeDamage());
        // productBox[1].enhanceButton.onClick.AddListener(() => UpgradeHp());
    }

    // UI 갱신
    public void Refresh()
    {
        if (productBox == null || productBox.Length == 0)
        {
            productBox = GetComponentsInChildren<ProductBox>();
        }


        damage = LocalizationManager.Instance.GetLocalizedValue("store.damage");
        max = LocalizationManager.Instance.GetLocalizedValue("store.max");
        hp = LocalizationManager.Instance.GetLocalizedValue("store.hp");
        enhance = LocalizationManager.Instance.GetLocalizedValue("store.enhance");
        level = LocalizationManager.Instance.GetLocalizedValue("store.level");
        upgrade = LocalizationManager.Instance.GetLocalizedValue("store.upgrade");
        gold = LocalizationManager.Instance.GetLocalizedValue("store.gold");

        var box = productBox[0];
        box.mainText.text = "<sprite name=\"Icon_Knife16\">" + damage + " " + enhance + " " + GameManager.Instance.GetEnhanceCount(GameManager.EPlayerStatus.damage) + " " + level;
        box.enhanceText.text = upgrade + "<br><sprite name=\"Icon_Coin\"> 100"; //+ 100.ToString();

        box = productBox[1];
        box.mainText.text = "<sprite name=\"Icon_Heart9\">" + max + " " + hp + " " + enhance + " " + GameManager.Instance.GetEnhanceCount(GameManager.EPlayerStatus.maxHp) + " " + level;
        box.enhanceText.text = upgrade + "<br><sprite name=\"Icon_Coin\"> 100 "; //+ 100.ToString();
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
