using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosion : MonoBehaviour
{
    // �G�t�F�N�g
    [SerializeField]
    [Tooltip("������")]
    private GameObject smallEffect;
    [SerializeField]
    [Tooltip("�唚��")]
    private GameObject bigEffect;

    [SerializeField]
    [Tooltip("1�x�ڂ̔����܂ł̃t���[��")]
    private int firstExplosionStartFlame;   
    [SerializeField]
    [Tooltip("2�x�ڂ̔����܂ł̃t���[��")]
    private int secondExplosionStartFlame;  // �Q�[�����X�^�[�g����������̃t���[���Ȃ̂Œ���
    [SerializeField]
    [Tooltip("3�x�ڂ̔����܂ł̃t���[��")]
    private int thirdExplosionStartFlame;   

    private int flame;

    private Vector3 vec;
    private float randX;
    private float randY;

    // Start is called before the first frame update
    void Start()
    {
        flame = 0;
        vec = new Vector3(gameObject.transform.position.x, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z);

        randX = Random.Range(-3.0f, 3.0f);
        randY = Random.Range(1.0f, 7.0f);
    }

    // Update is called once per frame
    void Update()
    {
        flame++;
       
        // �ŏ��̔����̏���(�������̏���)
        if (flame >= firstExplosionStartFlame && flame <= secondExplosionStartFlame)
        {
            // 2�x�ڂ̔����܂ŉ��x����������
            if ((flame % 50) == 0)      // ���݂�10�̔{���̃t���[�����Ƃɐݒ肵�Ă� (flame % �Z���̐����̔{���Ŕ�������)
            {
                // �����_���Ŕ��j����ꏊ��ς��Ă���
                randX = Random.Range(-3.00f, 3.00f);
                randY = Random.Range(4.00f, 7.00f);
                vec = new Vector3(gameObject.transform.position.x + randX,
                gameObject.transform.position.y + randY,
                gameObject.transform.position.z);
                
                GameObject InstantiateEffect
                = GameObject.Instantiate(smallEffect, vec, Quaternion.identity);
            }
        }

        // 2�x�ڂ̔����̏���(�������̏���)
        if (flame >= secondExplosionStartFlame && flame <= thirdExplosionStartFlame)
        {
            // �Ō�̔����܂ŉ��x����������
            if (flame == secondExplosionStartFlame)      // ���݂�20�̔{���̃t���[�����Ƃɐݒ肵�Ă�
            {
                randX = Random.Range(-3.00f, 3.00f);
                randY = Random.Range(0.00f, 4.00f);
                vec = new Vector3(gameObject.transform.position.x + randX,
                gameObject.transform.position.y + randY,
                gameObject.transform.position.z);

                GameObject InstantiateEffect
                    = GameObject.Instantiate(smallEffect, vec, Quaternion.identity);
            }
        }

        // �Ō�̔����̏���(�唚���̏���)
        if (flame == thirdExplosionStartFlame)
        {
            vec = new Vector3(gameObject.transform.position.x ,
            gameObject.transform.position.y + 5.0f,
            gameObject.transform.position.z);

            GameObject InstantiateEffect
                = GameObject.Instantiate(bigEffect, vec, Quaternion.identity);
        }
    }
}