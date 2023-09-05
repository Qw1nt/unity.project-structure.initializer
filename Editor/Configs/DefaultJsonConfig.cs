using ProjectStructure.Initializer.Editor.Core.Common;
using ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEngine;

namespace ProjectStructure.Initializer.Editor.Configs
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