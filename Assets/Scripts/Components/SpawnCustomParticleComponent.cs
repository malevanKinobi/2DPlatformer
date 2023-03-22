using System;
using UnityEngine;

public class SpawnCustomParticleComponent : MonoBehaviour
{
    [SerializeField] public CustomParticle[] _customParticles;

    [ContextMenu("Spawn")]
    public void Spawn(string nameParticle)
    {

        for (int indexParticle = 0; indexParticle < _customParticles.Length; indexParticle++)
        {
            if (_customParticles[indexParticle].Name.IndexOf(nameParticle) == 0)
            {
                var instantiate = Instantiate(_customParticles[indexParticle].SpawnObject, _customParticles[indexParticle].SpawnTarget.position, Quaternion.identity);
                instantiate.transform.localScale = _customParticles[indexParticle].SpawnTarget.lossyScale;
            }
        }
            
    }

    [Serializable]
    public class CustomParticle
    {
        [SerializeField] private string _name;
        [SerializeField] private GameObject _spawnObject;
        [SerializeField] private Transform _spawnTarget;

        public string Name => _name;
        public GameObject SpawnObject => _spawnObject;
        public Transform SpawnTarget => _spawnTarget;

    }
}
