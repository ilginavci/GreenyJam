using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Power_Earthquake : MonoBehaviour
{
    [SerializeField] BoardBehaviour _board;
    int earthquakeNumber;
    EntityType _sourceType;
    public void StartEartQuake(GridElement _currentGrid, EntityType srcType)
    {
        StartCoroutine(StartEarthquakeProggress(_currentGrid, srcType));
    }
    IEnumerator StartEarthquakeProggress(GridElement _currentGrid, EntityType srcType)
    {
        if(srcType != EntityType.EarthquakePower)
        {
            float lenght= SoundManager.Instance.PlayAudio(AudioStates.QuakeUse, _sourceType);
            yield return new WaitForSeconds(lenght-1);
        }
        InvokeEarthquake(_currentGrid, srcType);
        yield return null;
    }
    private void InvokeEarthquake(GridElement _currentGrid, EntityType srcType)
    {
        earthquakeNumber = 0;
        _sourceType = srcType;
        StartCoroutine(EarthQuake(_currentGrid, new Vector2Int(1, 0)));
        StartCoroutine(EarthQuake(_currentGrid, new Vector2Int(0, 1)));
        StartCoroutine(EarthQuake(_currentGrid, new Vector2Int(-1, 0)));
        StartCoroutine(EarthQuake(_currentGrid, new Vector2Int(0, -1)));
        StartCoroutine(CheckEarthquakeFinished());
    }
    IEnumerator CheckEarthquakeFinished()
    {
        yield return new WaitUntil(()=> earthquakeNumber >= 4);
        OnEarthquakeFinished();
    }
    public void OnEarthquakeFinished()
    {
        if(_sourceType == EntityType.EarthquakePower)
        {
            TurnBasedController.Instance.OnTurnFinished();
        }
        else
        {
            var playerEntity = TurnBasedController.Instance.GetPlayer(_sourceType);
            playerEntity._inventory.UpdateScore();
            playerEntity.TurnFinished();
        }
    }
    IEnumerator EarthQuake(GridElement _currentGrid, Vector2Int direction)
    {
        bool _completed=false;
        float _pow = .5f;
        Vector2Int _pos = _currentGrid.position;
        while(true)
        {
            _completed = false;
            _pos = _pos + direction;
            GridElement _grid = _board.GetGrid(_pos);
            if (_grid == null) break;
            
            if (_grid._entity != null)
            {
                _grid._entity.Shocked(_sourceType);
            }
            _grid.transform.DOLocalJump(_grid.transform.localPosition, (_pow*_pow), 1, _pow * .3f)
                .OnComplete(
                ()=> 
                {
                    _completed = true;
                });

            _pow += 0.1f;

            yield return new WaitUntil(()=> _completed);
        }
        earthquakeNumber++;
    }

}
