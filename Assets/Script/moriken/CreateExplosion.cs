using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosion : MonoBehaviour
{
    // エフェクト
    [SerializeField]
    [Tooltip("小爆発")]
    private GameObject smallEffect;
    [SerializeField]
    [Tooltip("大爆発")]
    private GameObject bigEffect;

    [SerializeField]
    [Tooltip("1度目の爆発までのフレーム")]
    private int firstExplosionStartFlame;   
    [SerializeField]
    [Tooltip("2度目の爆発までのフレーム")]
    private int secondExplosionStartFlame;  // ゲームがスタートした時からのフレームなので注意
    [SerializeField]
    [Tooltip("3度目の爆発までのフレーム")]
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
       
        // 最初の爆発の処理(小爆発の処理)
        if (flame >= firstExplosionStartFlame && flame <= secondExplosionStartFlame)
        {
            // 2度目の爆発まで何度か爆発する
            if ((flame % 50) == 0)      // 現在は10の倍数のフレームごとに設定してる (flame % 〇←の数字の倍数で爆発する)
            {
                // ランダムで爆破する場所を変えている
                randX = Random.Range(-3.00f, 3.00f);
                randY = Random.Range(4.00f, 7.00f);
                vec = new Vector3(gameObject.transform.position.x + randX,
                gameObject.transform.position.y + randY,
                gameObject.transform.position.z);
                
                GameObject InstantiateEffect
                = GameObject.Instantiate(smallEffect, vec, Quaternion.identity);
            }
        }

        // 2度目の爆発の処理(小爆発の処理)
        if (flame >= secondExplosionStartFlame && flame <= thirdExplosionStartFlame)
        {
            // 最後の爆発まで何度か爆発する
            if (flame == secondExplosionStartFlame)      // 現在は20の倍数のフレームごとに設定してる
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

        // 最後の爆発の処理(大爆発の処理)
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