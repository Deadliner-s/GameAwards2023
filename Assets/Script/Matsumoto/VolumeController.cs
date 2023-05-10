using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider BGM_Slider;      // BGMのスライダー
    private Slider SE_Slider;       // SEのスライダー
    private Slider VOICE_Slider;     // Voiceのスライダー

    // BGM SE以外を選択した時に他のスライダーを選択を外すためのスライダー
    private Slider BlankSlider;

    public float BGM_Volume;    // BGMの音量
    public float SE_Volume;     // SEの音量
    public float VOICE_Volume;  // VOICEの音量

    // Start is called before the first frame update
    void Start()
    {
        // スライダーを取得
        BGM_Slider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        SE_Slider = GameObject.Find("SESlider").GetComponent<Slider>();
        VOICE_Slider = GameObject.Find("VOICESlider").GetComponent<Slider>();
        BlankSlider= GameObject.Find("BlankSlider").GetComponent<Slider>();

        // 現在の音量をスライダーに適応
        BGM_Slider.value = SoundManager.instance.BGM_volume;
        SE_Slider.value = SoundManager.instance.SE_volume;
        VOICE_Slider.value = SoundManager.instance.VOICE_volume;
    }

    // Update is called once per frame
    void Update()
    {
        // 現在の音量をスライダーに適応
        BGM_Slider.value = SoundManager.instance.BGM_volume;
        SE_Slider.value = SoundManager.instance.SE_volume;
        VOICE_Slider.value = SoundManager.instance.VOICE_volume;
    }

    // スライダーの値をSoundManagerに渡す
    public float GetBGMVolume()
    {
        return BGM_Slider.value;
    }
    public float GetSEVolume()
    {
        return SE_Slider.value;
    }
    public float GetVOICEVolume()
    {
        return VOICE_Slider.value;
    }

    // SoundManagerで使用
    // Nullチェック　要らないかも？1
    public bool NullCheckBGMSlider()
    {
        return BGM_Slider;
    }
    public bool NullCheckSESlider()
    {
        return SE_Slider;
    }

    // OptionCursorで使用
    // Handleを選択
    public void SetBGMSlider()
    {
        BGM_Slider.Select();
    }
    public void SetSESlider()
    {
        SE_Slider.Select();
    }
    public void SetVOICESlider()
    {
        VOICE_Slider.Select();
    }
    public void SetBlankSlider()
    {
        BlankSlider.Select();
    }
}
