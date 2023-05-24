using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionCursor : MonoBehaviour
{
    //private Myproject InputActions;
    
    private GameObject VolCon;          // VolumeControllerのオブジェクト

    [SerializeField]
    private GameObject Window;
    [SerializeField]
    private GameObject BGM_Text;        // BGMのテキスト
    [SerializeField]
    private GameObject SE_Text;         // SEのテキスト
    [SerializeField]
    private GameObject VOICE_Text;      // Voiceのテキスト
    [SerializeField]
    private GameObject VibrationText;   // Vibrationのテキスト
    [SerializeField]
    private GameObject BackText;        // 戻るのテキスト

    private GameObject VibrationToggle; // Vibrationのトグル
    private bool isVibration;

    [SerializeField]
    [Header("メニュー")]
    private GameObject Menu;            // メニューのオブジェクト

    private const int MAX_OPTION = 5;   // オプションの数
    private int Selected = 0;           // 選択中のオプション

    private float Max_Height = 14.30278f;

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

        // VolumeControllerのオブジェクトを取得
        VolCon = transform.Find("VolumeControllerObj").gameObject;
        VibrationToggle = transform.Find("VibrationToggle").gameObject;
        //VibrationToggle = transform.FindChild("VolumeControllerObj").gameObject;

        // オプションのテキストを取得
        //BGM_Text = transform.Find("BGMText").gameObject;
        //SE_Text = transform.Find("SEText").gameObject;
        //VOICE_Text = transform.Find("VOICEText").gameObject;
        //BackText = transform.Find("BackText").gameObject;

        isVibration = VibrationManager.instance.GetisVibration();
        VibrationToggle.GetComponent<Toggle>().isOn = isVibration;
        VibrationToggle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // オプションが表示されている場合は入力を受け付ける
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

        // オプション選択 更新
        Selected = (Selected + MAX_OPTION) % MAX_OPTION;

        // 選択中のオプションによって処理を変える
        switch (Selected)
        {
            case (0):
                // BGMのボリュームを変更する
                // BGMスライダーのHandleを選択
                VolCon.GetComponent<VolumeController>().SetBGMSlider();

                // テキストの色を変更
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;
            case (1):
                // SEのボリュームを変更する
                // SEスライダーのHandleを選択
                VolCon.GetComponent<VolumeController>().SetSESlider();

                // テキストの色を変更
                BGM_Text.GetComponent <TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (2):
                // ボイスのボリュームを変更する
                // VOICEのスライダーのHandleを選択
                VolCon.GetComponent<VolumeController>().SetVOICESlider();

                // テキストの色を変更
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (3):
                // バイブレーションを切り替える 186行目
                // 他のスライダーのSelectを外す(BlankSliderのHandleを選択
                VolCon.GetComponent<VolumeController>().SetBlankSlider();
                // テキストの色を変更
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.white;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.black;
                break;

            case (4):
                // 戻るを選択
                // 他のスライダーのSelectを外す(BlankSliderのHandleを選択
                VolCon.GetComponent <VolumeController>().SetBlankSlider();

                // テキストの色を変更
                BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
                VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
                BackText.GetComponent<TextMeshProUGUI>().color = Color.white;
                break;
        }

        if (InputManager.instance.OnSelect())
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
                    // バイブレーションのオンオフを切り替える
                    VibrationToggle.GetComponent<Toggle>().isOn = !VibrationToggle.GetComponent<Toggle>().isOn;
                    isVibration = VibrationToggle.GetComponent<Toggle>().isOn;
                    VibrationManager.instance.SetisVibration(isVibration);
                    break;

                case (4):
                    // タイトルに戻る
                    Selected = 0;
                    SoundManager.instance.PlaySE("Decision");
                    StartCoroutine("WindowScaleDown");
                    break;
            }
        }

    }
    private void OnEnable()
    {
        // ウィンドウ表示
        Window.transform.localScale = new Vector3(
            Window.transform.localScale.x,
            0.0f,
            Window.transform.localScale.z
            );
        StartCoroutine("WindowScaleUp");
    }
    IEnumerator WindowScaleUp()
    {
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y += Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }
        Selected = 0;
        // BGMのボリュームを変更する
        // BGMスライダーのHandleを選択
        VolCon.GetComponent<VolumeController>().SetBGMSlider();

        // テキストの色を変更
        BGM_Text.GetComponent<TextMeshProUGUI>().color = Color.white;
        SE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
        VOICE_Text.GetComponent<TextMeshProUGUI>().color = Color.black;
        VibrationText.GetComponent<TextMeshProUGUI>().color = Color.black;
        BackText.GetComponent<TextMeshProUGUI>().color = Color.black;

        VibrationToggle.SetActive(true);
    }
    IEnumerator WindowScaleDown()
    {
        VibrationToggle.SetActive(false);
        for (float i = 1; i < 2; i += 0.1f)
        {
            Vector3 scale = Window.transform.localScale;
            scale.y -= Max_Height * 0.1f;
            Window.transform.localScale = scale;
            yield return new WaitForSeconds(0.01f);
        }
        this.gameObject.SetActive(false);
        Menu.SetActive(true);
    }
}
