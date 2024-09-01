using System;
using System.Collections;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

[assembly: InternalsVisibleTo("PlayModeTests")]
/// <summary>
/// Handles playing back .ogg files with optional loop markers
/// </summary>
public class LoopableMusicClip
{
    private readonly string _clipName;
    private float[] _rawData;
    private int _preLoopSampleLength;
    private int _loopSampleLength;
    private int _fullTrackSampleLength;
    private int _sampleRate;
    private int _channelCount;

    /// <summary>
    /// Creates a new instance of LoopableMusicClip.<br />
    /// Use `this.Initialize(oggFilePath)` to await and load an .ogg file. After awaiting, use the `Clip` property to get an AudioClip for playback.
    /// </summary>
    /// <seealso cref="Initialize"/>
    /// <seealso cref="Clip"/>
    /// <seealso cref="ContinueLooping"/>"/>
    public LoopableMusicClip()
    {
        
    }

    internal int TimeToSamples(double time)
    {
        return (int) (time * _sampleRate * _channelCount);
    }
    
    internal double SamplesToTime(int samples)
    {
        return (double) samples / _sampleRate / _channelCount;
    }

    /// <summary>
    /// If true (default), the track will loop indefinitely.<br />
    /// If false, the track will finish the current loop, optionally play an outro, if data after the LOOP_END point exists and then stop.
    /// </summary>
    public bool ContinueLooping { get; set; } = true;

    private AudioClip _clip;
 
    internal void SetCurrentPlaybackHead(int position)
    {
        _currentPlaybackHead = position;
    }

    private int _currentPlaybackHead;

    internal void GenerateStream(float[] data)
    {
        int dataIndex = 0;

        while (dataIndex < data.Length)
        {
            if (_currentPlaybackHead < _preLoopSampleLength)
            {
                var availableSampleLength = Math.Min(data.Length - dataIndex, _preLoopSampleLength - _currentPlaybackHead);
                Array.Copy(_rawData, _currentPlaybackHead, data, dataIndex, availableSampleLength);
                _currentPlaybackHead += availableSampleLength;
                dataIndex += availableSampleLength;
            }

            if (dataIndex >= data.Length)
            {
                break;
            }

            if (_currentPlaybackHead >= _preLoopSampleLength && _currentPlaybackHead < (_loopSampleLength + _preLoopSampleLength))
            {
                var availableSampleLength = Math.Min(data.Length - dataIndex, _loopSampleLength + _preLoopSampleLength - _currentPlaybackHead);
                Array.Copy(_rawData, _currentPlaybackHead, data, dataIndex, availableSampleLength);
                _currentPlaybackHead += availableSampleLength;
                dataIndex += availableSampleLength;
            }

            if (dataIndex >= data.Length)
            {
                break;
            }

            if (_currentPlaybackHead >= (_loopSampleLength + _preLoopSampleLength))
            {
                if (!ContinueLooping)
                {
                    var availableSamplesOfEnd = _fullTrackSampleLength - _currentPlaybackHead;
                    Array.Copy(_rawData, _currentPlaybackHead, data, dataIndex, availableSamplesOfEnd);
                    dataIndex += availableSamplesOfEnd;
                    _currentPlaybackHead = _fullTrackSampleLength;
                    break;
                }
                _currentPlaybackHead = _preLoopSampleLength;
            }
        }

        if (dataIndex < data.Length)
        {
            Array.Clear(data, dataIndex, data.Length - dataIndex);
        }
    }
    
    /// <summary>
    /// Retrieves an AudioClip for the .ogg file, that will loop indefinitely by default.
    /// </summary>
    /// <remarks>
    /// An .ogg file can optionally specify a section to loop inside the file's metadata:<br />
    ///     `LOOP_START`: the start of the loop in the format`HH:MM:SSmmm<br />
    ///     `LOOP_END`: the end of the loop in the format`HH:MM:SSmmm<br />
    /// If no loop markers are specified, the entire file will be looped.<br />
    /// </remarks>
    /// <seealso cref="ContinueLooping"/>
    /// <exception cref="InvalidOperationException">Thrown when this.Initialize(oggFile) has not been called</exception>
    public AudioClip Clip
    {
        get
        {
            if (_rawData == null || _rawData.Length == 0)
            {
                throw new InvalidOperationException($"Clip has not been initialized yet; call {nameof(Initialize)}(pathToOGGWithinMusicDirectory) first");
            }
            if (_clip != null)
            {
                return _clip;
            }

            _clip = AudioClip.Create("preLoop", _preLoopSampleLength + _loopSampleLength, _channelCount, _sampleRate, true, GenerateStream, SetCurrentPlaybackHead);
            return _clip;
        }
    }

    /// <summary>
    /// Assigns an .ogg file, reads the loop markers and prepares `Clip`
    /// </summary>
    /// <returns>An IEnumerator to be used with `yield return` to wait loading and processing of the .ogg file</returns>
    /// <exception cref="NotSupportedException">Thrown when the file is not an `.ogg` file</exception>
    /// <seealso cref="Clip"/>
    public IEnumerator Initialize(string pathToOGGWithinMusicDirectory)
    {
        if (!pathToOGGWithinMusicDirectory.EndsWith(".ogg"))
        {
            throw new NotSupportedException("Only ogg files are supported");
        }

        var path = Application.streamingAssetsPath + "/Music/" + pathToOGGWithinMusicDirectory;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX || UNITY_EDITOR_LINUX || UNITY_STANDALONE_LINUX || UNITY_EMBEDDED_LINUX || UNITY_CLOUD_BUILD || UNITY_SERVER || UNITY_IOS || UNITY_TVOS
        path = "file://" + path;
#endif
        
        using var www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.OGGVORBIS);
        yield return www.SendWebRequest();

        using var vorbis = new NVorbis.VorbisReader(new MemoryStream(www.downloadHandler.data));
        
        _rawData = new float[vorbis.TotalSamples * vorbis.Channels];
        vorbis.ReadSamples(_rawData, 0, (int) vorbis.TotalSamples * vorbis.Channels);
        _sampleRate = vorbis.SampleRate;
        _channelCount = vorbis.Channels;
        _fullTrackSampleLength = TimeToSamples(vorbis.TotalTime.TotalSeconds);
        
        var loopStart = vorbis.Tags.GetTagSingle("LOOP_START");
        var loopEnd = vorbis.Tags.GetTagSingle("LOOP_END");
        
        if (string.IsNullOrEmpty(loopStart) || string.IsNullOrEmpty(loopEnd))
        {
            _preLoopSampleLength = TimeToSamples(0);
            _loopSampleLength = TimeToSamples(vorbis.TotalTime.TotalSeconds);
        }
        else
        {
            var loopStartSeconds = TimeSpan.Parse(loopStart).TotalSeconds;
            var loopEndSeconds = TimeSpan.Parse(loopEnd).TotalSeconds;
            _preLoopSampleLength = TimeToSamples(loopStartSeconds);
            _loopSampleLength = TimeToSamples(loopEndSeconds - loopStartSeconds);
        }
    }

}