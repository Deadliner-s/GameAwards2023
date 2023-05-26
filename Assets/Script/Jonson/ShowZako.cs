using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowZako : MonoBehaviour
{
    public float Speed = 0.1f;
    Vector3 move;
    // Start is called before the first frame update
    void Start()
    {
        float randY = Random.Range(-5.0f, 5.0f);
        randY = randY / 10;
        move = new Vector3(-1.0f, randY, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(-move.y * 90.0f,270.0f, 90.0f);
        gameObject.transform.position += move * Speed * Time.timeScale;
    }
}
