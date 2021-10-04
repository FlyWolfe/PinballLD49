using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LED : MonoBehaviour
{
    public new Renderer renderer;

    public int score = 200;

    private Material material;
    private Color emissionColor;

    private bool controlledByPlayer;
    private bool isOn;

    public Material Material
    {
        get
        {
            if(material == null)
            {
                material = renderer.material;
                emissionColor = material.GetColor("_EmissionColor");
            }
            return material;
        }
    
    }

    public void SetControlledByPlayer() {
        controlledByPlayer = true;
    }


    public void TurnOn()
    {
        Material.SetColor("_EmissionColor", emissionColor);
        isOn = true;
    }

    public void TurnOff()
    {
        Material.SetColor("_EmissionColor", Color.black);
        isOn = false;
    }

    private void OnTriggerEnter(Collider other) {
        Player player = other.gameObject.GetComponent<Player>();
        if (controlledByPlayer && player) {
            if (isOn)
                TurnOff();
            else {
                TurnOn();
                GameController.Instance.TargetScore += score;
            }
        }
    }
}
