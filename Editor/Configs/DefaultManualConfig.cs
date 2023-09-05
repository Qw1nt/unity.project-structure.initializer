using ProjectStructure.Initializer.Editor.Core.Common;
using ProjectStructure.Initializer.Editor.Core.Interfaces;

namespace ProjectStructure.Initializer.Editor.Configs
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