using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject LaserA;
    
    public float scaleSpeedP = 0.01f;
    public float scaleSpeedM = 0.01f;

    private float timer = 0.0f;
    private Vector3 StartScale;
    private Vector3 currentScale;
    private float ZeroX = 0.00001f;
    private float ZeroY = 0.00001f;

    // Start is called before the first frame update
    void Start()
    {
        StartScale = transform.localScale;
        transform.localScale = new Vector3(ZeroX, ZeroY, 1);
        currentScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        currentScale = transform.localScale;

        if (timer >= LaserA.GetComponent<LaserHead>().wait && timer < LaserA.GetComponent<LaserHead>().wait + LaserA.GetComponent<LaserHead>().LaserTime
            && transform.localScale.x <= StartScale.x && transform.localScale.y <= StartScale.y)
        {
            // �X�P�[���𑝂₷
            transform.localScale += new Vector3(scaleSpeedM, scaleSpeedM, 0);

            if(transform.localScale.x >= StartScale.x || transform.localScale.y >= StartScale.y)
            {
                transform.localScale = StartScale;
            }
        }

        if (timer >= LaserA.GetComponent<LaserHead>().wait + LaserA.GetComponent<LaserHead>().LaserTime)
        {
            // �X�P�[�������炷
            transform.localScale -= new Vector3(scaleSpeedP, scaleSpeedP, 0);
        }

        // �X�P�[����0�ȉ��ɂȂ�����I�u�W�F�N�g���폜����
        if (timer >= LaserA.GetComponent<LaserHead>().wait + LaserA.GetComponent<LaserHead>().LaserTime 
            && transform.localScale.x <= 0.0f || transform.localScale.x <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}