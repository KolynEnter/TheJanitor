namespace CS576.Janitor.Process
{
    // Has one child and is capable of augmenting the return state of it's child
    public abstract class DecoratorNode : Node
    {
        public Node child;
    }
}
