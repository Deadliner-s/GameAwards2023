using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class VibrationValue
{
    [Tooltip("バイブレーションの名前")]
    public string name;
    [Tooltip("低周波")]
    public float LowFrequency = 0.0f;
    [Tooltip("高周波")]
    public float HighFrequency = 0.0f;
    [Tooltip("時間")]
    public float TimeVibration = 1.0f;
    [Tooltip("徐々に弱くなる")]
    public bool StepVibration = false;
}

public sealed class VibrationManager : MonoBehaviour
{
    // インスタンス
    public static VibrationManager instance;

    // バイブレーションの値
    [SerializeField]
    public VibrationValue[] vibration;

    // バイブレーションのオンオフ
    public bool isVibration;                    // バイブレーションのオンオフ
    private bool isVibrationOption = false;     // オプション画面でバイブレーションが切り替えられたか

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
    }

    void Start()
    {
        // isVibration = true;
    }

    // バイブレーション構造体
    private void Update()
    {
        //var gamepad = Gamepad.current;
        //if (gamepad != null)
        //{
        //    // A ボタンが押されたら
        //    if (gamepad.aButton.wasPressedThisFrame)
        //    {
        //        // 低周波（左）モーターの強さを 1、
        //        // 高周波（右）モーターの強さを 0 で振動させる
        //        StartCoroutine(Vibration(1.0f, 0.0f, 5.0f, true));
        //    }
        //    // B ボタンが押されたら
        //    else if (gamepad.bButton.wasPressedThisFrame)
        //    {
        //        // 低周波（左）モーターの強さを 0、
        //        // 高周波（右）モーターの強さを 1 で振動させる
        //        StartCoroutine(Vibration(0.0f, 1.0f, 1.0f, false));
        //    }
        //}

        // オプション画面でバイブレーションのオンオフを切り替えるとtrueになる
        if (isVibrationOption == false)
        {
            if (isVibration == false)
            {
                isVibrationOption = true;
            }
        }
        // バイブレーションを一瞬だけ再生する
        if (isVibrationOption == true)
        {
            if (isVibration == true)
            {
                StartCoroutine(PlayVibration("Option"));
                isVibrationOption = false;
            }
        }
    }

    public IEnumerator PlayVibration(string name)
    {
        if (isVibration == false)
        {
            yield return null;
        }
        if (isVibration == true)
        {
            // 名前の確認
            VibrationValue v = Array.Find(instance.vibration, vib => vib.name == name);
            // 存在しない場合
            if (v == null)
            {
                print("not found " + name);
                yield return null;
            }

            // 存在する場合
            if (v != null)
            {
                // 現在のゲームパッドを取得
                var gamepad = Gamepad.current;
                if (gamepad == null)
                {
                    print("gamepad null");
                }

                // ゲームパッドが接続されている場合
                if (gamepad != null)
                {
                    // 何秒間振動させる
                    if (v.StepVibration == false)
                    {
                        gamepad.SetMotorSpeeds(v.LowFrequency, v.HighFrequency);
                        yield return new WaitForSeconds(v.TimeVibration);
                    }

                    // time間かけて段々弱くなる
                    if (v.StepVibration == true)
                    {
                        for (float t = 0; t < v.TimeVibration; t += Time.deltaTime)
                        {
                            var rate = 1.0f - t / v.TimeVibration;
                            gamepad.SetMotorSpeeds(v.LowFrequency * rate, v.HighFrequency * rate);
                            yield return null;
                        }
                    }
                    // 振動を止める
                    gamepad.SetMotorSpeeds(0, 0);
                }
            }
        }
    }

    public bool GetisVibration()
    {
        return isVibration;
    }
    public void SetisVibration(bool value)
    {
        isVibration = value;
    }


    // アプリケーション終了時の処理
    private void OnApplicationQuit()
    {
        // 現在のゲームパッドを取得
        var gamepad = Gamepad.current;

        // ゲームパッドが接続されている場合
        if (gamepad != null)
        {
            // 振動を止める
            gamepad.SetMotorSpeeds(0, 0);
        }
    }
}