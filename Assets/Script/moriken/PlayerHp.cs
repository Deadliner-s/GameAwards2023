using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�V�[���̈ړ��������s���@�\
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    
    public GameObject GaugeObj;
    Slider HpGauge;

    public float PlayerHP;
    float damage;

    public float MaxInvflame;
    float Invflame;

    bool UseFlag;

    [SerializeField] ParticleSystem particle;
    [SerializeField] Color[] color = new Color[3];

    // Start is called before the first frame update
    void Start()
    {        
        HpGauge = GaugeObj.GetComponent<Slider>();
        HpGauge.maxValue = PlayerHP;
        HpGauge.value = PlayerHP;
        //�t���O���\������
        UseFlag = false;
     
    }

    // Update is called once per frame
    void Update()
    {
        var main = particle.main;
        // �G�ɂԂ��������ɃV�[���h��\������
        if (UseFlag == true)
        {   
            Invflame++;
        }

        // ���G���ԂɊւ��鏈��
        if (MaxInvflame < Invflame)
        {
            UseFlag = false;
            Invflame = 0;
        }

        if(PlayerHP >= 66)
        { 
            main.startColor = color[0];
        }
        else if(PlayerHP >= 33)
        {
            main.startColor = color[1];
        }
        else
        {
            main.startColor = color[2];
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (Invflame == 0)
        {
            // �����Փ˂�������I�u�W�F�N�g�̃^�O��"Enemy"�Ȃ�Β��̏��������s
            if (collision.gameObject.CompareTag("Enemy"))
            {
                // "Enemy"�^�O�����Ă���I�u�W�F�N�g�ɂ���"PlayerDamage"�ϐ����󂯂Ƃ�
                damage = collision.gameObject.GetComponent<Damage>().PlayerDamage;
                PlayerHP -= damage;
                HpGauge.value -= damage;
                
                if (PlayerHP >= 0)
                {
                    UseFlag = true;
                }
                else
                {
                    Destroy(this.gameObject);
                    //�V�[���ړ�
                    SceneManager.LoadScene("SceneGameOver");
                }
            }
        }
    }
}
