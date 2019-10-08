using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GeneratorClickMode { SpeedUp, Upgrade}
    public GeneratorClickMode generatorClickMode;

    public static GameManager Instance { get; set; }
    public UIManager uiManager;

    public GameObject[] generatorVariants;
    public Cell selectedCell;
    private float energy;
    public float Energy
    {
        get
        {
            return energy;
        }
        set
        {
            energy = value;
            uiManager.energyText.text = "Energy: " + energy.ToString();
        }
    }

    public float energyDef = 100f;

    /////////////////////////////////////////////////////////////// UNITY MESSAGES ////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        Instance = this;
        Energy = energyDef;
    }

    // по кнопке на любой кнопке в меню покупки
    public void Buy(int generatorType)      //от 1 до 10, соответствует G1-G10
    {
        // проверяем, хватит ли денег на покупку
        Generator generator = generatorVariants[generatorType - 1].GetComponent<Generator>();       // generatorType-1, потому что в массиве индексация с нуля
        if (generator.cost > Energy)
        {
            Debug.Log("Недостаточно средств");
            return;
        }

        // проверяем, поддерживает ли ячейка такой тип генератора
        if (!selectedCell.generatorTypes.Contains((Generator.GeneratorType)generatorType))
        {
            Debug.Log("Такой тип генератора не поддерживается данной ячейкой");
            return;
        }

        // покупаем
        Energy -= generator.cost;
        var generatorInstance = Instantiate(generator.gameObject, selectedCell.transform);
        selectedCell.isFree = false;
        uiManager.buyMenu.SetActive(false);
    }

    // по кнопке переключения режима
    public void SwitchMode()
    {
        switch (generatorClickMode)
        {
            case GeneratorClickMode.SpeedUp:
                generatorClickMode = GeneratorClickMode.Upgrade;
                uiManager.currentModeText.text = "Current mode: Upgrade";
                uiManager.switchModeButtonText.text = "Speedup";
                break;
            case GeneratorClickMode.Upgrade:
                generatorClickMode = GeneratorClickMode.SpeedUp;
                uiManager.currentModeText.text = "Current mode: SpeedUp";
                uiManager.switchModeButtonText.text = "Upgrade";
                break;            
        }
    }
}
