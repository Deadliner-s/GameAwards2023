using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private float playerDistance = 0.0f; // �v���C���[�܂ł̋���
    private Vector3 centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        //�J�����ƃv���C���[�̋����𒲂ׂ�
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // ���̒���

        // ��̈ʒu��ݒ� (x,y,z)���W
        centerPoint = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // �J�������v���C���[�ɒǏ]������
        // ��x�v���C���[�̍��W�Ɠ����ɂ�����
        Vector3 pos = player.transform.position;
        pos += centerPoint;
        // �J�����������Ă�������Ƃ͋t�����Ƀv���C���[���痣��
        pos -= transform.forward * playerDistance;
        // �V�����J�����̈ʒu
        transform.position = pos;

        this.transform.LookAt(enemy.transform.position);
    }
}
