using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove_new : MonoBehaviour
{
    [SerializeField]
    public GameObject player;               // �v���C���[�̃I�u�W�F�N�g
    private Vector3 playerPos;

    [Header("�J�����������n�߂鎞�̃v���C���[�̈ʒu")]
    public float StartMoveTop = 1.2f;       // �J�����������n�߂鎞�̃v���C���[�̈ʒu
    public float StartMoveBottom = 0.5f;

    [Header("�J�����̈ړ����")]
    public float LimitTop = 2.0f;           // �J�����̈ړ����
    public float LimitBottom = -0.8f;


    private Vector3 nextPosition;   // �ړ���̈ʒu
    private Vector3 initialPos;     // �J�����̏����ʒu

    // Start is called before the first frame update
    void Start()
    {
        nextPosition = transform.position; ;
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        nextPosition = this.transform.position;
        playerPos = player.transform.position;

        // Player���ړ��������A�J�������ړ�����
        if (playerPos.y <= LimitTop)
        {
            if (playerPos.y >= StartMoveTop)
            {
                nextPosition.y = initialPos.y + playerPos.y - StartMoveTop;
            }
        }
        if (playerPos.y >= LimitBottom)
        {
            if (playerPos.y <= StartMoveBottom)
            {
                nextPosition.y = initialPos.y + playerPos.y - StartMoveBottom;
            }
        }

        // �ړ�
        transform.position = nextPosition;
    }
}
