using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NoEnergyTextScript : MonoBehaviour
{

    public TextMeshProUGUI textmeshPro;
     
    public void Appear()
    {
        textmeshPro.outlineColor = new Color32(0, 0, 0, 255);
        textmeshPro.faceColor = new Color32(255, 0, 0, 255);
        Invoke("Disappear", 0.5f);
    }

    void Disappear()
    {
        textmeshPro.outlineColor = new Color32(255, 0, 0, 0);
        textmeshPro.faceColor = new Color32(0, 0, 0, 0);
    }
}
