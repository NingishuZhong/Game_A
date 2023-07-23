using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContorller : MonoBehaviour
{
    public static GameObject heart;

    private void Awake()
    {
        heart = gameObject;
        for (int i = 0; i < 5; i++)
        {
            heart.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public static void SetHeart(int HP)
    {
        for (int i = HP; i < 5; i++)
        {
            heart.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
