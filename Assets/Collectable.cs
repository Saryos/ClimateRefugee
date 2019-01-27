using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public int resourceAmount_ = 0;
    public int id;

    public float CollectionWork = 1.0f;
    private float currentProgress_ = 0.0f;

    private gameMaster master_;
    public SpriteRenderer renderer_;

    public Sprite usedSprite_;

    public bool collect(float timeSpent)
    {
        if (resourceAmount_ == 0)
        {
            Debug.Log("ERROR: no resource amount set for collectable");
            return true;
        }

        if (currentProgress_ + timeSpent > CollectionWork)
        {

            --resourceAmount_;
            master_.addResource(id);

            if (resourceAmount_ == 0)
            {
                renderer_.sprite = usedSprite_;
                return true;
            }
            currentProgress_ = 0;
        }
        else
        {
            currentProgress_ += timeSpent;
        }
        return false;
    }



    // Start is called before the first frame update
    void Start()
    {
        GameObject[] master = GameObject.FindGameObjectsWithTag("Master");
        master_ = master[0].GetComponent<gameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
