using Qw1nt.ProjectStructure.Initializer.Editor.Core.Common;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Interfaces;

namespace Qw1nt.ProjectStructure.Initializer.Editor.Configs
{
    public class DefaultManualConfig : IProjectStructureConfig
    {
        public void Setup(ProjectBuilder builder)
        {
            builder
                .AddFolder("3rdParty")
                .AddFolder("Art", projectFolder => projectFolder
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
                .AddFolder("Documentation")
                .AddFolder("Prefabs")
                .AddFolder("Presets")
                .AddFolder("Resources")
                .AddFolder("Scenes")
                .AddFolder("ScriptableObjects")
                .AddFolder("Scripts", subFolder => subFolder
                    .AddSubfolder("Editor", folder => folder
                        .AddFile("Editor.asmdef"))
                    .AddSubfolder("Runtime", folder => folder
                        .AddFile("Runtime.asmdef"))
                    .AddSubfolder("Tests"))
                .AddFolder("Settings");
        }
    }
}