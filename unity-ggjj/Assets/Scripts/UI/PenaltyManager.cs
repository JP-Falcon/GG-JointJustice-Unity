using System.Collections.Generic;
using UnityEngine;

public class PenaltyManager : MonoBehaviour
{
    [Tooltip("Drag the prefab for the penalty UI object here.")]
    [SerializeField]private Animator _penaltyObject;

    private readonly Queue<Animator> _penaltyObjects = new Queue<Animator>();

    public int PenaltiesLeft => _penaltyObjects.Count;

    /// <summary>
    /// Creates a specified number of penalties.
    /// </summary>
    /// <param name="penaltyCount">The number of penalties to create.</param>
    public void Initialize(int penaltyCount)
    {
        for (int i = 0; i < penaltyCount; i++)
        {
            _penaltyObjects.Enqueue(Instantiate(_penaltyObject, transform));
        }
    }
    
    /// <summary>
    /// Decreases the number of penalties available to the player by one.
    /// </summary>
    /// <returns>True if number of penalties left is greater than 0, false if it is not.</returns>
    public void Decrement()
    {
        Debug.Assert(PenaltiesLeft > 0, "Decrement must not be called with 0 or fewer penalty lifelines left");
        _penaltyObjects.Dequeue().Play("Explosion");
    }
}
