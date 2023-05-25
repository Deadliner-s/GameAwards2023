using UnityEngine;
using UnityEngine.Events;

public class StartParticle : MonoBehaviour
{
    [SerializeField]
    [Header("ミサイルのトレイルエフェクト")]
    private ParticleSystem missileTrail;
    private ParticleSystem TrailParticle;

    [SerializeField]
    [Header("ミサイルが爆発したときのエフェクト")]
    private ParticleSystem missileExplosion;
    private ParticleSystem ExplosionParticle;

    float DestroyTime = 1.0f;

    private void Start()
    {
        // エフェクトを生成
        TrailParticle = Instantiate(missileTrail);
        // MissileObjをタグ検索
        GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
        // ミサイルオブジェクトの子にする
        TrailParticle.transform.parent = missileObj.transform;
        TrailParticle.transform.position = this.transform.position;
        TrailParticle.transform.rotation = this.transform.rotation;
        TrailParticle.Play();
    }

    private void Update()
    {
        if (TrailParticle != null)
        {
            // エフェクトの位置を更新
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
        // プレイヤーかターゲットに当たったらトレイルエフェクトを消す
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Target")
        {
            if (TrailParticle != null)
            {
                TrailParticle.Stop();
                Destroy(TrailParticle.gameObject,DestroyTime);
            }
        }
        // プレイヤーの弾に当たったら爆発エフェクトを生成
        if(this.gameObject.tag == "Target")
        {
            if (collision.gameObject.tag == "PlayerBullet")
            {
                ExplosionParticle = Instantiate(missileExplosion);
                //// MissileObjをタグ検索
                //GameObject missileObj = GameObject.FindGameObjectWithTag("MissileObj");
                //// ミサイルオブジェクトの子にする
                //ExplosionParticle.transform.parent = missileObj.transform;
                ExplosionParticle.transform.position = this.transform.position;
                ExplosionParticle.transform.rotation = this.transform.rotation;
                ExplosionParticle.Play();
            }
        }
    }
}