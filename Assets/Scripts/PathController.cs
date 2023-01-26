using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum Path
{
    path1,
    path2,
    path3
}


public class PathController : MonoBehaviour
{
    [SerializeField] private GameObject path1;
    [SerializeField] private GameObject path2;
    [SerializeField] private GameObject path3;
    private Tilemap path1TileMap;
    private Tilemap path2TileMap;
    private Tilemap path3TileMap;

    private Color highlight = Color.white;

    private void Start()
    {
        path1TileMap = path1.GetComponent<Tilemap>();
        path2TileMap = path2.GetComponent<Tilemap>();
        path3TileMap = path3.GetComponent<Tilemap>();
        highlight.a = 0.5f;
    }

    public Vector3 GetPathPosition(Path path)
    {
        Vector3 pathPos;
        
        if (path == Path.path1)
        {
            pathPos = path1.transform.position;
        }
        else if (path == Path.path2)
        {
            pathPos = path2.transform.position;
        }
        else
        {
            pathPos = path3.transform.position;
        }
        return pathPos;
    }
    public void PathUpdate(Path path)
    {
        path1TileMap.color = Color.grey;
        path2TileMap.color = Color.grey;
        path3TileMap.color = Color.grey;
        switch (path)
        {
            case Path.path1:
                path1TileMap.color = highlight;
                break;
            case Path.path2:
                path2TileMap.color = highlight;
                break;
            case Path.path3:
                path3TileMap.color = highlight;
                break;
        }
    }

    public bool PathChanger(Transform objectOnPath, Path moveTo)
    {
        Vector3 pos = objectOnPath.transform.position;

        pos.y = GetPathPosition(moveTo).y;
        
        objectOnPath.transform.position = Vector3.MoveTowards(objectOnPath.transform.position, pos, 0.1f);
        //Debug.Log("new pos: " + transform.position + "target :" + pos);
        if (objectOnPath.transform.position.y == pos.y)
        {
            return true;
        }
        else
        {
            return false;
        }
    }






}
