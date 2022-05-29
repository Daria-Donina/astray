using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class Speaker : MonoBehaviour
    {
        [SerializeField] private List<string> _dialogKeys;
        [SerializeField] private string[] _detectedLayers;
        
        private LayerMask _layerMask;
        private List<string>.Enumerator _enumerator;
        private static DialogPresenter _dialogService => Initializer.GetService<DialogPresenter>();
        private UniTask _speakingTask;

        private void Awake()
        {
            _layerMask = LayerMask.GetMask(_detectedLayers);
            _enumerator = _dialogKeys.GetEnumerator();
            _enumerator.MoveNext();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if ((_layerMask.value & (1 << other.transform.gameObject.layer)) > 0 
                && (_speakingTask.Status != UniTaskStatus.Pending))
            {
                _speakingTask = TrySpeak();
            }
        }

        private async UniTask TrySpeak()
        {
            var currentKey = _enumerator.Current;
            if (currentKey == null)
                return;
            
            var dialog = Dialogs.GetBy(currentKey);
            if (!dialog.HasValue)
            {
                _enumerator.MoveNext();
                return;
            }
            
            var dialogValue = dialog.Value;
            if (dialogValue.IsLocked())
                return;
                
            await _dialogService.Show(dialogValue);
            _enumerator.MoveNext();
        }
    }
}