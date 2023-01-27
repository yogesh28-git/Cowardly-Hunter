using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAndMover : MonoBehaviour
{
    private float treeTimer = 0;
    private int treeSpawnTime = 1;
    private float coinTimer = 0;
    private int coinSpawnTime = 1;
    private int pathNumber;
    private int treeIndex;

    private float backSpeed;
    private float backgroundSpeed;
    [SerializeField] private GameObject[] treeList = new GameObject[2];
    [SerializeField] private PathController pathcontroller;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject tree1, tree2, tree3, tree4;
    [SerializeField] private GameObject greenBG;
    [SerializeField] private GameObject bg;
    private GameObject bg1;
    private GameObject bg2;
    private SpawnerAndMover spawnerAndMover;
    Vector3 bgPos1 = new Vector3(12, 11, 0);
    Vector3 bgPos2 = new Vector3(61, 11, 0);

    private void Start()
    {
        SetBackSpeed(4f);
        spawnerAndMover = GetComponent<SpawnerAndMover>();
        backgroundSpeed = 0.75f * backSpeed;
        tree1.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree2.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree3.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree4.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        bg1 = bg;
        bg2 = Instantiate(greenBG, bgPos2, Quaternion.identity);
    }
    private void Update()
    {
        backgroundSpeed = 0.75f * backSpeed;

        BackGroundSpawner();
        TreeSpawner();
        CoinSpawner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 || collision.gameObject.layer == 10)
        {
            Destroy(collision.gameObject);
        }
    }
    public void SetBackSpeed(float _backspeed)
    {
        backSpeed = _backspeed;
    }
    public void ObjectMover(GameObject spawnedObj)
    {
        spawnedObj.transform.position += Vector3.left * Time.deltaTime * backSpeed;
    }
    private void TreeSpawner()
    {
        if (treeTimer >= treeSpawnTime)
        {
            treeTimer = 0;
            treeSpawnTime = (int)Random.Range(2, 5);
            pathNumber = (int)Random.Range(1, 4);
            treeIndex = (int)Random.Range(0, 2);
            Vector3 pos = transform.position;
            pos.y = pathcontroller.GetPathPosition((Path)pathNumber).y;
            GameObject tree = Instantiate(treeList[treeIndex], pos, Quaternion.identity);
            tree.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.layers[pathNumber].id;
            BackGroundScroller script = tree.GetComponent<BackGroundScroller>();
            script.moverScript = spawnerAndMover;
        }
        treeTimer += Time.deltaTime;
    }

    private void CoinSpawner()
    {
        if (coinTimer >= coinSpawnTime)
        {
            coinTimer = 0;
            coinSpawnTime = (int)Random.Range(1,7);
            pathNumber = (int)Random.Range(1, 4);
            Vector3 pos = transform.position;
            pos.y = pathcontroller.GetPathPosition((Path)pathNumber).y - 1;
            GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity);
            coin.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.layers[pathNumber].id;
            CollectiblesController script = coin.GetComponent<CollectiblesController>();
            script.moverScript = spawnerAndMover;
        }
        coinTimer += Time.deltaTime;
    }

    private void BackGroundSpawner()
    {
       if(bg2.transform.position.x >= bgPos1.x)
       {
           Destroy(bg1);
           bg1 = bg2;
           bg2 = Instantiate(greenBG, bgPos2, Quaternion.identity);
       }

        bg1.transform.position += Vector3.left * Time.deltaTime * backgroundSpeed;
        bg2.transform.position += Vector3.left * Time.deltaTime * backgroundSpeed;
    }
}
