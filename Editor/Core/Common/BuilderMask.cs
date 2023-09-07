using System.Collections.Generic;
using System.Linq;
using ProjectStructure.Initializer.Editor.Core.Interfaces;

namespace ProjectStructure.Initializer.Editor.Core.Common
{
    internal class BuilderMask : IBuilderMask
    {
        private readonly HashSet<string> _excludes = new();

        internal void Apply(IHierarchyParser parser, IEnumerable<Folder> structure, Queue<Folder> result)
        {
            parser.Execute(structure, result);
            result = new Queue<Folder>(result.Where(x => _excludes.Contains(x.FullPath) == false));
        }
        
        internal void Include(Folder folder)
        {
            if (_excludes.Contains(folder.FullPath) == true)
                _excludes.Remove(folder.FullPath);
        }

        internal void Exclude(Folder folder)
        {
            _excludes.Add(folder.FullPath);
        }

        public bool IsExclude(Folder folder)
        {
            return _excludes.Contains(folder.FullPath);
        }
    }
}