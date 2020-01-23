using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] Symbols = null;

    private bool[] IsSymbolActive;

    private void Awake()
    {
        foreach (SpriteRenderer s in Symbols)
        {
            s.enabled = false;
        }
    }

    void Update()
    {
        
    }
    public void Setup(bool[] pIsSymbolActive)
    {
        IsSymbolActive = pIsSymbolActive;
    }

    public void TurnCard()
    {
        for(int i = 0; i < IsSymbolActive.Length; i++)
        {
            Symbols[i].enabled = IsSymbolActive[i];
        }
    }
}
