using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Inventory m_kInventory;
    void Start()
    {
        CSVManager.Instance.Create();
        m_kInventory.Create();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
