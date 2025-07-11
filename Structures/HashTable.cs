namespace Structures;

public abstract class OriginalAbstractHashTable<T>
{
    protected int Capacity;
    protected int Size;
    
    public const int PutNil = 0;
    public const int PutOk = 1;
    public const int PutErr = 2;
    
    public const int RemoveNil = 0;
    public const int RemoveOk = 1;
    public const int RemoveErr = 2;
    
    // Конструктор
    // постусловие создаёт пустую хэш-таблицу размером с capacity 
    public OriginalAbstractHashTable(int capacity)
    {
        Capacity = capacity;
        Size = 0;
    }
    
    // команды
    // предусловие: хэш-таблица не заполнена
    // постусловие: в хэш таблицу добавлен новый элемент
    public abstract void Put(T value);
    
    // предусловие: в хэш-таблице есть удаляемый элемент
    // постусловие: из хэш-таблицы удалён переданный элемент
    public abstract void Remove(T value);
    
    // постусловие: хэш таблица полностью очищена
    public abstract void Clear();
    
    // запросы: 
    public abstract bool Contains(T value);
    
    public abstract int GetCapacity();

    public abstract int Count();
    
    // дополнительные запросы:
    public abstract int GetPutStatus(); // успешно; элемент уже существует; хэш-таблица заполнены
    public abstract int GetRemoveStatus(); // // успешно; удаляемого элемента не было 
}

public class HashTable<T> : OriginalAbstractHashTable<T>
{
    private int _putStatus = PutNil;
    private int _removeStatus = RemoveNil;
    
    private List<T>[] buckets;

    public HashTable(int capacity) : base(capacity)
    {
        buckets = new List<T>[Capacity];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = [];
        }
    }

    private int Hash(T value) => Math.Abs(value.GetHashCode()) % Capacity;
    
    public override void Put(T value)
    {
        if (Size == Capacity)
        {
            _putStatus = PutErr;
            return;
        }
        
        if (Contains(value))
        {
            _putStatus = PutErr;
            return;
        }
        
        int index = Hash(value);
        List<T> bucket = buckets[index];
        bucket.Add(value);
        _putStatus = PutOk;
        Size += 1;
    }

    public override void Remove(T value)
    { 
        if (!Contains(value))
        {
            _removeStatus = RemoveErr;
            return;
        }

        int bucketIndex = Hash(value);
        List<T> bucket = buckets[bucketIndex];
        bucket.Remove(value);
        Size -= 1;
        _removeStatus = RemoveOk;
    }

    public override void Clear()
    {
        buckets = new List<T>[Capacity];
        foreach (var bucket in buckets)
        {
            bucket.Clear();
        }
        Size = 0;
    }

    public override bool Contains(T value)
    {
        int index = Hash(value);
        List<T> bucket = buckets[index];
        return bucket.Contains(value);
    }

    public override int GetCapacity() => Capacity;

    public override int Count() => Size;

    public override int GetPutStatus() => _putStatus;
    public override int GetRemoveStatus() => _removeStatus;

}