using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEditor;
using UnityEngine;

namespace ProjectStructure.Initializer.Editor.Core.Common
{
    public class ProjectBuilder
    {
        private readonly Dictionary<string, Folder> _structure = new();

        public ProjectBuilder(IProjectStructureConfig config)
        {
            config.Setup(this);
        }
        
        public ProjectBuilder AddRootFolder(string name, Action<Folder> initialize = null)
        {
            var folder = new Folder(name);
            
            _structure.Add(folder.Name, folder);
            initialize?.Invoke(folder);
            
            return this;
        }

        public ProjectBuilder ExcludeFolder(string name)
        {
            if (_structure.ContainsKey(name) == true)
                _structure.Remove(name);

            return this;
        }

        public void Build()
        {
            foreach (var folder in _structure)
                CreateRootFolder(folder.Value);

            AssetDatabase.Refresh();
        }

        private void CreateRootFolder(Folder root)
        {
            var rootFolderPath = $"Assets/{root.Name}";
            SafeDirectoryCreate(rootFolderPath);

            var subFolders = root.SubFolders;
            foreach (var folder in subFolders)
            {
                var basePath = $"{rootFolderPath}/{folder.Name}";
                SafeDirectoryCreate(basePath);
                
                while (folder.SubFolders.Count > 0)
                {
                    if(folder.SubFolders.TryDequeue(out var target) == false)
                        break;

                    var createPath = $"{basePath}/{target.Name}";
                    SafeDirectoryCreate(createPath);
                }   
            }
        }

        private void SafeDirectoryCreate(string path)
        {
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
        }
    }
}