﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlaceExample : MonoBehaviour
{
    public GameObject terrain;
    public GameObject target;

    private PolygonGenerator tScript;
    private LayerMask layerMask = (1 << 0);

    // Start is called before the first frame update
    void Start()
    {
        tScript = terrain.GetComponent("PolygonGenerator") as PolygonGenerator;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        float distance = Vector3.Distance(transform.position, target.transform.position);

        if (Physics.Raycast(transform.position, (target.transform.position -
            transform.position).normalized, out hit, distance, layerMask))
        {
            Debug.DrawLine(transform.position, hit.point, Color.red);

            Vector2 point = new Vector2(hit.point.x, hit.point.y);
            point += (new Vector2(hit.normal.x, hit.normal.y)) * 0.5f;

            tScript.blocks[Mathf.RoundToInt(point.x - 0.5f), Mathf.RoundToInt(point.y + 0.5f)] = 1;

            tScript.update = true;
        }
        else
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.blue);
        }
    }
}