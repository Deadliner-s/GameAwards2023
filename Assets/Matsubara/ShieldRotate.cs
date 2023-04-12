using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldRotate : MonoBehaviour
{
    [SerializeField]
    [Tooltip("�Ώە�(��������)")]
    private GameObject target;

    [SerializeField]
    private float rotateY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        // Quaternion qrot = Quaternion.LookRotation(target.transform.position - this.transform.position) * Quaternion.Euler(0, 45f, 0);
        // this.transform.rotation = qrot;

        // �Ώە��Ǝ������g�̍��W����x�N�g�����Z�o
        Vector3 vector3 = target.transform.position - this.transform.position;
        // �����㉺�����̉�]�͂��Ȃ��悤�ɂ�������Έȉ��̂悤�ɂ���B
        // vector3.y = 0f;

        // Quaternion(��]�l)���擾
        Quaternion quaternion = Quaternion.LookRotation(vector3);

        // �Z�o������]�l�����̃Q�[���I�u�W�F�N�g��rotation�ɑ��
        this.transform.rotation = quaternion * Quaternion.Euler(0, rotateY, 0);
    }
}
