using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera instance;

    private Vector3 _originalPos;
    private float _timeAtCurrentFrame;
    private float _timeAtLastFrame;
    private float _fakeDelta;
    private bool shaking;
    void Awake()
    {
        instance = this;
        _originalPos = instance.gameObject.transform.localPosition;
        shaking = false;
    }

    void Update()
    {
        // Calculate a fake delta time, so we can Shake while game is paused.
        _timeAtCurrentFrame = Time.realtimeSinceStartup;
        _fakeDelta = _timeAtCurrentFrame - _timeAtLastFrame;
        _timeAtLastFrame = _timeAtCurrentFrame;

        if (shaking == false && gameObject.transform.localPosition != _originalPos)
        {
            transform.localPosition = _originalPos;
        }
    }

    public static void Shake(float duration, float amount)
    {
        //instance._originalPos = instance.gameObject.transform.localPosition;
        //instance.StopAllCoroutines();
        instance.StartCoroutine(instance.cShake(duration, amount));
    }

    public IEnumerator cShake(float duration, float amount)
    {
        float endTime = Time.time + duration;

        while (duration > 0)
        {
            transform.localPosition = _originalPos + Random.insideUnitSphere * amount;
            shaking = true;
            duration -= _fakeDelta;

            yield return null;
        }

        transform.localPosition = _originalPos;
        shaking = false;
    }
}
