using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveManever : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;

    private float playerDistance = 0.0f; // �v���C���[�܂ł̋���
    private Vector3 centerPoint;
    private GameObject parentObj;
    private CameraMove cameraMove;

    // Start is called before the first frame update
    void Start()
    {
        //�J�����ƃv���C���[�̋����𒲂ׂ�
        Vector3 toPlayer =
            player.transform.position - transform.position;
        playerDistance = toPlayer.magnitude; // ���̒���

        parentObj = transform.parent.gameObject;
        cameraMove = parentObj.GetComponent<CameraMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraMove.bInput)
        {
            // �e(�v���C���[)�ɒǏ]������
            // ��x�v���C���[�̍��W�Ɠ����ɂ�����
            Vector3 pos = player.transform.position;
            pos += centerPoint;
            // �{���̃J�����������Ă�������Ƃ͋t�����Ƀv���C���[���痣��
            pos -= transform.forward * playerDistance;
            // �V�����_�~�[�I�u�W�F�N�g�̈ʒu
            transform.position = pos;
            this.transform.LookAt(enemy.transform.position);
        }
    }
}
