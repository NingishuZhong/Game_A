using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFunc : MonoBehaviour
{
    private static Animator panel;

    private void Awake()
    {
        panel = gameObject.GetComponent<Animator>();
    }

    public static void OpenPanel()
    {
        panel.Play("Enter");
        TimeController.speedScale = 0;
    }
}
