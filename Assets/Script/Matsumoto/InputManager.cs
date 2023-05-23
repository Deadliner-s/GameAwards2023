using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public  Myproject InputActions;                         // 入力
    private SceneLoadStartUnload.SCENE_NAME currentScene;   // 現在のシーン
    private SceneLoadStartUnload.SCENE_NAME nextScene;      // 次のシーン

    public static InputManager instance;                    // インスタンス

    void Awake()
    {
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

        // 入力の初期化
        InputActions = new Myproject();
        InputActions.Enable();

        // キーコンフィグのデータをロード
        //InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));

        // プレイヤーの入力
        InputActions.Player.Move.performed += context => OnMove();
        InputActions.Player.Reticle.performed += context => OnReticleMove();
        InputActions.Player.Shot.performed += context => OnShot();
        InputActions.Player.Manever.performed += context => OnManever();
        // UIの入力
        InputActions.UI.Up.performed += context => OnUp();
        InputActions.UI.Down.performed += context => OnDown();
        InputActions.UI.Right.performed += context => OnRight();
        InputActions.UI.Left.performed += context => OnLeft();
        InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        // プレイヤーの入力を無効にする
        InputActions.Player.Disable();
        // UIの入力を有効にする
        InputActions.UI.Enable();

        // シーン取得
        currentScene = SceneNow.instance.sceneNowCatch;
        nextScene = currentScene;

        // シーンがステージならプレイヤーの入力を有効にする
        if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 ||
            currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE2 ||
            currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
        {
            InputActions.UI.Disable();
            InputActions.Player.Enable();
        }
        // シーンがステージ以外ならUIの入力を有効にする
        else
        {
            InputActions.Player.Disable();
            InputActions.UI.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 現在のシーンを取得
        currentScene = SceneNow.instance.sceneNowCatch;
        if (SceneNow.instance != null)
        {
            // シーンが変わったら
            if (currentScene != nextScene)
            {
                nextScene = currentScene;

                // シーンがステージならプレイヤーの入力を有効にする
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 ||
                    currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE2 ||
                    currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                {
                    InputActions.UI.Disable();
                    InputActions.Player.Enable();
                }
                // シーンがステージ以外ならUIの入力を有効にする
                else
                {
                    InputActions.Player.Disable();
                    InputActions.UI.Enable();
                }

            }
        }
    }

    public Vector3 OnMove()
    {
        return InputActions.Player.Move.ReadValue<Vector2>();
    }
    public Vector2 OnReticleMove()
    {
        return InputActions.Player.Reticle.ReadValue<Vector2>();
    }
    public bool OnShot()
    {
        return InputActions.Player.Shot.triggered;
    }
    public bool OnManever()
    {
        return InputActions.Player.Manever.triggered;
    }
    public bool OnUp()
    {
        return InputActions.UI.Up.triggered;
    }
    public bool OnDown()
    {
        return InputActions.UI.Down.triggered;
    }
    public bool OnRight()
    {
        return InputActions.UI.Right.triggered;
    }
    public bool OnLeft()
    {
        return InputActions.UI.Left.triggered;
    }
    public bool OnSelect()
    {
        return InputActions.UI.Select.triggered;
    }
    public void UI_Disable()
    {
        InputActions.UI.Disable();
    }
}
