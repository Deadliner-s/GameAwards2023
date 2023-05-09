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
            s.audioSource.volume = SE_volume;
        }
    }

    void Start()
    {
        SceneManager.activeSceneChanged += ActiveSceneChanged;

        // �^�C�g���V�[����BGM���Đ�
        if(SceneManager.GetActiveScene().name == "Title")
        {
            PlayBGM("Title");
        }


        // �A�N�e�B�u�V�[���̐؂�ւ�
        //Scene scene = SceneManager.GetSceneByName("Scene_B");
        //SceneManager.SetActiveScene(scene);
       // PlayBgm(BGM_clip);
    }

    private void Update()
    {
        // �V�[�����^�C�g���̎��̂݉��ʒ����p�̃I�u�W�F�N�g��T��
        if (SceneManager.GetActiveScene().name == "Title")
        {
            volumeController = GameObject.FindWithTag("VolumeController");
            // �I�u�W�F�N�g�����邩�ǂ���
            if(volumeController != null)
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

                            ChangeVolumeBGM("Title");
                        }
                    }
                }
            }
        }
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

    // �t�F�[�h�C��
    IEnumerator DoFadeIn(BGM b)
    {
        float bgmVolume = 0.0f;
        b.audioSource.Play();
        while (bgmVolume < 1.0f)
        {
            bgmVolume += Time.deltaTime / b.TimefadeIn;
            b.audioSource.volume = bgmVolume;
            Debug.Log(bgmVolume);
            yield return null;
        }
    }

    // �t�F�[�h�A�E�g
    IEnumerator DoFadeOut(BGM b)
    {
        float bgmVolume = 1.0f;

        while (bgmVolume > 0.0f)
        {
            bgmVolume -= Time.deltaTime / b.TimefadeOut;
            b.audioSource.volume = bgmVolume;
            Debug.Log(bgmVolume);
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
        s.audioSource.volume = SE_volume;
        s.audioSource.Play();
    }


    // �V�[�����؂�ւ�������ɌĂ΂��
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        //Debug.Log(thisScene.name);
        //Debug.Log(nextScene.name);

        // �V�[�����؂�ւ������
        // BGM�̐؂�ւ�
        if (thisScene != nextScene)
        {
            thisScene = nextScene;
            if (nextScene.name == "Title")
            {
                PlayBGM("Title");
            }
            if (nextScene.name == "Prologue")
            {
                PlayBGM("Stage");
                PlayBGM("BGM");
            }
            if (nextScene.name == "Stage3Event")
            {
                PlayBGM("BossStage");
            }
            if(nextScene.name == "GameClear")
            {
                PlayBGM("GameClear");
            }

            // BGM�̒�~
            if (nextScene.name != "Title")
            {
                StopBGM("Title");
            }
            if (nextScene.name != "Prologue" && nextScene.name != "Stage1" && nextScene.name != "Stage2" && nextScene.name != "Stage2Event")
            {
                StopBGM("Stage");
            }
            if (nextScene.name == "Stage3Event" && nextScene.name != "merge_2")
            {
                StopBGM("BossStage");
            }

            if (nextScene.name != "Stage1" && nextScene.name != "Stage2" && nextScene.name != "merge_2" &&
                nextScene.name != "Prologue" && nextScene.name != "Stage2Event" && nextScene.name != "Stage3Event" && nextScene.name != "Epilogue")
            {
                StopBGM("BGM");
            }

            if (nextScene.name != "GameClear")
            {
                StopBGM("GameClear");
            }
        }
    }
}