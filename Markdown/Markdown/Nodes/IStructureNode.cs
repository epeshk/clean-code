using System.Collections.Generic;

namespace Markdown.Nodes
{
    public interface IStructureNode : INode
    {
        IEnumerable<INode> NestedNodes { get; }
    }
}