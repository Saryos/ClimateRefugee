using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    public GameObject gridPF_;
    private GameObject grid;

    public Tile grassTile_;
    public Tile waterTile_;
    public Tile sandTile_;

    public GameObject tree_;

    private NoiseTest.OpenSimplexNoise noise_ = new NoiseTest.OpenSimplexNoise();



    public void generateScenario(int width, int height, float dryOffset, float heightOffset)
    {
        Debug.Log("Creating map");
        grid = Instantiate(gridPF_, transform.position, Quaternion.identity);

        Tilemap tilemap = grid.GetComponentInChildren<Tilemap>();

        Debug.Log(grid);
        Debug.Log(tilemap);

        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {
                double heightValue = noise_.Evaluate(j*0.05, i* 0.05); 
                double drynessValue = noise_.Evaluate(j * 0.03 + 2250, i * 0.03 + 2250);

                if (heightValue > -0.5 ) // too high for water
                {
                    if (heightValue < -0.3 || drynessValue > 0.5) // close to water or too dry
                    {
                        tilemap.SetTile(new Vector3Int(i, j, 0), sandTile_);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(i, j, 0), grassTile_);
                        bool isTree = Random.Range(0, 2) == 1;
                        if (heightValue > 0 && isTree && drynessValue < 0.4)
                        {
                            Vector3 treePosition = tilemap.CellToWorld(new Vector3Int(i,j,0));
                            Instantiate(tree_, treePosition + new Vector3(0,0.5f,0), Quaternion.identity);
                        }
                    }
                }
                else
                {
                    if (drynessValue > 0.5) // no water if too dry
                    {
                        tilemap.SetTile(new Vector3Int(i, j, 0), sandTile_);
                    }
                    else
                    {
                        tilemap.SetTile(new Vector3Int(i, j, 0), waterTile_);
                    }
                }

            }
        }
    }


    public bool IsTileWalkable(Vector2 tileLocation)
    {
        Grid gridComp = grid.GetComponent<Grid>();
        Vector3Int tileLoc = gridComp.WorldToCell(tileLocation);
        Tilemap tilemap = grid.GetComponentInChildren<Tilemap>();

        Vector3Int mapSize = tilemap.size;

        if (tileLoc.x >= 0 && tileLoc.y >= 0 && tileLoc.x < tilemap.size.x && tileLoc.y < tilemap.size.y)
        {
            TileBase tile = tilemap.GetTile(tileLoc);
            Debug.Log(tile.ToString());
        }
        else
        {
            return false;
        }

        return true;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
