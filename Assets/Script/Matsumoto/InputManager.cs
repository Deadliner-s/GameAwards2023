using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public  Myproject InputActions;                         // ����
    private SceneLoadStartUnload.SCENE_NAME currentScene;   // ���݂̃V�[��
    private SceneLoadStartUnload.SCENE_NAME nextScene;      // ���̃V�[��

    public static InputManager instance;                    // �C���X�^���X

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

        // ���͂̏�����
        InputActions = new Myproject();
        InputActions.Enable();

        // �L�[�R���t�B�O�̃f�[�^�����[�h
        //InputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));

        // �v���C���[�̓���
        InputActions.Player.Move.performed += context => OnMove();
        InputActions.Player.Reticle.performed += context => OnReticleMove();
        InputActions.Player.Shot.performed += context => OnShot();
        InputActions.Player.Manever.performed += context => OnManever();
        // UI�̓���
        InputActions.UI.Up.performed += context => OnUp();
        InputActions.UI.Down.performed += context => OnDown();
        InputActions.UI.Right.performed += context => OnRight();
        InputActions.UI.Left.performed += context => OnLeft();
        InputActions.UI.Select.performed += context => OnSelect();
    }

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[�̓��͂𖳌��ɂ���
        InputActions.Player.Disable();
        // UI�̓��͂�L���ɂ���
        InputActions.UI.Enable();

        // �V�[���擾
        currentScene = SceneNow.instance.sceneNowCatch;
        nextScene = currentScene;

        // �V�[�����X�e�[�W�Ȃ�v���C���[�̓��͂�L���ɂ���
        if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 ||
            currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE2 ||
            currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
        {
            InputActions.UI.Disable();
            InputActions.Player.Enable();
        }
        // �V�[�����X�e�[�W�ȊO�Ȃ�UI�̓��͂�L���ɂ���
        else
        {
            InputActions.Player.Disable();
            InputActions.UI.Enable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ���݂̃V�[�����擾
        currentScene = SceneNow.instance.sceneNowCatch;
        if (SceneNow.instance != null)
        {
            // �V�[�����ς������
            if (currentScene != nextScene)
            {
                nextScene = currentScene;

                // �V�[�����X�e�[�W�Ȃ�v���C���[�̓��͂�L���ɂ���
                if (currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE1 ||
                    currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE2 ||
                    currentScene == SceneLoadStartUnload.SCENE_NAME.E_STAGE3)
                {
                    InputActions.UI.Disable();
                    InputActions.Player.Enable();
                }
                // �V�[�����X�e�[�W�ȊO�Ȃ�UI�̓��͂�L���ɂ���
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
