using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioStates
{
    FirstStep,
    QuakeTake,
    QuakeUse,
    Collect,
    RoundWin,
    Win,
    Lose
}
[System.Serializable]
public class EnumSoundDict{
    public AudioStates _state;
    public AudioClip[] _clips;
}

public class SoundManager : Singleton<SoundManager>
{
    private void OnEnable()
    {
        
    }
    [SerializeField] EnumSoundDict[] _player1Sounds;
    [SerializeField] EnumSoundDict[] _player2Sounds;
    [SerializeField] AudioSource _textSoundSource;
    [SerializeField] AudioSource _gameSoundSource;
    // Start is called before the first frame update
    public float PlayAudio(AudioStates _state, EntityType _entityType)
    {
        AudioClip _clip = null;
        switch (_entityType)
        {
            case EntityType.Player1:
               _clip=  GetClip(_state, _player1Sounds);
                break;
            case EntityType.Player2:
                _clip = GetClip(_state, _player2Sounds);
                break;
            default:
                return 0;
        }
        float lenght = _clip.length;
        _textSoundSource.PlayOneShot(_clip);
        return lenght;
    }

    AudioClip GetClip(AudioStates _state, EnumSoundDict[] _list)
    {
        for (int i = 0; i < _list.Length; i++)
        {
            if(_state == _list[i]._state)
            {
                var clips = _list[i]._clips;
                return clips[Random.Range(0,clips.Length)];
            }
        }
        return null;
    }
}
