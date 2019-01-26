using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public int resourceAmount_ = 0;
    public int id;

    public gameMaster master_;
    public SpriteRenderer renderer_;

    public Sprite usedSprite_;

    public void collect()
    {
        if (resourceAmount_ == 0)
        {
            Debug.Log("ERROR: no resource amount set for collectable");
            return;
        }

        --resourceAmount_;
        master_.addResource(id);

        if (resourceAmount_ == 0)
        {
            renderer_.sprite = usedSprite_;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
