using System;
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
    [SerializeField] private Tilemap path1TileMap;
    [SerializeField] private Tilemap path2TileMap;
    [SerializeField] private Tilemap path3TileMap;

    private Color highlight = Color.white;

    [Serializable]
    public struct PathAndPos
    {
        public Path path;
        public Transform pathTransform;
    }
    [SerializeField] private PathAndPos[] pathAndPosArray;

    private void Start()
    {
        path1TileMap.color = Color.white;
        path2TileMap.color = Color.white;
        path3TileMap.color = Color.white;
        highlight.a = 0.7f;
    }

  
    public Vector3 GetPathPosition(Path path)
    {
        PathAndPos item = Array.Find(pathAndPosArray, i => i.path == path);
        return item.pathTransform.position;
    }

    public Path GetPath(Transform objTransform)
    {
        float posY = objTransform.position.y;
        PathAndPos item = Array.Find(pathAndPosArray, i => i.pathTransform.position.y == posY);
        return item.path;
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
