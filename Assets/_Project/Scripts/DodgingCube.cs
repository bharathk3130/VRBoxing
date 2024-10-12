using UnityEngine;

public class DodgingCube : MonoBehaviour
{
    // Settings
    public float Speed = 15;
    public int Lifetime = 20;
    public int DeathPenalty = 15;

    GameManager gameManager;

    public void Initialize(GameManager gameManager)
    {
        this.gameManager = gameManager;
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
        gameManager.AddScore(-DeathPenalty);
        Destroy(gameObject);
    }
}