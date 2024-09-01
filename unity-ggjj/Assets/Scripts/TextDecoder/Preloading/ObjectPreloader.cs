using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

/// <summary>
/// Assigned to an ActionDecoder in place of the usual controller interfaces.
/// Action decoder calls the methods which are used to load any required objects.
/// Objects are then stored in the assign ObjectStorage object.
/// </summary>
public class ObjectPreloader : ActionDecoderBase
{
    private readonly ObjectStorage _objectStorage;

    public ObjectPreloader(ObjectStorage objectStorage)
    {
        _objectStorage = objectStorage;
    }

    #region Actions
    protected override void ADD_EVIDENCE(EvidenceAssetName evidenceName)
    {
        LoadEvidence(evidenceName);
    }

    protected override void ADD_RECORD(ActorAssetName actorName)
    {
        LoadActor(actorName);
    }

    protected override void PLAY_SFX(SfxAssetName sfx)
    {
        LoadObject<AudioClip>($"Audio/SFX/{sfx}");
    }

    protected override void PLAY_SONG(StaticSongAssetName staticSongName, float transitionTime = 0)
    {
        var loop = new LoopableMusicClip();
        var coroutine = loop.Initialize($"Audio/Music/Static/{staticSongName}");
        while (coroutine.MoveNext())
        {
            Task.Delay(1).Wait();
        }

        loop.Clip.name = Path.GetFileNameWithoutExtension(staticSongName);
        if (!_objectStorage.Contains(loop.Clip))
        {
            _objectStorage.Add(loop.Clip);
        }
    }

    protected override void PLAY_SONG_VARIANT(DynamicSongAssetName dynamicSongName, DynamicSongVariantAssetName variantName, float transitionTime = 0)
    {
        LoadObject<DynamicMusicData>($"Audio/Music/Dynamic/{dynamicSongName}/{dynamicSongName}");
    }

    protected override void SCENE(SceneAssetName sceneName)
    {
        LoadObject<BGScene>($"BGScenes/{sceneName}");
    }

    protected override void SHOW_ITEM(CourtRecordItemName itemName, ItemDisplayPosition itemPos)
    {
        LoadEvidence(itemName);
    }

    protected override void ACTOR(ActorAssetName actorName)
    {
        LoadActor(actorName);
    }

    protected override void SPEAK(ActorAssetName actorName)
    {
        LoadActor(actorName);
    }

    protected override void SPEAK_UNKNOWN(ActorAssetName actorName)
    {
        LoadActor(actorName);
    }

    protected override void THINK(ActorAssetName actorName)
    {
        LoadActor(actorName);
    }

    protected override void SET_ACTOR_POSITION(string slotName, ActorAssetName actorName)
    {
        LoadActor(actorName);
    }
    #endregion
    
    private void LoadActor(string actorName)
    {
        LoadObject<ActorData>($"Actors/{actorName}");
    }
    
    private void LoadEvidence(string evidenceName)
    {
        LoadObject<EvidenceData>($"Evidence/{evidenceName}");
    }

    /// <summary>
    /// Loads an object and adds it to the object storage
    /// </summary>
    /// <param name="path">The path to the object to load</param>
    private void LoadObject<T>(string path) where T : Object
    {
        try
        {
            var obj = Resources.Load<T>(path);
            if (!_objectStorage.Contains(obj))
            {
                _objectStorage.Add(obj);
            }
        }
        catch (NullReferenceException exception)
        {
            throw new ObjectLoadingException($"{exception.GetType().Name}: Object at path {path} could not be loaded", exception);
        }
    }
}