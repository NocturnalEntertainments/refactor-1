using UnityEngine;

public class ParticleSys : MonoBehaviour
{
    [SerializeField] private ParticleSystem particEffect;

    public void PlayParticleEffect()
    {
        if (particEffect != null && !particEffect.isPlaying)
        {
            particEffect.Stop();
            particEffect.Play();
        }
    }
}