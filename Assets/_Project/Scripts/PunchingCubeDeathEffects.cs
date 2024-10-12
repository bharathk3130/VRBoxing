using UnityEngine;

[RequireComponent(typeof(DissolveEffect))]
public class PunchingCubeDeathEffects : MonoBehaviour
{
    [SerializeField] ParticleSystem _deathEffectParticles;
    [SerializeField] AudioSource _deathAudio;
    DissolveEffect _dissolveEffect;

    void Awake()
    {
        _dissolveEffect = GetComponent<DissolveEffect>();
    }

    public void Play()
    {
        _dissolveEffect.Play(DestroySelf);
        _deathAudio.Play();
        _deathEffectParticles.Play();
    }
    
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}