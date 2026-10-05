
using UnityEngine;

public class AmbientParticlesPSX : MonoBehaviour
{
    [Header("Configuración PSX")]
    public ParticleSystem dustSystem;
    public ParticleSystem ashSystem;
    public ParticleSystem fogParticles;

    [Header("Look PSX")]
    [Range(0, 1)] public float jitterAmount = 0.02f;
    public bool enableVertexSnapping = true;

    void Start()
    {
        SetupDustParticles();
        SetupAshParticles();
        SetupFogParticles();
    }

    void SetupDustParticles()
    {
        var main = dustSystem.main;
        main.startLifetime = new ParticleSystem.MinMaxCurve(4f, 8f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.01f, 0.08f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.005f, 0.02f);
        main.startColor = new Color(0.8f, 0.75f, 0.6f, 0.4f); // sepia
        main.maxParticles = 200;
        main.simulationSpace = ParticleSystemSimulationSpace.World;

        var emission = dustSystem.emission;
        emission.rateOverTime = 15f;

        var shape = dustSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(20f, 5f, 20f);

        // Movimiento errático tipo PSX
        var noise = dustSystem.noise;
        noise.enabled = true;
        noise.strength = 0.3f;
        noise.frequency = 0.15f;
        noise.scrollSpeed = 0.05f;
        noise.quality = ParticleSystemNoiseQuality.Low; // baja calidad = más PSX

        // Fade in/out
        var colorOverLifetime = dustSystem.colorOverLifetime;
        colorOverLifetime.enabled = true;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(Color.white, 0f),
                new GradientColorKey(Color.white, 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(0f, 0f),
                new GradientAlphaKey(0.4f, 0.2f),
                new GradientAlphaKey(0.4f, 0.8f),
                new GradientAlphaKey(0f, 1f)
            }
        );
        colorOverLifetime.color = gradient;
    }

    void SetupAshParticles()
    {
        var main = ashSystem.main;
        main.startLifetime = new ParticleSystem.MinMaxCurve(6f, 12f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(-0.05f, -0.15f); // cae hacia abajo
        main.startSize = new ParticleSystem.MinMaxCurve(0.01f, 0.04f);
        main.startColor = new Color(0.3f, 0.3f, 0.3f, 0.6f);
        main.gravityModifier = -0.02f; // cae muy lento
        main.maxParticles = 100;

        var emission = ashSystem.emission;
        emission.rateOverTime = 5f;

        var shape = ashSystem.shape;
        shape.shapeType = ParticleSystemShapeType.Box;
        shape.scale = new Vector3(25f, 1f, 25f);
        // Posicionar arriba del jugador
        ashSystem.transform.localPosition = new Vector3(0, 8f, 0);

        // Drift lateral
        var velocityOverLifetime = ashSystem.velocityOverLifetime;
        velocityOverLifetime.enabled = true;
        velocityOverLifetime.x = new ParticleSystem.MinMaxCurve(-0.1f, 0.1f);
        velocityOverLifetime.z = new ParticleSystem.MinMaxCurve(-0.05f, 0.05f);
    }

    void SetupFogParticles()
    {
        var main = fogParticles.main;
        main.startLifetime = new ParticleSystem.MinMaxCurve(8f, 15f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(0.005f, 0.03f);
        main.startSize = new ParticleSystem.MinMaxCurve(2f, 5f);  // grande y difuso
        main.startColor = new Color(0.7f, 0.65f, 0.6f, 0.05f);   // muy transparente
        main.maxParticles = 30;

        // Renderer con soft particles
        var renderer = fogParticles.GetComponent<ParticleSystemRenderer>();
        renderer.renderMode = ParticleSystemRenderMode.Billboard;
        renderer.sortingFudge = -1f; // render detrás
    }
}