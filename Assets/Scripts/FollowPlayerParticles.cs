using UnityEngine;

public class FollowPlayerParticles : MonoBehaviour
{
    public Transform player;
    public float updateRate = 0.1f; 

    private float timer;
    private Vector3 targetPos;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateRate)
        {
            timer = 0f;
            targetPos = new Vector3(
                Mathf.Round(player.position.x),
                transform.position.y,
                Mathf.Round(player.position.z)
            );
            transform.position = targetPos;
        }
    }
}