﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderExample : MonoBehaviour
{
    public GameObject terrain;
    public int size = 4;
    public bool circular = false;

    private PolygonGenerator tScript;

    // Start is called before the first frame update
    private void Start()
    {
        tScript = terrain.GetComponent("PolygonGenerator") as PolygonGenerator;
    }

    // Update is called once per frame
    private void Update()
    {
        bool collision = false;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if(circular)
                {
                    if(Vector2.Distance(new Vector2(x - (size/2), 
                        y - (size/2)), Vector2.zero) <= (size/3))
                    {
                        if(RemoveBlock(x - (size/2), y - (size/2)))
                        {
                            collision = true;
                        }
                    }
                }
                else
                {
                    if(RemoveBlock(x - (size/2), y - (size/2)))
                    {
                        collision = true;
                    }
                }
            }
        }

        if(collision)
        {
            tScript.update = true;
        }
    }

    private bool RemoveBlock(float offsetX, float offsetY)
    {
        int x = Mathf.RoundToInt(transform.position.x + offsetX);
        int y = Mathf.RoundToInt(transform.position.y + 1f + offsetY);

        if(x < tScript.blocks.GetLength(0) && y < tScript.blocks.GetLength(1) && x >= 0 && y >= 0)
        {
            if(tScript.blocks[x, y] != 0)
            {
                tScript.blocks[x, y] = 0;
                return true;
            }
        }

        return false;
    }
}
