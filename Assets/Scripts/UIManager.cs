using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text energyText;
    public GameObject buyMenu;
    public Button switchModeButton;
    [HideInInspector]
    public Text switchModeButtonText;
    public Text currentModeText;

    private void Awake()
    {
        switchModeButtonText = switchModeButton.GetComponentInChildren<Text>();
    }
}
