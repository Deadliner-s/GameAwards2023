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
    public float radius = 2.0f;

    // ���W
    float x;
    float y;
    float z;

    // ���ʍ��W�̈ړ����x
    public float thetaSpeed = 0.1f;
    public float phiSpeed = 0.1f;

    // ���ʍ��W�̃V�[�^�ƃt�@�C
    private float radianTheta = 0;
    private float radianPhi = 0;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();
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
    }

    // Update is called once per frame
    void Update()
    {
        // �L�[����
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();

        // ���ʍ��W�̍X�V
        radianTheta += thetaSpeed * inputMove.y * Mathf.Deg2Rad;
        radianPhi += phiSpeed * inputMove.x * Mathf.Deg2Rad;

        // ���ʍ��W�𒼌����W�ɕϊ�
        x = radius * Mathf.Cos(radianTheta) * Mathf.Cos(radianPhi);
        y = radius * Mathf.Sin(radianTheta);
        z = radius * Mathf.Cos(radianTheta) * Mathf.Sin(radianPhi);

        Debug.Log("theta:" + radianTheta);
        Debug.Log("phi:" + radianPhi);

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
}
