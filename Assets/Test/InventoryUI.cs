using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MTSingleton<InventoryUI>
{

    public GameObject m_gvisible;
    public Slot m_SlotPrefab;

    List<Slot> m_Slots;
    public void Open()
    {
        m_gvisible.SetActive(true);
        UpdateData();
    }
    public void Close()
    {
        m_gvisible.SetActive(false);
    }
    void UpdateData()
    {
        m_Slots = new List<Slot>();
        // 인벤토리 
        int cnt = InventoryManager.Instance.m_kData.Count;

        foreach ()
        //for (int i = 0; i < cnt; i++)
        //{
        //    Slot ss = Instantiate(m_SlotPrefab);
        //    ss.Create(i, InventoryManager.Instance.m_kData[i].ID);
        //    m_Slots.Add(ss);
        //}


    }

}
