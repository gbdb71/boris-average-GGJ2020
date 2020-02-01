using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public enum toolType {Empty, Tape, Gum, Sock}
    public toolType handType = toolType.Empty;
    public float repairCounter = 0.0f;

    private bool leakBlocked = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 9) {

            repairCounter += Time.deltaTime;

            if (!leakBlocked)
            {
                // ParticleSystem leakParticleSystem = other.gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
                // ParticleSystem blockedParticleSystem = other.gameObject.transform.GetChild(1).GetComponent<ParticleSystem>();
                Transform leakParticle = other.gameObject.transform.GetChild(0);
                Transform blockedParticle = other.gameObject.transform.GetChild(1);

                leakParticle.gameObject.SetActive(false);
                blockedParticle.gameObject.SetActive(true);

                leakBlocked = true;
            }

            if (repairCounter > getToolTimer(handType))
                destroyLeaking(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            repairCounter = 0.0f;
            leakBlocked = false;
        }
    }

    private void destroyLeaking(GameObject leakingGO) {
        repairCounter = 0.0f;
        GameManager.Instance.levelManager.numOfLeakings--;
        Destroy(leakingGO);
    }


    private float getToolTimer(toolType type) {
        float timer = 0.0f;

        switch (type)
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

}
