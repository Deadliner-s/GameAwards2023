using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public GameObject HP;
    GameObject HpGauge;
    Shield shield;

    float NowHp;
    float OldHp;

    float Damage;

    // Start is called before the first frame update
    void Start()
    {
        
        HpGauge = GameObject.Find("Cube");
        shield = HpGauge.GetComponent<Shield>();

        NowHp = shield.Hp;
        Damage = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 HpScale = this.transform.localScale;

        HpGauge = GameObject.Find("Cube");
        shield = HpGauge.GetComponent<Shield>();
        NowHp = shield.Hp;

        if (OldHp > NowHp)
        {
            HpScale.x -= Damage;
            HP.transform.localScale = HpScale;
        }
        OldHp = NowHp;
    }
}
