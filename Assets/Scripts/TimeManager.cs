using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{

    public float slowdownFactor = 0.0f;
    public float slowdownLength = 1f;
    public Text dbg;
    void Update()
    {
        //return;
        dbg.text = Time.timeScale.ToString();
    }

    public void DoNormalSpeed()
    {
        //print("NORMAL");
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }
    public void DoSlowmotion()
    {
        //return;
        //print("SLOMO");

        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        float timeDecreese = (1f / slowdownLength) * Time.unscaledDeltaTime;
        if ((Time.timeScale - timeDecreese) > 0)
        {
            Time.timeScale -= timeDecreese;
        }
        else
        {
            Time.timeScale = 0;
        }
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        //Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }

}