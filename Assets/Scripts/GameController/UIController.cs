using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TMP_Text progressDisplay;


    private void Update()
    {
        progressDisplay.text = $"{(GameController.Instance.LevelProgress * 100f).ToString("F2")}%";
    }
}
