using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveSphere : MonoBehaviour
{
    // ����
    private Myproject InputActions;
    private Vector2 inputMove;

    // ���S
    [SerializeField] GameObject center;

    // �f�o�b�O�p
#if _DEBUG
    private bool bInput = false;
    private bool bInputU = false;
    private bool bInputO = false;
#endif // _DEBUG

    // ���̔��a
    [Header("���񎞂̔��a")]
    [Tooltip("���񎞂̔��a")]
    public float radius = 2.0f;

    // ���W
    private float x;
    private float y;
    private float z;

    // ���ʍ��W�̈ړ�
    [Header("�㉺���E�̈ړ����x")]
    [Tooltip("�㉺�ړ����x")]
    public float thetaSpeed = 0.01f;
    [Tooltip("���E�ړ����x")]
    public float phiSpeed = 0.01f;

    // ���ʍ��W�̒ʏ펞�̈ړ����x
    [Header("�ʏ펞�̏㉺���E�S�Ă̑��x�␳")]
    [Tooltip("�ʏ펞�̑��x�␳")]
    public float DefaultSpeed = 1.0f;

    // ���ʍ��W�̃V�[�^�ƃt�@�C
    private float radianTheta = 0;
    private float radianPhi = 0;
    private bool maneverFlg = false;

    // �N�C�b�N�}�j���[�o�p�ϐ�
    [Header("�}�j���[�o")]
    [Tooltip("�}�j���[�o�̎�������")]
    public float ManerverTime = 5.0f;   // ��������
    [Tooltip("�N�[���^�C��")]
    public float coolTime = 1.0f;       // �Ďg�p�ł���悤�ɂȂ鎞��
    [Tooltip("���x")]
    public float ManeverSpeed = 0.5f;   // �}�j���[�o���̑��x

    // �}�j���[�o�J�E���g
    private float elapsedTime;          // �}�j���[�o�o�ߎ���
    private float coolTimeCnt;          // �N�[���^�C���̌o�ߎ���

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
        InputActions.Player.Boost.performed += context => OnBoost();
    }

    // Start is called before the first frame update
    void Start()
    {
        x = this.transform.position.x;
        y = this.transform.position.y;
        z = this.transform.position.z;

        // �������W�����ʍ��W�ɕϊ�
        radius = Mathf.Sqrt(x * x + y * y + z * z);
        radianTheta = Mathf.Atan(Mathf.Sqrt(x * x + y * y) / z);
        radianPhi = Mathf.Atan(y / x);
        elapsedTime = ManerverTime + coolTime;
        coolTimeCnt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[����
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // ���ʍ��W�̍X�V
        if (maneverFlg == false)
        {
            radianTheta += thetaSpeed * inputMove.y * Mathf.Deg2Rad;
            radianPhi += phiSpeed * inputMove.x * Mathf.Deg2Rad * DefaultSpeed;
        }
        else
        {
            // �N�C�b�N�}�j���[�o
            radianTheta += ManeverSpeed * inputMove.y * Mathf.Deg2Rad;
            radianPhi += ManeverSpeed * inputMove.x * Mathf.Deg2Rad * DefaultSpeed;
            // �}�j���[�o�o�ߎ���
            elapsedTime += Time.deltaTime;
        }
        if (elapsedTime > ManerverTime)
        {
            // �}�j���[�o�ł��鎞�Ԃ��߂�����
            maneverFlg = false;
            // �N�[���^�C���̃J�E���g���n�߂�
            coolTimeCnt += Time.deltaTime;
        }


        // ���ʍ��W�𒼌����W�ɕϊ�
        x = radius * Mathf.Cos(radianTheta) * Mathf.Cos(radianPhi);
        y = radius * Mathf.Sin(radianTheta);
        z = radius * Mathf.Cos(radianTheta) * Mathf.Sin(radianPhi);

        //Debug.Log("theta:" + radianTheta);
        //Debug.Log("phi:" + radianPhi);

        // ���W�X�V
        transform.position = new Vector3(x, y, z);
    }

    // X����]
    private Vector3 RotateAroundX(Vector3 pos, float angle, float radius)
    {
        Vector3 v = pos;
        v.z += radius;
        float a;
        float x;
        float y;
        float z;

        a = angle * Mathf.Deg2Rad;
        x = v.x;
        y = Mathf.Cos(a) * v.y - Mathf.Sin(a) * v.z;
        z = Mathf.Sin(a) * v.y - Mathf.Cos(a) * v.z;

        return new Vector3(x, y, z);
    }
    // Y����]
    private Vector3 RotateAroundY(Vector3 pos, float angle, float radius)
    {
        Vector3 v = pos;
        v.z += radius;
        float a;
        float x;
        float y;
        float z;

        a = angle * Mathf.Deg2Rad;
        x = Mathf.Cos(a) * v.x - Mathf.Sin(a) * v.z;
        y = v.y;
        z = -Mathf.Sin(a) * v.x - Mathf.Cos(a) * v.z;

        return new Vector3(x, y, z);
    }
    // Z����]
    private Vector3 RotateAroundZ(Vector3 pos, float angle, float radius)
    {
        Vector3 v = pos;
        v.y += radius;
        float a;
        float x;
        float y;
        float z;

        a = angle * Mathf.Deg2Rad;
        x = Mathf.Cos(a) * v.x - Mathf.Sin(a) * v.y;
        y = Mathf.Sin(a) * v.x + Mathf.Cos(a) * v.y;
        z = v.z;

        return new Vector3(x, y, z);
    }

    private void OnBoost()
    {
        // �N�[���^�C�����I����Ă�����
        if (coolTimeCnt > coolTime)
        {
            maneverFlg = true;
            elapsedTime = 0;
            coolTimeCnt = 0;
        }
    }
}
