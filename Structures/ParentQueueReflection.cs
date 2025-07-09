namespace Structures;

public class ParentQueueReflection
{
    // Сравнив моё решение я нашел несколько отличий:
    // Первое — отсутствие конструкторов
    // В моих АТД (AbstractParentQueue, AbstractQueue, AbstractDeque) отсутствуют объявления конструкторов. 
    // В эталонных АТД конструкторы явно объявлены:
    // public ParentQueue<T> ParentQueue();
    // public Queue<T> Queue();
    // public Deque<T> Deque();
        
    // Второе — дополнительные методы
    // Мой AbstractParentQueue содержит дополнительные методы Size(), IsEmpty() и Clear(), которые отсутствуют 
    // в эталонном ParentQueue. Эталон содержит только базовые операции: add_tail, remove_front, get_front и 
    // соответствующие статусы.
    
    // Третье — конвенция именования методов
    // Мои АТД используют C# конвенцию PascalCase (AddTail, RemoveFront, GetFront, AddFront, RemoveTail, GetTail),
    // в то время как эталонные АТД используют snake_case (add_tail, remove_front, get_front, add_front, remove_tail, 
    // get_tail).
    
    // Четвёртое — названия классов
    // Мой базовый АТД называется AbstractParentQueue, в то время как в эталоне он называется просто ParentQueue.
    // Это сделано для того что бы использовать имена ParentQueue, Queue и Deque для конкретных реализаций данных АТД.
}

