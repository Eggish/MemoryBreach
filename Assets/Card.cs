using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer[] Symbols = null;

    private CardBase Base;

    [SerializeField]
    private SpriteRenderer SelectionMarker = null;

    [SerializeField]
    private SpriteRenderer Background = null;

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
    
    public void Setup(CardBase pCardBase)
    {
        Base = pCardBase;
    }

    public void Turn(bool pActivate)
    {
        if(!pActivate)
        {
            foreach(SpriteRenderer s in Symbols)
            {
                s.enabled = false;
            }
            return;
        }
        if (Base.First)
        {
            Symbols[0].enabled = true;
        }
        if (Base.Second)
        {
            Symbols[1].enabled = true;
        }
        if (Base.Third)
        {
            Symbols[2].enabled = true;
        }
    }
    public CardBase GetBase()
    {
        return Base;
    }

    public void Deactivate()
    {
        Background.enabled = false;
    }

    public bool IsDeactivated()
    {
        if (Background.enabled)
            return false;
        return true;
    }


    public void ToggleSelection(bool pIsSelected)
    {
        SelectionMarker.enabled = pIsSelected;
    }
}
