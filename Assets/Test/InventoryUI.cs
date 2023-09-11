using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MTSingleton<InventoryUI>
{

    public GameObject m_gvisible;
    public Slot m_SlotPrefab;
    public GameObject m_gDir;
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
    public Transform GetDir()
    {
        return m_gDir.transform;
    }
    void UpdateData()
    {
        m_Slots = new List<Slot>();
        // 인벤토리 
        int cnt = 0;
        foreach (var v in InventoryManager.Instance.m_kData)
        {
            Slot ss = Instantiate(m_SlotPrefab);
            ss.Create(cnt,v,this);
            cnt++;
        }

    }

}