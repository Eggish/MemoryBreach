using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    private Card[] Cards = null;

    void Start()
    {
        Cards = GetComponentsInChildren<Card>();
        foreach(Card c in Cards)
        {
            ActivateRandomSymbols(c);
        }
    }

    void Update()
    {
        
    }

    private void ActivateRandomSymbols(Card pCard)
    {
        bool[] isSymbolActive = 
        { 
            false,
            false,
            false
        };
        for(int i = 0; i < 3; i++)
        {
            int randomComparison = Random.Range(0, 2);
            if(randomComparison == 1)
            {
                isSymbolActive[i] = true;
            }
        }

        pCard.Setup(isSymbolActive);

        pCard.TurnCard();
    }
}
