namespace Structures;

public abstract class AbstractParentQueue<T>
{
    // конструктор
    // постусловие: создана новая пустая очередь
    public AbstractParentQueue()
    {
    }

    // команды
    // постусловие: в конец очереди добавлен новый элемент.
    public abstract void AddTail(T item);
    
    // предусловие: очередь не должна быть пустой
    // постусловие: из головы очереди удалён элемент.
    public abstract void RemoveFront();
    
    // постусловие: из очереди удалены все значения
    public abstract void Clear();
    
    // запросы:
    public abstract int Size();
    
    public abstract bool IsEmpty();
    
    // предусловие: очередь не должна быть пустой
    public abstract T GetFront();
    
    // дополнительные запросы:
    public abstract int GetRemoveFrontStatus(); // успешно; очередь была пустой
    public abstract int GetGetFrontStatus(); // успешно; очередь была пустой
}

public abstract class AbstractQueue<T> : AbstractParentQueue<T>
{
} 

public abstract class AbstractDeque<T> : AbstractParentQueue<T>
{
    // команды
    // постусловие: в начало дека был добавлен элемент
    public abstract void AddFront(T item);
    
    // предусловие: дек не должен быть пустой
    // постусловие: из хвоста дека был удалён элемент
    public abstract void RemoveTail();
    
    // запросы
    // предусловие: дек не должен быть пустой
    public abstract T GetTail();
    
    //дополнительные запросы:
    public abstract int GetRemoveTailStatus(); // успешно; дек был пустой
    public abstract int GetGetTailStatus(); // успешно; дек был пустой
}

public class ParentQueue<T> : AbstractParentQueue<T>
{
    public const int RemoveFrontNil = 0;   // RemoveFront ещё не вызывался
    public const int RemoveFrontOk = 1;   // узел из головы удалён удалён
    public const int RemoveFrontErr = 2;  // ParentQueue пустая, удалять нечего
    
    public const int GetFrontNil = 0;   // GetFront ещё не вызывался
    public const int GetFrontOk = 1;   // первый узел возвращён
    public const int GetFrontErr = 2;  // ParentQueue пустая, возвращать нечего

    private int _getFrontStatus = GetFrontNil;
    private int _removeFrontStatus = RemoveFrontNil;
    
    protected List<T> SourceParentQueue;

    public ParentQueue()
    {
        SourceParentQueue = [];
    }
    // команды
    public override void AddTail(T item)
    {
        SourceParentQueue.Add(item);
    }

    public override void RemoveFront()
    {
        if (IsEmpty())
        {
            _removeFrontStatus = RemoveFrontErr;
            return;
        }
        
        SourceParentQueue.RemoveAt(0); 
        _removeFrontStatus = RemoveFrontOk;
    }

    public override void Clear()
    {
        SourceParentQueue.Clear();
        ClearStatuses();
    }

    protected virtual void ClearStatuses()
    {
        _removeFrontStatus = RemoveFrontNil;
        _getFrontStatus = GetFrontNil;
    }
    
    // запросы
    public override int Size() => SourceParentQueue.Count;
    public override bool IsEmpty() => Size() == 0;
    
    public override T GetFront()
    {
        if (IsEmpty())
        {
            _getFrontStatus = GetFrontErr;
            return default!;
        }

        _getFrontStatus = GetFrontOk;
        return SourceParentQueue[0];
    }

    // дополнительные запросы:
    public override int GetRemoveFrontStatus() =>_removeFrontStatus;
    public override int GetGetFrontStatus() => _getFrontStatus;
}

public class Queue<T> : ParentQueue<T>;

public class Deque<T> : ParentQueue<T>
{
    public const int RemoveTailNil = 0;   // RemoveTail ещё не вызывался
    public const int RemoveTailOk = 1;   // первый узел удалён
    public const int RemoveTailErr = 2;  // RemoveTail пустая, удалять нечего
    
    public const int GetTailNil = 0;   // GetTail ещё не вызывался
    public const int GetTailOk = 1;   // первый узел возвращён
    public const int GetTailErr = 2;  // Deque пустая, возвращать нечего

    private int _getTailStatus = GetTailNil;
    private int _removeTailStatus = RemoveTailNil;
    
    // команды
    public void AddFront(T item)
    {
        SourceParentQueue.Insert(0, item);
    }
    
    public void RemoveTail()
    {
        if (IsEmpty())
        {
            _removeTailStatus = RemoveTailErr;
            return;
        }
        SourceParentQueue.RemoveAt(Size() - 1);
        _removeTailStatus = RemoveTailOk;
    }

    protected override void ClearStatuses()
    {
        base.ClearStatuses();
        _removeTailStatus = RemoveTailNil;
        _getTailStatus = GetTailNil;
    }

    // запросы
    public T GetTail()
    {
        if (IsEmpty())
        {
            _getTailStatus = GetTailErr;
            return default!;
        }
        _getTailStatus = GetTailOk;
        return SourceParentQueue[Size() - 1];
    }
    
    //дополнительные запросы:
    public int GetRemoveTailStatus() => _removeTailStatus;
    public int GetGetTailStatus() => _getTailStatus;
}