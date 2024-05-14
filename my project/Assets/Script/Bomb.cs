using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameAudio gameAudio;
    private void Awake()
    {
        gameAudio = GameObject.FindGameObjectWithTag("audio").GetComponent<GameAudio>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameAudio.PlaySFX(gameAudio.bomb);
            FindObjectOfType<GameManager>().Explode();
        }
    }
}
