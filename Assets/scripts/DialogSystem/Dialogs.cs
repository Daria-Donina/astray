using UnityEngine;
using System.Collections.Generic;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "DialogSystem/Dialogs")]
    public class Dialogs : ScriptableObject
    {
        [SerializeField] private List<Dialog> _dialogs;

        public void Initialize()
        {
            _dialogsDictionary = new Dictionary<string, Dialog>();

            foreach (var dialog in _dialogs)
            {
                _dialogsDictionary.Add(dialog.DialogKey, dialog);
            }
        }

        private static Dictionary<string, Dialog> _dialogsDictionary;

        public static Dialog? GetBy(string key)
        {
            if (_dialogsDictionary.ContainsKey(key))
                return _dialogsDictionary[key];
            return null;
        }
    }
}