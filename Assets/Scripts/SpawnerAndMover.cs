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
    [SerializeField] private GameObject[] treeList = new GameObject[2];
    [SerializeField] private PathController pathcontroller;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject tree1, tree2, tree3, tree4;
    private SpawnerAndMover spawnerAndMover;


    private void Start()
    {
        SetBackSpeed(4f);
        spawnerAndMover = GetComponent<SpawnerAndMover>();

        tree1.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree2.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree3.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree4.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
    }
    private void Update()
    {
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
            pathNumber = (int)Random.Range(0, 3);
            treeIndex = (int)Random.Range(0, 2);
            Vector3 pos = transform.position;
            pos.y = pathcontroller.GetPathPosition((Path)pathNumber).y;
            GameObject tree = Instantiate(treeList[treeIndex], pos, Quaternion.identity);
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
            pathNumber = (int)Random.Range(0, 3);
            Vector3 pos = transform.position;
            pos.y = pathcontroller.GetPathPosition((Path)pathNumber).y - 1;
            GameObject coin = Instantiate(coinPrefab, pos, Quaternion.identity);
            CollectiblesController script = coin.GetComponent<CollectiblesController>();
            script.moverScript = spawnerAndMover;
        }
        coinTimer += Time.deltaTime;
    }
}
