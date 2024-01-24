using DG.Tweening;
using UnityEngine;

public class SellArea : MonoBehaviour
{

    [SerializeField]
    private float animTime = 0.25f;
    [SerializeField]
    private float gemSellTime = 0.25f;

    private EventTrigger _eventTrigger;
    
    float gemSellTimer;

    private void Awake()
    {
        _eventTrigger = GetComponentInChildren<EventTrigger>();
        _eventTrigger.OnPlayerStayTrigger += _eventTrigger_OnPlayerStayTrigger;
    }

    private void _eventTrigger_OnPlayerStayTrigger()
    {
        gemSellTimer += Time.deltaTime;
        if(gemSellTimer >= gemSellTime)
        {
            gemSellTimer = 0;
            SellGem();
        }
    }

    private void SellGem()
    {
        if (StackManager.instance.IsStackEmpty() == true)
            return;
        GemSingle gem = StackManager.instance.GetAGem();
        StackManager.instance.RemoveMeFromStack(gem);
        gem.transform.SetParent(this.transform);

        gem.transform.DOLocalMove(Vector3.zero, animTime).OnComplete(() => Destroy(gem.gameObject));

        GameManager.instance.IncreaseTotalGold(gem.myPrice);
    }
}
