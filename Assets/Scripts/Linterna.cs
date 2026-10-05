using UnityEngine;

public class Linterna : MonoBehaviour
{
    public Light LuzLinterna;

    [Header("Sonidos")]
    public AudioSource audioSource;
    public AudioClip sonidoEncender;
    public AudioClip sonidoApagar;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            LuzLinterna.enabled = !LuzLinterna.enabled;

            if (LuzLinterna.enabled)
            {
                if (audioSource != null && sonidoEncender != null)
                    audioSource.PlayOneShot(sonidoEncender);
            }
            else
            {
                if (audioSource != null && sonidoApagar != null)
                    audioSource.PlayOneShot(sonidoApagar);
            }
        }
    }
}