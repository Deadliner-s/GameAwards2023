using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountTimer : MonoBehaviour
{
    public float time;
    public TextMeshProUGUI Timertext;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (0 < time)
        {
            time -= Time.deltaTime;
            Timertext.text = time.ToString("F1");
        }
    }
}