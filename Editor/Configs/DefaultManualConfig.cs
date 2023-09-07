using Qw1nt.ProjectStructure.Initializer.Editor.Core.Common;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Interfaces;

namespace Qw1nt.ProjectStructure.Initializer.Editor.Configs
{
    public class DefaultManualConfig : IProjectStructureConfig
    {
        public void Setup(ProjectBuilder builder)
        {
            builder
                .AddRootFolder("3rdParty")
                .AddRootFolder("Art", projectFolder => projectFolder
                    .AddSubfolder("Animation", subFolder => subFolder
                        .AddSubfolder("AnimationClips")
                        .AddSubfolder("Animators"))
                    .AddSubfolder("Audio", subFolder => subFolder
                        .AddSubfolder("AudioClips")
                        .AddSubfolder("AudioMixers"))
                    .AddSubfolder("Fonts")
                    .AddSubfolder("Materials")
                    .AddSubfolder("Models")
                    .AddSubfolder("PhysicMaterials")
                    .AddSubfolder("Shaders")
                    .AddSubfolder("Sprites")
                    .AddSubfolder("Textures")
                    .AddSubfolder("Timeline")
                    .AddSubfolder("UIToolkit", subFolder => subFolder
                        .AddSubfolder("Resources", data => data
                            .AddSubfolder("Layouts")
                            .AddSubfolder("Settings")
                            .AddSubfolder("Styles")
                            .AddSubfolder("Themes"))))
                .AddRootFolder("Documentation")
                .AddRootFolder("Prefabs")
                .AddRootFolder("Presets")
                .AddRootFolder("Resources")
                .AddRootFolder("Scenes")
                .AddRootFolder("ScriptableObjects")
                .AddRootFolder("Scripts", subFolder => subFolder
                    .AddSubfolder("Editor", folder => folder
                        .AddFile("Editor.asmdef"))
                    .AddSubfolder("Runtime", folder => folder
                        .AddFile("Runtime.asmdef"))
                    .AddSubfolder("Tests"))
                .AddRootFolder("Settings");
        }
    }
}