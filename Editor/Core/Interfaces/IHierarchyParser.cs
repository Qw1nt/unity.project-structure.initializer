using System.Collections.Generic;
using ProjectStructure.Initializer.Editor.Core.Common;

namespace ProjectStructure.Initializer.Editor.Core.Interfaces
{
    public interface IHierarchyParser
    {
        void Execute(IEnumerable<Folder> hierarchy, Queue<Folder> result);
    }
}