using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStatus : MonoBehaviour
{
    public Text[] status;
    public Text point;

    public void ShowStatus()
    {
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        yield return new WaitForSeconds(1f);
        status[0].text += ((int)PointCalulater.aliveTimer).ToString();
        yield return new WaitForSeconds(1f);
        status[1].text += PointCalulater.scissors.ToString();
        yield return new WaitForSeconds(1f);
        status[2].text += PointCalulater.lastScissors.ToString();
        yield return new WaitForSeconds(1f);
        status[3].text += ((int)PointCalulater.totalPoint).ToString();
        yield return new WaitForSeconds(2f);
        float Tpoints = PointCalulater.aliveTimer * 100 + PointCalulater.scissors * 300 + PointCalulater.lastScissors * 1000 + PointCalulater.totalPoint;
        if (Tpoints > 150000)
            point.text = "P!";
        else if(Tpoints > 110000)
            point.text = "A";
        else if(Tpoints > 80000)
            point.text = "B";
        else if (Tpoints > 55000)
            point.text = "C";
        else if (Tpoints > 30000)
            point.text = "D";
        else
            point.text = "N";
    }
}
