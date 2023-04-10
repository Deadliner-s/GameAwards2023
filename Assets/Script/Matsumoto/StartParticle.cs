using UnityEngine;
using UnityEngine.Events;

public class StartParticle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("����������G�t�F�N�g(�p�[�e�B�N��)")]
    private ParticleSystem particle;
    private ParticleSystem newParticle;

    private void Start()
    {
        // �G�t�F�N�g�𐶐�
        newParticle = Instantiate(particle);
        newParticle.transform.position = this.transform.position;
        newParticle.transform.rotation = this.transform.rotation;
        newParticle.Play();
    }

    private void Update()
    {
        if (newParticle != null)
        {
            // �G�t�F�N�g�̈ʒu���X�V
            newParticle.transform.position = this.transform.position;
            newParticle.transform.rotation = this.transform.rotation;
        }
    }

    private void OnDestroy()
    {
        if (newParticle != null)
        {
            Destroy(newParticle.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (newParticle != null)
            {
                Destroy(newParticle.gameObject);
            }
        }
    }
}