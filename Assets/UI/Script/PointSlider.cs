using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointSlider : MonoBehaviour
{
    public Text point;
    public Slider pointSlider;
    public Text levelPointDisplay;
    private float[] pointLevel = new float[] { 10, 50, 80, 130, 150, 200 };
    public Color[] levelColors;
    private float[] levelPointUsed = new float[] { 0, 10, 60, 140, 270, 420 };
    private int level;
    public static float levelPoint;

    private void Awake()
    {
        levelPoint = 0;
        level = 0;
        point.text = "N";
        point.color = levelColors[level];
        pointSlider.maxValue = 10;
    }

    void Update()
    {
        
        levelPointDisplay.text = ((int)levelPoint).ToString();
        pointSlider.value = levelPoint - levelPointUsed[level];
        if (levelPoint - levelPointUsed[level] >= pointSlider.maxValue)
        {
            //Debug.Log(level);
            pointSlider.value = levelPoint - levelPointUsed[level] - pointSlider.maxValue;
            pointSlider.maxValue = pointLevel[level + 1];
            switch (level)
            {
                case 0:
                    //Debug.Log(level);
                    point.text = "D";
                    level += 1;
                    break;
                case 1:
                    point.text = "C";
                    level += 1;
                    break;
                case 2:
                    point.text = "B";
                    level += 1;
                    break;
                case 3:
                    point.text = "A";
                    level += 1;
                    break;
                case 4:
                    point.text = "P!";
                    level += 1;
                    break;
            }
            point.color = levelColors[level];
        }

        if (pointSlider.value <= 0)
        {
            switch(level)
            {
                case 1:
                    level -= 1;
                    point.text = "N";
                    break;
                case 2:
                    level -= 1;
                    point.text = "D";
                    break;
                case 3:
                    level -= 1;
                    point.text = "C";
                    break;
                case 4:
                    level -= 1;
                    point.text = "B";
                    break;
                case 5:
                    level -= 1;
                    point.text = "A";
                    break;
            }
            pointSlider.maxValue = pointLevel[level];
            if (level != 0)
                pointSlider.value = pointSlider.maxValue;
            point.color = levelColors[level];
        }
        if (PlayerController.gameOver == false)
        {
            //Debug.Log(PlayerController.HP);
            levelPoint = Mathf.Max(0, levelPoint - (pointLevel[level] / 10 + level) * TimeController.speedScale * Time.deltaTime);
        }
    }
}
