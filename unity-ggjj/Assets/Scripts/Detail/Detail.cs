using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PolygonCollider2D))]
public class Detail : MonoBehaviour
{
    [SerializeField] private bool _isPickup;
    [SerializeField] private TextAsset _narrativeScriptToPlay;

    private void Awake()
    {
        if (_narrativeScriptToPlay == null)
        {
            Debug.LogError("No narrative script assigned to detail", this);
        }
    }

    public void AttemptPickUp()
    {
        if (!_isPickup)
        {
            return;
        }
        
        gameObject.SetActive(false);
    }
    
    public TextAsset NarrativeScriptToPlay => _narrativeScriptToPlay;
}
