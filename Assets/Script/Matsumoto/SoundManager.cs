using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class BGM
{
    //public enum FadeBGM
    //{
    //    None,
    //    FadeIn,
    //    FadeOut,
    //    //CrossFade
    //}

    [Tooltip("BGM�̖��O")]
    public string name;
    // AudioSource�ɕK�v�ȏ��
    [Tooltip("BGM�̉���")]
    public AudioClip clip;
    //[Tooltip("�T�E���h�{�����[��, 0.0����1.0�܂�")]
    //public float volume;
    [Tooltip("���[�v")]
    public bool loop = true;
    [Tooltip("�t�F�[�h�C��")]
    public bool fadeIn = false;
    public float TimefadeIn = 0.0f;
    [Tooltip("�t�F�[�h�A�E�g")]
    public bool fadeOut = false;
    public float TimefadeOut = 0.0f;
    //[Tooltip("�N���X�t�F�[�h�p ����BGM")]
    //public string nextBGM;

    // AudioSource�DInspector�ɕ\�����Ȃ�
    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class SE
{
    [Tooltip("SE�̖��O")]
    public string name;
    // AudioSource�ɕK�v�ȏ��
    [Tooltip("SE�̉���")]
    public AudioClip clip;
    //[Tooltip("�T�E���h�{�����[��, 0.0����1.0�܂�")]
    //public float volume;
    // AudioSource�DInspector�ɕ\�����Ȃ�
    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class VOICE
{
    [Tooltip("�{�C�X�̖��O")]
    public string name;
    // AudioSource�ɕK�v�ȏ��
    [Tooltip("�{�C�X�̉���")]
    public AudioClip clip;
    [HideInInspector]
    public AudioSource audioSource;
}

public class SoundManager : MonoBehaviour
{
    private SceneLoadStartUnload.SCENE_NAME currentScene;
    private SceneLoadStartUnload.SCENE_NAME nextScene;

    [SerializeField]
    [Header("BGM")]
    public BGM[] bgm;

    [SerializeField]
    [Header("SE")]
    private SE[] se;

    [SerializeField]
    [Header("VOICE")]
    private VOICE[] voice;

    private GameObject volumeController;    // ���ʒ����p�̃I�u�W�F�N�g

    public float BGM_volume = 0.5f;         // BGM�̉���
    public float SE_volume = 0.5f;          // SE�̉���
    public float VOICE_volume = 0.5f;       // VOICE�̉���

    // �V���O���g����
    public static SoundManager instance;

    private void Awake()
    {
        // SoundManager�C���X�^���X�����݂��Ȃ���ΐ���
        // ���݂����Destroy�Creturn
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Sound�N���X�ɓ��ꂽ�f�[�^��AudioSource�ɓ��Ă͂߂�
        foreach (BGM s in bgm)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            //s.audioSource.volume = s.volume;
            s.audioSource.loop = s.loop;
            s.audioSource.volume = BGM_volume;
        }

        foreach (SE s in se)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            //s.audioSource.volume = s.volume;

            s.audioSource.volume = SE_volume;
        }

        foreach (VOICE s in voice)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.volume = VOICE_volume;
        }
    }

    void Start()
    {
        // �^�C�g���V�[����BGM���Đ�
        //if(SceneManager.GetActiveScene().name == "Title")
        //{
        //    PlayBGM("Title");
        //}

        // �A�N�e�B�u�V�[���̐؂�ւ�
        //Scene scene = SceneManager.GetSceneByName("Scene_B");
        //SceneManager.SetActiveScene(scene);
        // PlayBgm(BGM_clip);
    }

    private void Update()
    {
        currentScene = SceneNowBefore.instance.sceneNowCatch;

        // �V�[�����^�C�g���̎��̂݉��ʒ����p�̃I�u�W�F�N�g��T��
        if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_TITLE)
        {
            volumeController = GameObject.FindWithTag("VolumeController");
            // �I�u�W�F�N�g�����邩�ǂ���
            if (volumeController != null)
            {
                // �R���|�[�l���g�����邩�ǂ���
                if (volumeController.GetComponent<VolumeController>() != null)
                {
                    // �X���C�_�[null�`�F�b�N
                    if (volumeController.GetComponent<VolumeController>().NullCheckBGMSlider())
                    {
                        if (volumeController.GetComponent<VolumeController>().NullCheckBGMSlider())
                        {
                            BGM_volume = volumeController.GetComponent<VolumeController>().GetBGMVolume();
                            SE_volume = volumeController.GetComponent<VolumeController>().GetSEVolume();
                            VOICE_volume = volumeController.GetComponent<VolumeController>().GetVOICEVolume();

                            ChangeVolumeBGM("Title");
                        }
                    }
                }
            }
        }


        BGMPlayer();
    }

    // BGM�Đ�
    public void PlayBGM(string name)
    {
        BGM s1 = Array.Find(bgm, sound => sound.name == name);

        if (s1 == null)
        {
            print("Sound" + name + "was not found");
            return;
        }

        // �t�F�[�h����
        if (s1.fadeIn == false)
        {
            // �����Play()
            s1.audioSource.volume = BGM_volume;
            s1.audioSource.Play();
        }

        // �t�F�[�h�C��
        if (s1.fadeIn == true)
        {
            StartCoroutine(DoFadeIn(s1));
        }
    }

    // BGM��~
    public void StopBGM(string name)
    {
        BGM s1 = Array.Find(bgm, sound => sound.name == name);
        // �Ȃ����return
        if (s1 == null)
        {
            print("Sound" + name + "was not found");
            return;
        }

        // �t�F�[�h����
        if (s1.fadeOut == false)
        {
            s1.audioSource.Stop();
        }

        // �t�F�[�h�A�E�g
        if (s1.fadeOut == true)
        {
            StartCoroutine(DoFadeOut(s1));
        }
    }

    public bool CheckPlayBGM(string name)
    {
        BGM s1 = Array.Find(bgm, sound => sound.name == name);
        // �Ȃ����return
        if (s1 == null)
        {
            print("Sound" + name + "was not found");
            return false;
        }
        return s1.audioSource.isPlaying;
    }

    // �t�F�[�h�C��
    IEnumerator DoFadeIn(BGM b)
    {
        float bgmVolume = 0.0f;
        b.audioSource.volume = 0.0f;
        b.audioSource.Play();
        while (bgmVolume < BGM_volume)
        {
            bgmVolume += Time.deltaTime / b.TimefadeIn;
            b.audioSource.volume = bgmVolume;
            b.audioSource.volume = BGM_volume;
            //Debug.Log(bgmVolume);
            yield return null;
        }
    }

    // �t�F�[�h�A�E�g
    IEnumerator DoFadeOut(BGM b)
    {
        float bgmVolume = BGM_volume;

        while (bgmVolume > 0.0f)
        {
            bgmVolume -= Time.deltaTime / b.TimefadeOut;
            b.audioSource.volume = bgmVolume;
            //Debug.Log(bgmVolume);
            yield return null;
        }
        b.audioSource.Stop();
    }

    private void ChangeVolumeBGM(string name)
    {
        BGM s1 = Array.Find(bgm, sound => sound.name == name);

        if (s1 == null)
        {
            print("Sound" + name + "was not found");
            return;
        }

        s1.audioSource.volume = BGM_volume;
    }

    // SE�̍Đ�
    public void PlaySE(string name)
    {
        // �����_���@��������Predicate
        // Sound�N���X�̔z��̒��̖��O�ɁC
        // ����name�ɓ��������̂����邩�ǂ����m�F
        SE s = Array.Find(se, sound => sound.name == name);
        // �Ȃ����return
        if (s == null)
        {
            print("Sound" + name + "was not found");
            return;
        }
        // �����Play()
        s.audioSource.volume = SE_volume;
        s.audioSource.Play();
    }
    // �{�C�X�̍Đ�
    public void PlayVOICE(string name)
    {
        VOICE s = Array.Find(voice, sound => sound.name == name);

        if (s == null)
        {
            print("Sound" + name + "was not found");
            return;
        }
        s.audioSource.volume = VOICE_volume;
        s.audioSource.Play();
    }

    // �{�C�X�̒�~
    public void StopVOICE()
    {
        VOICE s = Array.Find(voice, sound => sound.audioSource.isPlaying == true);

        if (s == null)
        {
            //print("�Đ����̃{�C�X������܂���");
            return;
        }
        s.audioSource.Stop();

        // DoFadeOut(s);
    }


    // �Đ�����BGM��S�Ď擾���ꎞ��~����
    public void PauseBGM()
    {
        foreach (BGM b in bgm)
        {
            if (b.audioSource.isPlaying)
            {
                b.audioSource.Pause();
            }
        }
    }
    // �ꎞ��~����BGM��S�Ď擾���Đ�����
    public void UnPauseBGM()
    {
        foreach (BGM b in bgm)
        {
            if (b.audioSource.isPlaying == false)
            {
                b.audioSource.UnPause();
            }
        }
    }
    // �Đ�����SE��S�Ď擾���ꎞ��~����
    public void PauseSE()
    {
        foreach (SE s in se)
        {
            if (s.audioSource.isPlaying)
            {
                s.audioSource.Pause();
            }
        }
    }
    // �ꎞ��~����SE��S�Ď擾���Đ�����
    public void UnPauseSE()
    {
        foreach (SE s in se)
        {
            if (s.audioSource.isPlaying == false)
            {
                s.audioSource.UnPause();
            }
        }
    }
    // �Đ�����VOICE��S�Ď擾���ꎞ��~����
    public void PauseVOICE()
    {
        foreach (VOICE v in voice)
        {
            if (v.audioSource.isPlaying)
            {
                v.audioSource.Pause();
            }
        }
    }
    // �ꎞ��~����VOICE��S�Ď擾���Đ�����
    public void UnPauseVOICE()
    {
        foreach (VOICE v in voice)
        {
            if (v.audioSource.isPlaying == false)
            {
                v.audioSource.UnPause();
            }
        }
    }

    

    void BGMPlayer()
    {
        // �V�[�����؂�ւ�������ɌĂ΂��֐���o�^
        if (SceneNowBefore.instance != null)
        {
            if (currentScene != nextScene)
            {
                nextScene = currentScene;

                // BGM�̍Đ�
                // �^�C�g���V�[���̎�
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_TITLE)
                {
                    PlayBGM("Title");
                }
                // �v�����[�O�V�[���`�X�e�[�W2�̎�
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE)
                {
                    PlayBGM("Stage");
                    PlayBGM("BGM");
                }
                // Stage2Event�̎���BGM�����Ă��Ȃ������ꍇ
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT)
                {
                    if (CheckPlayBGM("Stage") == false)
                    {
                        PlayBGM("Stage");
                    }
                    if (CheckPlayBGM("BGM") == false)
                    {
                        PlayBGM("BGM");
                    }
                }
                // Stage3Event�`�X�e�[�W3
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT)
                {
                    PlayBGM("BossStage");
                    if (CheckPlayBGM("BGM") == false)
                    {
                        PlayBGM("BGM");
                    }
                }


                // �R���e�B�j���[�̎�
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE1)
                {
                    if (CheckPlayBGM("Stage") == false)
                    {
                        PlayBGM("Stage");
                    }
                    if (CheckPlayBGM("BGM") == false)
                    {
                        PlayBGM("BGM");
                    }
                }
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
                {
                    if (CheckPlayBGM("Stage") == false)
                    {
                        PlayBGM("Stage");
                    }
                    if (CheckPlayBGM("BGM") == false)
                    {
                        PlayBGM("BGM");
                    }
                }
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                {
                    if (CheckPlayBGM("BossStage") == false)
                    {
                        PlayBGM("BossStage");
                    }
                    if (CheckPlayBGM("BGM") == false)
                    {
                        PlayBGM("BGM");
                    }
                }


                // �Q�[���N���A
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE)
                {
                    PlayBGM("GameClear");
                }

                // BGM�̒�~
                // �^�C�g���V�[���łȂ��ꍇ
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_TITLE)
                {
                    StopBGM("Title");
                }
                // �v�����[�O�V�[���`�X�e�[�W2�łȂ��ꍇ
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE1 &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
                {
                    StopBGM("Stage");
                }
                // Stage3Event�`�X�e�[�W3�łȂ��ꍇ
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT &&
                   currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                {
                    StopBGM("BossStage");
                }
                // �Q�[���N���A�łȂ��ꍇ
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_RESULT_COMPLETED)
                {
                    StopBGM("GameClear");
                }
                // �v�����[�O�V�[���`�X�e�[�W3�łȂ��ꍇ
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE1 &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE2 &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                {
                    StopBGM("BGM");
                }
            }
        }
    }
}

    // �V�[�����؂�ւ�������ɌĂ΂��
//    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
//    {
//        //Debug.Log(thisScene.name);
//        //Debug.Log(nextScene.name);

//        // �V�[�����؂�ւ������
//        // BGM�̐؂�ւ�
//        if (thisScene != nextScene)
//        {
//            thisScene = nextScene;
//            if (nextScene.name == "Title")
//            {
//                PlayBGM("Title");
//            }
//            if (nextScene.name == "Prologue")
//            {
//                PlayBGM("Stage");
//                PlayBGM("BGM");
//            }

//            if (nextScene.name == "Stage2Event")
//            {
//                if (CheckPlayBGM("Stage") == false)
//                {
//                    PlayBGM("Stage");
//                }
//            }

//            if (nextScene.name == "Stage3Event")
//            {
//                PlayBGM("BossStage");
//            }
//            if (nextScene.name == "GameClear")
//            {
//                PlayBGM("GameClear");
//            }

//            // BGM�̒�~
//            if (nextScene.name != "Title")
//            {
//                StopBGM("Title");
//            }
//            if (nextScene.name != "Prologue" && nextScene.name != "Stage1" && nextScene.name != "Stage2" && nextScene.name != "Stage2Event")
//            {
//                StopBGM("Stage");
//            }
//            if (nextScene.name != "Stage3Event" && nextScene.name != "merge_2")
//            {
//                StopBGM("BossStage");
//            }

//            if (nextScene.name != "Stage1" && nextScene.name != "Stage2" && nextScene.name != "merge_2" &&
//                nextScene.name != "Prologue" && nextScene.name != "Stage2Event" && nextScene.name != "Stage3Event" && nextScene.name != "Epilogue")
//            {
//                StopBGM("BGM");
//            }

//            if (nextScene.name != "GameClear")
//            {
//                StopBGM("GameClear");
//            }
//        }
//    }
//}