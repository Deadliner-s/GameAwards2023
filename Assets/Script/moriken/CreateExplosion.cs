using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExplosion : MonoBehaviour
{
    // �G�t�F�N�g
    [SerializeField]
    [Tooltip("������")]
    private GameObject smallEffect;
    [SerializeField]
    [Tooltip("�唚��")]
    private GameObject bigEffect;

    [SerializeField]
    private int firstExplosionStartFlame;
    [SerializeField]
    private int secondExplosionStartFlame;
    [SerializeField]
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

       
        if (flame >= firstExplosionStartFlame && flame <= secondExplosionStartFlame)
        {
            if ((flame % 10) == 0)
            {
                randX = Random.Range(-3.00f, 3.00f);
                randY = Random.Range(4.00f, 7.00f);
                vec = new Vector3(gameObject.transform.position.x + randX,
                gameObject.transform.position.y + randY,
                gameObject.transform.position.z);
                // �G�t�F�N�g����
                GameObject InstantiateEffect
                = GameObject.Instantiate(smallEffect, vec, Quaternion.identity);
            }
        }

        if (flame >= secondExplosionStartFlame && flame <= thirdExplosionStartFlame)
        {
            if ((flame % 20) == 0)
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