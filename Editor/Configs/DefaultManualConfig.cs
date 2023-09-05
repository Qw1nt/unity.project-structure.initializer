using ProjectStructure.Initializer.Runtime.Core;
using ProjectStructure.Initializer.Runtime.Interfaces;

namespace ProjectStructure.Initializer.Runtime.Configs
{
    public class DefaultManualConfig : IProjectStructureConfig
    {
        public void Build(ProjectBuilder builder)
        {
            builder
                .AddRootFolder("3rdParty")
                .AddRootFolder("Art", folder => folder
                    .AddSubfolder("Animation")
                    .AddSubfolder("Audio")
                    .AddSubfolder("Fonts"))
                .AddRootFolder("Documentation")
                .AddRootFolder("Prefabs")
                .AddRootFolder("Presets")
                .AddRootFolder("Resources")
                .AddRootFolder("Scenes")
                .AddRootFolder("ScriptableObjects")
                .AddRootFolder("Editor")
                .AddRootFolder("Settings");
        }
    }
}