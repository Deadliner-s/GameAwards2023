using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider BGM_Slider;
    private Slider SE_Slider;

    public float BGM_Volume = 1.0f;
    public float SE_Volume = 1.0f;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        BGM_Slider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        SE_Slider = GameObject.Find("SESlider").GetComponent<Slider>();

        BGM_Slider.value = SoundManager.instance.BGM_volume;
        SE_Slider.value = SoundManager.instance.SE_volume;
    }

    // Update is called once per frame
    void Update()
    {
        BGM_Volume = BGM_Slider.value;
        SE_Volume = SE_Slider.value;
    }

    public float GetBGMVolume()
    {
        return BGM_Volume;
    }
    public float GetSEVolume()
    {
        return SE_Volume;
    }

}
