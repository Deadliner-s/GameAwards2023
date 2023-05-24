using UnityEngine;

public class PauseTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject TutorialImage;       // 操作説明画像
    public float startTime = 1.0f;          // 操作説明画像を表示するまでの時間

    private　bool pauseFlg = false;         // フラグ
    private float time = 0.0f;              // 経過時間

    // Start is called before the first frame update
    void Start()
    {
        pauseFlg = false;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // 時間更新
        time += Time.deltaTime;

        // 時間が経過したら画像を表示 & 一時停止
        if (time > startTime && pauseFlg == false)
        {
            TutorialImage.SetActive(true);
            pauseFlg = true;
            Time.timeScale = 0.0f;
            InputManager.instance.UI_Enable();
        }

        // 操作説明画像が表示されているときにボタンを押したら画像を消す & 一時停止解除
        if (pauseFlg == true)
        {
            if (InputManager.instance.OnSelect())
            {
                InputManager.instance.UI_Disable();
                Time.timeScale = 1.0f;
                TutorialImage.SetActive(false);
            }
        }

    }

}
