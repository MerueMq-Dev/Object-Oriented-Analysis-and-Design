using Structures;

BoundedStack<int> boundedStack = new BoundedStack<int>(3);
boundedStack.Push(1);
boundedStack.Push(2);
boundedStack.Push(3);
int pushStatus = boundedStack.GetPushStatus();
Console.WriteLine(pushStatus);
boundedStack.Push(4);
int newPushStatus = boundedStack.GetPushStatus();
Console.WriteLine(newPushStatus);