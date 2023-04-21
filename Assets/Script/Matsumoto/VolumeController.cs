using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private Slider BGM_Slider;      // BGM�̃X���C�_�[
    private Slider SE_Slider;       // SE�̃X���C�_�[

    // BGM SE�ȊO��I���������ɑ��̃X���C�_�[��I�����O�����߂̃X���C�_�[
    private Slider BlankSlider;

    public float BGM_Volume; // BGM�̉���
    public float SE_Volume;  // SE�̉���

    // Start is called before the first frame update
    void Start()
    {
        // �X���C�_�[���擾
        BGM_Slider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        SE_Slider = GameObject.Find("SESlider").GetComponent<Slider>();
        BlankSlider= GameObject.Find("BlankSlider").GetComponent<Slider>();

        // ���݂̉��ʂ��X���C�_�[�ɓK��
        BGM_Slider.value = SoundManager.instance.BGM_volume;
        SE_Slider.value = SoundManager.instance.SE_volume;
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̉��ʂ��X���C�_�[�ɓK��
        BGM_Slider.value = SoundManager.instance.BGM_volume;
        SE_Slider.value = SoundManager.instance.SE_volume;
    }

    // �X���C�_�[�̒l��SoundManager�ɓn��
    public float GetBGMVolume()
    {
        return BGM_Slider.value;
    }
    public float GetSEVolume()
    {
        return SE_Slider.value;
    }

    // SoundManager�Ŏg�p
    // Null�`�F�b�N
    public bool NullCheckBGMSlider()
    {
        return BGM_Slider;
    }
    public bool NullCheckSESlider()
    {
        return SE_Slider;
    }

    // OptionCursor�Ŏg�p
    // Handle��I��
    public void SetBGMSlider()
    {
        BGM_Slider.Select();
    }
    public void SetSESlider()
    {
        SE_Slider.Select();
    }
    public void SetBlankSlider()
    {
        BlankSlider.Select();
    }
}
