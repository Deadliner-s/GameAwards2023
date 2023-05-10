using TMPro;
using UnityEditor;
using UnityEngine;

public class OptionCursor : MonoBehaviour
{
    private Myproject InputActions;

    private GameObject VolCon;          // VolumeControllerのオブジェクト

    private GameObject BGM_Text;        // BGMのテキスト
    private GameObject SE_Text;         // SEのテキスト
    private GameObject VOICE_Text;      // Voiceのテキスト
    private GameObject BackText;        // 戻るのテキスト

    [SerializeField]
    [Header("メニュー")]
    private GameObject Menu;            // メニューのオブジェクト

    private const int MAX_OPTION = 4;   // オプションの数
    private int Selected = 0;           // 選択中のオプション

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

        // VolumeControllerのオブジェクトを取得
        VolCon = transform.Find("VolumeControllerObj").gameObject;

        // オプションのテキストを取得
        BGM_Text = transform.Find("BGMText").gameObject;
        SE_Text = transform.Find("SEText").gameObject;
        VOICE_Text = transform.Find("VOICEText").gameObject;
        BackText = transform.Find("BackText").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // オプションが表示されている場合は入力を受け付ける
        if (Menu.activeSelf == false)
        {
            InputActions.Enable();
        }

        // オプション選択 更新
        Selected = (Selected + MAX_OPTION) % MAX_OPTION;

        // 選択中のオプションによって処理を変える
        switch (Selected)
        {
            case (0):
                // スライダーのHandleを選択
                VolCon.GetComponent<VolumeController>().SetBGMSlider();

                // テキストの色を変更
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;
            case (1):
                // スライダーのHandleを選択
                VolCon.GetComponent<VolumeController>().SetSESlider();

                // テキストの色を変更
                BGM_Text.GetComponent <TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (2):
                // スライダーのHandleを選択
                VolCon.GetComponent<VolumeController>().SetVOICESlider();

                // テキストの色を変更
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (3):
                // 他のスライダーのSelectを外す
                VolCon.GetComponent <VolumeController>().SetBlankSlider();

                // テキストの色を変更
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
        // 選択されたオプションによって処理を変える
        switch (Selected)
        {
            case (0):
                break;

            case (1):
                break;

            case (2):
                break;

            case (3):
                // タイトルに戻る
                Selected = 0;
                this.gameObject.SetActive(false);
                InputActions.Disable();
                Menu.SetActive(true);
                SoundManager.instance.PlaySE("Decision");
                break;
        }
    }
}
