using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EvidenceController : MonoBehaviour, IEvidenceController
{
    [SerializeField] private NarrativeGameState _narrativeGameState;
    
    [Tooltip("This event is called when the PRESENT_EVIDENCE action is called.")]
    [SerializeField] private UnityEvent _onRequirePresentEvidence;
    
    public List<Evidence> CurrentEvidence { get; } = new List<Evidence>();
    public List<ActorData> CurrentProfiles { get; } = new List<ActorData>();

    /// <summary>
    /// Adds a piece of evidence to the evidence menu.
    /// </summary>
    /// <param name="evidence">The evidence to add.</param>
    public void AddEvidence(Evidence evidence)
    {
        CurrentEvidence.Add(evidence);
    }

    /// <summary>
    /// Removes a piece of evidence from the evidence menu.
    /// </summary>
    /// <param name="evidence">The evidence object to remove</param>
    public void RemoveEvidence(Evidence evidence)
    {
        CurrentEvidence.Remove(evidence);
    }

    /// <summary>
    /// Adds an actor to the court record.
    /// </summary>
    /// <param name="actor">The actor to add.</param>
    public void AddRecord(ActorData actor)
    {
        CurrentProfiles.Add(actor);
    }

    /// <summary>
    /// Method called by DirectorActionDecoder to open the evidence menu and require the user to present a piece of evidence.
    /// Calls an event which should open (and disable closing of) the evidence menu.
    /// </summary>
    public void RequirePresentEvidence()
    {
        _onRequirePresentEvidence.Invoke();
    }

    /// <summary>
    /// Substitutes a piece of evidence with its assigned alternate evidence.
    /// </summary>
    /// <param name="initialEvidence">The evidence to be substituted</param>
    /// <param name="substituteEvidence">The evidence to substitute <see cref="initialEvidence"/> with</param>
    public void SubstituteEvidence(Evidence initialEvidence, Evidence substituteEvidence)
    {
        CurrentEvidence[CurrentEvidence.IndexOf(initialEvidence)] = substituteEvidence;
    }
}
