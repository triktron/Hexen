using System.Collections.Generic;
using UnityEngine;

namespace Deck
{
    [System.Serializable]
    public class Card
    {
        public string Name;
        public Sprite Icon;

        public Card(string name, Sprite icon)
        {
            Name = name;
            Icon = icon;
        }

        public Card Clone()
        {
            return new Card(Name, Icon);
        }
    }

    [CreateAssetMenu(fileName = "DefaultDeckFactory", menuName = "GameSystem/Deck Factory")]
    public class DeckFactory : ScriptableObject
    {
        public List<Card> Cards = new List<Card>();
        public List<int> CardAmounts = new List<int>();
        public bool RandomizeOrder = true;
    }
}
