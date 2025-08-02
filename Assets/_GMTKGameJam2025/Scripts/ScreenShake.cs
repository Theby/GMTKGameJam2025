using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float dampingSpeed = 1.0f;

    Vector3 _initialPosition;
    float _shakeDuration = 0f;
    float _shakeMagnitude = 0.1f;

    void Awake()
    {
        _initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (_shakeDuration > 0)
        {
            transform.localPosition = _initialPosition + Random.insideUnitSphere * _shakeMagnitude;
            _shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            _shakeDuration = 0f;
            transform.localPosition = _initialPosition;
        }
    }

    public void TriggerShake(float duration, float magnitude)
    {
        _shakeDuration = duration;
        _shakeMagnitude = magnitude;
    }
}