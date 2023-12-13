using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

internal abstract class ButtonComponents : MonoBehaviour
{
    internal CanvasManager canvasManager;
    //private Button button1, button2, button3;
    //private Image Img1, Img2, Img3;

    internal Button button1, button2, button3, button4;
    internal Image img1, img2, img3, img4;

    public virtual Button Button1 { get; set; }
    public virtual Button Button2 { get; set; }
    public virtual Button Button3 { get; set; }
    public virtual Button Button4 { get; set; }

    public virtual Image Img1 { get; set; }
    public virtual Image Img2 { get; set; }
    public virtual Image Img3 { get; set; }
    public virtual Image Img4 { get; set; }

    internal virtual void Start()
    {
        canvasManager = GetComponentInParent<CanvasManager>();
    }

    public virtual void ActivateComponents()
    {
        Button1.enabled = true;
        Button2.enabled = true;
        Button3.enabled = true;

        Img1.enabled = true;
        Img2.enabled = true;
        Img3.enabled = true;
    }
    public virtual void DeactivateComponents()
    {
        Button1.enabled = false;
        Button2.enabled = false;
        Button3.enabled = false;

        Img1.enabled = false;
        Img2.enabled = false;
        Img3.enabled = false;
    }
}
