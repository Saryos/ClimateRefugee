using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Generator : MonoBehaviour
{
    public Tilemap tilemap_;

    public Tile grassTile_;
    public Tile waterTile_;

    public int levelWidth_ = 40;
    public int levelHeight_ = 40;

    private NoiseTest.OpenSimplexNoise noise_ = new NoiseTest.OpenSimplexNoise();

    // Start is called before the first frame update
    void Start()
    {
        generateScenario(levelWidth_, levelHeight_, 1, 1);
    }

    void generateScenario(int width, int height, int dryOffset, int heightOffset)
    {
        for (int i = 0; i < height; ++i)
        {
            for (int j = 0; j < width; ++j)
            {

                double tileValue = noise_.Evaluate(j*0.05, i*0.05);
                if (tileValue > 0.05)
                {
                    tilemap_.SetTile(new Vector3Int(i, j, 0), grassTile_);
                }
                else
                {
                    tilemap_.SetTile(new Vector3Int(i, j, 0), waterTile_);
                }


                //switch (tile)
                //{
                //    case 1:
                //        {
                            
                //            break;
                //        }
                //    case 2:
                //        {
                //            break;
                //        }
                //    case 3:
                //        {
                //            break;
                //        }
                //    case 4:
                //        {
                //            break;
                //        }
                //}

            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
