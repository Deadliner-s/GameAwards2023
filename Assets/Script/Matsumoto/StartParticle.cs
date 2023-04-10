using UnityEngine;
using UnityEngine.Events;

public class StartParticle : MonoBehaviour
{
    [SerializeField]
    [Tooltip("発生させるエフェクト(パーティクル)")]
    private ParticleSystem particle;
    private ParticleSystem newParticle;

    private void Start()
    {
        // エフェクトを生成
        newParticle = Instantiate(particle);
        newParticle.transform.position = this.transform.position;
        newParticle.transform.rotation = this.transform.rotation;
        newParticle.Play();
    }

    private void Update()
    {
        if (newParticle != null)
        {
            // エフェクトの位置を更新
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