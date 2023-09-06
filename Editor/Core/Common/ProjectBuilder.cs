using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEditor;
using UnityEngine;

namespace ProjectStructure.Initializer.Editor.Core.Common
{
    public class ProjectBuilder
    {
        private readonly Dictionary<string, Folder> _structure = new();
        private readonly BuilderMask _mask = new();

        public ProjectBuilder(IProjectStructureConfig config)
        {
            config.Setup(this);
        }

        internal IBuilderMask Mask => _mask;

        public ProjectBuilder AddRootFolder(string name, Action<Folder> initialize = null)
        {
            var folder = new Folder(name);

            _structure.Add(folder.Name, folder);
            initialize?.Invoke(folder);

            return this;
        }

        public ProjectBuilder ExcludeFolder(Folder folder)
        {
            _mask.Exclude(folder);
            return this;
        }

        public ProjectBuilder IncludeFolder(Folder folder)
        {
            _mask.Include(folder);
            return this;
        }

        internal void Build()
        {
            _mask.Apply(_structure);
            
            var structure = new Queue<Folder>();
            CreateTree(structure);
            
            AssetDatabase.Refresh();
        }

        internal void CreateTree(Queue<Folder> result)
        {
            result.Clear();
            foreach (var folder in _structure)
                CreateRootFolder(folder.Value, result);
        }

        private void CreateRootFolder(Folder root, Queue<Folder> collection = null)
        {
            var rootFolderPath = $"Assets/{root.Name}";
            SafeDirectoryCreate(rootFolderPath);

            var structure = collection ?? new Queue<Folder>();
            var target = root;
            structure.Enqueue(target);

            while (target.SubFolders.TryDequeue(out var child))
            {
                target = child;
                structure.Enqueue(target);

                while (target.SubFolders.Count == 0 && target.Root != null)
                    target = target.Root;
            }
        }

        private void SafeDirectoryCreate(string path)
        {
            if (Directory.Exists(path) == false)
                Directory.CreateDirectory(path);
        }
    }
}