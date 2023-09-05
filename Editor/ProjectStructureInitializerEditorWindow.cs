using System;
using System.Linq;
using DG.DOTweenEditor;
using Mono.Cecil;
using ProjectStructure.Initializer.Editor.Core.Common;
using ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using Directory = UnityEngine.Windows.Directory;

namespace ProjectStructure.Initializer.Editor
{
    public class ProjectStructureInitializerEditorWindow : EditorWindow
    {
        private readonly TypeCache.TypeCollection _configsAssembliesNames;
        private readonly string[] _configsDisplayNames;

        private int _selectedConfigIndex;

        [MenuItem("Tools/SnailBee/ProjectStructure/Initializer")]
        private static void Open()
        {
            var window = CreateInstance<ProjectStructureInitializerEditorWindow>();
            window.position = new Rect(Screen.width / 2f, Screen.height / 2f, 400f, 150f);
            window.titleContent = new GUIContent("Project Structure - Initializer");
            window.Show();
        }

        public ProjectStructureInitializerEditorWindow()
        {
            _configsAssembliesNames = FindAvailableConfigs();
            _configsDisplayNames = _configsAssembliesNames.Select(x => x.Name).ToArray();
        }

        private void OnGUI()
        {
            _selectedConfigIndex = EditorGUILayout.Popup("Конфигурация: ", _selectedConfigIndex, _configsDisplayNames);


            if (GUILayout.Button("Инициализировать") == false)
                return;

            var configInstance = Activator.CreateInstance(_configsAssembliesNames[_selectedConfigIndex]);
            var builder = new ProjectBuilder((IProjectStructureConfig) configInstance);
            builder.Build();
        }

        private static TypeCache.TypeCollection FindAvailableConfigs()
        {
            return TypeCache.GetTypesDerivedFrom<IProjectStructureConfig>();
        }
    }
}