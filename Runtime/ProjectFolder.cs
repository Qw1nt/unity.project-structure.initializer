using System;
using System.Collections.Generic;

namespace TemplateBuilder.Runtime
{
    public class ProjectFolder
    {
        private readonly List<ProjectFolder> _child = new();

        public ProjectFolder(string name, ProjectFolder root = null)
        {
            Name = name;
            Root = root;
        }
        
        public string Name { get; }
        
        public ProjectFolder Root { get; }

        public IReadOnlyList<ProjectFolder> SubFolders => _child;

        public ProjectFolder AddSubfolder(string name)
        {
            _child.Add(new ProjectFolder(name, this));
            return this;
        } 
        
        public ProjectFolder AddSubfolder(string name, Action<ProjectFolder> onCreate)
        {
            var instance = new ProjectFolder(name, this);
            _child.Add(instance);
            onCreate?.Invoke(instance);
            return this;
        }
    }
}