namespace Structures;

public abstract class AbstractBloomFilter
{
    protected readonly int FilterLength; 
    
    // Конструктор
    // постусловие: создан пустой фильтр Блюма заданной длины
    public AbstractBloomFilter(int filterLength)
    {
        FilterLength = filterLength;
    }
     
    // предусловие: повторный вызов Add c той же строкой не изменяет состояние фильтра
    // постусловие: строка item добавлена в фильтр;
    public abstract void Add(string item);
    
    // постусловие: если item добавлялся ранее, то возвращается true;
    // постусловие: если item не добавлялся, то может вернуться true или false
    public abstract bool IsValue(string item);

    // постусловие: фильтр Блюма очищен. 
    public abstract void Clear(); 
}

public class BloomFilter: AbstractBloomFilter
{
    private bool[] bitArray;
    private readonly int hashFunctionCount;
    
    public BloomFilter(int filterLength) : base(filterLength)
    {
        bitArray = new bool[filterLength];
        hashFunctionCount = 3;
    }
    
    public override void Add(string item)
    {
        int[] hashes = GetHashes(item);
        
        for (int i = 0; i < hashFunctionCount; i++)
        {
            int index = Math.Abs(hashes[i]) % FilterLength;
            bitArray[index] = true;
        }
    }
    
    public override bool IsValue(string item)
    {
        int[] hashes = GetHashes(item);
        
        for (int i = 0; i < hashFunctionCount; i++)
        {
            int index = Math.Abs(hashes[i]) % FilterLength;
            if (!bitArray[index])
            {
                return false;
            }
        }
        
        return true;
    }

    public override void Clear()
    {
        bitArray = new bool[FilterLength];
    }
    
    private int[] GetHashes(string item)
    {
        int[] hashes = new int[hashFunctionCount];
        
        hashes[0] = item.GetHashCode();
        hashes[1] = Hash(item, 17);
        hashes[2] = Hash(item, 223);
        
        return hashes;
    }

    private int Hash(string str1, int salt)
    {
        var hash = 0;
        for (int i = 0; i < str1.Length; i++)
        {
            hash = (hash * salt + str1[i]) % FilterLength;
        }
            
        return hash;
    }
}
