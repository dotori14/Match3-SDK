using System;
using Common.Interfaces;
using Common.Models;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Common
{
    public class CanvasInputSystem : MonoBehaviour, IInputSystem
    {
        public static CanvasInputSystem instance;

        [SerializeField] private Camera _camera;
        [SerializeField] private EventTrigger _eventTrigger;
        [SerializeField] public AudioSource SLIDE;
        [SerializeField] public AudioSource BOMB;
        [SerializeField] public AudioSource RESET;
        [SerializeField] public TMP_Text scoreText;

        public event EventHandler<PointerEventArgs> PointerDown;
        public event EventHandler<PointerEventArgs> PointerDrag;
        public event EventHandler<PointerEventArgs> PointerUp;

        public int score = 0;

        private void Awake()
        {
            instance = this;

            var pointerDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
            pointerDown.callback.AddListener(data => { OnPointerDown((PointerEventData) data); });

            var pointerDrag = new EventTrigger.Entry { eventID = EventTriggerType.Drag };
            pointerDrag.callback.AddListener(data => { OnPointerDrag((PointerEventData) data); });

            var pointerUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
            pointerUp.callback.AddListener(data => { OnPointerUp((PointerEventData) data); });

            _eventTrigger.triggers.Add(pointerDown);
            _eventTrigger.triggers.Add(pointerDrag);
            _eventTrigger.triggers.Add(pointerUp);

        }

        public void SetScore(int num)
        {
            score += num;
            scoreText.GetComponent<TMP_Text>().text = String.Format(score.ToString());
        }

        public void ResetScore()
        {
            score = 0;
            scoreText.GetComponent<TMP_Text>().text = String.Format(score.ToString());
        }

        private void OnPointerDown(PointerEventData e)
        {
            PointerDown?.Invoke(this, GetPointerEventArgs(e));
        }

        private void OnPointerDrag(PointerEventData e)
        {
            // 드래그 효과음
            //if (this.SLIDE.isPlaying == false)
            //    this.SLIDE.Play();
            PointerDrag?.Invoke(this, GetPointerEventArgs(e));
        }

        private void OnPointerUp(PointerEventData e)
        {
            PointerUp?.Invoke(this, GetPointerEventArgs(e));
        }

        private PointerEventArgs GetPointerEventArgs(PointerEventData e)
        {
            return new PointerEventArgs(e.button, GetWorldPosition(e.position));
        }

        private Vector2 GetWorldPosition(Vector2 screenPosition)
        {
            return _camera.ScreenToWorldPoint(screenPosition);
        }
    }
}