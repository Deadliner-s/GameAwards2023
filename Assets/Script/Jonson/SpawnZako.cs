using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZako : MonoBehaviour
{
    public GameObject Zako;
    public int SpawnZakoTime;
    public float ZakoSpeed;
    public float ZakoDestroyTime;
    public int SpawnMissileTime;
    public float MissileSpeed;
    public float MissileAccel;
    public float MissileDestroyTime;


    private PhaseManager.Phase currentPhase;

    int time;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {

        currentPhase = PhaseManager.instance.GetPhase();
        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            time++;
            if (time >= SpawnZakoTime)
            {
                time = 0;
                GameObject obj = Instantiate(Zako, transform.position, Quaternion.Euler(0,0,90));
                obj.GetComponent<ZakoMove>().ZakoSpeed = ZakoSpeed;
                obj.GetComponent<ZakoMove>().DestroyTime = ZakoDestroyTime;
                obj.GetComponent<ZakoMove>().MissileSpeed = MissileSpeed;
                obj.GetComponent<ZakoMove>().Accel = MissileAccel;
                obj.GetComponent<ZakoMove>().SpawnMissileTime = SpawnMissileTime;
                obj.GetComponent<ZakoMove>().MissileDestroyTime = MissileDestroyTime;
            }
        }
        else
        {
            //time = 0;
        }
    }
}
