using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource musicControllerAS, menuAS;
    [SerializeField] private AudioClip inGameAudio, menuAudio;
    [SerializeField] private GameObject inGameObj;
    private bool menuPlayed, inGamePlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        menuPlayed = false;
        inGamePlayed = false;

        musicControllerAS.loop = true;
        menuAS.loop = true;
        musicControllerAS.volume = 0.4f;
    }


    // Update is called once per frame
    void Update()
    {
        if (inGamePlayed == false && inGameObj.activeSelf.Equals(true))
        {
            StartMusic(inGameAudio, musicControllerAS);
            inGamePlayed = true;
        }
        else if (inGamePlayed == true && inGameObj.activeSelf.Equals(false))
        {
            inGamePlayed = false;
            PauseMusic(musicControllerAS);
        }
        if (menuPlayed == false && menuAS.gameObject.activeSelf.Equals(true))
        {
            StartMusic(menuAudio, menuAS);
            menuPlayed = true;
        }
        else if (menuPlayed == true && menuAS.gameObject.activeSelf.Equals(false))
        {
            menuPlayed = false;
            PauseMusic(menuAS);
        }
    }

    private void StartMusic(AudioClip audioTrack, AudioSource audioSource)
    {
        if (audioSource != null && audioTrack != null)
        {
            audioSource.clip = audioTrack;
            audioSource.volume = 0.4f;

            audioSource.Play();

        }
    }

    private void PauseMusic(AudioSource audioSource)
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }

    }
}
