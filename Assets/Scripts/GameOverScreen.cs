using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Animator animator;

    public void GameOver() {
        animator.SetTrigger("Fade_In");
    }

    public void GameRestart() {
        Debug.Log("CLICK");
        animator.SetTrigger("Fade_Out");
    }

    public void Update() {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameOver();
		}
    }

}
