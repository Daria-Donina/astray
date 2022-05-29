using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DefaultNamespace
{
    public class DialogPresenter : IService
    {
        private readonly DialogWindow _view;
        private Dialog _showingDialog;
        private List<Phrase>.Enumerator _currentEnumerator;
        private bool _isClosed;

        public DialogPresenter(DialogWindow view)
        {
            _view = view;
            _view.Init(OnWindowClick, OnWindowClosed);
        }

        public async UniTask Show(Dialog dialog)
        {
            _isClosed = false;
            _showingDialog = dialog;
            _currentEnumerator = _showingDialog.Phrases.GetEnumerator();
            _currentEnumerator.MoveNext();
            UpdateVisual();
            _view.ShowOnlyThis();

            await UniTask.WaitUntil(() => _isClosed);
        }

        private void UpdateVisual()
        {
            var phrase = _currentEnumerator.Current;
            var characterData = CharactersData.GetBy(phrase.Speaker);
            if (!characterData.HasValue)
            {
                Debug.LogError($"No data for character {phrase.Speaker}. Check scriptable object CharactersData");
                return;
            }

            var characterDataValue = characterData.Value;
            _view.UpdateVisual(characterDataValue.Image, phrase.Text);
        }

        private void OnWindowClick()
        {
            var morePhrases = _currentEnumerator.MoveNext();
            
            if (morePhrases)
                UpdateVisual();
            else
                _view.Close();
        }
        
        private void OnWindowClosed()
        {
            _isClosed = true;
        }

        public void Dispose()
        {
            _currentEnumerator.Dispose();
        }
    }
}