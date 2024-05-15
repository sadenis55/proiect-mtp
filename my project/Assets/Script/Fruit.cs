using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;
    public GameAudio gameAudio;

    private Rigidbody fruitRigidbody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    public bool IsSliced { get; private set; } = false;

    public int NumOfPasses { get; set; } = 0;

    public float startTime;

    private void Awake()
    {
        fruitRigidbody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();
        gameAudio = GameObject.FindGameObjectWithTag("audio").GetComponent<GameAudio>();
        startTime = Time.time;
    }
    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        gameAudio.PlaySFX(gameAudio.slice);
        FindObjectOfType<GameManager>().IncreaseScore();

        whole.SetActive(false);
        sliced.SetActive(true);
        IsSliced = true;

        fruitCollider.enabled = false;
        juiceParticleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach(Rigidbody r in slices) 
        {
            r.velocity = fruitRigidbody.velocity;
            r.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        }
    }
}
