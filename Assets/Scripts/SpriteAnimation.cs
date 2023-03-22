using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts
{

    [RequireComponent(typeof(SpriteRenderer))]

    public class SpriteAnimation : MonoBehaviour
    {
        [SerializeField] private AnimationClip[] _clips;

        private int _currentSprite;
        private float _secondsPerFrame;
        private float _timeForNextFrame;
        private int _currentClip = 0;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _currentSprite = 0;
            _secondsPerFrame = SetSecondsPerFrame();
        }

        private void OnBecameVisible()
        {
            enabled = true;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void Update()
        {
            _timeForNextFrame += Time.deltaTime;

            if (_timeForNextFrame < _secondsPerFrame) return;

            var clip = _clips[_currentClip];

            if (_currentSprite >= clip.Sprites.Length)
            {
                if (clip.Loop)
                {
                    _currentSprite = 0;
                }
                else
                {
                    clip.EventOnCompletion?.Invoke();
                    enabled = clip.AllowNextClip;
                    if (clip.AllowNextClip)
                    {
                        _currentSprite = 0;
                        _currentClip = (int)Mathf.Repeat(_currentClip + 1, _clips.Length);
                        _secondsPerFrame = SetSecondsPerFrame();
                    }
                }

                return;
            }

            _spriteRenderer.sprite = clip.Sprites[_currentSprite];
            _timeForNextFrame = 0;
            _currentSprite++;
        }

        private float SetSecondsPerFrame()
        {
            return 1f / _clips[_currentClip].CountFrames;
        }

        public void SetClip(string nameClip)
        {
            for (int indexClip = 0; indexClip < _clips.Length; indexClip++)
            {
                if (_clips[indexClip].Name.IndexOf(nameClip) == 0)
                {
                    _currentSprite = 0;
                    _currentClip = indexClip;
                    _secondsPerFrame = SetSecondsPerFrame();
                    return;
                }
            }
        }

        [Serializable]
        public class AnimationClip
        {
            [SerializeField] private string _name;
            [SerializeField][Range(1, 30)] private int _countFrames = 10;
            [SerializeField] private Sprite[] _sprites;
            [SerializeField] private bool _loop;
            [SerializeField] private bool _allowNextClip;
            [SerializeField] private UnityEvent _eventOnCompletion;

            public string Name => _name;
            public Sprite[] Sprites => _sprites;
            public bool Loop => _loop;
            public bool AllowNextClip => _allowNextClip;
            public UnityEvent EventOnCompletion => _eventOnCompletion;
            public int CountFrames => _countFrames;
        }
    }
}
