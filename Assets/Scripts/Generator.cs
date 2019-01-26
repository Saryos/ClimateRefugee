using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    public Tilemap tilemap_;

    public Tile grassTile_;
    public Tile waterTile_;
    public Tile sandTile_;

    public GameObject tree_;

    public int levelWidth_ = 40;
    public int levelHeight_ = 40;

    private NoiseTest.OpenSimplexNoise noise_ = new NoiseTest.OpenSimplexNoise();

    // Start is called before the first frame update
    void Start()
    {
        generateScenario(levelWidth_, levelHeight_, 0.0f, 0.0f);
    }

    void generateScenario(int width, int height, float dryOffset, float heightOffset)
    {
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
                        tilemap_.SetTile(new Vector3Int(i, j, 0), sandTile_);
                    }
                    else
                    {
                        tilemap_.SetTile(new Vector3Int(i, j, 0), grassTile_);
                        bool isTree = Random.Range(0, 2) == 1;
                        if (heightValue > 0 && isTree && drynessValue < 0.4)
                        {
                            Vector3 treePosition = tilemap_.CellToWorld(new Vector3Int(i,j,0));
                            Instantiate(tree_, treePosition + new Vector3(0,0.5f,0), Quaternion.identity);
                        }
                    }
                }
                else
                {
                    if (drynessValue > 0.5) // no water if too dry
                    {
                        tilemap_.SetTile(new Vector3Int(i, j, 0), sandTile_);
                    }
                    else
                    {
                        tilemap_.SetTile(new Vector3Int(i, j, 0), waterTile_);
                    }
                }

            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
