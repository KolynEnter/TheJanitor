#nullable enable


/*
    A nullable element with its quantity
*/
namespace CS576.Janitor.Process
{
    public struct ElementWithNumber<W> where W : class?
    {
        public W? element;
        public int number;
    }
}
