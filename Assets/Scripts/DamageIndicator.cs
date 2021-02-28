using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicator : MonoBehaviour
{
    private const float maxTimer = 3.0f;
    private float timer = maxTimer;

    private CanvasGroup canvasGroup;
    protected CanvasGroup CanvasGroup
    {
        get
        {
            if (canvasGroup == null)
            {
                canvasGroup = GetComponent<CanvasGroup>();
                if (canvasGroup == null)
                {
                    canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }
            return canvasGroup;
        }
    }

    private RectTransform rect = null;
    protected RectTransform Rect
    {
        get
        {
            if (rect == null)
            {
                rect = GetComponent<RectTransform>();
                if (rect == null)
                {
                    rect = gameObject.AddComponent<RectTransform>();
                }
            }
            return rect;
        }
    }

    public Transform Target { get; protected set; } = null;
    private Transform Player = null;

    private IEnumerator IE_Countdown = null;
    private Action unregister = null;

    private Quaternion targetRotation = Quaternion.identity;
    private Vector3 targetPosition = Vector3.zero;

    public void Register(Transform t, Transform p, Action ur)
    {
        this.Target = t;
        this.Player = p;
        this.unregister = ur;

        StartCoroutine(RotateToTheTarget());
        StartTimer();
    }

    public void Restart()
    {
        timer = maxTimer;
        StartTimer();
    }

    public void StartTimer()
    {
        if (IE_Countdown != null) { StopCoroutine(IE_Countdown); }

        IE_Countdown = Countdown();
        StartCoroutine(IE_Countdown);
    }

    IEnumerator RotateToTheTarget()
    {
        while (enabled)
        {
            if (Target)
            {
                targetPosition = Target.position;
                targetRotation = Target.rotation;
            }

            Vector3 direction = Player.position - targetPosition;

            targetRotation = Quaternion.LookRotation(direction);
            targetRotation.z = -targetRotation.y;
            targetRotation.x = 0;
            targetRotation.y = 0;

            Vector3 northDirection = new Vector3(0, 0, Player.eulerAngles.y);
            Rect.localRotation = targetRotation * Quaternion.Euler(northDirection);

            yield return null;
        }
    }

    private IEnumerator Countdown()
    {
        while (CanvasGroup.alpha < 1.0f)
        {
            CanvasGroup.alpha += 4 * Time.deltaTime;
            yield return null;
        }

        while (timer > 0)
        {
            timer--;
            yield return new WaitForSeconds(1);
        }

        while (CanvasGroup.alpha > 0.0f)
        {
            CanvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;
        }

        unregister();
        Destroy(gameObject);
    }
}
