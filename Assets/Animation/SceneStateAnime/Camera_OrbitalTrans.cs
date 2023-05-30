using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camera_OrbitalTrans : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera;
    private CinemachineOrbitalTransposer _orbitaltransposer;

    // Start is called before the first frame update
    void Start()
    {
        this._virtualCamera = GetComponent<CinemachineVirtualCamera>();
        this._orbitaltransposer = this._virtualCamera.GetComponentInChildren<CinemachineOrbitalTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        _orbitaltransposer.m_XAxis.Value += 0.05f;
    }
}
