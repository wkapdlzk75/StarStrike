using System.Collections.Generic;
using UnityEngine;

public class MobDataManager : SSSingleton<MobDataManager>
{
    public struct st_MobData
    {
        public int key;
        public string name;
        public int max_Hp;
        public int damage;
        public float speed;
        public float firingInterval;
        public int score;
    }

    public Dictionary<string, st_MobData> mobDataDic = new Dictionary<string, st_MobData>();


    public void CreateMobData()
    {
        //if (mobDataDic.Count > 0) { mobDataDic.Clear(); }
        if (mobDataDic.Count > 0)
        {
            return;
        }

        var data = CSVManager.Read("MobData");

        for (int i = 0; i < data.Count; i++)
        {
            var mobData = new st_MobData();
            // mobData.key = (int)data[i]["Key"];
            // mobData.key = int.Parse(data[i]["Key"].ToString());

            if (!int.TryParse(data[i]["Key"].ToString(), out mobData.key))   // 성공 했을 때에만 값이 들어감
            {
                // Debug.Log("몹 키 파싱 실패");
                Debug.LogError("몹 키 파싱 실패"); // 심각한 상황인 경우
                // Debug.LogWarning(); // 경미한 상황인 경우
                continue;
            }

            mobData.name = data[i]["Name"].ToString();

            if (!int.TryParse(data[i]["MaxHp"].ToString(), out mobData.max_Hp))
            {
                continue;
            }

            if (!int.TryParse(data[i]["Damage"].ToString(), out mobData.damage))
            {
                continue;
            }

            if (!float.TryParse(data[i]["Speed"].ToString(), out mobData.speed))
            {
                continue;
            }

            if (!float.TryParse(data[i]["FiringInterval"].ToString(), out mobData.firingInterval))
            {
                continue;
            }

            if (!int.TryParse(data[i]["Score"].ToString(), out mobData.score))
            {
                continue;
            }

            mobDataDic.Add(mobData.name, mobData);

            // float a = mobDataDic[3].speed;
        }
    }
}
