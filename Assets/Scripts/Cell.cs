using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler
{
    public Generator.GeneratorType[] generatorTypes;       // типы генераторов, которые возможно установить
    public Room room;       // в какой комнате находится

    public float speedMultiplier = 1f;
    public float performanceMultiplier = 1f;
    public float costMultiplier = 1f;

    public GameObject buyMenu;
    public bool isFree = true;

    private void Awake()
    {
        room = GetComponentInParent<Room>();
    }

    public void OnPointerClick(PointerEventData pData)
    {
        if (!isFree) return;
        Debug.Log("Cell clicked: " + pData.pointerCurrentRaycast.gameObject.transform.parent.name);
        GameManager.Instance.selectedCell = this;
        GameManager.Instance.uiManager.buyMenu.SetActive(true);
    }
}
