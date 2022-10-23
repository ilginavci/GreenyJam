using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private PlayerEntity _player;
    int _earthquakePower = 0;
    public int EarthquakePower
    {
        get { return _earthquakePower; }
        set { _earthquakePower = value; }
    }
    int _collectedItem= 0;
    int _collectedOtherPlayerItem= 0;
    int _score;
    [Header("ScorSettings")]
    [SerializeField] int _earnedScoreByCollectable;
    [SerializeField] int _lostedScoreByCollectable;
    [SerializeField] int _lostedScoreByLostingCollectable;
    List<int> _scoreBuffer = new List<int>();
    public int Score => _score;
    public void AddInventory(Entity _entity)
    {
        EntityType _entityType = _entity._type;

        switch (_entityType)
        {
            case EntityType.Player1Item:
                CheckScore(EntityType.Player1);
                break;
            case EntityType.Player2Item:
                CheckScore(EntityType.Player2);
                break;
            case EntityType.EarthquakePower:
                SoundManager.Instance.PlayAudio(AudioStates.QuakeTake, _player._type);
                _earthquakePower++;
                break;
            default:
                break;
        }
    }
    void CheckScore(EntityType _playerType)
    {
        if (_player._type == _playerType)
        {
            AddScore(_earnedScoreByCollectable);
            _collectedItem++;
            SoundManager.Instance.PlayAudio(AudioStates.Collect, _player._type);
        }
        else
        {
            AddScore(-_lostedScoreByCollectable);
            _collectedOtherPlayerItem++;
        }
    }
    public void AddScore(int value)
    {
        _scoreBuffer.Add(value);
    }
    public void UpdateScore()
    {
        foreach (var item in _scoreBuffer)
        {
            _score += item;
        }
        _scoreBuffer.Clear();
    }
}
