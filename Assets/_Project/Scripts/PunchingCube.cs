using UnityEngine;

public class PunchingCube : MonoBehaviour
{
    public DodgingCubeDeathEffects DeathEffects;

    // Settings
    public float Speed = 5;
    public int Lifetime = 20;
    public int Score = 5;

    bool _isDead;
    string _handTag;
    
    public void Initialize(CubeType cubeType)
    {
        if (cubeType == CubeType.LeftJab || cubeType == CubeType.LeftHook)
        {
            _handTag = "LeftHand";
        }
        else if (cubeType == CubeType.RightJab || cubeType == CubeType.RightHook)
        {
            _handTag = "RightHand";
        }
        
        Destroy(gameObject, Lifetime);
    }

    void Update()
    {
        if (!_isDead)
        {
            transform.Translate(Vector3.forward * (Speed * Time.deltaTime));
        }
    }
    
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag(_handTag))
        {
            Die();
        }
    }

    void Die()
    {
        _isDead = true;
        GameManager.Instance.AddScore(Score);
        DeathEffects.Play();
    }
}