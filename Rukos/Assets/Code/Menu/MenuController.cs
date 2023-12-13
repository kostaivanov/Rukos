using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

internal class MenuController : MenusComponents
{
    [SerializeField] internal GameObject shapes, tubes, fittings, shadow, intersection, stopObject, loadingObject;
    [SerializeField] private Button volumeButton, playButton, RecordsButton, exitButton;
    [SerializeField] private Image volumeImg, playImg, RecordsImg, exitImg;

    public override GameObject Obj1 { get { return shapes; } set { shapes = value; } }
    public override GameObject Obj2 { get { return tubes; } set { tubes = value; } }
    public override GameObject Obj3 { get { return fittings; } set { fittings = value; } }
    public override GameObject Obj4 { get { return shadow; } set { shadow = value; } }
    public override Button Button1 { get { return volumeButton; } set { volumeButton = value; } }
    public override Button Button2 { get { return playButton; } set { playButton = value; } }
    public override Button Button3 { get { return RecordsButton; } set { RecordsButton = value; } }
    public override Button Button4 { get { return exitButton; } set { exitButton = value; } }
    public override Image Img1 { get { return volumeImg; } set { volumeImg = value; } }
    public override Image Img2 { get { return playImg; } set { playImg = value; } }
    public override Image Img3 { get { return RecordsImg; } set { RecordsImg = value; } }
    public override Image Img4 { get { return exitImg; } set { exitImg = value; } }


    private void OnEnable()
    {
        StopAllCoroutines();

        base.DeactivateObjects();
        base.ActivateComponents();

        loadingObject.SetActive(false);
        intersection.SetActive(false);
        stopObject.SetActive(false);

        Button4.enabled = true;
        Img4.enabled = true;

        Application.targetFrameRate = 30;
        QualitySettings.vSyncCount = 0;       
    }
}
