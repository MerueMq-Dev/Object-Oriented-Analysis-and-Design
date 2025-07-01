namespace Structures;

public abstract class AbstractBoundedStack<T>
{
    public const int PopNil = 0; // push() ещё не вызывалась
    public const int PopOk = 1; // последняя pop() отработала нормально
    public const int PopErr = 2; // стек пуст

    public const int PeekNil = 0; // push() ещё не вызывалась
    public const int PeekOk = 1; // последняя peek() вернула корректное значение 
    public const int PeekErr = 2; // стек пуст

    public const int PushNil = 0; // push() ещё не вызывалась
    public const int PushOk = 1; // последняя push() отработала нормально 
    public const int PushErr = 2; // стек полон
    
    // конструктор
    public AbstractBoundedStack(int maxSize = 32)
    {
        // постусловие: создан новый пустой стек с максимальной вместимостью maxSize
    }
    
    // команды:
    // предусловие: стек не полный;
    // постусловие: в стек добавлено новое значение
    public abstract void Push(T value);

    // предусловие: стек не пустой; 
    // постусловие: из стека удалён верхний элемент
    public abstract void Pop();

    // постусловие: из стека удалятся все значения
    public abstract void Clear();

    // запросы:
    // предусловие: стек не пустой
    public abstract T Peek();
    public abstract int Size();
    
    // дополнительные запросы:
    public abstract int GetPopStatus(); // возвращает значение POP_*
    public abstract int GetPeekStatus(); // возвращает значение PEEK_*
    public abstract int GetPushStatus(); //  // возвращает значение PUSH_*
}

public class BoundedStack<T>(int maxSize = 32) : AbstractBoundedStack<T> // конструктор
{
    // скрытые поля
    private List<T> _stack = []; // основное хранилище стека
    private int _peekStatus = PeekNil; // статус запроса peek()
    private int _popStatus = PopNil; // статус команды pop()
    private int _pushStatus = PushNil; // статус команды pop()
    
    // интерфейс класса, реализующий АТД BoundedStack
    public const int PopNil = 0;
    public const int PopOk = 1;
    public const int PopErr = 2;
    
    public const int PeekNil = 0;
    public const int PeekOk = 1;
    public const int PeekErr = 2;
    
    public const int PushNil = 0;
    public const int PushOk = 1;
    public const int PushErr = 2;
    
    public override void Push(T value)
    {
        if (Size() < maxSize)
        {
            _stack.Add(value);
            _pushStatus = PushOk;
            return;
        }
        _pushStatus = PushErr;
    }

    public override void Pop()
    {
        if (Size() > 0)
        {
            _stack.RemoveAt(Size() - 1);
            _peekStatus = PopOk;
        }
        else
            _popStatus = PopErr;
    }
    
    public override void Clear()
    {
        _stack = []; // пустой список/стек
        // начальные статусы для предусловий push, peek() и pop() 
        _pushStatus = PushNil;
        _peekStatus = PeekNil;
        _popStatus = PopNil;
    }

    public override T Peek()
    {
        var result = default(T);

        if (Size() > 0)
        {
            result = _stack[^1];
            _peekStatus = PeekOk;
        }
        else
            _peekStatus = PeekErr;

        return result!;
    }

    public override int Size() => _stack.Count;
    // запросы статусов
    public override int GetPopStatus() => _popStatus;
    public override int GetPeekStatus() => _peekStatus;
    public override int GetPushStatus() => _pushStatus;
}