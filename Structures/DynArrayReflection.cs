﻿namespace Structures;

public abstract class DynArrayReflection
{
    // Мое решение получилось довольно простым. Я сделал основные операции - добавление 
    // в конец, вставку со сдвигом элементов, удаление и получение элемента, то есть все
    // операции которые были в оригинальном занятии. В эталонном решении же появились дополнительные операции
    // Во-первых, у меня нет метода size() - а это базовая штука, которая 
    // должна быть. Во-вторых, мой Insert только вставляет со сдвигом, а в эталоне есть отдельный 
    // put для замены элемента на месте. Это разные операции, и обе нужны. Еще в эталоне есть put_left 
    // и put_right для точного позиционирования, что дает больше гибкости. Мой подход более 
    // прямолинейный - работаю только с абсолютными индексами. В целом я сделал минимально 
    // работающую версию, которая решает основные задачи. 
    // Понятно, что стоит добавить size() и подумать о разделении операций замены и вставки.
}
