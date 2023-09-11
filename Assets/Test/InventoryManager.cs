using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MTSingleton<InventoryManager>
{
    public List<ItemDB> m_kItems;// 가상으로 로드 추후에 바꿈

    public List<int> m_kData = new List<int> ();
    public void Create()
    {
        // 로딩, 아이템 로딩 

        for(int i = 0; i < m_kItems.Count; i++) 
        {
            int id = m_kItems[i].ID;
            m_kData.Add (id );
        }

    }
    
    public void AddItem(int id)
    {
        //return ItemManager.instance.GetItem(id);    

        m_kData.Add(id);
    }
    
}
