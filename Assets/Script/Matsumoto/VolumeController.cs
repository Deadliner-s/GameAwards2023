using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider BGM_Slider;      // BGM�̃X���C�_�[
    private Slider SE_Slider;       // SE�̃X���C�_�[

    public float BGM_Volume = 1.0f; // BGM�̉���
    public float SE_Volume = 1.0f;  // SE�̉���

    // Start is called before the first frame update
    void Start()
    {
        BGM_Slider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        SE_Slider = GameObject.Find("SESlider").GetComponent<Slider>();

        // ���݂̉��ʂ��X���C�_�[�ɓK��
        BGM_Slider.value = SoundManager.instance.BGM_volume;
        SE_Slider.value = SoundManager.instance.SE_volume;
    }

    // Update is called once per frame
    void Update()
    {
        // ���ʕύX
        BGM_Volume = BGM_Slider.value;
        SE_Volume = SE_Slider.value;
    }

    // SoundManager�Ŏg�p
    public float GetBGMVolume()
    {
        return BGM_Volume;
    }
    // SoundManager�Ŏg�p
    public float GetSEVolume()
    {
        return SE_Volume;
    }

    // OptionCursor�Ŏg�p
    public void SetBGMSlider()
    {
        BGM_Slider.Select();
    }
    // OptionCursor�Ŏg�p
    public void SetSESlider()
    {
        SE_Slider.Select();
    }

}
