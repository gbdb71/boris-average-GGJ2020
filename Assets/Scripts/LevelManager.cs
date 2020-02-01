using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool gameInitiated = false;
    public int score;

    public Transform leakingsParent;
    public SpawnZone spawnZone;
    public GameObject leakingPrefab;

    // Start is called before the first frame update
    void Start()
    {
        gameInitiated = false;
        InitGame();
    }

    public void InitGame()
    {
        Time.timeScale = 1f;
        score = 0;

        StartCoroutine(Spawner());

        gameInitiated = true;
    }


    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(3f);
        SpawnLeaking();
        StartCoroutine(Spawner());
    }

    public void SpawnLeaking()
    {
        GameObject newLeaking = Instantiate(leakingPrefab, spawnZone.spawnPoint, Quaternion.identity);
        newLeaking.transform.parent = leakingsParent;

    }


}
