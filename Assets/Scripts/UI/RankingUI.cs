using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RankingUI : MonoBehaviour
{
    RankingBox[] m_RankingBox;
    public TextMeshProUGUI m_TextMeshProUGUI;


    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        m_RankingBox = GetComponentsInChildren<RankingBox>(true);   // true 로 하면 꺼진 오브젝트도 가져옴
    }

    private void OnEnable()
    {
        RefreshRank();
    }

    public void RefreshLanguage(string lang)
    {
        //LocalizationManager.Instance.LoadLocalizedText(lang);
        string greeting = LocalizationManager.Instance.GetLocalizedValue("greeting");
    }

    public void RefreshRank()
    {
        var rankData = Ranking.GetScores();
        if (rankData.Count > 0)
        {
            m_TextMeshProUGUI.gameObject.SetActive(false);
            for (int i = 0; i < m_RankingBox.Length; i++)
            {
                if (i < rankData.Count)
                {
                    m_RankingBox[i].rankText.text = (i + 1).ToString() + "위";
                    m_RankingBox[i].nameText.text = rankData[i].Key;
                    m_RankingBox[i].scoreText.text = rankData[i].Value.ToString("N0") + "점";
                    m_RankingBox[i].gameObject.SetActive(true);
                }
                else
                {

                    m_RankingBox[i].gameObject.SetActive(false);
                }

            }
        }
        else
        {
            m_TextMeshProUGUI.gameObject.SetActive(true);
            for (int i = 0; i < m_RankingBox.Length; i++)
                m_RankingBox[i].gameObject.SetActive(false);
        }
    }

    public void OpenRanking()
    {
        gameObject.SetActive(true);
    }

    public void CloseRanking()
    {
        gameObject.SetActive(false);
    }
}
