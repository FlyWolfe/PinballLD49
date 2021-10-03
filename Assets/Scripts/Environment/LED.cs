using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LED : MonoBehaviour
{
    public new Renderer renderer;

    private Material material;
    private Color emissionColor;

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


    public void TurnOn()
    {
        Material.SetColor("_EmissionColor", emissionColor);
    }

    public void TurnOff()
    {
        Material.SetColor("_EmissionColor", Color.black);
    }
}
