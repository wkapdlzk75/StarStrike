using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : CSVManager
{
    struct ITEMSTRUCT
    {
        public int id;
        public string name;
        public int value;
    }
    List<string[]> ItemArray = new List<string[]>();
    //public void Create()
    //{
    //    MakeData("test", ItemArray);
    //}

    public override void Create()
    {
        MakeData("test", ItemArray);
    }

}
/*
public class StageManger: CSVManager
{
    struct STAGESTRUCT
    {
        public int id;
        public string name;
        public int value;
    }
    List<string[]> StageArray = new List<string[]>();
    void Create()
    {
        MakeData("stage", StageArray);
    }

}
public class TEST
{
    public void Play()
    {

        int price = ItemManager.Instance.GetItemInt(0, "pricead");
        
        int stage = StageManger.Instance.GetItemInt(1, "Stage");
        string image = StageManger.Instance.GetItemString(1, "image");

    }
}
*/