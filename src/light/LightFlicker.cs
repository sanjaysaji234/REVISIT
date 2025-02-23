using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal; // Required for Light2D

public class LightFlicker : MonoBehaviour
{
    private Light2D light2D;
    public float minFlickerTime = 0.1f;
    public float maxFlickerTime = 1f;
    public float flickerDuration = 0.05f;
    public float minIntensity = 0.3f;
    public float maxIntensity = 1.2f;

    void Start()
    {
        light2D = GetComponent<Light2D>();
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minFlickerTime, maxFlickerTime));

            float originalIntensity = light2D.intensity;
            light2D.intensity = Random.Range(minIntensity, maxIntensity);

            yield return new WaitForSeconds(flickerDuration);

            light2D.intensity = originalIntensity;
        }
    }
}
