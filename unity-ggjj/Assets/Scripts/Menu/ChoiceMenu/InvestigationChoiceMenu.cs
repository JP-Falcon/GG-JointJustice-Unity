using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ChoiceMenu))]
public class InvestigationChoiceMenu : MonoBehaviour
{
    [field: SerializeField] public ChoiceMenu ChoiceMenu { get; private set; }
    [field: SerializeField] public Image SceneImage { get; private set; }
}
