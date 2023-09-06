﻿using System;
using System.Collections.Generic;

namespace ProjectStructure.Initializer.Editor.Core.Common
{
    [Serializable]
    public class Folder
    {
        private readonly List<string> _files = new();

        public Folder(string name, Folder root = null)
        {
            Name = name;
            Root = root;
        }
        
        public string Name { get; set; }

        public Folder Root { get; }

        public Queue<Folder> SubFolders { get; } = new();

        public IReadOnlyList<string> Files => _files;

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