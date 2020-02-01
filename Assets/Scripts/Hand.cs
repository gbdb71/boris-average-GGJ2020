using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public enum toolType {Empty, Tape, Gum, Sock}
    public toolType handType = toolType.Empty;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) {
            Destroy(other.gameObject);
        }
    }

}
