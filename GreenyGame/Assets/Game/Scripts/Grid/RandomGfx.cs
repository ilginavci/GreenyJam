using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGfx : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> _meshes;
    public MeshRenderer _current;
    private void OnEnable()
    {
        var randomGfx = _meshes[Random.Range(0, _meshes.Count)];
        _current = Instantiate(randomGfx);
        Vector3 rotation = Vector3.zero;
        rotation.y = 90 * Random.Range(0,4);
        _current.transform.localRotation = Quaternion.Euler(rotation);
    }
}
