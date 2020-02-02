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
    public Vector3 initialWaterLevel;
    public GameObject acdc;
    public HandsManager handsManagerScript;
    public GameObject gameOverScreen;
    public GameOverScreen gameoverScript;
    public AudioSource musicSource;
    public Transform toolsParent;


    public float spawnDelay;
    private Transform waterlevelTransform;
    public int numOfLeakings;
    public float waterRaiseSpeed;


    public void InitGame()
    {
        Cursor.visible = false;
        gameInitiated = false;
        waterlevelTransform = waterLevel.transform;
        waterlevelTransform.localPosition = initialWaterLevel;

        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        score = 0;
        numOfLeakings = 0;
        timerScript.StartTimer();
        spawnDelay = 5;

        StopAllCoroutines();
        StartCoroutine(Spawner());

        gameInitiated = true;
        handsManagerScript.InitHandsmanager();
        musicSource.Play();
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
            waterlevelTransform.localPosition = new Vector3(waterlevelTransform.localPosition.x, waterlevelTransform.localPosition.y + Time.deltaTime * waterRaiseSpeed * numOfLeakings, initialWaterLevel.z);

            if (waterlevelTransform.position.y >= acdc.transform.position.y && gameInitiated == true)
                gameOver();
        }

    }


    private void gameOver() {

        if (gameInitiated) {
            gameInitiated = false;
            musicSource.Stop();
            Time.timeScale = 0f;
            gameoverScript.UpdateText(timerScript.getScore());
            gameOverScreen.SetActive(true);
            Cursor.visible = true;

            foreach (Transform child in leakingsParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            foreach (Transform child in toolsParent.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

}
