using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    public Image m_kImage;
    int m_nItem;
    int m_nSlot;
    Inventory m_kParent;
    public void Create(int slotIndex,int Item,Inventory parent)
    {
        m_nItem = Item;
        m_nSlot = slotIndex;
        m_kParent = parent;
        transform.SetParent(parent.GetDir(), false);

        transform.localScale = Vector3.one;
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
        if(Item == 0)
        {
              
        }
        else
        {
            string ss = ItemManager.Instance.GetItemString(Item, "image");
            string strimage = "sprite/" + ss;
            m_kImage.sprite = Resources.Load(strimage) as Sprite;

        }



    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
