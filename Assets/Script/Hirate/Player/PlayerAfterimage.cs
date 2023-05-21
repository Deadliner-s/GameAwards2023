using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAfterimage : MonoBehaviour
{
    //[Header("�I�u�W�F�N�g�ݒ�")]
    //[Tooltip("�v���C���[")]
    private GameObject player;
    [Tooltip("�c��")]
    [SerializeField] GameObject afterimageObj;
    private GameObject[] obj = new GameObject[5];
    private Vector3 pos; // ���݂̍��W
    private Quaternion quaternion;
    private Quaternion[] lateQuaternion = new Quaternion[5];
    private Vector3[] latePos = new Vector3[5]; // ���O�̍��W

    private GameObject playerManager;
    //private PlayerMove playerMove;
    private int saveObj;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerManager = GameObject.Find("PlayerManager");
        // PlayerMove�̃X�N���v�g�̒��g���g�p����p
        //playerMove = player.GetComponent<PlayerMove>();

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
        if (player == null)
        {
            player = GameObject.Find("Player");
        }

        // �}�j���[�o�J�n��
        if (playerManager.GetComponent<PlayerMove>().maneverFlg == true)
        {
            if (playerManager.GetComponent<PlayerMove>().inputMove.y >= 0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x >= 0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.y <= -0.5f ||
                playerManager.GetComponent<PlayerMove>().inputMove.x <= -0.5f)
            {
                //---- ����
                // �c���𐶐�
                for (int i = 4; i >= 0; i--)
                {
                    if (obj[i] != null) { continue; } // null�ł͂Ȃ��Ȃ��΂�
                    obj[i] = Instantiate(
                    afterimageObj,                 // �c���̃I�u�W�F�N�g
                    new Vector3(9999, 9999, 9999), // ���W
                    quaternion);                   // ��]
                    obj[i].transform.parent = player.transform;
                }

                // ���o�p
                saveObj = 0;
                // ���݂̍��W�Ƀv���C���[�̍��W����
                pos = player.transform.position;
                // ���݂̉�]�Ƀv���C���[�̉�]����
                quaternion = player.transform.rotation;
                // ���O�̍��W�Ɍ��݂̍��W����
                latePos[5 - 1] = pos;
                // ���O�̉�]�Ɍ��݂̉�]����
                lateQuaternion[5 - 1] = quaternion;
                //---- �c���𓮂���
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

                    //if ((obj[i].transform.position.y - obj[i + 1].transform.position.y) < 0.3f)
                    //{
                        //Debug.Log(i + "�F" + obj[i].transform.position);
                    //}
                }
            }
        }

        // �}�j���[�o�I����
        if (playerManager.GetComponent<PlayerMove>().maneverFlg == false && saveObj < 5) {
            // ���f���̍폜����
            //Destroy(obj[saveObj]);
            // �c���𐶐�
            for (int i = 4; i >= 0; i--)
            {
                if (obj[i] == null) { continue; }
                obj[i].transform.position = new Vector3(9999, 9999, 9999);
            }
            // ���o�p
            saveObj++;
        }
    }
}
