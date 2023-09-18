using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 데이타 로드 

       // CSVManager.Instance.Create();
        ItemDataManager.Instance.Create();
        InventoryManager.Instance.Create();

        // UI 로드 
        InventoryUI.Instance.Open();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAddItem()
    {
        InventoryManager.Instance.AddItem(1);
        InventoryUI.Instance.UpdateData();
    }
}
