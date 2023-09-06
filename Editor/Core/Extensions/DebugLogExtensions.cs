using System.Collections.Generic;
using System.Text;
using ProjectStructure.Initializer.Editor.Core.Common;
using UnityEngine;

namespace ProjectStructure.Initializer.Editor.Core.Extensions
{
    internal static class DebugLogExtensions
    {
        internal static void LogHierarchy(this Queue<Folder> structure, string title = null)
        {
            var stringBuilder = new StringBuilder();

            if (title != null)
                stringBuilder.Append($"{title}\n");

            while (structure.TryDequeue(out var folder) == true)
            {
                stringBuilder.Append(folder.Depth > 0 ? "|" : "| ");

                for (int i = 0; i < folder.Depth; i++)
                    stringBuilder.Append("- ");

                stringBuilder.Append(folder.Name);
                stringBuilder.Append("\n");
            }

            Debug.Log(stringBuilder.ToString());
        }
    }
}