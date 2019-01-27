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
    public GameObject rock_;

    private NoiseTest.OpenSimplexNoise noise_ = new NoiseTest.OpenSimplexNoise();


    Vector3 PlayerStart_;

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

        // generate rocks

        Instantiate(rock_, RandomWalkableLoc(width, height), Quaternion.identity);
        Instantiate(rock_, RandomWalkableLoc(width, height), Quaternion.identity);
        Instantiate(rock_, RandomWalkableLoc(width, height), Quaternion.identity);
        Instantiate(rock_, RandomWalkableLoc(width, height), Quaternion.identity);
        Instantiate(rock_, RandomWalkableLoc(width, height), Quaternion.identity);
        Instantiate(rock_, RandomWalkableLoc(width, height), Quaternion.identity);
        Instantiate(rock_, RandomWalkableLoc(width, height), Quaternion.identity);


        PlayerStart_ = RandomWalkableLoc(width, height);

    }

    Vector2 RandomWalkableLoc(int width, int height)
    {
        bool found = false;
        int attempts = 30;

        for (int i = 0; i < attempts && !found; ++i)
        {
            int xloc = Random.Range(0, width);
            int yloc = Random.Range(0, height);


            if (IsTileWalkable(new Vector2(xloc, yloc)))
            {
                found = true;
                return new Vector2(xloc, yloc);
            }
        }
        return new Vector2(0, 0);
    }

    public Vector3 GetPlayerStartPosition()
    {
        return new Vector3(PlayerStart_.x, PlayerStart_.y, 0);
    }



    public bool CanBuild(Vector2 location)
    {
        Vector2 v1 = location;
        Vector2 v2 = new Vector2(location.x + 1, location.y);
        Vector2 v3 = new Vector2(location.x, location.y + 1);
        Vector2 v4 = new Vector2(location.x + 1, location.y + 1);
        return IsTileWalkable(v1) && IsTileWalkable(v2) && IsTileWalkable(v3) && IsTileWalkable(v4);
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
            Debug.Log(tile.name);

            if (tile.name == "Water")
            {
                return false;
            }

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
