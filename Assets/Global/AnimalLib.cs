using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLib : MonoBehaviour
{
    public Sprite[] animalsLib;
    public static Sprite[] animals;
    public Sprite[] hairBallLib;
    public static Sprite[] hairBalls;


    private void Awake()
    {
        animals = animalsLib;
        hairBalls = hairBallLib;
    }
}
