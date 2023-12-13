using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSoundController : MonoBehaviour
{
    [SerializeField] private AudioSource playAS;
    [SerializeField] private AudioClip playSound;

    public void PlayButtonSound()
    {
        playAS.PlayOneShot(playSound);
    }
}
