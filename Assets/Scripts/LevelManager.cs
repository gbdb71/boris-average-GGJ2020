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
    public Timer timerScript;

    public GameObject waterLevel;
    public GameObject acdc;
    public HandsManager handsManagerScript;
    public GameObject gameOverScreen;

    public float spawnDelay;
    private Transform waterlevelTransform;
    public int numOfLeakings;
    public float waterRaiseSpeed;


    public void InitGame()
    {
        gameInitiated = false;
        waterlevelTransform = waterLevel.transform;

        Time.timeScale = 1f;
        score = 0;
        numOfLeakings = 0;
        timerScript.StartTimer();

        StartCoroutine(Spawner());

        gameInitiated = true;
        handsManagerScript.InitHandsmanager();
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
        if (gameInitiated) {
            waterlevelTransform.position = new Vector3(waterlevelTransform.position.x, waterlevelTransform.position.y + Time.deltaTime * waterRaiseSpeed * numOfLeakings, waterlevelTransform.position.z);

            if (waterlevelTransform.position.y >= acdc.transform.position.y)
                gameOver();
        }

    }


    private void gameOver() {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

}
