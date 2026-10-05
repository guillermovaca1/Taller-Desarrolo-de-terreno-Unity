using UnityEngine;

public class SilentHillFogPulse : MonoBehaviour
{
    public ParticleSystem[] fogSystems;
    public float pulseSpeed = 0.3f;
    public float minDensity = 5f;
    public float maxDensity = 20f;

    private float noiseOffset;

    void Start()
    {
        noiseOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * pulseSpeed, noiseOffset);
        float density = Mathf.Lerp(minDensity, maxDensity, noise);

        foreach (var system in fogSystems)
        {
            var emission = system.emission;
            emission.rateOverTime = density;
        }
    }
}