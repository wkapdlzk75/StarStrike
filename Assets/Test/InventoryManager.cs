using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MTSingleton<InventoryManager>
{
    public List<ItemDB> m_kItems;// 가상으로 로드 추후에 바꿈

    Dictionary<int,ItemDB> m_kData = new Dictionary<int, ItemDB> ();
    void Start()
    {
        // 로딩, 아이템 로딩 

        foreach (var item in m_kItems)
        {
            m_kData.Add (item.ID, item);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
