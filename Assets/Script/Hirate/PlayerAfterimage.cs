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
    private Vector3[] latePos = new Vector3[5]; // ���O�̍��W
    private float elapsedTime; // �o�ߎ���

    // Start is called before the first frame update
    void Start()
    {
        // ���݂̍��W�Ƀv���C���[���W����
        pos = player.transform.position;
        // �c���𐶐�
        for (int i = 0; i < 5; i++)
        {
            obj[i] = Instantiate(
                afterimageObj, // �c���̃I�u�W�F�N�g
                new Vector3(pos.x, pos.y, pos.z), // ���݂̍��W
                Quaternion.identity); // ��]
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �o�ߎ���
        //elapsedTime += Time.deltaTime;
        for (int i = 0; i < 5; i++)
        {
            if (i < 4) {
                latePos[i] = latePos[i + 1];
            }
            obj[i].transform.position = latePos[i];
            Debug.Log(obj[0].transform.position);
        }
        // ���O�̍��W�Ɍ��݂̍��W����
        latePos[5 - 1] = pos;
        // ���݂̍��W�Ƀv���C���[���W����
        pos = player.transform.position;
    }
}
