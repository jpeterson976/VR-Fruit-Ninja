using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageIndicatorSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private DamageIndicator indicatorPrefab = null;
    [SerializeField] private RectTransform holder = null;
    [SerializeField] private new Camera camera = null;
    [SerializeField] private Transform player = null;

    private Dictionary<Transform, DamageIndicator> indicators = new Dictionary<Transform, DamageIndicator>();

    #region Delegates
    public static Action<Transform> CreateIndicator = delegate { };
    public static Func<Transform, bool> CheckIfObjectInSight = null;
    #endregion

    private void OnEnable()
    {
        CreateIndicator += Create;
        CheckIfObjectInSight += InSight;
    }
    private void OnDisable()
    {
        CreateIndicator -= Create;
        CheckIfObjectInSight -= InSight;
    }

    void Create(Transform target)
    {
        if (indicators.ContainsKey(target))
        {
            indicators[target].Restart();
            return;
        }

        DamageIndicator newIndicator = Instantiate(indicatorPrefab, holder);
        newIndicator.Register(target, player, new Action( () => { indicators.Remove(target); } ));

        indicators.Add(target, newIndicator);
    }

    bool InSight(Transform t)
    {
        Vector3 screenpoint = camera.WorldToViewportPoint(t.position);
        return screenpoint.z > 0 && screenpoint.x > 0 && screenpoint.x < 1 && screenpoint.y > 0 && screenpoint.y < 1;
    }
}
