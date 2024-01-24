using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class GemSingle : MonoBehaviour
{
    public GemInfo myGemInfo;
    [SerializeField]
    private GameObject gemHolder;

    public int myPrice;

    public event Action<GemSingle> OnGemAddedToStack;
    private EventTrigger _eventTrigger;
    private bool collected;
    Sequence seqLooped;

    private void Awake()
    {
        _eventTrigger = GetComponent<EventTrigger>();
        _eventTrigger.OnPlayerEnterTrigger += _eventTrigger_OnPlayerEnterTrigger;
        _eventTrigger.OnPlayerStayTrigger += _eventTrigger_OnPlayerStayTrigger; ;
    }

    private void _eventTrigger_OnPlayerStayTrigger()
    {
        if (collected == true)
            return;
        if (CanCollectMe() == false)
            return;

        CollectMe();
    }

    private void _eventTrigger_OnPlayerEnterTrigger()
    {
        if (collected == true)
            return;
        if (CanCollectMe() == false)
            return;

        CollectMe();
    }

    private void CollectMe()
    {
        //Calculate Price when collected
        float myScale = transform.localScale.y;
        myPrice = GemManager.instance.CalculateGemPrice(myGemInfo, myScale);

        //Add to stack
        DOTween.Complete(this.transform);
        seqLooped.Kill();
        StackManager.instance.AddMeToStack(this);
        OnGemAddedToStack?.Invoke(this);

        collected = true;
    }

    

    public bool CanCollectMe()
    {
        float myScale = transform.localScale.y;

        return myScale >= 0.25f;
    }
    public void OnGemSpawned()
    {
        myGemInfo = GemManager.instance.GetMeRandomGem();
        GameObject gem = Instantiate(myGemInfo.gemPrefab, gemHolder.transform);
        gem.transform.localPosition = Vector3.zero;
        StartGrowing();

    }
    public void StartGrowing()
    {
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        float totalTime = 5f;

        //transform.DOLocalRotate(Vector3.up * 180, 0.5f, RotateMode.LocalAxisAdd).SetRelative().SetLoops(-1);
        transform.DOScale(endScale, totalTime).From(startScale);

        seqLooped = DOTween.Sequence();
        seqLooped.Append(transform.DOLocalMoveY(0.25f, 0.5f).SetRelative().SetLoops(-1, LoopType.Yoyo));
        seqLooped.SetLoops(-1, LoopType.Yoyo);
    }

}
