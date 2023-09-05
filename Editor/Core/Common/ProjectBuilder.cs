using System;
using System.Collections.Generic;
using ProjectStructure.Initializer.Editor.Core.Interfaces;

namespace ProjectStructure.Initializer.Editor.Core.Common
{
    public class ProjectBuilder
    {
        private readonly List<ProjectFolder> _structure = new();

        public ProjectBuilder(IProjectStructureConfig config)
        {
            config.Build(this);
        }
        
        public ProjectBuilder AddRootFolder(string name, Action<ProjectFolder> initialize = null)
        {
            var folder = new ProjectFolder(name);
            
            _structure.Add(folder);
            initialize?.Invoke(folder);
            
            return this;
        }
    }
}