using System;
using System.Collections.Generic;

namespace Qw1nt.ProjectStructure.Initializer.Editor.Core.Common
{
    [Serializable]
    public class Folder
    {
        private readonly List<string> _files = new();
        
        public Folder(string name, Folder root = null)
        {
            Name = name;
            Root = root;
            Depth = Root != null ? Root.Depth + 1 : 0;
            FullPath = Root != null ? Root.FullPath + $"/{Name}" : $"Assets/{Name}";
        }
        
        public string Name { get; set; }

        public Folder Root { get; }
        
        public int Depth { get; }
        
        public string FullPath { get; }

        public Queue<Folder> SubFolders { get; } = new();

        public Folder AddFile(string name)
        {
            _files.Add(name);
            return this;
        }
        
        public Folder AddSubfolder(string name)
        {
            SubFolders.Enqueue(new Folder(name, this));
            return this;
        }

        public Folder AddSubfolder(string name, Action<Folder> onCreate)
        {
            var instance = new Folder(name, this);
            SubFolders.Enqueue(instance);
            onCreate?.Invoke(instance);
            return this;
        }
    }
}