using UnityEngine;

public class PauseTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject TutorialImage;       // ��������摜
    public float startTime = 1.0f;          // ��������摜��\������܂ł̎���

    private�@bool pauseFlg = false;         // �t���O
    private float time = 0.0f;              // �o�ߎ���

    // Start is called before the first frame update
    void Start()
    {
        pauseFlg = false;
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // ���ԍX�V
        time += Time.deltaTime;

        // ���Ԃ��o�߂�����摜��\�� & �ꎞ��~
        if (time > startTime && pauseFlg == false)
        {
            TutorialImage.SetActive(true);
            pauseFlg = true;
            Time.timeScale = 0.0f;
            InputManager.instance.UI_Enable();
        }

        // ��������摜���\������Ă���Ƃ��Ƀ{�^������������摜������ & �ꎞ��~����
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
