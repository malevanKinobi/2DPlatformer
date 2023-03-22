using UnityEngine;

public class SpawnComponent : MonoBehaviour
{
    [SerializeField] private GameObject _spawnObject;
    [SerializeField] private Transform _spawnTarget;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Instantiate(_spawnObject, _spawnTarget);
    }    
}
