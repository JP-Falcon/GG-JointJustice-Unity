using UnityEngine;

public enum ItemDisplayPosition
{
    Left,
    Right,
    Middle
}

public interface ISceneController
{
    bool WitnessTestimonyActive { set; }
    string ActiveSceneName { get; }
    
    void FadeIn(float seconds);
    void FadeOut(float seconds);
    void ShakeScreen(float intensity, float duration, bool isBlocking);
    void SetScene(string background);
    void SetCameraPos(Vector2Int position);
    void PanCamera(float seconds, Vector2Int finalPosition, bool isBlocking = false);
    void PanToActorSlot(string slotName, float seconds);
    void JumpToActorSlot(string slotName);
    void ShowItem(ICourtRecordObject item, ItemDisplayPosition position);
    void Wait(float seconds);
    void HideItem();
    void PlayAnimation(string animationName);
    void Shout(string actorName, string shoutName, bool allowRandomShouts);
    void ReloadScene();
}
