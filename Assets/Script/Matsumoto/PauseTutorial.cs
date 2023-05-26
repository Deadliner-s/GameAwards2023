using UnityEngine;

public class PauseTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject TutorialWindow;      // ��������摜
    [SerializeField]
    private GameObject PushWindow;
    public float startDisplayTime = 1.0f;   // ��������摜��\������܂ł̎���
    public float targetScaleY;              // �ڕW�̃X�P�[��
    public float animationDuration = 1.0f;  // �A�j���[�V�����̎���

    private�@bool pauseFlg = false;         // �t���O
    private float time = 0.0f;              // �o�ߎ���

    [SerializeField]
    private float changetimer = 0.0f;
    [SerializeField]
    private float changetime = 0.7f;
    private bool pushActive = true;

    private float initialScaleY;            // �����̃X�P�[��
    private float elapsedTime = 0.0f;       // �o�ߎ���

    private bool isOpened = false;
    private bool startClose = false;

    // Start is called before the first frame update
    void Start()
    {
        // ������
        pauseFlg = false;
        isOpened = false;
        startClose = false;
        time = 0.0f;
        changetimer = 0.0f;
        elapsedTime = 0.0f;
        pushActive = true;

        // �����̃X�P�[����ۑ�
        initialScaleY = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        // ���ԍX�V
        time += Time.deltaTime;
        // ���Ԃ��o�߂�����摜��\�� & �ꎞ��~
        if (time > startDisplayTime && pauseFlg == false)
        {
            TutorialWindow.SetActive(true);
            PushWindow.SetActive(true);
            pauseFlg = true;
            Time.timeScale = 0.0f;
        }

        // �ꎞ��~��
        if (pauseFlg == true)
        {
            if (elapsedTime < animationDuration && isOpened == false)
            {
                elapsedTime += Time.unscaledDeltaTime;
                float t = Mathf.Clamp01(elapsedTime / animationDuration);
                // �`���[�g���A���E�B���h�E
                Vector3 newScale = TutorialWindow.transform.localScale;
                newScale.y = Mathf.Lerp(initialScaleY, targetScaleY, t);
                TutorialWindow.transform.localScale = newScale;
                // �v�b�V���E�B���h�E
                Vector3 newScale1 = PushWindow.transform.localScale;
                newScale1.y = Mathf.Lerp(initialScaleY, targetScaleY, t);
                PushWindow.transform.localScale = newScale1;
            }
            // �E�B���h�E���J���I����Ă�����͂��󂯕t����
            if (TutorialWindow.transform.localScale.y == targetScaleY && isOpened == false)
            {
                InputManager.instance.UI_Enable();
                isOpened = true;
            }

            // �v�b�V���{�^���p�J�p�J����
            if (isOpened == true && startClose == false)
            {
                changetimer += Time.unscaledDeltaTime;
                if (changetimer >= changetime)
                {
                    // �E�B���h�E�\�����t�ɂ���
                    if (pushActive)
                    {
                        PushWindow.SetActive(false);
                        pushActive = false;
                        changetimer = 0.0f;
                    }
                    else
                    {
                        PushWindow.SetActive(true);
                        pushActive = true;
                        changetimer = 0.0f;
                    }
                }
            }

            // �{�^���������ꂽ��E�B���h�E�����
            if (InputManager.instance.OnSelect()  && isOpened == true && startClose == false)
            {
                startClose = true;
                elapsedTime = 0.0f;
                InputManager.instance.UI_Disable();
            }

            // �E�B���h�E������A�j���[�V����
            if (startClose == true)
            {
                if (elapsedTime < animationDuration)
                {
                    elapsedTime += Time.unscaledDeltaTime;
                    float t = Mathf.Clamp01(elapsedTime / animationDuration);
                    // �`���[�g���A���E�B���h�E
                    Vector3 newScale = TutorialWindow.transform.localScale;
                    newScale.y = Mathf.Lerp(targetScaleY, 0.0f, t);
                    TutorialWindow.transform.localScale = newScale;
                    // �v�b�V���E�B���h�E
                    Vector3 newScale1 = PushWindow.transform.localScale;
                    newScale1.y = Mathf.Lerp(targetScaleY, 0.0f, t);
                    PushWindow.transform.localScale = newScale1;
                }
            }
            // �E�B���h�E��������ꎞ��~����
            if (TutorialWindow.transform.localScale.y == 0.0f && startClose == true)
            {
                Time.timeScale = 1.0f;
                TutorialWindow.SetActive(false);
                PushWindow.SetActive(false);
                enabled = false;
            }
        }
    }

}
