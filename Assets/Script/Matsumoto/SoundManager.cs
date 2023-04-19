using UnityEngine;
using System;
using UnityEngine.SceneManagement;


[System.Serializable]
public class BGM
{
    [Tooltip("�T�E���h�̖��O")]
    public string name;
    // AudioSource�ɕK�v�ȏ��
    [Tooltip("�T�E���h�̉���")]
    public AudioClip clip;
    [Tooltip("�T�E���h�{�����[��, 0.0����1.0�܂�")]
    //public float volume;
    // AudioSource�DInspector�ɕ\�����Ȃ�
    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class SE
{
    [Tooltip("�T�E���h�̖��O")]
    public string name;
    // AudioSource�ɕK�v�ȏ��
    [Tooltip("�T�E���h�̉���")]
    public AudioClip clip;
    [Tooltip("�T�E���h�{�����[��, 0.0����1.0�܂�")]
    //public float volume;
    // AudioSource�DInspector�ɕ\�����Ȃ�
    [HideInInspector]
    public AudioSource audioSource;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    [Header("BGM")]
    private BGM[] bgm;

    // Sound�N���X�z��
    [SerializeField]
    [Header("BGM")]
    private SE[] se;


    private GameObject volumeController;
    private bool findFlg = false;

    public float BGM_volume = 1.0f;
    public float SE_volume = 1.0f;

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

            s.audioSource.volume = BGM_volume;
        }

        foreach (SE s in se)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            //s.audioSource.volume = s.volume;

            s.audioSource.volume = SE_volume;
        }
    }

    void Start()
    {
        SceneManager.activeSceneChanged += ActiveSceneChanged;

        // �A�N�e�B�u�V�[���̐؂�ւ�
        //Scene scene = SceneManager.GetSceneByName("Scene_B");
        //SceneManager.SetActiveScene(scene);
       // PlayBgm(BGM_clip);
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Option")
        {
            if (findFlg == false)
            {
                volumeController = GameObject.Find("VolumeControllerObj");
                findFlg = true;
            }

            if (volumeController != null)
            {
                if (volumeController.GetComponent<VolumeController>() != null)
                {
                    BGM_volume = volumeController.GetComponent<VolumeController>().GetBGMVolume();
                    SE_volume = volumeController.GetComponent<VolumeController>().GetSEVolume();
                }
            }
        }
        if (SceneManager.GetActiveScene().name != "Option")
        {
            findFlg = false;
            if (volumeController != null)
            {
                volumeController = null;
            }
        }
    }

    public void PlayBGM(string name)
    {
        // �����_���@��������Predicate
        // Sound�N���X�̔z��̒��̖��O�ɁC
        // ����name�ɓ��������̂����邩�ǂ����m�F
        BGM s = Array.Find(bgm, sound => sound.name == name);
        // �Ȃ����return
        if (s == null)
        {
            print("Sound" + name + "was not found");
            return;
        }
        // �����Play()
        s.audioSource.volume = BGM_volume;
        s.audioSource.Play();
    }

    public void StopBGM(string name)
    {
        BGM s = Array.Find(bgm, sound => sound.name == name);
        // �Ȃ����return
        if (s == null)
        {
            print("Sound" + name + "was not found");
            return;
        }
        s.audioSource.Stop();
    }

    public void Play(string name)
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

    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        //Debug.Log(thisScene.name);
        //Debug.Log(nextScene.name);
        if (thisScene != nextScene)
        {
            thisScene = nextScene;
            if (nextScene.name == "Stage1")
            {
                PlayBGM("Stage3");
            }
            if (nextScene.name == "Stage2")
            {
                PlayBGM("Stage3");
            }
            if (nextScene.name == "merge_2")
            {
                PlayBGM("Stage3");
                PlayBGM("BGM");
            }
            if (nextScene.name != "merge_2")
            {
                StopBGM("Stage3");
                StopBGM("BGM");
            }
        }
    }
}