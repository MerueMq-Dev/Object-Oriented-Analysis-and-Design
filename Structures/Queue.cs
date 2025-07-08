namespace Structures;

public abstract class OriginalAbstractQueue<T>
{
    // конструктор
    // постусловие: создана новая пустая очередь
    public OriginalAbstractQueue()
    {
    }

    // команды
    // постусловие: в конец очереди добавлен новый элемент.
    public abstract void Enqueue(T item);
    
    // предусловие: очередь не должна быть пустой
    // постусловие: из очереди удалён первый элемент.
    public abstract void Dequeue();
    
    // постусловие: из очереди удалены все значения
    public abstract void Clear();
    
    // запросы:
    public abstract int Size();
    
    public abstract bool IsEmpty();
    
    // предусловие: очередь не должна быть пустой
    public abstract T Peek();
    
    // дополнительные запросы:
    public abstract int GetDequeStatus(); // успешно; очередь была пустой
    public abstract int GetPeekStatus(); // успешно; очередь была пустой
}

public class OriginalQueue<T>:OriginalAbstractQueue<T>
{
    public const int DequeueNil = 0;   // Deque ещё не вызывался
    public const int DequeueOk = 1;   // первый узел удалён
    public const int DequeueErr = 2;  // Deque пустая, удалять нечего
    
    public const int PeekNil = 0;   // Peek ещё не вызывался
    public const int PeekOk = 1;   // первый узел возвращён
    public const int PeekErr = 2;  // Deque пустая, возвращать нечего

    private int _peekStatus = PeekNil;
    private int _dequeueStatus = DequeueNil;
    
    private List<T> _queueSource;

    public OriginalQueue()
    {
        _queueSource = new List<T>();
    }
    
    public override void Enqueue(T item)
    {
        _queueSource.Add(item);
    }

    public override void Dequeue()
    {
        if (IsEmpty())
        {
            _dequeueStatus = DequeueErr;
            return;
        }
        
        _queueSource.RemoveAt(0); 
        _dequeueStatus = DequeueOk;
    }

    public override void Clear()
    {
        _queueSource.Clear();
        _peekStatus = PeekNil;
        _dequeueStatus = DequeueNil;
    }

    public override int Size()
    {
        return _queueSource.Count; 
    }

    public override bool IsEmpty()
    {
        return Size() == 0;
    }

    public override T Peek()
    {
        if (IsEmpty())
        {
            _peekStatus = PeekErr;
            return default!;
        }

        _peekStatus = PeekOk;
        return _queueSource[0];
    }

    public override int GetDequeStatus() => _dequeueStatus;
    
    public override int GetPeekStatus() => _peekStatus;
}