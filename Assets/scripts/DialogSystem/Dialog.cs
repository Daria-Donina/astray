using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct Dialog
    {
        public string DialogKey;
        public List<Phrase> Phrases;

        [SerializeField] private List<Unlock> _unlocks;

        public bool IsLocked()
        {
            return UnlockManager.IsLocked(_unlocks);
        }
    }
}