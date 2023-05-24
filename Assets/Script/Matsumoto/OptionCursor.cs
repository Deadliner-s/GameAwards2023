using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionCursor : MonoBehaviour
{
    //private Myproject InputActions;

    private GameObject VolCon;          // VolumeController�̃I�u�W�F�N�g

    [SerializeField]
    private GameObject BGM_Text;        // BGM�̃e�L�X�g
    [SerializeField]
    private GameObject SE_Text;         // SE�̃e�L�X�g
    [SerializeField]
    private GameObject VOICE_Text;      // Voice�̃e�L�X�g
    [SerializeField]
    private GameObject VibrationText;   // Vibration�̃e�L�X�g
    [SerializeField]
    private GameObject BackText;        // �߂�̃e�L�X�g

    private GameObject VibrationToggle; // Vibration�̃g�O��
    private bool isVibration;

    [SerializeField]
    [Header("���j���[")]
    private GameObject Menu;            // ���j���[�̃I�u�W�F�N�g

    private const int MAX_OPTION = 5;   // �I�v�V�����̐�
    private int Selected = 0;           // �I�𒆂̃I�v�V����

    void Awake()
    {
        ////InputActions = new Myproject();
        ////InputActions.Enable();
        ////InputActions.UI.Up.performed += context => OnUp();
        ////InputActions.UI.Down.performed += context => OnDown();
        ////InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        Selected = 0;

        // VolumeController�̃I�u�W�F�N�g���擾
        VolCon = transform.Find("VolumeControllerObj").gameObject;
        VibrationToggle = transform.Find("VibrationToggle").gameObject;

        // �I�v�V�����̃e�L�X�g���擾
        //BGM_Text = transform.Find("BGMText").gameObject;
        //SE_Text = transform.Find("SEText").gameObject;
        //VOICE_Text = transform.Find("VOICEText").gameObject;
        //BackText = transform.Find("BackText").gameObject;

        isVibration = VibrationManager.instance.GetisVibration();
        VibrationToggle.GetComponent<Toggle>().isOn = isVibration;
    }

    // Update is called once per frame
    void Update()
    {
        // �I�v�V�������\������Ă���ꍇ�͓��͂��󂯕t����
        if (Menu.activeSelf == false)
        {
            //InputActions.Enable();
        }


        if (InputManager.instance.OnUp())
        {
            Selected--;
            SoundManager.instance.PlaySE("Select");
        }
        if (InputManager.instance.OnDown())
        {
            Selected++;
            SoundManager.instance.PlaySE("Select");
        }

        // �I�v�V�����I�� �X�V
        Selected = (Selected + MAX_OPTION) % MAX_OPTION;

        // �I�𒆂̃I�v�V�����ɂ���ď�����ς���
        switch (Selected)
        {
            case (0):
                // BGM�̃{�����[����ύX����
                // BGM�X���C�_�[��Handle��I��
                VolCon.GetComponent<VolumeController>().SetBGMSlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;
            case (1):
                // SE�̃{�����[����ύX����
                // SE�X���C�_�[��Handle��I��
                VolCon.GetComponent<VolumeController>().SetSESlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent <TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (2):
                // �{�C�X�̃{�����[����ύX����
                // VOICE�̃X���C�_�[��Handle��I��
                VolCon.GetComponent<VolumeController>().SetVOICESlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (3):
                // �o�C�u���[�V������؂�ւ��� 186�s��
                // ���̃X���C�_�[��Select���O��(BlankSlider��Handle��I��
                VolCon.GetComponent<VolumeController>().SetBlankSlider();
                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.white;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (4):
                // �߂��I��
                // ���̃X���C�_�[��Select���O��(BlankSlider��Handle��I��
                VolCon.GetComponent <VolumeController>().SetBlankSlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.white;
                break;
        }

        if (InputManager.instance.OnSelect())
        {
            // �I�����ꂽ�I�v�V�����ɂ���ď�����ς���
            switch (Selected)
            {
                case (0):
                    break;

                case (1):
                    break;

                case (2):
                    break;

                case (3):
                    // �o�C�u���[�V�����̃I���I�t��؂�ւ���
                    VibrationToggle.GetComponent<Toggle>().isOn = !VibrationToggle.GetComponent<Toggle>().isOn;
                    isVibration = VibrationToggle.GetComponent<Toggle>().isOn;
                    VibrationManager.instance.SetisVibration(isVibration);
                    break;

                case (4):
                    // �^�C�g���ɖ߂�
                    Selected = 0;
                    this.gameObject.SetActive(false);
                    //InputActions.Disable();
                    Menu.SetActive(true);
                    SoundManager.instance.PlaySE("Decision");
                    break;
            }
        }

    }

    //private void OnUp()
    //{

    //}

    //private void OnDown()
    //{

    //}

    //private void OnSelect()
    //{

    //}
}
