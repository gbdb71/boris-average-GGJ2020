using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [Header("Animator")]
    public Animator animator;

    [Header("Text")]
    public TextMeshProUGUI textScore = null;

  /*  public void GameOver()
    {
        GameOver(0);
    }*/
        
   /* public void GameOver(float score)
    {
        animator.SetTrigger("Fade_In");
        UpdateText(score);
    }*/


    public void UpdateText(float score)
    {
        float seconds = score % 60;
        float minutes = ((int)(score / 60) % 60);
        textScore.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    } 

    private void ClearText()
    {
        textScore.text = "";
    } 

   /* public void GameRestart() {
        animator.SetTrigger("Fade_Out");
    }*/
}
