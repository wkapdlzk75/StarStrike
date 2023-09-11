using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<int> m_Items;

    List<Slot> slots = new List<Slot>();
    public Slot SlotPrefab;
    const int MAXCOUNT = 16;
    public Transform m_kdir;
    public Transform GetDir()
    {
        return m_kdir;
    }

    public void Create()
    {
        

    }

}
