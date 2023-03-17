using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpBar : MonoBehaviour
{
    int maxHp = 100;
    int currentHp;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 1;

        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
    }

    private void Update()
    {
        slider.value = currentHp;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "PlayerBullet")
        {
            int damage = 10;
            Debug.Log("damage : " + damage);

            currentHp = currentHp - damage;
            Debug.Log("After currentHp : " + currentHp);

            slider.value = (float)currentHp / (float)maxHp; ;
            Debug.Log("slider.value : " + slider.value);
        }
    }
}
