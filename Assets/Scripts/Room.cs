using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public Cell[] cells;        // ячейки в этой комнате
    public float speedMultiplier = 1f;
    public float performanceMultiplier = 1f;
    public float costMultiplier = 1f;

    private void Awake()
    {
        cells = GetComponentsInChildren<Cell>();
    }
}
