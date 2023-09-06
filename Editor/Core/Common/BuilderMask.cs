using System.Collections.Generic;
using System.Linq;
using ProjectStructure.Initializer.Editor.Core.Interfaces;

namespace ProjectStructure.Initializer.Editor.Core.Common
{
    internal class BuilderMask : IBuilderMask
    {
        private readonly HashSet<string> _excludes = new();

        public void Apply(Dictionary<string, Folder> structure)
        {
            
        }

        public void Include(Folder folder)
        {
            if (_excludes.Contains(folder.Name) == true)
                _excludes.Remove(folder.Name);
        }
        
        public void Exclude(Folder folder)
        {
            _excludes.Add(folder.Name);
        }

        public bool IsExclude(Folder folder)
        {
            return _excludes.Contains(folder.Name);
        }
    }
}