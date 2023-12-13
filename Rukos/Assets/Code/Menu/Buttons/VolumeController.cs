using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class VolumeController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] internal List<Sprite> soundIcons;
    private Button volumeButton;



    // Start is called before the first frame update
    void Start()
    {
        volumeButton = GetComponent<Button>();

        if (AudioListener.volume == 0)
        {
            volumeButton.image.sprite = soundIcons[1];
        }
        else
        {
            volumeButton.image.sprite = soundIcons[0];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }

        if (AudioListener.volume == 0)
        {
            volumeButton.image.sprite = soundIcons[1];
        }
        else
        {
            volumeButton.image.sprite = soundIcons[0];
        }
    }
}
