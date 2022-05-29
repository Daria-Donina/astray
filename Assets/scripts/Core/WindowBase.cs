using System;
using JetBrains.Annotations;
using UnityEngine;

namespace DefaultNamespace
{
    public class WindowBase : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private static readonly int ShowTrigger = Animator.StringToHash("ShowTrigger");
        private static readonly int HideTrigger = Animator.StringToHash("HideTrigger");
        private Action _onClosedWindow;
        private Action _onClickAction;
        
        public event Action<WindowBase> WindowClosed;

        public void Init(Action onCLickAction = null, Action onClosedWindow = null)
        {
            _onClickAction = onCLickAction;
            _onClosedWindow = onClosedWindow;
        }
        
        public virtual void Show()
        {
            gameObject.SetActive(true);

            ResetTriggers();
            _animator.SetTrigger(ShowTrigger);
        }
        
        public virtual void ShowOnlyThis()
        {
            this.CloseAllWindowsExcept();
            Show();
        }

        public virtual void Close()
        {
            ResetTriggers();
            _animator.SetTrigger(HideTrigger);
        }
        
        public virtual void CloseInstant()
        {
            gameObject.SetActive(false);
        }
        
        public void OnClick()
        {
            _onClickAction?.Invoke();
        }
        
        [UsedImplicitly]
        public void OnClosedAnimationEvent()
        {
            gameObject.SetActive(false);
            _onClosedWindow?.Invoke();
            WindowClosed?.Invoke(this);
        }
        
        private void ResetTriggers()
        {
            _animator.ResetTrigger(ShowTrigger);
            _animator.ResetTrigger(HideTrigger);
        }
        
        public void Dispose()
        {
            WindowClosed = null;
        }
    }
}