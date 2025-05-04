using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grid", menuName = "ScriptableObject/Grid", order = 1)]
public class GridSO : ScriptableObject
{
    [SerializeField] public int rows;
    [SerializeField] public int cols;
    [SerializeField] public ShapeSO shapeSO;
    [SerializeField] public List<TileObj> tiles;
    [SerializeField] public TileObj[,] matrix;
    
}

