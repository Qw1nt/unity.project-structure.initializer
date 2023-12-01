using System.Collections.Generic;
using System.Linq;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Interfaces;

namespace Qw1nt.ProjectStructure.Initializer.Editor.Core.Common
{
    internal class BuilderMask : IBuilderMask
    {
        private readonly HashSet<string> _excludes = new();

        internal Queue<Folder> Apply(IEnumerable<Folder> parsedHierarchy)
        {
            var result =  new Queue<Folder>(parsedHierarchy.Where(x => _excludes.Any(path => path == x.FullPath) == false));
            return result;
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