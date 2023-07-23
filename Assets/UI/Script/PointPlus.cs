using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointPlus : MonoBehaviour
{
    public static float displayExistTimer;
    public Animator animator;
    public GameObject textObj;
    public Text display;
    public static Text displayText;

    private void Awake()
    {
        displayText = display;
    }

    private void Update()
    {
        if (displayExistTimer > 0)
        {
            textObj.SetActive(true);
            displayExistTimer -= Time.deltaTime * TimeController.speedScale;
        }
        else if (textObj.activeSelf == true)
        {
            animator.Play("TextExit");
        }
    }
}
