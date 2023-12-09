using System.Collections.Generic;


namespace CS576.Janitor.Process
{
    // Has a list of children and is the control flow of the behavior tree
    // like switch statements and for loops
    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();
    }
}
