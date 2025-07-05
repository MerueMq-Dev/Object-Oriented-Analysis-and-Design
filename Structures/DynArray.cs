namespace Structures;

public abstract class AbstractDynArray<T>
{
    
    // Конструктор
    // постусловие: создан пустой динамический массив размером в 16 элементов.
    public AbstractDynArray()
    { 
    }
    // Команды
    // постусловие: создан пустой динамический массив с размером newCapacity
    // с минимальным размером в 16 элементов.
    protected abstract void MakeArray(int newCapacity);
    
    // постусловие: новый элемент добавлен в конец динамического массива если не хватало места
    // то буфер динамического массива был увеличен в два раза 
    public abstract void Append(T itm);
    
    // предусловие: индекс должен быть в границах массива
    // постусловие: элемент itm вставлен в динамический массив по переданному индексу
    public abstract void Insert(T itm, int index);
    
    // предусловие: индекс должен быть в границах массива
    // постусловие: элемент по индексу по переданному индексу 
    public abstract void Remove(int index);

    // Запросы:
    // предусловие: индекс должен быть в границах массива
    public abstract T GetItem(int index);

    // Доп запросы:
    public abstract int GetAppendStatus(); // метод ещё не вызвался; успешно;
    public abstract int GetInsertStatus(); // метод ещё не вызвался; успешно; вне границ массива 
    public abstract int GetRemoveStatus(); // метод ещё не вызвался; успешно; вне границ массива
    public abstract int GetGetStatus(); // метод ещё не вызвался; успешно; вне границ массива
}


public class DynArray<T> : AbstractDynArray<T>
{
    private T[] Array;
    public int Count { get; private set; }
    public int Capacity { get; private set; }

    private const int GrowthFactor = 2;
    private const double ShrinkFactor = 1.5;

    private int _appendStatus = AppendNil; // статус команды append()
    private int _insertStatus = InsertNil; // статус команды insert()
    private int _removeStatus = RemoveNil; // статус команды remove()
    private int _getStatus = GetNil; // статус запроса get()
    
    public const int AppendNil = 0;
    public const int AppendOk = 1;

    public const int InsertNil = 0;
    public const int InsertOk = 1;
    public const int InsertErr = 2;

    public const int RemoveNil = 0;
    public const int RemoveOk = 1;
    public const int RemoveErr = 2;

    public const int GetNil = 0;
    public const int GetOk = 1;
    public const int GetErr = 2;


    // Конструктор создаёт пустой динамический массив с буфером в 16 элементов.
    public DynArray()
    {
        Count = 0;
        MakeArray(16);
    }


    protected override void MakeArray(int newCapacity)
    {
        Capacity = newCapacity < 16 ? 16 : newCapacity;
        Array ??= new T[Capacity];
        var newArr = new T[Capacity];
        System.Array.Copy(Array, newArr, Count);
        Array = newArr;
    }

    public override void Append(T itm)
    {
        if (Count == Capacity)
            MakeArray(Capacity * GrowthFactor);

        Array[Count] = itm;
        Count++;
        _appendStatus = AppendOk;
    }

    public override void Insert(T itm, int index)
    {
        if (index > Count || index < 0)
        {
            _insertStatus = InsertErr;
            return;
        }

        if (Count == Capacity)
            MakeArray(Capacity * GrowthFactor);

        for (int i = Count; i > index; i--)
            Array[i] = Array[i - 1];

        Array[index] = itm;
        Count++;
        _insertStatus = InsertOk;
    }

    public override void Remove(int index)
    {
        if (index >= Count || index < 0)
        {
            _removeStatus = RemoveErr;
            return;
        }

        for (int i = index; i < Count; i++)
            Array[i] = Array[i + 1];
        Count--;
        _removeStatus = RemoveOk;
        
        if (Count >= Capacity / 2)
            return;

        var newCapacity = (int)(Capacity / ShrinkFactor);
        MakeArray(newCapacity);
    }

    // Запросы:
    public override T GetItem(int index)
    {
        if (index >= Count || index < 0)
        {
            _getStatus = GetErr;
            return default!;
        }

        _getStatus = GetOk;
        return Array[index];
    }

    // Доп запросы:
    public override int GetAppendStatus() => _appendStatus;
    public override int GetInsertStatus() => _insertStatus;
    public override int GetRemoveStatus() => _removeStatus;
    public override int GetGetStatus() => _getStatus;
}