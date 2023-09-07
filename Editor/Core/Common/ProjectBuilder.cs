using System;
using System.Collections.Generic;
using System.IO;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEditor;

namespace Qw1nt.ProjectStructure.Initializer.Editor.Core.Common
{
    public class ProjectBuilder
    {
        private readonly List<Folder> _structure = new();
        private readonly BuilderMask _mask = new();

        public ProjectBuilder(IProjectStructureConfig config, IHierarchyParser parser)
        {
            config.Setup(this);
            _parser = parser;
        }

        private readonly IHierarchyParser _parser;

        internal IBuilderMask Mask => _mask;

        public ProjectBuilder AddRootFolder(string name, Action<Folder> initialize = null)
        {
            var folder = new Folder(name);

            _structure.Add(folder);
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

        internal void Parse(Queue<Folder> result)
        {
            _parser.Execute(_structure, result);
        }

        internal void Build()
        {
            var parsedTree = new Queue<Folder>();
            _parser.Execute(_structure, parsedTree);
            parsedTree = _mask.Apply(parsedTree);

            foreach (var folder in parsedTree)
                SafeDirectoryCreate(folder);

            AssetDatabase.Refresh();
        }

        private void SafeDirectoryCreate(Folder folder)
        {
            if (Directory.Exists(folder.FullPath) == false)
                Directory.CreateDirectory(folder.FullPath);
        }
    }
}