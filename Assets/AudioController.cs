using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance;
    public AudioClip clickClip;
    public AudioClip dropClip;
    public AudioClip pickUpClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (Instance == null )
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayClick()
    {
        audioSource.clip = clickClip;
        audioSource.Play();
    }

    public void PlayDrop()
    {
        audioSource.clip = dropClip;
        audioSource.Play();
    }

    public void PlayPickUp()
    {
        audioSource.clip = pickUpClip;
        audioSource.Play();
    }
}
