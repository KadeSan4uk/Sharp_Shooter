using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class LowHPImage : MonoBehaviour
{
    public Image lowHPImage;
    public float blinkSpeed = 1f;
    

    private void OnEnable()
    {
        StartCoroutine(BlinkEffect());
    }

    private void OnDisable()
    {
        StopCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        while (true)
        {
            float alpha = Mathf.PingPong(Time.time * blinkSpeed, 0.4f) + 0.1f;
            Color color = lowHPImage.color;
            color.a = alpha;
            lowHPImage.color = color;
            yield return null;
        }
    }


}
