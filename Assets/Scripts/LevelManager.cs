using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class LevelManager : MonoBehaviour
{
    public LevelData currentLevel;
    public TileObj item;
    public GameObject grid;
    public int layer;
    public float tileSize = 165f;
    private Dictionary<TileType, string> tileTypeSpriteNameMap;
    [SerializeField] private SpriteAtlas spriteAtlas;
    [SerializeField] Transform content;
    private void Start()
    {
        InitalizeTileType();
    }
    public void CreateGrid()
    {
        for(int k = 0; k < currentLevel.grids.Count; k++)
        {
            currentLevel.grids[k].matrix = new TileObj[currentLevel.grids[k].rows, currentLevel.grids[k].cols];
            GameObject gridSpawn = Instantiate(grid, content);
            layer++;
            gridSpawn.transform.localPosition += new Vector3(0,0,layer);
            bool isOddGrid = (k % 2 != 0);
            if (isOddGrid)
            {
                gridSpawn.transform.localPosition += new Vector3(tileSize / 2f,-(tileSize+tileSize/2));
            }
            for (int i = 0; i < currentLevel.grids[k].rows; i++)
            {
                for(int j = 0; j < currentLevel.grids[k].cols; j++)
                {
                    TileObj tileObj = Instantiate(item,gridSpawn.transform);
                    currentLevel.grids[k].matrix[i, j] = tileObj;
                    if (!currentLevel.grids[k].shapeSO.IsBlocked(i, j))
                    {
                        AssignType(tileObj, currentLevel, i, j);
                        currentLevel.grids[k].tiles.Add(tileObj);
                    }
                    else
                    {
                        tileObj.SetBlocked();
                    }
                }
            }
        }
    }
    public void OverLappedTiles(TileObj tile)
    {
        for(int k = 0; k < currentLevel.grids.Count; k++)
        {
            for (int i = 0; i < currentLevel.grids[k].rows; i++)
            {
                for (int j = 0; j < currentLevel.grids[k].cols; j++)
                {
                    tile.overlappedTiles.Add(currentLevel.grids[k + 1].matrix[i, j]);
                    tile.overlappedTiles.Add(currentLevel.grids[k + 1].matrix[i + 1, j]);
                    tile.overlappedTiles.Add(currentLevel.grids[k + 1].matrix[i, j + 1]);
                    tile.overlappedTiles.Add(currentLevel.grids[k + 1].matrix[i + 1, j + 1]);
                }
            }
        }
    }
    public void AssignType(TileObj tile, LevelData levelData,int row,int column)
    {
        TileType tileType = levelData.spawnableTileType[Random.Range(0, levelData.spawnableTileType.Length)];
        tile.tileType = tileType;
        string spriteName;
        if (tileTypeSpriteNameMap.TryGetValue(tileType, out spriteName))
        {
            Sprite tileSprite = spriteAtlas.GetSprite(spriteName);
            tile.tileImg.sprite = tileSprite;
            tile.x = row;
            tile.y = column;
        }
    }
    private void InitalizeTileType()
    {
        tileTypeSpriteNameMap = new Dictionary<TileType, string>
        {
            {TileType.Apple,"Apple"},
            {TileType.Banana,"Banana"},
            {TileType.BlueBerry,"BlueBerry" },
            {TileType.Coconuit,"Coconut"},
            {TileType.DragonFruit,"DragonFruit"},
            {TileType.Grape,"Grape" },
            {TileType.GreenPepper,"GreenPepper" },
            {TileType.Lemon,"Lemon" },
            {TileType.Melon,"Melon" },
            {TileType.Orange,"Orange" },
            {TileType.Peach,"Peach" },
            {TileType.Pearl,"Pearl" },
            {TileType.Pepper,"Pepper" },
            {TileType.PupGrape,"PupGrape" },
            {TileType.Tomato,"Tomato" },
        };
    }
}
