using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Conwey : MonoBehaviour
{
    [SerializeField] private GameObject Cell;
    [SerializeField] private int cellCountRow;
    [SerializeField] private int cellCountColumn;
    [SerializeField] private float width;
    [SerializeField] private float height;

    private Cell[,] cells;

    private void Start()
    {
        cells = new Cell[cellCountRow, cellCountColumn];
        for (int x = 0; x < cellCountRow; x++)
        {
            for (int y = 0; y < cellCountColumn; y++)
            {
                GameObject gameObject = Instantiate(Cell, new Vector3(transform.position.x + ((width / cellCountRow) * x), transform.position.y + ((height / cellCountColumn) * y), 0), Quaternion.identity, transform);
                gameObject.name = "cell" + ((cellCountRow * x) - (cellCountColumn - y));
                cells[x, y] = gameObject.GetComponent<Cell>();
                cells[x, y].SetScale(width / cellCountRow, height / cellCountColumn);
            }
        }

        for (int x = 0; x < cellCountRow; x++)
        {
            for (int y = 0; y < cellCountColumn; y++)
            {
                int xL = (x > 0) ? x - 1 : cellCountRow - 1;
                int xR = (x < cellCountRow - 1) ? x + 1 : 0;

                int yT = (y > 0) ? y - 1 : cellCountColumn - 1;
                int yB = (y < cellCountColumn - 1) ? y + 1 : 0;

                cells[x, y].neighbors.Add(cells[xL, yT]);
                cells[x, y].neighbors.Add(cells[x, yT]);
                cells[x, y].neighbors.Add(cells[xR, yT]);
                cells[x, y].neighbors.Add(cells[xL, y]);
                cells[x, y].neighbors.Add(cells[xR, y]);
                cells[x, y].neighbors.Add(cells[xL, yB]);
                cells[x, y].neighbors.Add(cells[x, yB]);
                cells[x, y].neighbors.Add(cells[xR, yB]);

               //StartCoroutine(cells[x, y].StartLife());
            }
        }
    }

}
