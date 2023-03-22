using UnityEngine;

namespace Scripts.Components
{
    public class AddCoinComponent : MonoBehaviour
    {
        [SerializeField] private int _numCoin;
        private Hero _hero;

        private void Start()
        {
            _hero = FindObjectOfType<Hero>();
        }

        public void Add()
        {
            _hero.AddCoins(_numCoin);
        }

    }
}