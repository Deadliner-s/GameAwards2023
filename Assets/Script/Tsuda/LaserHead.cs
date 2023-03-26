using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour
{
    public float speed = 5f;  // �ړ����x
    public GameObject otherObject;  // ��������v���n�u�I�u�W�F�N�g
    public float lifetime = 5f;  // �I�u�W�F�N�g�̎����i�b�j

    private Vector3 targetScreenPosition;  // �ڕW�X�N���[�����W
    private Vector3 targetWorldPosition;  // �ڕW���[���h���W
    private Camera mainCamera;  // ���C���J����
    private float timer;  // �^�C�}�[
    GameObject newObj;

    void Start()
    {
        mainCamera = Camera.main;  // ���C���J�������擾����

        targetScreenPosition.x = Random.Range(200.0f, 1720.0f);  // 0�`1920�̃����_���Ȑ��l
        targetScreenPosition.y = Random.Range(150.0f, 830.0f);  // 0�`1080�̃����_���Ȑ��l
        targetScreenPosition.z = 2.0f;
        targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // �ڕW�X�N���[�����W�����[���h���W�ɕϊ�����

        transform.LookAt(targetWorldPosition);  // �ڕW���W�̕���������

        newObj = Instantiate(otherObject, targetWorldPosition, transform.rotation);  // �x��UI�̐���

        timer = lifetime;  // �^�C�}�[��ݒ肷��
    }

    void Update()
    {
        timer -= Time.deltaTime;  // �^�C�}�[�����Z����

        if (timer >= lifetime - 12.0f)
        {
            targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // �ڕW�X�N���[�����W�����[���h���W�ɕϊ�����
            transform.LookAt(targetWorldPosition);

//            newObj.transform.position = targetWorldPosition;
 //           newObj.transform.LookAt(targetWorldPosition);
        }


        if (timer <= lifetime - 12.0f)
        {
            transform.position += transform.forward * speed * Time.deltaTime;  // �ڕW���W�̕����Ɉړ�����
        }
     
        if (timer <= 0)
        {
            Destroy(gameObject);  // �I�u�W�F�N�g���폜����
        }
    }
}

