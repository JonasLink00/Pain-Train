using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //public IEnumerator Shake(float duration, float magnitude)
    //{
    //    Vector3 startposCam = transform.position;

    //    float elapsed = 0.0f;

    //    while (elapsed < duration)
    //    {
    //        float x = Random.Range(-1f, 1f) * magnitude;
    //        float y = Random.Range(-1f, 1f) * magnitude;

    //        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, startposCam.z);

    //        elapsed += Time.deltaTime;

    //        yield return null;
    //    }

    //    transform.position = startposCam;
    //}

    public static CameraShake instance;

    private void Awake() => instance = this;

    private void OnShake(float duration, float strength)
    {
        transform.DOShakePosition(duration, strength);
        transform.DOShakeRotation(duration, strength);
    }

    public static void Shake(float duration, float strength) => instance.OnShake(duration, strength);
}
