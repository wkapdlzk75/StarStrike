using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    public Image image;
    public Slider slider;


    private void Awake()
    {
        
    }

    private void Start()
    {
        StartCoroutine(LoadSound());
    }

    IEnumerator LoadSound()
    {
        yield return new WaitUntil(()=>SaveLoadManager.loadEnd);
        slider.value = GameManager.Instance.wholeVolume;
        SetRefresh(slider.value);

    }

    public void SetRefresh(float amount)
    {
        image.fillAmount = amount;

    }

    public void ValueChange()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        SetRefresh(slider.value);
        GameManager.Instance.wholeVolume = slider.value;
    }

    
}
