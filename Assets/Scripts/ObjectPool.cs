using System;
using System.Collections.Generic;

public class ObjectPool<T>
{

    private Func<T> CreateFunc;
    private Action<T> GetAction;
    private Action<T> ReleaseAction;
    private Queue<T> pool;
    private List<T> active;

    public ObjectPool(Func<T> createFunc,Action<T> getAction = null,Action<T> releaseAction = null,int defaultCapacity =10)
    {
        pool = new Queue<T>(defaultCapacity);
        active = new List<T>();
        CreateFunc = createFunc;
        GetAction = getAction;
        ReleaseAction = releaseAction;
        for (int i = 0; i < defaultCapacity; i++)Release(Create());
    }
        
    public T Get()
    {
        T obj;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            obj = CreateFunc();
        }

        GetAction?.Invoke(obj);
        active.Add(obj);
        return obj;
    }

    private T Create()
    {
        return CreateFunc.Invoke();
    }
        
    public void Release(T releaseObj)
    {
        ReleaseAction?.Invoke(releaseObj);;
        pool.Enqueue(releaseObj);
        active.Remove(releaseObj);
    }

    public void ReleaseAll()
    {
        foreach (T variable in active.ToArray())
        {
            Release(variable);
        }
    }
}