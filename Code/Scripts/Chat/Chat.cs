using UnityEngine;
using System.Collections.Generic;
using CS576.Janitor.Process;


/*
    This is the little 'chat' you see in game mode,
    located on the lower section of the screen.

    New message will appear at the bottom of the old message

    At most 4 messages will be displayed
    If it exceed that, new one will replace the older one

    A message fades away in approximately 2 seconds
    If a new message appear the fading ones will be obvious again
    and starts to fade away in approximately 2 seconds
*/
namespace CS576.Janitor.UI
{
    public class Chat : StringEventListener
    {
        private const int MAX_MESSAGE_NUMBER = 4;
        private const int MAX_SENTENCE_LEN = 35;

        [SerializeField]
        private Message[] _messages = new Message[MAX_MESSAGE_NUMBER];

        private List<string> _sentences = new List<string>();

        private float _elapsedOpaqueTime = 0f;

        private float _elapsedFadeTime = 0f;

        private bool _isCountingOpaque;

        private bool _isStartingFade;

        [SerializeField]
        private float _opaqueDuration;

        [SerializeField]
        private float _fadeDuration;

        [SerializeField]
        private Color _initialColor;

        private float _alpha = 1f; // the alpha for all messages

        private void Start()
        {
            DisableAllMessages();
        }

        private void Update()
        {
            if (_alpha <= 0)
            {
                EmptyChat();
            } 
            else if (_isCountingOpaque)
            {
                _elapsedOpaqueTime += Time.deltaTime;
                if (_elapsedOpaqueTime >= _opaqueDuration)
                {
                    StartFade();
                }
            }
            else if (_isStartingFade)
            {
                _elapsedFadeTime += Time.deltaTime;
                float normalizedTime = Mathf.Clamp01(_elapsedFadeTime / _fadeDuration);
                _alpha = Mathf.Lerp(_alpha, 0.0f, normalizedTime);
                ChangeAlpha(_alpha);
            }
        }

        public override void OnEventTriggered(string newSentence)
        {
            EnqueueSentence(newSentence);
        }

        private void EnqueueSentence(string newSentence)
        {
            if (_sentences.Count >= MAX_MESSAGE_NUMBER)
                DequeuePeekSentence();
            
            _sentences.Add(newSentence);
            RefreshUI();
            CountOpaqueTime();
        }

        private void DequeuePeekSentence()
        {
            _sentences.RemoveAt(0);
        }

        private void EmptyChat()
        {
            _alpha = 1f;
            _sentences.Clear();
            for (int i = 0; i < MAX_MESSAGE_NUMBER; i++)
            {
                _messages[i].TextComponent.text = "";
            }
            RefreshUI();
            DisableAllMessages();
            _isStartingFade = false;
            _isCountingOpaque = false;
        }

        private void RefreshUI()
        {
            // the latter sentence should be put lower
            // the lowest message is the first element

            DisableAllMessages();
            for (int i = 0; i < _sentences.Count; i++)
            {
                _messages[i].TextComponent.text = _sentences[i];
            }
            _alpha = 1f;
            ChangeAlpha(_alpha);
            _isStartingFade = false;
        }

        private void CountOpaqueTime()
        {
            _isCountingOpaque = true;
            _elapsedOpaqueTime = 0f;
        }

        private void StartFade()
        {
            _isStartingFade = true;
            _isCountingOpaque = false;
            _elapsedFadeTime = 0f;
        }

        private void ChangeAlpha(float toAlpha)
        {
            for (int i = 0; i < MAX_MESSAGE_NUMBER; i++)
            {
                if (_messages[i].enabled)
                {
                    _messages[i].TextComponent.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, toAlpha);
                }
            }
        }

        private void DisableAllMessages()
        {
            ChangeAlpha(0);
        }
    }
}
