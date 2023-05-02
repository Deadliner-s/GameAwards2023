using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZako : MonoBehaviour
{
    public GameObject Zako;
    public int SpawnZakoTime1;
    public int SpawnZakoTime2;
    public int SpawnZakoTime3;
    public float ZakoSpeed;
    public float ZakoDestroyTime;
    public int SpawnMissileTime;
    public float MissileSpeed;
    public float MissileAccel;
    public float MissileDestroyTime;


    private PhaseManager.Phase currentPhase;

    int time;
    bool use1;
    bool use2;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        use1 = false;
        use2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentPhase = PhaseManager.instance.GetPhase();
        if (currentPhase == PhaseManager.Phase.Speed_Phase)
        {
            time++;
            if (time >= SpawnZakoTime1 && !use1)
            {
                use1 = true;
                CreateZako();
            }
            if(time >= SpawnZakoTime2 && !use2)
            {
                use2 = true;
                CreateZako();
            }
            if(time >= SpawnZakoTime3)
            {
                time = 0;
                CreateZako();
            }
        }
        else
        {
            time = 0;
        }
    }

    void CreateZako()
    {
        GameObject obj = Instantiate(Zako, transform.position, Quaternion.identity);
        obj.GetComponent<ZakoMove>().ZakoSpeed = ZakoSpeed;
        obj.GetComponent<ZakoMove>().DestroyTime = ZakoDestroyTime;
        obj.GetComponent<ZakoMove>().MissileSpeed = MissileSpeed;
        obj.GetComponent<ZakoMove>().Accel = MissileAccel;
        obj.GetComponent<ZakoMove>().SpawnMissileTime = SpawnMissileTime;
        obj.GetComponent<ZakoMove>().MissileDestroyTime = MissileDestroyTime;
    }


}
