using System;
using SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Sprite _alternateSprite;
    [SerializeField] private SceneLoader _sceneLoader;
    public Func<float> GetRandomValue = () => Random.Range(0f, 1f);
    
    private void Start()
    {
        if (GetRandomValue() < 0.05f)
        {
            GetComponent<Image>().sprite = _alternateSprite;
        }

        _sceneLoader.LoadScene("MainMenu");
    }
}
