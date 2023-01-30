using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAndMover : MonoBehaviour
{
    private float treeTimer = 0;
    private int treeSpawnTime = 1;
    private float coinTimer = 0;
    private int coinSpawnTime = 1;
    private float rhinoTimer = 0;
    private int rhinoSpawnTime = 15;
    private int pathNumber;
    private int treeIndex;


    private float backSpeed;
    private float backgroundSpeed;
    [SerializeField] private GameObject tiger;
    private GameObject cave;
    [SerializeField] private GameObject cavePrefab;
    [SerializeField] private GameObject[] treeList = new GameObject[2];
    [SerializeField] private PathController pathcontroller;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject tree1, tree2, tree3, tree4;
    [SerializeField] private GameObject greenBG;
    [SerializeField] private GameObject rhinoPrefab;
    [SerializeField] private UI_Controller uiScript;
    [SerializeField] private TigerAlert tigerAlert;
    private bool isTigerDead = false;
    private GameObject bg1;
    private GameObject bg2;
    private SpawnerAndMover spawnerAndMover;
    Vector3 bgPos1 = new Vector3(12, 13, 0);
    Vector3 bgPos2 = new Vector3(61, 13, 0);
    Vector3 tigerPos = new Vector3(32, 0, 0);

    private IEnumerator huntDuration;
    private void Start()
    {
        SetBackSpeed(4f);
        spawnerAndMover = GetComponent<SpawnerAndMover>();
        backgroundSpeed = 0.75f * backSpeed;
        tree1.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree2.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree3.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        tree4.GetComponent<BackGroundScroller>().moverScript = spawnerAndMover;
        bg1 = Instantiate(greenBG, bgPos1, Quaternion.identity);
        bg2 = Instantiate(greenBG, bgPos2, Quaternion.identity);

        TigerEntry();
    }
    private void Update()
    {
        backgroundSpeed = 0.6f * backSpeed;

        BackGroundSpawner();
        TreeSpawner();
        CoinSpawner();
        RhinoSpawner();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 7 || collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
        {
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.layer == 6)
        {
            uiScript.GameOver();
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
            script.scoreScript = uiScript;
        }
        coinTimer += Time.deltaTime;
    }

    private void RhinoSpawner()
    {
        if (rhinoTimer >= rhinoSpawnTime)
        {
            rhinoTimer = 0;
            rhinoSpawnTime = (int)Random.Range(15, 25);
            pathNumber = (int)Random.Range(1, 4);
            Vector3 pos = transform.position;
            pos.y = pathcontroller.GetPathPosition((Path)pathNumber).y;
            GameObject rhino = Instantiate(rhinoPrefab, pos, Quaternion.identity);
            rhino.GetComponent<SpriteRenderer>().sortingLayerID = SortingLayer.layers[pathNumber].id;
        }
        rhinoTimer += Time.deltaTime;
    }

    private void BackGroundSpawner()
    {
        bg1.transform.position += Vector3.left * Time.deltaTime * backgroundSpeed;
        bg2.transform.position += Vector3.left * Time.deltaTime * backgroundSpeed;

        if (bg2.transform.position.x <= bgPos1.x)
        {
            Destroy(bg1);
            bg1 = bg2;
            bg2 = Instantiate(greenBG, bgPos2, Quaternion.identity);
        }
    }
    private void TigerEntry()
    {
        huntDuration = HuntDuration();
        StartCoroutine(huntDuration);
    }

    public void TigerKilled()
    {
        Debug.Log("Tiger Killed");
        tigerAlert.RemoveAlerting();
        tiger.SetActive(false);
        uiScript.ScoreIncrease(100);
        StopCoroutine(huntDuration);
        TigerEntry();
    }
    IEnumerator HuntDuration()
    {
        yield return new WaitForSeconds(5);
        tiger.SetActive(true);
        tigerAlert.gameObject.SetActive(true);
        tiger.transform.position = tigerPos + new Vector3 (10, 0, 0);
        do
        {
            tiger.transform.position += Vector3.left * Time.deltaTime * backSpeed;
            yield return new WaitForEndOfFrame();
        } while (tiger.transform.position.x >= tigerPos.x);
        tiger.GetComponent<Animator>().SetBool("walking", true);

        yield return new WaitForSeconds(60);  // This is time given to hunt the tiger. After this, the tiger will escape

        cave = Instantiate(cavePrefab, tigerPos + new Vector3(10, 6, 0), Quaternion.identity);
        do
        {
            cave.transform.position += Vector3.left * Time.deltaTime * backSpeed;
            yield return new WaitForEndOfFrame();
        } while (cave.transform.position.x >= tigerPos.x);
        SetBackSpeed(0);
        tiger.GetComponent<TigerMovement>().TurnToCave();
        do
        {
            tiger.transform.position += Vector3.up * Time.deltaTime * 2 ;
            yield return new WaitForEndOfFrame();
        } while (tiger.transform.position.y <= cave.transform.position.y);
        tiger.SetActive(false);
        uiScript.GameOver();
    }
}
