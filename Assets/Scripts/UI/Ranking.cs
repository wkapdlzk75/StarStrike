using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ranking
{
    private const int MAX_RANK = 10;

    public static void SaveScore(string playerName, int score)
    {
        List<KeyValuePair<string, int>> allScores = GetScores();

        // 새로운 점수 추가
        allScores.Add(new KeyValuePair<string, int>(playerName, score));

        // 점수를 기준으로 정렬
        allScores.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));

        // 상위 10개만 저장
        for (int i = 0; i < Mathf.Min(MAX_RANK, allScores.Count); i++)
        {
            PlayerPrefs.SetString("RankingPlayer_" + (i + 1), allScores[i].Key);
            PlayerPrefs.SetInt("RankingName_" + (i + 1), allScores[i].Value);
        }

        PlayerPrefs.Save();
    }

    // 점수 불러오기
    public static List<KeyValuePair<string, int>> GetScores()
    {
        List<KeyValuePair<string, int>> allScores = new List<KeyValuePair<string, int>>();
        for (int i = 1; i <= MAX_RANK; i++)
        {
            if (PlayerPrefs.HasKey("RankingName_" + i))
            {
                string playerName = PlayerPrefs.GetString("RankingPlayer_" + i);
                int score = PlayerPrefs.GetInt("RankingName_" + i);
                allScores.Add(new KeyValuePair<string, int>(playerName, score));
            }
            else
            {
                break;
            }
        }
        return allScores;
    }
}
