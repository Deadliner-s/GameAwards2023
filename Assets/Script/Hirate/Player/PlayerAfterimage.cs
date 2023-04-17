using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimage : MonoBehaviour
{
    [Header("�I�u�W�F�N�g�ݒ�")]
    [Tooltip("�v���C���[")]
    [SerializeField] GameObject player;
    [Tooltip("�c��")]
    [SerializeField] GameObject afterimageObj;
    private GameObject[] obj = new GameObject[5];
    private Vector3 pos; // ���݂̍��W
    private Quaternion quaternion;
    private Quaternion[] lateQuaternion = new Quaternion[5];
    private Vector3[] latePos = new Vector3[5]; // ���O�̍��W
    private PlayerMove playerMove;
    private int saveObj;

    // Start is called before the first frame update
    void Start()
    {
        // PlayerMove�̃X�N���v�g�̒��g���g�p����p
        playerMove = gameObject.GetComponent<PlayerMove>();
        //// ���݂̍��W�Ƀv���C���[�̍��W����
        //pos = player.transform.position;
        //// ���݂̉�]�Ƀv���C���[�̉�]����
        //quaternion = player.transform.rotation;
        //// �c���𐶐�
        //for (int i = 0; i < 5; i++) {
        //    obj[i] = Instantiate(
        //        afterimageObj, // �c���̃I�u�W�F�N�g
        //        pos,           // ���݂̍��W
        //        quaternion);   // ��]
        //}
        // ���o�p
        saveObj = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // �}�j���[�o�J�n��
        if (playerMove.maneverFlg == true)
        {
            if (playerMove.inputMove.y >= 0.5f ||
                playerMove.inputMove.x >= 0.5f ||
                playerMove.inputMove.y <= -0.5f ||
                playerMove.inputMove.x <= -0.5f)
            {
                // ���o�p
                saveObj = 0;
                //---- ����
                // ���݂̍��W�Ƀv���C���[�̍��W����
                pos = player.transform.position;
                // ���݂̉�]�Ƀv���C���[�̉�]����
                quaternion = player.transform.rotation;
                // �c���𐶐�
                for (int i = 4; i >= 0; i--)
                {
                    if (obj[i] != null) { continue; } // null�ł͂Ȃ��Ȃ��΂�
                    obj[i] = Instantiate(
                    afterimageObj, // �c���̃I�u�W�F�N�g
                    pos,           // ���W
                    quaternion);   // ��]
                }
                //---- �c���𓮂���
                // ���ꂼ��̎c���ɒ��O�̍��W�Ɖ�]�������Ă���
                for (int i = 0; i < 5; i++)
                {
                    if (i < 4)
                    {
                        // ���W
                        latePos[i] = latePos[i + 1];
                        // ��]
                        lateQuaternion[i] = lateQuaternion[i + 1];
                    }
                    // ���O�̍��W�Ɍ��݂̍��W����
                    obj[i].transform.position = latePos[i];
                    // ���O�̉�]�Ɍ��݂̉�]����
                    obj[i].transform.rotation = lateQuaternion[i];
                }
                // ���O�̍��W�Ɍ��݂̍��W����
                latePos[5 - 1] = pos;
                // ���O�̉�]�Ɍ��݂̉�]����
                lateQuaternion[5 - 1] = quaternion;
                // ���݂̍��W�Ƀv���C���[�̍��W����
                pos = player.transform.position;
                // ���݂̉�]�Ƀv���C���[�̉�]����
                quaternion = player.transform.rotation;
            }
        }

        // �}�j���[�o�I����
        if (playerMove.maneverFlg == false && saveObj < 5) {
            // ���f���̍폜����
            Destroy(obj[saveObj]);
            // ���o�p
            saveObj++;
        }
    }
}
