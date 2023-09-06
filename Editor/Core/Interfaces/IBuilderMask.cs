using ProjectStructure.Initializer.Editor.Core.Common;

namespace ProjectStructure.Initializer.Editor.Core.Interfaces
{
    internal interface IBuilderMask
    {
        bool IsExclude(Folder folder);
    }
}