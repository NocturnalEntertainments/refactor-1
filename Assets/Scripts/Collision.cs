using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private LayerMask wallLayer;

    private Audio audioManager;
    private ParticleSys particleEffectsManager;

    private void Awake()
    {
        audioManager = GetComponent<Audio>();
        particleEffectsManager = GetComponent<ParticleSys>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if ((wallLayer.value & (1 << hit.gameObject.layer)) > 0)
        {

            audioManager?.PlayAudioClip();
            particleEffectsManager?.PlayParticleEffect();
        }
    }
}
