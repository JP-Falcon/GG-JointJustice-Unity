using System;
using UnityEngine;

public class Actor : Animatable
{
    private ActorData _actorData;
    private IActorController _attachedController; 
    private Vector3 _originalPosition;
    
    public ActorData ActorData
    {
        get => _actorData;
        set
        {
            _actorData = value;
            Animator.runtimeAnimatorController = value.AnimatorController;
            Animator.Play("Normal");
        }
    }
    
    public Renderer Renderer { get; private set; }

    /// <summary>
    /// Call base awake method and also set animator to keep state on disable.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        Renderer = GetComponent<Renderer>();
        Animator.keepAnimatorStateOnDisable = true;
        _originalPosition = transform.position;
    }

    /// <summary>
    /// This method is called by animations when they are completed and require the next line to be to be read.
    /// </summary>
    public override void OnAnimationComplete()
    {
        base.OnAnimationComplete();

        if (_attachedController != null)
        {
            _attachedController.OnAnimationDone();
        }
    }

    /// <summary>
    /// Used to attach an actor controller for callbacks
    /// </summary>
    /// <param name="controller">Actor controller to attach to this object</param>
    public void AttachController(IActorController controller)
    {
        _attachedController = controller;
    }

    /// <summary>
    /// Makes the actor talk while set to true
    /// </summary>
    /// <param name="isTalking">Move mouth or not</param>
    public void SetTalking(bool isTalking)
    {
        Animator.SetBool("Talking", isTalking);
    }

    /// <summary>
    /// Checks if this actor is the actor passed
    /// </summary>
    /// <param name="actor">Actor to compare to</param>
    /// <returns>If the actor is the actor given</returns>
    public bool MatchesActorData(ActorData actor)
    {
        return _actorData == actor;
    }

    public void SetAlignment(ActorAlignment actorAlignment)
    {
        if (Camera.main == null)
        {
            throw new NullReferenceException("Camera.main is null");
        }
        
        var cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
        
        var alignment = actorAlignment switch
        {
            ActorAlignment.Center => 0,
            ActorAlignment.Left => -cameraWidth / 2,
            ActorAlignment.Right => cameraWidth / 2,
            ActorAlignment.OobLeft => -cameraWidth * 2,
            ActorAlignment.OobRight => cameraWidth * 2,
            _ => throw new ArgumentOutOfRangeException(nameof(actorAlignment), actorAlignment, null)
        };
        
        SetSlotOffset(alignment);

        return;

        void SetSlotOffset(float offset)
        { 
            transform.position = new Vector3(
                _originalPosition.x + offset + transform.parent.transform.position.x,
                _originalPosition.y, 
                _originalPosition.z);
        }
    }
}