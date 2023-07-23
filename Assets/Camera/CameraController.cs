using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{   
    private static float switchTimer;
    public static CinemachineVirtualCamera playerFollow;
    public static CinemachineVirtualCamera EnemyBeat;
    public static GameObject EnemyTarget;
    private void Awake()
    {
        switchTimer = 0;
        playerFollow = gameObject.transform.GetChild(0).gameObject.GetComponent<CinemachineVirtualCamera>();
        EnemyBeat = gameObject.transform.GetChild(1).gameObject.GetComponent<CinemachineVirtualCamera>();
        EnemyTarget = gameObject.transform.GetChild(2).gameObject;
    }

    private void Update()
    {
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            TimeController.speedScale = 1;
            playerFollow.Priority = 1;
        }
    }

    public static void SwitchCamera()
    {
        switchTimer = 1f;
        TimeController.speedScale = 0.02f;
        playerFollow.Priority = -1;
    }
}
