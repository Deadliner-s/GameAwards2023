using UnityEngine;
using System;
using UnityEngine.SceneManagement;


[System.Serializable]
public class BGM
{
    [Tooltip("サウンドの名前")]
    public string name;
    // AudioSourceに必要な情報
    [Tooltip("サウンドの音源")]
    public AudioClip clip;
    [Tooltip("サウンドボリューム, 0.0から1.0まで")]
    //public float volume;
    // AudioSource．Inspectorに表示しない
    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class SE
{
    [Tooltip("サウンドの名前")]
    public string name;
    // AudioSourceに必要な情報
    [Tooltip("サウンドの音源")]
    public AudioClip clip;
    [Tooltip("サウンドボリューム, 0.0から1.0まで")]
    //public float volume;
    // AudioSource．Inspectorに表示しない
    [HideInInspector]
    public AudioSource audioSource;
}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    [Header("BGM")]
    private BGM[] bgm;

    // Soundクラス配列
    [SerializeField]
    [Header("BGM")]
    private SE[] se;


    private GameObject volumeController;
    private bool findFlg = false;

    public float BGM_volume = 1.0f;
    public float SE_volume = 1.0f;

    // シングルトン化
    public static SoundManager instance;

    private void Awake()
    {
        // SoundManagerインスタンスが存在しなければ生成
        // 存在すればDestroy，return
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

        // Soundクラスに入れたデータをAudioSourceに当てはめる
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

        // アクティブシーンの切り替え
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
        // ラムダ式　第二引数はPredicate
        // Soundクラスの配列の中の名前に，
        // 引数nameに等しいものがあるかどうか確認
        BGM s = Array.Find(bgm, sound => sound.name == name);
        // なければreturn
        if (s == null)
        {
            print("Sound" + name + "was not found");
            return;
        }
        // あればPlay()
        s.audioSource.volume = BGM_volume;
        s.audioSource.Play();
    }

    public void StopBGM(string name)
    {
        BGM s = Array.Find(bgm, sound => sound.name == name);
        // なければreturn
        if (s == null)
        {
            print("Sound" + name + "was not found");
            return;
        }
        s.audioSource.Stop();
    }

    public void Play(string name)
    {
        // ラムダ式　第二引数はPredicate
        // Soundクラスの配列の中の名前に，
        // 引数nameに等しいものがあるかどうか確認
        SE s = Array.Find(se, sound => sound.name == name);
        // なければreturn
        if (s == null)
        {
            print("Sound" + name + "was not found");
            return;
        }
        // あればPlay()
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