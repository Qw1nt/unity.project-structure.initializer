using System;
using System.Collections.Generic;
using System.Linq;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Common;
using Qw1nt.ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEditor;
using UnityEngine;

namespace Qw1nt.ProjectStructure.Initializer.Editor
{
    internal class ProjectStructureInitializerEditorWindow : EditorWindow
    {
        private readonly TypeCache.TypeCollection _configsAssembliesNames;
        private readonly string[] _configsDisplayNames;
        private readonly Queue<Folder> _viewHierarchy = new();

        private ProjectBuilder _builder;
        private int _configIndex;

        [MenuItem("Tools/Qw1nt/ProjectStructure/Initializer")]
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
            UpdateSelectedConfig(0);
            CreateBuilder();
        }

        private static TypeCache.TypeCollection FindAvailableConfigs()
        {
            return TypeCache.GetTypesDerivedFrom<IProjectStructureConfig>();
        }

        private void OnGUI()
        {
            var selectedIndex = EditorGUILayout.Popup("Конфигурация: ", _configIndex, _configsDisplayNames);
            if (_configIndex != selectedIndex)
                UpdateSelectedConfig(selectedIndex);

            DrawConfigStructure();
            if (GUILayout.Button("Инициализировать") == false)
                return;

            CreateBuilder().Build();
            EditorUtility.DisplayDialog("Информация", "Структура успешно инициализирована", "Ок");
            UpdateSelectedConfig(_configIndex);
        }

        private void DrawConfigStructure()
        {
            foreach (var folder in _viewHierarchy)
            {
                GUILayout.BeginHorizontal();

                var isExclude = _builder.Mask.IsExclude(folder);
                var isInclude = EditorGUILayout.Toggle(!isExclude, GUILayout.Width(15));

                if (isInclude != !isExclude)
                {
                    if (isInclude == true)
                        _builder.IncludeFolder(folder);
                    else
                        _builder.ExcludeFolder(folder);
                }

                EditorGUILayout.LabelField($"{string.Concat(Enumerable.Repeat("- ", folder.Depth))}{folder.Name}");
                GUILayout.EndHorizontal();
            }
        }

        private ProjectBuilder CreateBuilder()
        {
            var configInstance = Activator.CreateInstance(_configsAssembliesNames[_configIndex]);
            _builder = new ProjectBuilder((IProjectStructureConfig) configInstance, new HierarchyParser());
            return _builder;
        }

        private void UpdateSelectedConfig(int index)
        {
            _configIndex = index;
            CreateBuilder().Parse(_viewHierarchy);
        }
    }
}