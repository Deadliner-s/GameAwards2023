using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Fade : MonoBehaviour
{
    public GameObject LaserA;

    public float FadeSpeed;
    public int type;    

    private float timer = 0.0f; // �o�ߎ���
    private float decrease;
    private bool played = false; // �Đ��������ǂ����̃t���O  
    private Vector3 StartScale;
    private Vector3 currentScale;

    [SerializeField] private ParticleSystem particleSystem; // �Đ�����p�[�e�B�N���I�u�W�F�N�g

    void Start()
    {
        decrease = transform.localScale.x / FadeSpeed;

        StartScale = transform.localScale;
        particleSystem.Stop();
    }

    private void Update()
    {        
        // �o�ߎ��Ԃ����Z
        timer += Time.deltaTime;        

        // �w�肵�����Ԃ��o�߂�����Đ�
        if (timer >= LaserA.GetComponent<LaserHead>().wait && !played)
        {
            particleSystem.Play();
            played = true;
        }

        transform.localScale = currentScale;

        switch (type)
        {
            case 1:
                if (!played)
                {
                    currentScale.x = 0.0000001f;
                    currentScale.z = 0.0000001f;
                }

                if (played && currentScale.x <= StartScale.x && currentScale.z <= StartScale.z && timer <= LaserA.GetComponent<LaserHead>().wait + LaserA.GetComponent<LaserHead>().LaserTime)
                {
                    currentScale.x += decrease;
                    currentScale.z += decrease;                    
                }

                if (timer >= LaserA.GetComponent<LaserHead>().wait + LaserA.GetComponent<LaserHead>().LaserTime)
                {
                    currentScale.x -= decrease;
                    currentScale.z -= decrease;
                }

                if (currentScale.x <= 0.0f && currentScale.x <= 0.0f)
                {
                    Destroy(this.gameObject);
                }

                break;

            case 2:
                
                break;
        }                        
    }

}
