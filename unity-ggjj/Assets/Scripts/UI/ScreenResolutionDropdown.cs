using System.Linq;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Dropdown))]
public class ScreenResolutionDropdown : MonoBehaviour
{
    private TMP_Dropdown _dropdown;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
    }

    private void OnEnable()
    {
        var options = Screen.resolutions
            .Where(resolution => Mathf.Approximately((float)resolution.width / resolution.height, 16f / 9f))
            .Select(res => $"{res.width}x{res.height}")
            .ToList();
        
        _dropdown.ClearOptions();
        _dropdown.AddOptions(options);
        
        _dropdown.value = Screen.resolutions.Select((res, index) => new {res, index})
            .OrderBy(x => Mathf.Abs(x.res.width - Screen.width) + Mathf.Abs(x.res.height - Screen.height))
            .First().index;
        
        _dropdown.RefreshShownValue();
        
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }

    private static void OnDropdownValueChanged(int currentlySelectedIndex)
    {   
        var currentlySelectedResolution = Screen.resolutions[currentlySelectedIndex];
        Screen.SetResolution(currentlySelectedResolution.width, currentlySelectedResolution.height, Screen.fullScreen);
    }
}
