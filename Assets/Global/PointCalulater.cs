using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCalulater : MonoBehaviour
{
    public static float aliveTimer;
    public static int scissors;
    public static float totalPoint;
    public static int lastScissors;

    private void Awake()
    {
        aliveTimer = 0;
        scissors = 0;
        totalPoint = 0;
        lastScissors = 0;
    }

    private void Update()
    {
        if (PlayerController.HP > 0)
        {
            aliveTimer += Time.deltaTime;
            totalPoint += PointSlider.levelPoint * TimeController.speedScale * Time.deltaTime;
            lastScissors = PlayerController.slashAmount;
            //Debug.Log(totalPoint);
        }
    }
}
