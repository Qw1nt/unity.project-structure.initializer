using System.Collections.Generic;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Common;

namespace Qw1nt.ProjectStructure.Initializer.Editor.Core.Interfaces
{
    public interface IHierarchyParser
    {
        void Execute(IEnumerable<Folder> hierarchy, Queue<Folder> result);
    }
}