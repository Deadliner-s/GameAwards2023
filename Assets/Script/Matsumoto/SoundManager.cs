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

    [Tooltip("BGMの名前")]
    public string name;
    // AudioSourceに必要な情報
    [Tooltip("BGMの音源")]
    public AudioClip clip;
    //[Tooltip("サウンドボリューム, 0.0から1.0まで")]
    //public float volume;
    [Tooltip("ループ")]
    public bool loop = true;
    [Tooltip("フェードイン")]
    public bool fadeIn = false;
    public float TimefadeIn = 0.0f;
    [Tooltip("フェードアウト")]
    public bool fadeOut = false;
    public float TimefadeOut = 0.0f;
    //[Tooltip("クロスフェード用 次のBGM")]
    //public string nextBGM;

    // AudioSource．Inspectorに表示しない
    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class SE
{
    [Tooltip("SEの名前")]
    public string name;
    // AudioSourceに必要な情報
    [Tooltip("SEの音源")]
    public AudioClip clip;
    //[Tooltip("サウンドボリューム, 0.0から1.0まで")]
    //public float volume;
    // AudioSource．Inspectorに表示しない
    [HideInInspector]
    public AudioSource audioSource;
}

[System.Serializable]
public class VOICE
{
    [Tooltip("ボイスの名前")]
    public string name;
    // AudioSourceに必要な情報
    [Tooltip("ボイスの音源")]
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

    private GameObject volumeController;    // 音量調整用のオブジェクト

    public float BGM_volume = 0.5f;         // BGMの音量
    public float SE_volume = 0.5f;          // SEの音量

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

        // タイトルシーンのBGMを再生
        if(SceneManager.GetActiveScene().name == "Title")
        {
            PlayBGM("Title");
        }


        // アクティブシーンの切り替え
        //Scene scene = SceneManager.GetSceneByName("Scene_B");
        //SceneManager.SetActiveScene(scene);
       // PlayBgm(BGM_clip);
    }

    private void Update()
    {
        // シーンがタイトルの時のみ音量調整用のオブジェクトを探す
        if (SceneManager.GetActiveScene().name == "Title")
        {
            volumeController = GameObject.FindWithTag("VolumeController");
            // オブジェクトがあるかどうか
            if(volumeController != null)
            {
                // コンポーネントがあるかどうか
                if (volumeController.GetComponent<VolumeController>() != null)
                {
                    // スライダーnullチェック
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

    // BGM再生
    public void PlayBGM(string name)
    {
        BGM s1 = Array.Find(bgm, sound => sound.name == name);

        if (s1 == null)
        {
            print("Sound" + name + "was not found");
            return;
        }

        // フェード無し
        if (s1.fadeIn == false)
        {
            // あればPlay()
            s1.audioSource.volume = BGM_volume;
            s1.audioSource.Play();
        }

        // フェードイン
        if (s1.fadeIn == true)
        {
            StartCoroutine(DoFadeIn(s1));
        }
    }

    // BGM停止
    public void StopBGM(string name)
    {
        BGM s1 = Array.Find(bgm, sound => sound.name == name);
        // なければreturn
        if (s1 == null)
        {
            print("Sound" + name + "was not found");
            return;
        }

        // フェード無し
        if (s1.fadeOut == false)
        {
            s1.audioSource.Stop();
        }

        // フェードアウト
        if (s1.fadeOut == true)
        {
            StartCoroutine(DoFadeOut(s1));
        }
    }

    // フェードイン
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

    // フェードアウト
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

    // SEの再生
    public void PlaySE(string name)
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

    // ボイスの再生
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


    // シーンが切り替わった時に呼ばれる
    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
    {
        //Debug.Log(thisScene.name);
        //Debug.Log(nextScene.name);

        // シーンが切り替わった時
        // BGMの切り替え
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

            // BGMの停止
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