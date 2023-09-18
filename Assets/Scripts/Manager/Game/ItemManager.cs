using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : SSSingleton<ItemManager>
{
    Dictionary<int, int> itemDic = new Dictionary<int, int>();

    public void AddItem(int itemCode, int amount)
    {
        // 키 추가
        if (!itemDic.ContainsKey(itemCode))
            itemDic.Add(itemCode, 0);

        itemDic[itemCode] += amount;
    }

    public void UseItem(int itemCode, int amount = 1)
    {
        if (!itemDic.ContainsKey(itemCode))
            itemDic.Add(itemCode, 0);

        if (itemDic[itemCode] >= amount)
            itemDic[itemCode] -= amount;
    }
}
