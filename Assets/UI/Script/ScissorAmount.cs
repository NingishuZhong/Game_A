using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScissorAmount : MonoBehaviour
{
    public Text amount;

    private void Update()
    {
        amount.text = "x " + PlayerController.slashAmount.ToString();
    }
}
