using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    private ParticleSystem ps;
    // Start is called before the first frame update
    void Start()
    {
        ps = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        // �G�t�F�N�g���I��������
        if (ps.isStopped)
        {
            // �G�t�F�N�g�̃I�u�W�F�N�g���폜
            Destroy(gameObject);
        }
    }
}
