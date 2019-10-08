using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Generator : MonoBehaviour, IPointerClickHandler
{
    public enum GeneratorType { G1 = 1, G2, G3, G4, G5, G6, G7, G8, G9, G10}
    public GeneratorType generatorType;
    public Cell cell;       // на какую ячейку установлен    
    public float performance;
    public float speed;
    public int level = 1;    
    public float cost;
    public float speedupTime = 1f;
    private float generationTimer;

    private void Awake()
    {
        generationTimer = speed;
        cell = GetComponentInParent<Cell>();
    }

    private void Update()
    {
        generationTimer -= Time.deltaTime;
        if (generationTimer < 0f)
        {
            Generate();
            generationTimer = speed * cell.speedMultiplier * cell.room.speedMultiplier;
            // бонусы к скорости
            if (level >= 25) generationTimer /= 2f;
            if (level >= 50) generationTimer /= 2f;
            if (level >= 100) generationTimer /= 3f;
        }
    }

    public void Generate()
    {        
        float generatedEnergy = performance * level * cell.performanceMultiplier * cell.room.performanceMultiplier;
        // бонус к производительности
        if (level >= 200) generatedEnergy *= 3f;
       
        GameManager.Instance.Energy += generatedEnergy;
    }

    private void Upgrade()
    {
        GameManager.Instance.Energy -= level * cell.costMultiplier;
        level++;
        Debug.Log("Генератор улучшен на уровень: " + level);
    }

    public void OnPointerClick(PointerEventData pData)
    {
        Debug.Log("Generator clicked: " + pData.pointerCurrentRaycast.gameObject.transform.parent.name);

        switch (GameManager.Instance.generatorClickMode)
        {
            case GameManager.GeneratorClickMode.SpeedUp:
                generationTimer -= speedupTime;
                break;

            case GameManager.GeneratorClickMode.Upgrade:
                Upgrade();
                break;            
        }
    }
}
