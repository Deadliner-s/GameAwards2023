using UnityEngine;
using UnityEngine.Events;

public class StartParticle : MonoBehaviour
{
    [SerializeField]
    [Header("�~�T�C���̃g���C���G�t�F�N�g")]
    private ParticleSystem missileTrail;
    private ParticleSystem TrailParticle;

    [SerializeField]
    [Header("�~�T�C�������������Ƃ��̃G�t�F�N�g")]
    private ParticleSystem missileExplosion;
    private ParticleSystem ExplosionParticle;

    float DestroyTime = 1.0f;

    private void Start()
    {
        // �G�t�F�N�g�𐶐�
        TrailParticle = Instantiate(missileTrail);
        // MissileObj���^�O����
        GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
        // �~�T�C���I�u�W�F�N�g�̎q�ɂ���
        TrailParticle.transform.parent = missileObj.transform;
        TrailParticle.transform.position = this.transform.position;
        TrailParticle.transform.rotation = this.transform.rotation;
        TrailParticle.Play();
    }

    private void Update()
    {
        if (TrailParticle != null)
        {
            // �G�t�F�N�g�̈ʒu���X�V
            TrailParticle.transform.position = this.transform.position;
            TrailParticle.transform.rotation = this.transform.rotation;
        }
    }

    private void OnDestroy()
    {
        if (TrailParticle != null)
        {
            TrailParticle.Stop();
            Destroy(TrailParticle.gameObject,DestroyTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // �v���C���[���^�[�Q�b�g�ɓ���������g���C���G�t�F�N�g������
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Target")
        {
            if (TrailParticle != null)
            {
                TrailParticle.Stop();
                Destroy(TrailParticle.gameObject,DestroyTime);
            }
        }
        // �v���C���[�̒e�ɓ��������甚���G�t�F�N�g�𐶐�
        if(this.gameObject.tag == "Target")
        {
            if (collision.gameObject.tag == "PlayerBullet")
            {
                ExplosionParticle = Instantiate(missileExplosion);
                //// MissileObj���^�O����
                //GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
                //// �~�T�C���I�u�W�F�N�g�̎q�ɂ���
                //ExplosionParticle.transform.parent = missileObj.transform;
                ExplosionParticle.transform.position = this.transform.position;
                ExplosionParticle.transform.rotation = this.transform.rotation;
                ExplosionParticle.Play();
            }
        }
    }
}