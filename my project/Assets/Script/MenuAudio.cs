using UnityEngine;

public class MenuAudio : MonoBehaviour
{
    [SerializeField] AudioSource music;
    public AudioClip background;

    private void Start()
    {
        music.clip = background;
        music.Play();
    }
}
