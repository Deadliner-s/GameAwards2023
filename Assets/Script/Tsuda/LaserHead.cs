using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour
{
    public static LaserHead instance;

    public float speed = 5f;  // �ړ����x
    public GameObject otherObject;  // ��������v���n�u�I�u�W�F�N�g
    public float lifetime = 5f;  // �I�u�W�F�N�g�̎����i�b�j
    public int Split;

//    private PhaseManager.Phase currentPhase;  // ���݃t�F�C�Y

    private float splitX;
    private float splitY;
    private Vector3 targetScreenPosition;  // �ڕW�X�N���[�����W
    private Vector3 targetWorldPosition;  // �ڕW���[���h���W
    private Camera mainCamera;  // ���C���J����
    private float timer;  // �^�C�}�[
    GameObject newObj;

    void Start()
    {
  //      currentPhase = PhaseManager.instance.GetPhase();  // �t�F�C�Y�擾
    }

    void Update()
    {        
//        currentPhase = PhaseManager.instance.GetPhase();  // �t�F�C�Y�擾        

//        if (currentPhase == PhaseManager.Phase.Speed_Phase)
//        {
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
//        }
    }

    public void SetSplit(int num)
    {
        mainCamera = Camera.main;  // ���C���J�������擾����

        Split = num;

        switch (Split)
        {
            case 1: splitX = 1; splitY = 3; break;
            case 2: splitX = 3; splitY = 3; break;
            case 3: splitX = 5; splitY = 3; break;
            case 4: splitX = 1; splitY = 1; break;
            case 5: splitX = 3; splitY = 1; break;
            case 6: splitX = 5; splitY = 1; break;
        }

        targetScreenPosition.x = 320 * splitX;
        targetScreenPosition.y = 270 * splitY;
        targetScreenPosition.z = 1.3f;
        targetWorldPosition = mainCamera.ScreenToWorldPoint(targetScreenPosition);  // �ڕW�X�N���[�����W�����[���h���W�ɕϊ�����

        transform.LookAt(targetWorldPosition);  // �ڕW���W�̕���������

        newObj = Instantiate(otherObject, targetWorldPosition, transform.rotation);  // �x��UI�̐���

        timer = lifetime;  // �^�C�}�[��ݒ肷��
    }
}

