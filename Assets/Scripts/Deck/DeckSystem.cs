using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Deck
{
    public class DeckSystem : MonoBehaviour
    {
        DeckFactory Deck = null;

        public List<Card> Cards = new List<Card>();

        private void Awake()
        {
            ResetDeck();
        }

        public void ResetDeck()
        {
            Cards.Clear();

            for (int i = 0; i < Deck.Cards.Count; i++)
            {
                for (int x = 0; x < Deck.CardAmounts[i]; x++)
                {
                    Cards.Add(Deck.Cards[i].Clone());
                }
            }

            if (Deck.RandomizeOrder)
            {
                Cards.Shuffle();
            }
        }

        public void Take(Card card)
        {
            Cards.Remove(card);
        }
    }
}
