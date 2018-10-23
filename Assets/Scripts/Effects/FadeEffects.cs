using UnityEngine;
using System.Collections;

public class FadeEffects : MonoBehaviour {

    private CanvasGroup canvas;

    void Start()
    {
        canvas = this.gameObject.GetComponent<CanvasGroup>();
    }

    public IEnumerator FadeCanvas(float startAlpha, float endAlpha, float duration)
    {
        // keep track of when the fading started, when it should finish, and how long it has been running&lt;/p&gt; &lt;p&gt;&a
        float startTime = Time.time;
        float endTime = Time.time + duration;
        float elapsedTime = 0f;

        // set the canvas to the start alpha – this ensures that the canvas is ‘reset’ if you fade it multiple times
        canvas.alpha = startAlpha;
        // loop repeatedly until the previously calculated end time
        while (Time.time <= endTime)
        {
            elapsedTime = Time.time - startTime; // update the elapsed time
            var percentage = 1 / (duration / elapsedTime); // calculate how far along the timeline we are
            if (startAlpha > endAlpha) // if we are fading out/down 
            {
                canvas.alpha = startAlpha - percentage; // calculate the new alpha
            }
            else // if we are fading in/up
            {
                canvas.alpha = startAlpha + percentage; // calculate the new alpha
            }

            yield return new WaitForEndOfFrame(); // wait for the next frame before continuing the loop
        }
        canvas.alpha = endAlpha;
    }

    public void InstantChange(float alpha)
    {
        canvas.alpha = alpha;
    }
}
