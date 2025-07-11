using System.Collections;

namespace Structures;

public abstract class AbstractHashTable<T>
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
    public AbstractHashTable(int capacity)
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
    public abstract int GetPutStatus(); // успешно; система коллизий не смогла найти
    // свободный слот для значения
    public abstract int GetRemoveStatus(); // // успешно; удаляемого элемента не было 
}


public abstract class AbstractPowerSet<T>:AbstractHashTable<T>, IEnumerable<T>
{
    public const int PutNil = 0;
    public const int PutOk = 1;
    public const int PutErr = 2;
    
    // конструктор
    // постусловие: создано пустое множество с заданным размером
    public AbstractPowerSet(int capacity) : base(capacity)
    {
    
    }
    
    // команды
    // предусловие: множество не заполнена
    // постусловие: элемент добавлен во множество, если его там не было
    public abstract override void Put(T value);
    
    // пустое ли множество
    public abstract bool IsEmpty();
    
    // объединение текущего и переданного множества
    public abstract AbstractPowerSet<T> Union(AbstractPowerSet<T> set2);

    // пересечение текущего и переданного множества
    public abstract AbstractPowerSet<T> Intersection(AbstractPowerSet<T> set2);

    // возвращает разницу текущего и переданного множества 
    public abstract AbstractPowerSet<T> Difference(AbstractPowerSet<T> set2);

    // проверяет, является ли переданное множество подмножеством текущего множества.
    public abstract bool IsSubset(AbstractPowerSet<T> set2);

    //  сравнивает текущее множество с переданным
    public abstract bool Equals(AbstractPowerSet<T> set2);

    // дополнительные запросы:
    public abstract override int GetPutStatus(); // успешно; система коллизий не смогла найти
                                                 // свободный слот для значения
                                                 
    public abstract IEnumerator<T> GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class PowerSet<T> : AbstractPowerSet<T>, IEnumerable<T>
{
    private int _putStatus = PutNil;
    private int _removeStatus = RemoveNil;
    
    private List<T>[] buckets;
    
    public PowerSet(int capacity) : base(capacity)
    {
        buckets = new List<T>[Capacity];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new List<T>();
        }
    }
    
    private int Hash(T value) => Math.Abs(value.GetHashCode()) % Capacity;
    
    public override void Put(T value)
    {
        if (Size >= Capacity)
        {
            _putStatus = PutErr;
            return;
        }
        
        if (Contains(value))
        {
            _putStatus = PutOk;
            return;
        }
        
        int index = Hash(value);
        List<T> bucket = buckets[index];
        bucket.Add(value);
        Size += 1;
        _putStatus = PutOk;
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
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new List<T>();
        }
        Size = 0;
    }
    
    
    public override bool Contains(T value)
    {
        int index = Hash(value);
        List<T> bucket = buckets[index];
        return bucket.Contains(value);
    }
    
    public override bool IsEmpty()
    {
        return Size == 0;
    }
    
    public override int GetCapacity()
    {
        return Capacity;
    }
    
    public override int Count()
    {
        return Size;
    }
    
    public override AbstractPowerSet<T> Union(AbstractPowerSet<T> set2)
    {
        PowerSet<T> result = new PowerSet<T>(Capacity + set2.GetCapacity());
        
        foreach (T element in this)
        {
            result.Put(element);
        }
        
        foreach (T element in set2)
        {
            result.Put(element);
        }
        
        return result;
    }
    
    public override AbstractPowerSet<T> Intersection(AbstractPowerSet<T> set2)
    {
        PowerSet<T> result = new PowerSet<T>(Math.Min(Capacity, set2.GetCapacity()));
        
        foreach (T element in this)
        {
            if (set2.Contains(element))
            {
                result.Put(element);
            }
        }
        
        return result;
    }
    
    public override AbstractPowerSet<T> Difference(AbstractPowerSet<T> set2)
    {
        PowerSet<T> result = new PowerSet<T>(Capacity);
        
        foreach (T element in this)
        {
            if (!set2.Contains(element))
            {
                result.Put(element);
            }
        }
        
        return result;
    }
    
    public override bool IsSubset(AbstractPowerSet<T> set2)
    {
        foreach (T element in set2)
        {
            if (!Contains(element))
            {
                return false;
            }
        }
        
        return true;
    }
    
    public override bool Equals(AbstractPowerSet<T> set2)
    {
        if (Size != set2.Count())
        {
            return false;
        }
        
        foreach (T element in this)
        {
            if (!set2.Contains(element))
            {
                return false;
            }
        }
        
        return true;
    }
    
    public override int GetPutStatus()
    {
        return _putStatus;
    }
    
    public override int GetRemoveStatus()
    {
        return _removeStatus;
    }
    public override IEnumerator<T> GetEnumerator()
    {
        foreach (var bucket in buckets)
        {
            foreach (var element in bucket)
            {
                yield return element;
            }
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
