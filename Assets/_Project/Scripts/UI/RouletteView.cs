using System;
using System.Collections;
using UnityEngine;

public class RouletteView : MonoBehaviour
{
    [SerializeField] private Transform wheel;
    [SerializeField] private float spinDuration = 2f;

    private Coroutine spinRoutine;

    public void PlaySpin(Action onComplete)
    {
        if (spinRoutine != null)
            StopCoroutine(spinRoutine);

        float targetRotation = UnityEngine.Random.Range(720f, 1080f);
        spinRoutine = StartCoroutine(SpinRoutine(targetRotation, onComplete));
    }

    private IEnumerator SpinRoutine(float rotationAmount, Action onComplete)
    {
        float elapsed = 0f;
        float startRotation = wheel.eulerAngles.z;
        float targetRotation = startRotation + rotationAmount;

        while (elapsed < spinDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / spinDuration);

            // Ease Out Cubic
            float easedT = 1f - Mathf.Pow(1f - t, 3);

            float currentRotation = Mathf.Lerp(startRotation, targetRotation, easedT);
            wheel.eulerAngles = new Vector3(0f, 0f, currentRotation);

            yield return null;
        }

        wheel.eulerAngles = new Vector3(0f, 0f, targetRotation);
        spinRoutine = null;
        onComplete?.Invoke();
    }
}
