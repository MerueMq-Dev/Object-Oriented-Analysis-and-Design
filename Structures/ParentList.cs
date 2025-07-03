namespace Structures;

public abstract class ParentList<T>
{
    // Конструктор
    // постусловие: создан новый пустой список
    public ParentList() { }

    // Команды
    // предусловие: список не пуст; 
    // постусловие: курсор установлен на первый узел в списке
    public abstract void Head();

    // предусловие: список не пуст; 
    // постусловие: курсор установлен на последний узел в списке
    public abstract void Tail();

    // предусловие: правее курсора есть элемент; 
    // постусловие: курсор сдвинут на один узел вправо
    public abstract void Right();

    // предусловие: список не пуст; 
    // постусловие: следом за текущим узлом добавлен 
    // новый узел с заданным значением
    public abstract void PutRight(T value);

    // предусловие: список не пуст; 
    // постусловие: перед текущим узлом добавлен 
    // новый узел с заданным значением
    public abstract void PutLeft(T value);

    // предусловие: список пуст; 
    // постусловие: в списке один узел
    public abstract void AddToEmpty(T value);

    // предусловие: список не пуст; 
    // постусловие: текущий узел удалён, 
    // курсор смещён к правому соседу, если он есть, 
    // в противном случае курсор смещён к левому соседу, если он есть
    public abstract void Remove();

    // постусловие: список очищен от всех элементов
    public abstract void Clear();

    // постусловие: новый узел добавлен в хвост списка
    public abstract void AddTail(T value);

    // постусловие: в списке удалены все узлы с заданным значением
    public abstract void RemoveAll(T value);

    // предусловие: список не пуст;
    // постусловие: значение текущего узла заменено на новое
    public abstract void Replace(T value);

    // постусловие: курсор установлен на следующий узел 
    // с искомым значением, если такой узел найден
    public abstract void Find(T value);

    // Запросы

    // предусловие: список не пуст
    public abstract T Get();
    
    public abstract bool IsHead();
    public abstract bool IsTail();
    public abstract bool IsValue();
    public abstract int Size();

    // Запросы статусов (возможные значения статусов)
    public abstract int GetHeadStatus();           // успешно; список пуст
    public abstract int GetTailStatus();           // успешно; список пуст
    public abstract int GetRightStatus();          // успешно; правее нет элемента
    public abstract int GetPutRightStatus();       // успешно; список пуст
    public abstract int GetPutLeftStatus();        // успешно; список пуст
    public abstract int GetAddToEmptyStatus();     // успешно
    public abstract int GetRemoveStatus();         // успешно; список пуст
    public abstract int GetReplaceStatus();        // успешно; список пуст
    public abstract int GetFindStatus();           // следующий найден; следующий не найден; список пуст
    public abstract int GetGetStatus();            // успешно; список пуст
}

public abstract class LinkedList<T> : ParentList<T>
{
    // Конструктор
    // постусловие: создан новый пустой список
    public LinkedList() { }
    
}

public abstract class TwoWayList<T> : ParentList<T>
{
    // Конструктор
    // постусловие: создан новый пустой список
    public TwoWayList() { }
    
    // команда
    // предусловие: левее курсора есть элемент;
    // постусловие: курсор сдвинут на один узел влево
    public abstract void Left();
    
    // запрос
    public abstract int GetLeftStatus(); // успешно; левее нет элемента
}
