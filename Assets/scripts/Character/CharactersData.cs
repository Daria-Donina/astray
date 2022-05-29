using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(menuName = "CharacterSystem/CharacterData")]
    public class CharactersData : ScriptableObject
    {
        public List<CharacterData> _charactersData;
        
        private static Dictionary<CharacterType, CharacterData> _charactersDataByType;

        public void Initialize()
        {
            _charactersDataByType = new Dictionary<CharacterType, CharacterData>();

            foreach (var data in _charactersData)
            {
                _charactersDataByType.Add(data.Type, data);
            }
        }

        public static CharacterData? GetBy(CharacterType type)
        {
            if (_charactersDataByType.ContainsKey(type))
                return _charactersDataByType[type];
            return null;
        }
    }
}