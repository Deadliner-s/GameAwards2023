using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //---- ƒCƒ“ƒXƒyƒNƒ^[‚ÉŒ©‚¹‚é•Ï” ----
    public Vector3 initPos;   // ‰ŠúˆÊ’u
    public Vector3 angle;     // ‰ñ“]
    public Vector3 moveAdd;   // ˆÚ“®—Ê
    public Vector3 angleAdd;  // ‰ñ“]—Ê
    public Vector2 TopBottom; // ‰æ–Ê‚ÌãŒÀ‰ºŒÀ
    public float angleCorrection; // ‰ñ“]—Ê‚Ì•â³
    public float horizon;     // …•½‚É–ß‚é‚Ì—Ê
    public float speed = 0.03f; // ˆÚ“®ƒXƒs[ƒh

    //---- •Ï”éŒ¾ ----
    private Vector3 pos;   // À•W
    private bool bInput;
    private@Myproject InputActions; // “ü—Í
    private Vector2 inputMove;

    void Awake()
    {
        InputActions = new Myproject();
        InputActions.Enable();        
    }

    // Start is called before the first frame update
    void Start()
    {
        // ‰Šú‰»
        initPos = transform.position; // À•W
        bInput = false; // “ü—Í‚³‚ê‚½‚©‚Ì”»’è
    }

    // Update is called once per frame
    void Update()
    {
        // Œ»İ‚Ì’l‚ğ‘ã“ü
        // À•W
        pos = transform.position;
        // ‰ñ“]
        angle = transform.eulerAngles;
        // ‚»‚Ì‚Ü‚Ü‚¾‚ÆŒvZ‚µ‚É‚­‚¢‚½‚ßA…•½‚ğ0‚Æ‚µ‚½”’l‚É‚·‚é
        if (angle.x >= 315) { angle.x = angle.x - 360; }
        // “ü—Í
        bInput = false;

        // ƒL[“ü—Í
        //KeyInput();

        // ƒL[“ü—Í
        inputMove = InputActions.Player.Move.ReadValue<Vector2>();
        // ˆÚ“®
        pos.y = pos.y + inputMove.y * speed;
        // ‰ñ“]
        if (inputMove.y < 0.0f)
        {
            angle += angleAdd;
        }
        if (inputMove.y > 0.0f)
        {
            angle -= angleAdd * angleCorrection;
        }


        // ˆÊ’u‚ÌC³
        // ã‚ÌC³
        if (pos.y >= TopBottom.x) { pos.y = TopBottom.x; }
        // ‰º‚ÌC³
        if (pos.y <= TopBottom.y) { pos.y = TopBottom.y; }
        // À•W‚Ì‘ã“ü
        transform.position = pos;

        // …•½‚É‚·‚é
        //Horizon();

        // ‰ñ“]‚ÌC³
        // ‰º•ûŒü‚ÌC³
        if (angle.x >=  45) { angle.x = 45; }
        // ã•ûŒü‚ÌC³
        if (angle.x <= -45) {
            angle.x = -45;
            angle.x = 360 + angle.x;
        }

        // ‰ñ“]‚Ì‘ã“ü
        transform.eulerAngles = angle;

        // Œ»İ‚ÌŠp“x = (I—¹‚ÌŠp“x) * Œ¸‘¬—¦ + Œ»İ‚ÌŠp“x
        //Late = (Rotate - Late) * 0.05f + Late;
        //transform.Rotate(new Vector3(0, rotate, 0));
    }

    //void FixedUpdate()
    //{

    //}


    //```` ƒL[“ü—Í‚ÌŠÖ” ````
    private void KeyInput()
    {

        if (Input.GetKey(KeyCode.W)) {
            // ˆÚ“®
            pos += moveAdd;
            // ‰ñ“]
            angle -= angleAdd * angleCorrection;
            //angle -= angleAdd;
            bInput = true;
        }
        if (Input.GetKey(KeyCode.S)) {
            // ˆÚ“®
            pos -= moveAdd;
            // ‰ñ“]
            angle += angleAdd;
            bInput = true;
        }
    }

    //```` …•½‚É‚·‚éŠÖ” ````
    private void Horizon()
    {
        // ‰º
        if (angle.x >= 0) {
            angle.x = angle.x - horizon;
            if (angle.x <= 0) { angle.x = 0; }
        }
        // ã
        if (angle.x < 0) {
            angle.x = angle.x + horizon;
            if (angle.x >= 0) { angle.x = 0; }
        }
    }
}
