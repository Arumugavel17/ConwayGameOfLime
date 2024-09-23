using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Cell : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;
    
    private List<string> causers;
    public bool IsAlive;
    public bool IsAliveNext;
    public List<bool> WasAlive;
    
    public int liveNeighbors;

    public List<Cell> neighbors;
    public List<string> liveNeighborsNames;
    public bool set;
    private void Awake()
    {
        neighbors = new List<Cell>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.color = Color.black;
    }
    public void SetScale(float x,float y)
    {
        transform.localScale = new Vector3(x,y,0);
    }

    public bool method(Cell cell)
    {
        if (cell.IsAlive)
        {
            liveNeighborsNames.Add(cell.name);
        }
        else
        {
            liveNeighborsNames.Remove(cell.name);
        }
        return cell.IsAlive;
    }
    public void DetermineNextLiveState()
    {
        liveNeighborsNames.Clear();
        neighbors.Where(method);
        liveNeighbors = neighbors.Where(x => x.IsAlive).Count();

        if (IsAlive)
        {
            IsAliveNext = liveNeighbors == 2 || liveNeighbors == 3;
            //if (IsAliveNext)
            //{
            //    Debug.Log(name);
            //}
        }
        else
        {
            IsAliveNext = liveNeighbors == 3 ;
        }
        
    }

    public void Advance()
    {
        WasAlive.Add(IsAlive);
        IsAlive = IsAliveNext;

    }

    public void StartLife()
    {
        //while (true)
        //{
        //DetermineNextLiveState();

        Advance();
        //yield return new WaitForSeconds(1f);
        //}
    }

    public void Update()
    {
        DetermineNextLiveState();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartLife();
#if UNITY_EDITOR
            EditorApplication.isPaused = true;
            Debug.Log("Paused");
#endif
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            IsAlive = WasAlive[WasAlive.Count - 1];
            WasAlive.RemoveAt(WasAlive.Count - 1);
        }
        if (IsAliveNext)
        {
            Debug.Log(name + "Undertaker");
        }

        if (IsAlive)
        {
            SpriteRenderer.color = Color.white;
        }
        else
        {
            SpriteRenderer.color = Color.black;
        }
        if (IsAliveNext && !IsAlive)
        {
            SpriteRenderer.color = Color.red;
        }

    }
}
