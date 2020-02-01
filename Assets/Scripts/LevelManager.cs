using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool gameInitiated = false;
    public int score;

    public Transform leakingsParent;
    public SpawnZone[] spawnZones;
    public GameObject leakingPrefab;

    public GameObject waterLevel;
    public GameObject acdc;

    public float spawnDelay;
    private Transform waterlevelTransform;
    public int numOfLeakings;
    public float waterRaiseSpeed;

    // Start is called before the first frame update
    void Start()
    {
        gameInitiated = false;
        waterlevelTransform = waterLevel.transform;
        InitGame();
    }

    public void InitGame()
    {
        Time.timeScale = 1f;
        score = 0;
        numOfLeakings = 0;

        StartCoroutine(Spawner());

        gameInitiated = true;
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnLeaking();
        StartCoroutine(Spawner());
    }

    public void SpawnLeaking()
    {
        int randomSpanwerZone = Random.Range(0, spawnZones.Length);

        GameObject newLeaking = Instantiate(leakingPrefab, spawnZones[randomSpanwerZone].spawnPoint, Quaternion.identity);
        newLeaking.transform.parent = leakingsParent;

        numOfLeakings++;
        if (spawnDelay >= 2f)
            spawnDelay -= 0.2f;
    }

    private void Update()
    {
        waterlevelTransform.position = new Vector3(waterlevelTransform.position.x, waterlevelTransform.position.y + Time.deltaTime*waterRaiseSpeed*numOfLeakings, waterlevelTransform.position.z);

        if (waterlevelTransform.position.y >= acdc.transform.position.y)
            gameOver();
    }


    private void gameOver() {
        Time.timeScale = 0f;
    }

}
