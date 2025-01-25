using UnityEngine;

public class ParticleSys : MonoBehaviour
{
    [SerializeField] private ParticleSystem particEffect;

    public void PlayParticleEffect()
    {
        particEffect.Play();
    }
}