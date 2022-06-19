using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{

    [SerializeField] public AudioClip[] footstepAudioClips;
    [SerializeField] public AudioClip[] rollAudioClips;
    [SerializeField] public AudioClip[] attackAudioClips;
    [SerializeField] public AudioClip[] shieldHitAudioClips;
    [SerializeField] public AudioClip[] damageAudioClips;
    [SerializeField] public AudioClip[] deathCriesAudioClips;

    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    IEnumerator CoPlayDelayedClip(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
    }

    public void PlayAudioEffect(AudioClip[] audioClips, float delay = 0.0f)
    {
        if (audioClips.Length > 0)
        {
            AudioClip audioClip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            StartCoroutine(CoPlayDelayedClip(audioClip, delay));
        }
    }
}
