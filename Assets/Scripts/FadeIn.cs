//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class FadeIn : MonoBehaviour
//{
//    public Canvas obj;

//    private bool canFade;
//    private float alpha = 1.0f;
//    private float timeToFade = 0.003f;

//    public void Start()
//    {
//        Debug.Log("Start transition");
//        startFading();
//        canFade = true;
//    }


//    public void Update()
//    {
//        if (canFade)
//        {
//            obj.GetComponent<CanvasGroup>().alpha = Color.Lerp(obj.color, alphaColor, timeToFade * Time.deltaTime);
//            if (100 - obj.color.a < 0.001) {
//                canFade = false;
//                Debug.Log("Stop transition");
//            }
//        }
//    }

//    public void startFading() {
//        canFade = true;

//    }
//}
