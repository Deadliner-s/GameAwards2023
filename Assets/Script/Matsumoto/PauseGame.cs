using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool pauseFlg = false;         // �|�[�Y����

    private const int MAX_PAUSEMENU = 4;   // �|�[�Y���j���[�̐�
    private int Selected = 0;              // �I�𒆂̃|�[�Y���j���[


    [SerializeField]
    private GameObject pauseWindow;         // �|�[�Y��ʂ̃I�u�W�F�N�g
    private GameObject pauseMenu;           // �|�[�Y���j���[�̃I�u�W�F�N�g
    private GameObject[] text = new GameObject[MAX_PAUSEMENU];                // �e�L�X�g�̃I�u�W�F�N�g

    private GameObject fade;                // �t�F�[�h�̃I�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        pauseFlg = false;

        pauseMenu = pauseWindow.transform.Find("PauseMenu").gameObject;
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
        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 ||
            SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2 ||
            SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
        {


            if (InputManager.instance.OnPause() && pauseFlg == false)
            {
                Time.timeScale = 0.0f;
                pauseFlg = true;
                SoundManager.instance.PauseBGM();
                SoundManager.instance.PauseSE();
                SoundManager.instance.PauseVOICE();

                InputManager.instance.Player_Disable();
                InputManager.instance.UI_Enable();

                pauseWindow.SetActive(true);
            }
            else if (InputManager.instance.OnBack() && pauseFlg == true)
            {
                PauseEndBack();
            }

            Selected = (Selected + MAX_PAUSEMENU) % MAX_PAUSEMENU;

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

            switch (Selected)
            {
                case 0:
                    // BACK
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.white;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
                case 1:
                    // RETRY
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.white;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
                case 2:
                    // RESTART
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.white;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.black;
                    break;
                case 3:
                    // RETURN TITLE
                    text[0].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[1].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[2].GetComponent<TextMeshProUGUI>().color = Color.black;
                    text[3].GetComponent<TextMeshProUGUI>().color = Color.white;
                    break;
            }

            if (InputManager.instance.OnSelect())
            {
                switch (Selected)
                {
                    case 0:
                        // BACK
                        PauseEndBack();
                        break;
                    case 1:
                        // RETRY
                        // �X�e�[�W1
                        if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
                        {
                            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE1,
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE1));
                        }
                        // �X�e�[�W2
                        else if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
                        {
                            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE2,
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE2));
                        }
                        // �X�e�[�W3
                        else if (SceneNowBefore.instance.sceneNowCatch == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                        {
                            StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE3,
                                SceneLoadStartUnload.SCENE_NAME.E_STAGE3));
                        }
                        Time.timeScale = 1.0f;
                        break;
                    case 2:
                        // RESTART
                        // �X�e�[�W1����
                        StartCoroutine(fade.GetComponent<Fade>().Color_FadeOut_NowNext(
                            SceneLoadStartUnload.SCENE_NAME.E_STAGE1,
                            SceneLoadStartUnload.SCENE_NAME.E_STAGE1));
                        Time.timeScale = 1.0f;
                        pauseWindow.SetActive(false);
                        break;
                    case 3:
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
                        Time.timeScale = 1.0f;
                        break;
                }
            }
        }
    }

    // �|�[�Y�̏I������
    private void PauseEndBack()
    {
        SoundManager.instance.UnPauseBGM();
        SoundManager.instance.UnPauseSE();
        SoundManager.instance.UnPauseVOICE();

        InputManager.instance.UI_Disable();
        InputManager.instance.Player_Enable();

        Time.timeScale = 1.0f;
        pauseFlg = false;

        pauseWindow.SetActive(false);
    }





}
