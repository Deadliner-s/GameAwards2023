using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    // �I�u�W�F�N�g�̐ݒ�
    [Header("��u���ݒ�")]
    [Tooltip("")]
    [SerializeField] float time;
    //[Tooltip("")]
    //[SerializeField] float move;
    //[Tooltip("")]
    //[SerializeField] float move;
    //[Tooltip("")]
    //[SerializeField] float move;
    //[Tooltip("")]
    //[SerializeField] bool move = false;
    [Tooltip("�t�F�[�h�A�E�g")]
    [SerializeField] bool fadeOut = false;


    // ���������̔���p
    private bool bInput = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            bInput = true;
        }
        // �����ꂽ���Ɏ��s
        if (bInput)
        {
            gameObject.transform.DOShakePosition(
                5f,
                0.3f,
                5,
                10,
                false,
                false
                ).OnComplete(() => bInput = false);
        }
    }
}
