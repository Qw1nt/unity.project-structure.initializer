using System.Collections.Generic;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEngine;

namespace Qw1nt.ProjectStructure.Initializer.Editor.Core.Common
{
    public class HierarchyParser : IHierarchyParser
    {
        public void Execute(IEnumerable<Folder> hierarchy, Queue<Folder> result)
        {
            foreach (var root in hierarchy)
                BuildBranch(root, result);
        }
        
        private void BuildBranch(Folder root, Queue<Folder> result)
        {
            var target = root;
            result.Enqueue(target);
            
            while (target.SubFolders.TryDequeue(out var child))
            {
                target = child;
                result.Enqueue(target);

                while (target.SubFolders.Count == 0 && target.Root != null)
                    target = target.Root;
            }
        }

        /*private void BuildBranch(Folder root, LinkedList<Folder> result)
        {
            var target = root;
            result.AddLast(root);

            var node = target.SubFolders.First;
            
            while (node.Next != null)
            {
                target = target.SubFolders.First.Value;
                result.AddLast(target);

                while (target.SubFolders.Count > 0 && target.Root != null)
                    target = target.Root;

                node = node.Next;
            }
        }*/
    }
}