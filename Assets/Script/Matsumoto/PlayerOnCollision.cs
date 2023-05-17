using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnCollision : MonoBehaviour
{
    private GameObject playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            playerManager.GetComponent<PlayerHp>().Damage(collision);
        }
    }

    //public float GetPlayerDamage()
    //{
    //    return PlayerDamage;
    //}


}
