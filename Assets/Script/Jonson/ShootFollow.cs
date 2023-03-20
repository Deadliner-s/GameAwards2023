using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFollow : MonoBehaviour
{
    GameObject Player;
    Vector3 move;
    public float DistanceX;
    public float DistanceY;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 backDirection = Player.transform.forward;
        Vector3 upDirection   = Player.transform.up;
        backDirection.y       = 0;
        upDirection.x         = 0;
        upDirection.z         = 0;
        backDirection         = backDirection.normalized;
        upDirection           = upDirection.normalized;
        transform.position    = Player.transform.position;
        transform.position   += backDirection * DistanceX;
        transform.position   += upDirection * DistanceY;
    }
}
