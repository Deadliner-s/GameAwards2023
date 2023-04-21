using UnityEngine;

public class OptionCursor : MonoBehaviour
{
    private Myproject InputActions;

    private GameObject VolCon;          // VolumeControllerのオブジェクト
    private const int MAX_OPTION = 3;   // オプションの数
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
        VolCon = GameObject.Find("VolumeControllerObj");
    }

    // Update is called once per frame
    void Update()
    {
        // オプション選択 更新
        Selected = (Selected + MAX_OPTION) % MAX_OPTION;

        switch (Selected)
        {
            case (0):
                VolCon.GetComponent<VolumeController>().SetBGMSlider();
                break;
            case (1):
                VolCon.GetComponent<VolumeController>().SetSESlider();
                break;
        }
    }

    private void OnUp()
    {
        Selected--;
    }

    private void OnDown()
    {
        Selected++;
    }

    private void OnSelect()
    {
        switch (Selected)
        {
            case (0):
                break;
            case (1):
                break;
            case (2):
                // タイトルに戻る
                // InputActions.Disable();
                break;
        }
    }
}
