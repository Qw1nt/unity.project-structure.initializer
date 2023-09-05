using System;
using System.Collections.Generic;
using System.IO;
using ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEditor;

namespace ProjectStructure.Initializer.Editor.Core.Common
{
    public class ProjectBuilder
    {
        private readonly List<ProjectFolder> _structure = new();

        public ProjectBuilder(IProjectStructureConfig config)
        {
            config.Setup(this);
        }
        
        public ProjectBuilder AddRootFolder(string name, Action<ProjectFolder> initialize = null)
        {
            var folder = new ProjectFolder(name);
            
            _structure.Add(folder);
            initialize?.Invoke(folder);
            
            return this;
        }

        public void Build()
        {
            CreateRootFolders();
            AssetDatabase.Refresh();
        }

        private void CreateRootFolders()
        {
            foreach (var folder in _structure)
            {
                var path = $"Assets/{folder.Name}";
                
                if(Directory.Exists(path) == true)
                    continue;
                
                Directory.CreateDirectory(path);
            }
        }
    }
}