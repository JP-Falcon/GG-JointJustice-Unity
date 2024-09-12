using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Detail : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private bool _isPickup;
    [SerializeField] private TextAsset _narrativeScriptToPlay;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
