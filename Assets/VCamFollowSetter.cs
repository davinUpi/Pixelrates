using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCamFollowSetter : MonoBehaviour
{
    private CinemachineVirtualCamera _vcam;

    private void Awake()
    {
        _vcam = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetFollowTarget(GameObject target)
    {
        Debug.Log("Set camera follow!");
        _vcam.m_Follow = target.transform;
    }
}
