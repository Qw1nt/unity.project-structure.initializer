﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProjectStructure.Initializer.Editor.Core.Common;
using ProjectStructure.Initializer.Editor.Core.Extensions;
using ProjectStructure.Initializer.Editor.Core.Interfaces;
using UnityEditor;
using UnityEngine;

namespace ProjectStructure.Initializer.Editor
{
    public class ProjectStructureInitializerEditorWindow : EditorWindow
    {
        private readonly TypeCache.TypeCollection _configsAssembliesNames;
        private readonly string[] _configsDisplayNames;
        private readonly Queue<Folder> _viewHierarchy = new();

        private ProjectBuilder _builder;
        private int _configIndex;

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

            if (GUILayout.Button("Инициализировать") == false)
                return;

            var structure = new Queue<Folder>();

            // builder.CreateTree(structure);
            // structure.LogHierarchy("Создана следующая структура:");
        }

        private ProjectBuilder CreateBuilder()
        {
            var configInstance = Activator.CreateInstance(_configsAssembliesNames[_configIndex]);
            _builder = new ProjectBuilder((IProjectStructureConfig) configInstance);
            return _builder;
        }

        private void UpdateSelectedConfig(int index)
        {
            _configIndex = index;
            CreateBuilder().CreateTree(_viewHierarchy);
        }
    }
}