using System.Collections.Generic;
using System.Linq;
using ProjectStructure.Initializer.Runtime.Core;
using ProjectStructure.Initializer.Runtime.Interfaces;
using UnityEngine;

namespace ProjectStructure.Initializer.Runtime.Configs
{
    public class DefaultJsonConfig : IProjectStructureConfig
    {
        private const string ConfigDataFilePath = "ProjectStructure.Initializer/DefaultJsonProjectConfigurationData";
        public void Build(ProjectBuilder builder)
        {
            var json = Resources.Load<TextAsset>(ConfigDataFilePath).text;
            var structure = JsonUtility.FromJson<ProjectFolder>(json);
            var mock = new ProjectFolder("Jojo");
        }
    }
}