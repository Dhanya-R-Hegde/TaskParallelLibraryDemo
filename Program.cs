namespace TaskParallelDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Task Parallel Library");

            Console.WriteLine("Using C# For loop");
            for(int i=0; i<=10; i++)
            {
                Console.WriteLine("i = {0}, thread = {1} ", i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            }

            Console.WriteLine("Using C# ParallelFor loop");
            Parallel.For(0, 10, i =>
            {
                Console.WriteLine("i = {0}, thread = {1} ", i, Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(1000);
            });

            
            List<string> list = new List<string>();
            list.Add("Apple");
            list.Add("Mango");
            list.add("banana");
            list.Add("WaterMelon");
            list.Add("Grapes");

            Console.WriteLine("Using Foreach loop");
            foreach (string item in list)
            {
                Console.WriteLine("Fruit name : {0}, Thread : {1}", item, Thread.CurrentThread.ManagedThreadId);    
            }

            Console.WriteLine("Using ParallelForEach lopp");
            Parallel.ForEach(list, listFruit =>
            {
                Console.WriteLine("Fruit name : {0}, Thread : {1}", item, Thread.CurrentThread.ManagedThreadId);
            });



        }
    }
}
