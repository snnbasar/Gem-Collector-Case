using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;


public class StackManager : StackManagerHelper<GemSingle>
{
    public static StackManager instance;

    [SerializeField] private Transform stackRef;
    [SerializeField] private float animTime = 0.25f;
    [SerializeField] private float stackOffset = 0.5f;
    [SerializeField] private float stackScale = 0.5f;

    public void Awake()
    {
        instance = this;
        base.Stack_Awake();
    }

    public override void AddMeToStack(GemSingle t)
    {
        base.AddMeToStack(t);
        t.transform.SetParent(stackRef);
        t.transform.DOLocalMove(Vector3.up * (currentStack - 1) * stackOffset, animTime);
        t.transform.DOScale(Vector3.one * stackScale, animTime);
    }

    public override void RemoveMeFromStack(GemSingle t)
    {
        base.RemoveMeFromStack(t);
        t.transform.SetParent(null);
        t.transform.DOScale(Vector3.one, animTime);
    }

    public GemSingle GetAGem() => stack[stack.Count - 1];

    public override void OnStackChanged()
    {
        
    }

}
public abstract class StackManagerHelper<T> : MonoBehaviour
{
    public int maxStack;
    public int currentStack;
    public List<T> _stack = new List<T>();
    public List<T> stack
    {
        get => _stack; set
        {
            _stack = value;
            OnStackListChanged?.Invoke();
        }
    }

    public event Action OnStackListChanged;


    //public abstract void OnStack_Awake();
    public void Stack_Awake()
    {
        OnStackListChanged += OnStackChanged;
    }



    public virtual void AddMeToStack(T t)
    {
        stack.Add(t);
        currentStack++;
    }


    public virtual void RemoveMeFromStack(T t)
    {
        stack.Remove(t);
        currentStack--;

    }


    public abstract void OnStackChanged();

    public bool IsStackEmpty() => currentStack <= 0;
    public bool IsStackFull() => currentStack >= maxStack;

}
