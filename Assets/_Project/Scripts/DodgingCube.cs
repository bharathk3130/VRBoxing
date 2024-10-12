using UnityEngine;

public class DodgingCube : MonoBehaviour
{
    // Settings
    public float Speed = 15;
    public int Lifetime = 20;
    public int DeathPenalty = 15;

    void Start()
    {
        Destroy(gameObject, Lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.Instance.AddScore(-DeathPenalty);
        Destroy(gameObject);
    }
}