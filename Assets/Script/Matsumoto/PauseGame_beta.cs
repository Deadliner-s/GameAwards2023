using TMPro;
using UnityEngine;

public class PauseGame_beta : MonoBehaviour
{
    private bool pauseFlg = false;                                              // �|�[�Y����

    private const int MAX_PAUSEMENU = 2;                                        // �|�[�Y���j���[�̐�
    private int Selected = 0;                                                   // �I�𒆂̃|�[�Y���j���[

    [SerializeField]
    private GameObject pauseWindow;                                             // �|�[�Y��ʂ̃I�u�W�F�N�g
    private GameObject pauseMenu;                                               // �|�[�Y���j���[�̃I�u�W�F�N�g
    private GameObject[] text = new GameObject[MAX_PAUSEMENU];                  // �e�L�X�g�̃I�u�W�F�N�g

    private GameObject fade;                                                    // �t�F�[�h�̃I�u�W�F�N�g

    private bool isAnimating = false;                                           // �A�j���[�V��������
    public float animationTime = 0.1f;                                          // �A�j���[�V�����ɂ����鎞��
    public float targetScaleY = 8.0f;
    private float scaleY = 8.0f;                                                // �ڕW�̃X�P�[���l
    private float initialScaleY;                                                // �����̃X�P�[���l
    private float animationStartTime;                                           // �A�j���[�V�����J�n����

    // Start is called before the first frame update
    void Start()
    {
        pauseFlg = false;
        Selected = 0;

        // �|�[�Y��ʂ̃I�u�W�F�N�g���擾
        pauseMenu = pauseWindow.transform.Find("PauseMenu").gameObject;
        // �|�[�Y���j���[�̃e�L�X�g���擾
        int cnt = pauseMenu.transform.childCount;
        for (int i = 0; i < cnt; i++)
        {
            text[i] = pauseMenu.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fade == null)
        {
            fade = GameObject.Find("Fade");
        }

        if (Time.timeScale == 1.0f)
        {
            if (InputManager.instance.OnPause() && pauseFlg == false)
            {
                // �|�[�Y�J�n
                PauseStart();
                Selected = 0;
            }
        }
        else if (InputManager.instance.OnBack() && pauseFlg == true)
        {
            // �|�[�Y�I��
            PauseEndBack();
            // �^�C���X�P�[����1�ɂ���
            Time.timeScale = 1.0f;
            SoundManager.instance.PlaySE("Decision");
        }
        else if (InputManager.instance.OnStart() && pauseFlg == true)
        {
            // �|�[�Y�I��
            PauseEndBack();
            // �^�C���X�P�[����1�ɂ���
            Time.timeScale = 1.0f;
            SoundManager.instance.PlaySE("Decision");
        }

        if (pauseFlg == true)
        {
            // �J�[�\���ړ�
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
            Selected = (Selected + MAX_PAUSEMENU) % MAX_PAUSEMENU;

            // �I�𒆂̃��j���[�̐F��ς���
            switch (Selected)
            {
                case 0:
                    // BACK
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.white;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
                case 1:
                    // RETRY
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.white;
                    break;
            }

            if (InputManager.instance.OnSelect())
            {
                switch (Selected)
                {
                    case 0:
                        // BACK
                        PauseEndBack();
                        // �^�C���X�P�[����1�ɂ���
                        Time.timeScale = 1.0f;
                        SoundManager.instance.PlaySE("Decision");
                        break;
                    case 1:
                        // RETURN TITLE
                        // �X�e�[�W1
                        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
                        {
                            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE1,
                                SceneLoadStartUnload.SCENE_NAME.E_TITLE));
                        }
                        // �X�e�[�W2
                        else if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
                        {
                            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE2,
                                SceneLoadStartUnload.SCENE_NAME.E_TITLE));
                        }
                        // �X�e�[�W3
                        else if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                        {
                            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE3,
                                SceneLoadStartUnload.SCENE_NAME.E_TITLE));
                        }
                        SoundManager.instance.PlaySE("Decision");
                        PauseEndBack();
                        // Fade.cs�Ń^�C���X�P�[����1.0f�A�Đ����̃T�E���h���~�߂�
                        break;
                }
            }
        }
        // �A�j���[�V�������̏���
        if (isAnimating)
        {
            float elapsedTime = Time.unscaledTime - animationStartTime;
            float t = Mathf.Clamp01(elapsedTime / animationTime);

            // �X�P�[���ύX
            float scale = Mathf.Lerp(initialScaleY, scaleY, t);
            pauseWindow.transform.localScale = new Vector3(pauseWindow.transform.localScale.x, scale, pauseMenu.transform.localScale.z);

            // �A�j���[�V�����I�����̏���
            if (t >= 1.0f)
            {
                isAnimating = false;

                // �E�B���h�E���J���I����Ă�����͂��󂯕t����
                if (scaleY == 8.0f && pauseFlg == true)
                {
                    InputManager.instance.UI_Enable();
                }
                // �E�B���h�E�����I����Ă�����͂��󂯕t����
                if (scaleY == 0.0f && pauseFlg == false)
                {
                    pauseWindow.SetActive(false);
                    InputManager.instance.Player_Enable();
                }
            }
        }
    }

    // �|�[�Y�̊J�n����
    private void PauseStart()
    {
        pauseFlg = true;
        pauseWindow.SetActive(true);

        // �T�E���h�̈ꎞ��~
        SoundManager.instance.PauseBGM();
        SoundManager.instance.PauseSE();
        SoundManager.instance.PauseVOICE();

        InputManager.instance.Player_Disable();

        // �^�C���X�P�[����0�ɂ���
        Time.timeScale = 0.0f;

        // �A�j���[�V�����J�n
        isAnimating = true;
        animationStartTime = Time.unscaledTime;
        initialScaleY = pauseWindow.transform.localScale.y;
        scaleY = targetScaleY;
    }

    // �|�[�Y�̏I������
    private void PauseEndBack()
    {
        pauseFlg = false;

        // �T�E���h�̍ĊJ
        SoundManager.instance.UnPauseBGM();
        SoundManager.instance.UnPauseSE();
        SoundManager.instance.UnPauseVOICE();

        InputManager.instance.UI_Disable();

        // �A�j���[�V�����J�n
        isAnimating = true;
        animationStartTime = Time.unscaledTime;
        initialScaleY = pauseWindow.transform.localScale.y;
        scaleY = 0.0f;
    }
}
