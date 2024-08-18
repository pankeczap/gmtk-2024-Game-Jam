using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WFCBuilder : MonoBehaviour
{
    [SerializeField] int Width;
    [SerializeField] int Height;

    WFCSegment[,] _grid;

    public List<WFCSegment> Segments = new List<WFCSegment>();

    List<Vector2Int> _toCollapse = new List<Vector2Int>();

    Vector2Int[] offsets = new Vector2Int[]
    {
        new Vector2Int(0,1),    //top
        new Vector2Int(0,-1),   //Bottom
        new Vector2Int(1,0),    //Right
        new Vector2Int(-1,0)    //Left
    };

    private void Start()
    {
        _grid = new WFCSegment[Width, Height];

        CollapseWorld();
    }


    private void CollapseWorld()
    {
        _toCollapse.Clear();

        _toCollapse.Add(new Vector2Int(Width / 2, Height / 2));

        while(_toCollapse.Count > 0)
        {
            int x = _toCollapse[0].x;
            int y = _toCollapse[0].y;

            List<WFCSegment> potentialSegments = new List<WFCSegment>(Segments);

            for (int i = 0; i < offsets.Length; i++)
            {
                Vector2Int neighbour = new Vector2Int(x + offsets[i].x, y + offsets[i].y);

                if (IsInsideGrid(neighbour))
                {
                    WFCSegment neighbourSegment = _grid[neighbour.x, neighbour.y];

                    if (neighbourSegment != null)
                    {
                        switch (i)
                        {
                            case 0:
                                WhittleSegments(potentialSegments, neighbourSegment.Bottom.ValidSegments);
                                break;
                            case 1:
                                WhittleSegments(potentialSegments, neighbourSegment.Top.ValidSegments);
                                break;
                            case 2:
                                WhittleSegments(potentialSegments, neighbourSegment.Left.ValidSegments);
                                break;
                            case 3:
                                WhittleSegments(potentialSegments, neighbourSegment.Right.ValidSegments);
                                break;
                        }
                    }
                    else
                    {
                        if (!_toCollapse.Contains(neighbour)) _toCollapse.Add(neighbour);
                        
                    }
                }
                
            }
            if (potentialSegments.Count < 1)
            {

                _grid[x, y] = Segments[0];
                Debug.Log("Oh fuck. Rule Issue");



            }
            else
            {
                _grid[x, y] = potentialSegments[Random.Range(0, potentialSegments.Count)];
            }

            GameObject newSegment = Instantiate(_grid[x, y].Prefab, new Vector3(x*9, y*6, 0f), Quaternion.identity);




            _toCollapse.RemoveAt(0);
        }

    }

    private void WhittleSegments(List<WFCSegment> potentialSegments, List<WFCSegment> validSegments)
    {
        for (int i = potentialSegments.Count -1; i > -1; i--)
        {
            if (!validSegments.Contains(potentialSegments[i]))
            {
                potentialSegments.RemoveAt(i);
            }

        }
    }

    bool IsInsideGrid(Vector2Int v2Int)
    {
        if (v2Int.x > - 1 && v2Int.x < Width && v2Int.y > -1 && v2Int.y < Height)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
