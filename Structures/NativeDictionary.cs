using System.Drawing;

namespace Structures;

public abstract class AbstractNativeDictionary<T>
{
    protected int Size;
    protected int Capacity;
    public const int GetNil = 0;
    public const int GetOk = 1;
    public const int GetErr = 2;

    public const int RemoveNil = 0;
    public const int RemoveOk = 1;
    public const int RemoveErr = 2;

    public const int PutNil = 0;
    public const int PutOk = 1;
    public const int PutErr = 2;

    // Конструктор создаёт ассоциативный массив заданного размера.  
    public AbstractNativeDictionary(int capacity)
    {
        Capacity = capacity;
        Size = 0;
    }

    // команды
    // предусловие: в массиве есть свободный слот для нового ключа, 
    // либо передаваемый ключ уже существует в массиве
    // постусловие: в пустой слот добавлен новый ключ и значение,
    // а для существующего ключа обновлено значение
    public abstract void Put(string key, T value);

    // постусловие: ассоциативный массив полностью очищен
    public abstract void Clear();

    // предусловие: в массиве есть значение с переданным ключом
    // постусловие: из массива удалён ключ и значение
    // которое хранилось по этому ключу
    public abstract void Remove(string key);

    // запросы
    // предусловие: в массиве есть переданные ключ и ассоциированное с ключом значение.
    public abstract T Get(string key); // возвращает ассоциированное с ключом значение

    // есть ли в массиве переданный ключ
    public abstract bool IsKey(string key);
    
    // вычисляет индекс внутреннего хранилища на основе ключа.
    protected abstract int HashFun(string key);

    // текущее количество элементов в массиве
    public abstract int GetSize();

    // дополнительные запросы
    public abstract int GetPutStatus(); // успешно; система коллизий не смогла найти свободный слот для значения
    public abstract int GetRemoveStatus(); // успешно; в массиве нет значения с переданным ключом
    public abstract int GetGetStatus(); // успешно; в массиве нет значения с переданным ключом
}

public class NativeDictionary<T> : AbstractNativeDictionary<T>
{
    private int _getStatus = GetNil;
    private int _removeStatus = RemoveNil;
    private int _putStatus = PutNil;

    private string?[] _keys;
    private T?[] _values;

    public NativeDictionary(int capacity) : base(capacity)
    {
        _keys = new string?[capacity];
        _values = new T?[capacity];
    }

    public override void Put(string key, T value)
    {
        bool isCollision = IsCollision(key);
        if (isCollision)
        {
            _putStatus = PutErr;
            return;
        }
        bool isKey = IsKey(key);
        int index = HashFun(key);
        if (isKey)
        {
            _values[index] = value;
            _putStatus = PutOk;
            return;
        }

        _keys[index] = key;
        _values[index] = value;
        _putStatus = PutOk;
        Size += 1;
    }

    public override void Clear()
    {
        _keys = new string?[Capacity];
        _values = new T?[Capacity];
        _getStatus = GetNil;
        _putStatus = PutNil;
        _removeStatus = RemoveNil;
        Size = 0;
    }

    public override void Remove(string key)
    {
        bool isCollision = IsCollision(key);
        if (isCollision)
        {
            _removeStatus = RemoveErr;
            return;
        }
        
        bool isKey = IsKey(key);
        if (!isKey)
        {
            _removeStatus = RemoveErr;
            return;
        }

        int index = HashFun(key);
        _keys[index] = null;
        _values[index] = default;
        Size -= 1;
        _removeStatus = RemoveOk;
    }

    public override T Get(string key)
    {
        bool isCollision = IsCollision(key);
        if (isCollision)
        {
            _getStatus = GetErr;
            return default!;
        }
        
        if (!IsKey(key))
        {
            _getStatus = GetErr;
            return default!;
        }
        
        int index = HashFun(key);
        _getStatus = GetOk;
        return _values[index]!;
    }

    public override bool IsKey(string key)
    {
        int index = HashFun(key);
        return _values[index] != null && _keys[index] == key;
    }

    private  bool IsCollision(string key)
    {
        int index = HashFun(key);
        return _values[index] != null && _keys[index] != key;
    }
    
    protected override int HashFun(string key)
    {
        var keyHash = key.GetHashCode();
        var slotIndex = Math.Abs(keyHash % Capacity);
        return slotIndex;
    }


    public override int GetSize() => Size;

    public override int GetPutStatus() => _putStatus;

    public override int GetRemoveStatus() => _removeStatus;

    public override int GetGetStatus() => _getStatus;
}