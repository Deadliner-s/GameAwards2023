using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFollow : MonoBehaviour
{
    public float DistanceX;
    public float DistanceY;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 P = GameObject.Find("Player").transform.position;
        Vector3 Player;
        Player.x = P.x;
        Player.y = P.y;
        Player.z = 0;

        float x = Player.x + DistanceX;
        float y = Player.y + DistanceY;
        transform.position = new Vector3(x, y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
