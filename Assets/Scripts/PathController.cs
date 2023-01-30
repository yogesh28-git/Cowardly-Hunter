using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum Path
{
    invalid,
    path3,
    path2,
    path1
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
        path1TileMap.color = Color.white;
        path2TileMap.color = Color.white;
        path3TileMap.color = Color.white;
        highlight.a = 0.7f;
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

    public Path GetPath(Transform objTransform)
    {
        if(objTransform.position.y == path1.transform.position.y)
        {
            return Path.path1;
        }
        else if (objTransform.position.y == path2.transform.position.y)
        {
            return Path.path2;
        }
        else if (objTransform.position.y == path3.transform.position.y)
        {
            return Path.path3;
        }
        else
        {
            return Path.invalid;
        }
    }
    public void PathUpdate(Path path)
    {
        path1TileMap.color = Color.white;
        path2TileMap.color = Color.white;
        path3TileMap.color = Color.white;
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
        
        objectOnPath.transform.position = Vector3.MoveTowards(objectOnPath.transform.position, pos, 0.05f);
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
