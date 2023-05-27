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

    private GameObject volumeController;    // 音量調整用のオブジェクト

    public float BGM_volume = 0.5f;         // BGMの音量
    public float SE_volume = 0.5f;          // SEの音量
    public float VOICE_volume = 0.5f;       // VOICEの音量

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
            s.audioSource.volume = VOICE_volume;
        }
    }

    void Start()
    {
        // タイトルシーンのBGMを再生
        //if(SceneManager.GetActiveScene().name == "Title")
        //{
        //    PlayBGM("Title");
        //}

        // アクティブシーンの切り替え
        //Scene scene = SceneManager.GetSceneByName("Scene_B");
        //SceneManager.SetActiveScene(scene);
        // PlayBgm(BGM_clip);
    }

    private void Update()
    {
        currentScene = SceneNowBefore.instance.sceneNowCatch;

        // シーンがタイトルの時のみ音量調整用のオブジェクトを探す
        if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_TITLE)
        {
            volumeController = GameObject.FindWithTag("VolumeController");
            // オブジェクトがあるかどうか
            if (volumeController != null)
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
                            VOICE_volume = volumeController.GetComponent<VolumeController>().GetVOICEVolume();

                            ChangeVolumeBGM("Title");
                        }
                    }
                }
            }
        }


        BGMPlayer();
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

    public bool CheckPlayBGM(string name)
    {
        BGM s1 = Array.Find(bgm, sound => sound.name == name);
        // なければreturn
        if (s1 == null)
        {
            print("Sound" + name + "was not found");
            return false;
        }
        return s1.audioSource.isPlaying;
    }

    // フェードイン
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

    // フェードアウト
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
        s.audioSource.volume = VOICE_volume;
        s.audioSource.Play();
    }

    // ボイスの停止
    public void StopVOICE()
    {
        VOICE s = Array.Find(voice, sound => sound.audioSource.isPlaying == true);

        if (s == null)
        {
            //print("再生中のボイスがありません");
            return;
        }
        s.audioSource.Stop();

        // DoFadeOut(s);
    }


    // 再生中のBGMを全て取得し一時停止する
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
    // 一時停止中のBGMを全て取得し再生する
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
    // 再生中のSEを全て取得し一時停止する
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
    // 一時停止中のSEを全て取得し再生する
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
    // 再生中のVOICEを全て取得し一時停止する
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
    // 一時停止中のVOICEを全て取得し再生する
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
        // シーンが切り替わった時に呼ばれる関数を登録
        if (SceneNowBefore.instance != null)
        {
            if (currentScene != nextScene)
            {
                nextScene = currentScene;

                // BGMの再生
                // タイトルシーンの時
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_TITLE)
                {
                    PlayBGM("Title");
                }
                // プロローグシーン～ステージ2の時
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE)
                {
                    PlayBGM("Stage");
                    PlayBGM("BGM");
                }
                // Stage2Eventの時にBGMが鳴っていなかった場合
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
                // Stage3Event～ステージ3
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT)
                {
                    PlayBGM("BossStage");
                    if (CheckPlayBGM("BGM") == false)
                    {
                        PlayBGM("BGM");
                    }
                }


                // コンティニューの時
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


                // ゲームクリア
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE)
                {
                    PlayBGM("GameClear");
                }

                // BGMの停止
                // タイトルシーンでない場合
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_TITLE)
                {
                    StopBGM("Title");
                }
                // プロローグシーン～ステージ2でない場合
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_PROLOGUE &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE1 &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE2_EVENT &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE2)
                {
                    StopBGM("Stage");
                }
                // Stage3Event～ステージ3でない場合
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE3_EVENT &&
                   currentScene != SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                {
                    StopBGM("BossStage");
                }
                // ゲームクリアでない場合
                if (currentScene != SceneLoadStartUnload.SCENE_NAME.E_EPILOGUE &&
                    currentScene != SceneLoadStartUnload.SCENE_NAME.E_RESULT_COMPLETED)
                {
                    StopBGM("GameClear");
                }
                // プロローグシーン～ステージ3でない場合
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

    // シーンが切り替わった時に呼ばれる
//    void ActiveSceneChanged(Scene thisScene, Scene nextScene)
//    {
//        //Debug.Log(thisScene.name);
//        //Debug.Log(nextScene.name);

//        // シーンが切り替わった時
//        // BGMの切り替え
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

//            // BGMの停止
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