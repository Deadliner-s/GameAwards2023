using TMPro;
using UnityEditor;
using UnityEngine;

public class OptionCursor : MonoBehaviour
{
    private Myproject InputActions;

    private GameObject VolCon;          // VolumeController�̃I�u�W�F�N�g

    private GameObject BGM_Text;        // BGM�̃e�L�X�g
    private GameObject SE_Text;         // SE�̃e�L�X�g
    private GameObject VOICE_Text;      // Voice�̃e�L�X�g
    private GameObject BackText;        // �߂�̃e�L�X�g

    [SerializeField]
    [Header("���j���[")]
    private GameObject Menu;            // ���j���[�̃I�u�W�F�N�g

    private const int MAX_OPTION = 4;   // �I�v�V�����̐�
    private int Selected = 0;           // �I�𒆂̃I�v�V����

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.UI.Up.performed += context => OnUp();
        InputActions.UI.Down.performed += context => OnDown();
        InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        Selected = 0;

        // VolumeController�̃I�u�W�F�N�g���擾
        VolCon = transform.Find("VolumeControllerObj").gameObject;

        // �I�v�V�����̃e�L�X�g���擾
        BGM_Text = transform.Find("BGMText").gameObject;
        SE_Text = transform.Find("SEText").gameObject;
        VOICE_Text = transform.Find("VOICEText").gameObject;
        BackText = transform.Find("BackText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // �I�v�V�������\������Ă���ꍇ�͓��͂��󂯕t����
        if (Menu.activeSelf == false)
        {
            InputActions.Enable();
        }

        // �I�v�V�����I�� �X�V
        Selected = (Selected + MAX_OPTION) % MAX_OPTION;

        // �I�𒆂̃I�v�V�����ɂ���ď�����ς���
        switch (Selected)
        {
            case (0):
                // �X���C�_�[��Handle��I��
                VolCon.GetComponent<VolumeController>().SetBGMSlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;
            case (1):
                // �X���C�_�[��Handle��I��
                VolCon.GetComponent<VolumeController>().SetSESlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent <TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (2):
                // �X���C�_�[��Handle��I��
                VolCon.GetComponent<VolumeController>().SetVOICESlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (3):
                // ���̃X���C�_�[��Select���O��
                VolCon.GetComponent <VolumeController>().SetBlankSlider();

                // �e�L�X�g�̐F��ύX
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.white;
                break;
        }
    }

    private void OnUp()
    {
        Selected--;
        SoundManager.instance.PlaySE("Select");
    }

    private void OnDown()
    {
        Selected++;
        SoundManager.instance.PlaySE("Select");
    }

    private void OnSelect()
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
                // �^�C�g���ɖ߂�
                Selected = 0;
                this.gameObject.SetActive(false);
                InputActions.Disable();
                Menu.SetActive(true);
                SoundManager.instance.PlaySE("Decision");
                break;
        }
    }
}
