using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : MonoBehaviour
{
    // “_–Å‚³‚¹‚é‘ÎÛ
    [SerializeField] private Renderer _target;
    // “_–ÅŽüŠú[s]
    private float _cycle = 0.5f;

    private double _time;

    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 1.0f) { _cycle = 0.2f; }        


        // “à•”Žž‚ðŒo‰ß‚³‚¹‚é
        _time += Time.deltaTime;

        // ŽüŠúcycle‚ÅŒJ‚è•Ô‚·’l‚ÌŽæ“¾
        // 0`cycle‚Ì”ÍˆÍ‚Ì’l‚ª“¾‚ç‚ê‚é
        var repeatValue = Mathf.Repeat((float)_time, _cycle);

        // “à•”Žžtime‚É‚¨‚¯‚é–¾–Åó‘Ô‚ð”½‰f
        _target.enabled = repeatValue >= _cycle * 0.5f;
    }
}
