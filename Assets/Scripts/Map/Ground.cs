/*
 *  This keeps track of the structures on the ground and has functions to see if there is build space : Chris
 */

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ground : InstanceMonoBehaviour<Ground>
{
    public List<GameObject> structure_prefabs;

    LayerMask structuresLayer;
    LayerMask groundLayer;

    GameObject[,] grid;

    Vector3 groundSize;
    int width;
    int height;

    protected override void Awake()
    {
        base.Awake();

        structuresLayer = LayerMask.NameToLayer("Structures");
        groundLayer = LayerMask.NameToLayer("Ground");

        groundSize = GetComponent<MeshFilter>().mesh.bounds.size;

        width = (int)groundSize.x;
        height = (int)groundSize.y;

        grid = new GameObject[width, height];

        RefreshGridObjects();

        //Debug_Print_Grid();
    }

    void RefreshGridObjects()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = null;
            }
        }

        List<GameObject> objects = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == structuresLayer) // only get objects that are in the layers structure
            {
                if (!objects.Contains(child.gameObject)) // if we don't already have this object recorded in the grid
                {
                    objects.Add(child.gameObject); // add the object so we don't add it again

                    AddObjectToGrid(child.gameObject);
                }
            }
        }
    }

    public (bool, float) GetGroundYAtPos(float x, float y)
    {
        // cast a ray from above and wait till it hits the ground, then get that y pos

        var direction = new Vector3(0, -1, 0);
        var start = new Vector3(x, 20, y);

        //Debug.Log("start: " + start + " direction: " + direction);

        var ray = new Ray(start, direction);

        //Debug.DrawRay(start, direction, Color.red, 100);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            var obj = hit.transform.gameObject;

            //Debug.Log("hit " + obj.name + " at " + obj.transform.position);
            if (obj.layer == groundLayer)
            {
                return (true, hit.point.y); // hit so return true
            }
        }

        return (false, -1); // didn't hit so return false
    }

    void AddObjectToGrid(GameObject obj) // add this object to the grid
    {
        int startx;
        int starty;
        int endx;
        int endy;

        (startx, starty, endx, endy) = GetGridSpaceForObject(obj, obj.transform.localPosition);

        for (int x = startx; x <= endx; x++)
        {
            for (int y = starty; y <= endy; y++)
            {
                if (!ValidPos(x, y)) continue;

                grid[x, y] = obj;
            }
        }
    }

    (int, int, int, int) GetGridSpaceForObject(GameObject obj, Vector3 at)
    {
        var size = obj.GetComponent<MeshFilter>().sharedMesh.bounds.size; // get the size
        int px, py;

        (px, py) = GroundToGridPos(at); // get starting position

        int startx = px - (int)(size.x / 2);
        int starty = py - (int)(size.y / 2);
        int endx = startx + (int)size.x + (size.x % 1 == 0 ? 0 : 1);
        int endy = starty + (int)size.y + (size.y % 1 == 0 ? 0 : 1);

        return (startx, starty, endx, endy);
    }

    Vector3 GridToGroundPos(int x, int y)
    {
        float startx = -groundSize.x / 2;
        float starty = groundSize.y / 2;

        return new Vector3(startx + x, starty - y);
    }

    (int, int) GroundToGridPos(Vector3 pos)
    {
        int x = (int)(50 + pos.x);
        int y = (int)(pos.y > 0 ? 50 - pos.y : 50 + -pos.y);

        return (x, y);
    }

    public bool BuildStructure(Item item, Vector3 where)
    {
        bool success;
        float ypos;
        (success, ypos) = GetGroundYAtPos(where.x, where.z);

        if (!success) return false;

        //where = new Vector3(where.x, ypos, where.z);
        var groundpos = new Vector3(where.x, where.z * -1, ypos);

        int s = GetStructureForItem(item);

        if (s < 0) return false;

        int startx, starty, endx, endy;
        (startx, starty, endx, endy) = GetGridSpaceForObject(structure_prefabs[s], groundpos);

        // check grid to make sure we have room

        bool is_space = true;

        for (int x = startx; x <= endx; x++)
        {
            for (int y = starty; y <= endy; y++)
            {
                if (!ValidPos(x, y) || grid[x, y] != null)
                {
                    is_space = false;
                    break;
                }
            }
        }

        if (!is_space) return false;

        var structure = Instantiate(structure_prefabs[s]);
        structure.SetActive(true);
        structure.transform.SetParent(gameObject.transform);
        //var groundpos = new Vector3(where.x, where.z * -1, where.y);
        structure.transform.localPosition = groundpos;

        Debug.Log("spawned " + structure.name + " at " + groundpos);

        for (int x = startx; x <= endx; x++)
        {
            for (int y = starty; y <= endy; y++)
            {
                if (!ValidPos(x, y)) continue;

                grid[x, y] = structure;
            }
        }

        return true;
    }

    int GetStructureForItem(Item item)
    {
        if (item.IsValid())
        {
            return (int)item.type % structure_prefabs.Count;
        }

        return -1;
    }

    bool ValidPos(int x, int y)
    {
        if (x >= width || x < 0 || y >= height || y < 0) return false;

        return true;
    }

    void Debug_Print_Grid() // debugging purposes
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (grid[x, y] != null)
                    Debug.Log("grid[" + x + "][" + y + "] = " + (grid[x, y] != null ? grid[x, y].gameObject.name : "null"));
            }
        }
    }

}
