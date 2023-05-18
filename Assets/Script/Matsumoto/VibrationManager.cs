using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class VibrationValue
{
    [Tooltip("�o�C�u���[�V�����̖��O")]
    public string name;
    [Tooltip("����g")]
    public float LowFrequency = 0.0f;
    [Tooltip("�����g")]
    public float HighFrequency = 0.0f;
    [Tooltip("����")]
    public float TimeVibration = 1.0f;
    [Tooltip("���X�Ɏキ�Ȃ�")]
    public bool StepVibration = false;
}

public sealed class VibrationManager : MonoBehaviour
{
    // �C���X�^���X
    public static VibrationManager instance;

    // �o�C�u���[�V�����̒l
    [SerializeField]
    public VibrationValue[] vibration;

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

    // �o�C�u���[�V�����\����
    private void Update()
    {
        //var gamepad = Gamepad.current;
        //if (gamepad != null)
        //{
        //    // A �{�^���������ꂽ��
        //    if (gamepad.aButton.wasPressedThisFrame)
        //    {
        //        // ����g�i���j���[�^�[�̋����� 1�A
        //        // �����g�i�E�j���[�^�[�̋����� 0 �ŐU��������
        //        StartCoroutine(Vibration(1.0f, 0.0f, 5.0f, true));
        //    }
        //    // B �{�^���������ꂽ��
        //    else if (gamepad.bButton.wasPressedThisFrame)
        //    {
        //        // ����g�i���j���[�^�[�̋����� 0�A
        //        // �����g�i�E�j���[�^�[�̋����� 1 �ŐU��������
        //        StartCoroutine(Vibration(0.0f, 1.0f, 1.0f, false));
        //    }
        //}
    }

    public IEnumerator PlayVibration(string name)
    {
        // ���O�̊m�F
        VibrationValue v = Array.Find(instance.vibration, vib => vib.name == name);
        // ���݂��Ȃ��ꍇ
        if (v == null)
        {
            print("not found " + name);
            yield return null;
        }

        // ���݂���ꍇ
        if (v != null)
        {
            // ���݂̃Q�[���p�b�h���擾
            var gamepad = Gamepad.current;
            if (gamepad == null)
            {
                print("gamepad null");
            }

            // �Q�[���p�b�h���ڑ�����Ă���ꍇ
            if (gamepad != null)
            {
                // ���b�ԐU��������
                if (v.StepVibration == false)
                {
                    gamepad.SetMotorSpeeds(v.LowFrequency, v.HighFrequency);
                    yield return new WaitForSeconds(v.TimeVibration);
                }

                // time�Ԃ����Ēi�X�キ�Ȃ�
                if (v.StepVibration == true)
                {
                    for (float t = 0; t < v.TimeVibration; t += Time.deltaTime)
                    {
                        var rate = 1.0f - t / v.TimeVibration;
                        gamepad.SetMotorSpeeds(v.LowFrequency * rate, v.HighFrequency * rate);
                        yield return null;
                    }
                }
                // �U�����~�߂�
                gamepad.SetMotorSpeeds(0, 0);
            }
        }
    }

    // �A�v���P�[�V�����I�����̏���
    private void OnApplicationQuit()
    {
        // ���݂̃Q�[���p�b�h���擾
        var gamepad = Gamepad.current;

        // �Q�[���p�b�h���ڑ�����Ă���ꍇ
        if (gamepad != null)
        {
            // �U�����~�߂�
            gamepad.SetMotorSpeeds(0, 0);
        }
    }
}