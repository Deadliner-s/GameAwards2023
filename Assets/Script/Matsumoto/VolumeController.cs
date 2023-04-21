using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider BGM_Slider;      // BGMのスライダー
    private Slider SE_Slider;       // SEのスライダー

    public float BGM_Volume = 1.0f; // BGMの音量
    public float SE_Volume = 1.0f;  // SEの音量

    // Start is called before the first frame update
    void Start()
    {
        BGM_Slider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        SE_Slider = GameObject.Find("SESlider").GetComponent<Slider>();

        // 現在の音量をスライダーに適応
        BGM_Slider.value = SoundManager.instance.BGM_volume;
        SE_Slider.value = SoundManager.instance.SE_volume;
    }

    // Update is called once per frame
    void Update()
    {
        // 音量変更
        BGM_Volume = BGM_Slider.value;
        SE_Volume = SE_Slider.value;
    }

    // SoundManagerで使用
    public float GetBGMVolume()
    {
        return BGM_Volume;
    }
    // SoundManagerで使用
    public float GetSEVolume()
    {
        return SE_Volume;
    }

    // OptionCursorで使用
    public void SetBGMSlider()
    {
        BGM_Slider.Select();
    }
    // OptionCursorで使用
    public void SetSESlider()
    {
        SE_Slider.Select();
    }

}
