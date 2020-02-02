using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public enum toolType { Empty, Tape, Gum, Sock }
    public toolType handType = toolType.Tape;
    public float repairCounter = 0.0f;
    public List<Mesh> handMeshes = new List<Mesh>();
    public GameObject[] blockPrefabs;
    public MeshFilter meshFilter;

    private ToolBox toolBoxScript;
    
    private bool leakBlocked = false;

    private void Start()
    {
        toolBoxScript = GetComponent<ToolBox>();

        assignNewToolMesh();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9) {

            repairCounter += Time.deltaTime;

            if (!leakBlocked)
            {
                //print("A");
                showBlockedParticles(other.gameObject);
                leakBlocked = true;
            }

            if (repairCounter > getToolTimer())
            {
                //print("b");
                showLeakParticles(other.gameObject);
                putNewBlocker(other.gameObject.transform.position);
                repairCounter = 0.0f;
                destroyLeaking(other.gameObject);
                assignNewToolMesh();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9 && leakBlocked == true)
        {
            repairCounter = 0.0f;
            leakBlocked = false;
            showLeakParticles(other.gameObject);
           // print("c");
        }
    }

    private void showBlockedParticles(GameObject gameobject) {
        // ParticleSystem leakParticleSystem = other.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        // ParticleSystem blockedParticleSystem = other.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        Transform leakParticle = gameobject.transform.GetChild(0);
        Transform blockedParticle = gameobject.transform.GetChild(1);

        if (leakParticle.gameObject.activeSelf == true)
        {
            leakParticle.gameObject.SetActive(false);
            blockedParticle.gameObject.SetActive(true);
        }    
    }

    private void showLeakParticles(GameObject gameobject)
    {
        // ParticleSystem leakParticleSystem = other.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        // ParticleSystem blockedParticleSystem = other.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
        Transform leakParticle = gameobject.transform.GetChild(0);
        Transform blockedParticle = gameobject.transform.GetChild(1);

        if (leakParticle.gameObject.activeSelf == false)
        {
            leakParticle.gameObject.SetActive(true);
            blockedParticle.gameObject.SetActive(false);
        }
    }

    private void destroyLeaking(GameObject leakingGO) {
        GameManager.Instance.levelManager.numOfLeakings--;
        Destroy(leakingGO);
    }


    private float getToolTimer() {
        float timer = 0.0f;

        switch (handType)
        {
            case toolType.Empty:
                timer = 0;
                break;
            case toolType.Tape:
                timer = 2;
                break;
            case toolType.Gum:
                timer = 1;
                break;
            case toolType.Sock:
                timer = 1.5f;
                break;

        }

        return timer;
    }

    private void putNewBlocker(Vector3 blockerPosition)
    {
        switch (handType)
        {
            case toolType.Empty:
                Instantiate(blockPrefabs[0], blockerPosition, Quaternion.identity);
                break;
            case toolType.Tape:
                Instantiate(blockPrefabs[1], blockerPosition, Quaternion.identity);
                break;
            case toolType.Gum:
                Instantiate(blockPrefabs[2], blockerPosition, Quaternion.identity);
                break;
            case toolType.Sock:
                Instantiate(blockPrefabs[3], blockerPosition, Quaternion.identity);
                break;
        }

    }

    private void assignNewToolType(int index)
    {
        switch (index)
        {
            case 0:
                handType = toolType.Empty;
                break;
            case 1:
                handType = toolType.Tape;
                break;
            case 2:
                handType = toolType.Sock;
                break;
            case 3:
                handType = toolType.Gum;
                break;
            default:
                break;
        }
    }

    private void assignNewToolMesh()
    {
        int newIndex = toolBoxScript.SetNextTool();
        assignNewToolType(newIndex);
        meshFilter.mesh = handMeshes[newIndex];
    }

}
