using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Image m_kImage;
    int m_nItem;
    int m_nSlot;
    InventoryUI m_kParent;

    // 초기화
    void Awake()
    {
       
    }

    public void Create(int slotIndex, int Item, InventoryUI parent)
    {
        m_nItem = Item;
        m_nSlot = slotIndex;
        m_kParent = parent;
        transform.SetParent(parent.GetDir(), false);

        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        if (Item == 0)// 아이템이 없을 경우
        {

        }
        else
        {
            string ss = CSVManager.Instance.GetItemString(Item, "image");
            string strimage = "Sprite/" + ss;
            m_kImage.sprite = Resources.Load<Sprite>(strimage);

        }
    }
}
