using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CardBase
{
    public CardBase(bool pFirst, bool pSecond, bool pThird)
    {
        First = pFirst;
        Second = pSecond;
        Third = pThird;
    }
    public bool First;
    public bool Second;
    public bool Third;
}

public class CardManager : MonoBehaviour
{
    [SerializeField]
    private int GridWidth = 3;
    [SerializeField]
    private int GridHeight = 4;

    [SerializeField]
    private float TurnDelay = 1.0f;

    private CanonManager CanonManager;
    private Vector2Int SelectorPos = new Vector2Int(1, 2);

    private Card[] Cards = null;
    private List<CardBase> PossibleCardBases = new List<CardBase>();

    private Card FirstSelection = null;

    void Start()
    {
        PossibleCardBases.Add(new CardBase(true, true, true));
        PossibleCardBases.Add(new CardBase(true, true, false));
        PossibleCardBases.Add(new CardBase(true, false, true));
        PossibleCardBases.Add(new CardBase(true, false, false));
        PossibleCardBases.Add(new CardBase(false, true, true));
        PossibleCardBases.Add(new CardBase(false, true, false));
        PossibleCardBases.Add(new CardBase(false, false, true));
        Cards = GetComponentsInChildren<Card>();
        CardSetup();
        GetCard(SelectorPos.x, SelectorPos.y).ToggleSelection(true);
        CanonManager = FindObjectOfType<CanonManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveSelector(Direction.RIGHT);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveSelector(Direction.LEFT);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            MoveSelector(Direction.DOWN);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            MoveSelector(Direction.UP);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectCard();
        }
    }

    private void SelectCard()
    {
        if(FirstSelection == null)
        {
            Card newCard = GetCard(SelectorPos.x, SelectorPos.y);

            if (newCard.IsDeactivated())
            {
                return;
            }
            
            FirstSelection = newCard;

            FirstSelection.Turn(true);
        }
        else
        {
            Card secondSelection = GetCard(SelectorPos.x, SelectorPos.y);
            if(FirstSelection != secondSelection
                && IsPair(FirstSelection, secondSelection))
            {
                secondSelection.Turn(true);

                StartCoroutine(DelayedDeactivation(TurnDelay, FirstSelection));
                StartCoroutine(DelayedDeactivation(TurnDelay, secondSelection));

                StartCoroutine(DelayedTurn(TurnDelay, FirstSelection));
                StartCoroutine(DelayedTurn(TurnDelay, secondSelection));

                FirstSelection = null;
            }
            else
            {
                secondSelection.Turn(true);
                CanonManager.IncreaseFireRate();
                StartCoroutine(DelayedTurn(TurnDelay, FirstSelection));
                StartCoroutine(DelayedTurn(TurnDelay, secondSelection));
                FirstSelection = null;
            }
        }
    }

    public IEnumerator DelayedTurn(float pDelay, Card pCard)
    {
        yield return new WaitForSeconds(pDelay);
        pCard.Turn(false);
        yield return null;
    }

    public IEnumerator DelayedDeactivation(float pDelay, Card pCard)
    {
        yield return new WaitForSeconds(pDelay);
        pCard.Deactivate();
        yield return null;
    }

    private bool IsPair(Card pFirstCard, Card pSecondCard)
    {
        CardBase lhs = pFirstCard.GetBase();
        CardBase rhs = pSecondCard.GetBase();
        if(lhs.First == rhs.First
            && lhs.Second == rhs.Second
            && lhs.Third == rhs.Third)
        {
            return true;
        }
        return false;
    }

    private void CardSetup()
    {
        for(int i = 0; i < Cards.Length / 2; i++)
        {
            ActivateRandomSymbols(Cards[i], Cards[Cards.Length - 1 - i]);
        }
    }

    private void ActivateRandomSymbols(Card pFirstCard, Card pSecondCard)
    {
        int randomIterator = Random.Range(0, PossibleCardBases.Count);
        CardBase randomBase = PossibleCardBases[randomIterator];
        PossibleCardBases.RemoveAt(randomIterator);

        pFirstCard.Setup(randomBase);
        pSecondCard.Setup(randomBase);

        //pFirstCard.Turn(true);
        //pSecondCard.Turn(true);
    }

    private Card GetCard(int x, int y)
    {
        if ((y * GridWidth + x) < 0
            || (y * GridWidth + x) > Cards.Length - 1)
            return null; //Outside array
        if (x > GridWidth - 1
            || y > GridHeight - 1
            || x < 0
            || y < 0)
            return null; //Edges
        return Cards[y * GridWidth + x];
    }

    private void MoveSelector(Direction pDirection)
    {
        Card selectedCard = null;
        switch (pDirection)
        {
            case Direction.UP:
                selectedCard = GetCard(SelectorPos.x, SelectorPos.y - 1);
                if (selectedCard != null)
                {
                    selectedCard.ToggleSelection(true);

                    GetCard(SelectorPos.x, SelectorPos.y).ToggleSelection(false);
                    SelectorPos.y--;
                }
                break;
            case Direction.RIGHT:
                selectedCard = GetCard(SelectorPos.x + 1, SelectorPos.y);

                if (selectedCard != null)
                {
                    selectedCard.ToggleSelection(true);
                    GetCard(SelectorPos.x, SelectorPos.y).ToggleSelection(false);
                    SelectorPos.x++;
                }
                break;
            case Direction.DOWN:
                selectedCard = GetCard(SelectorPos.x, SelectorPos.y + 1);

                if (selectedCard != null)
                {
                    selectedCard.ToggleSelection(true);
                    GetCard(SelectorPos.x, SelectorPos.y).ToggleSelection(false);
                    SelectorPos.y++;
                }
                break;
            case Direction.LEFT:
                selectedCard = GetCard(SelectorPos.x - 1, SelectorPos.y);

                if (selectedCard != null)
                {
                    selectedCard.ToggleSelection(true);
                    GetCard(SelectorPos.x, SelectorPos.y).ToggleSelection(false);
                    SelectorPos.x--;
                }
                break;
            default:
                break;
        }
    }
}
