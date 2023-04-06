using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLightning : MonoBehaviour
{
//    public GameObject prefabObject;
    public LineRenderer lineRenderer;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer.enabled = false;
    }

    // Update is called once per frame
    private void Update()
    {
        time += Time.deltaTime;

        if(time >= 2.0f)
        {
            lineRenderer.enabled = true;
        }

        /*
        // �w�肳�ꂽ�v���n�u�I�u�W�F�N�g�����݂���ꍇ�ɂ̂� LineRenderer ��L��������
        if (prefabObject != null)
        {
            lineRenderer.enabled = true;
        }
        else
        {
            lineRenderer.enabled = false;
        }
        */
    }
}