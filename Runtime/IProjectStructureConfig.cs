using System.Collections.Generic;

namespace ProjectStructure.Initializer.Runtime
{
    public interface IProjectStructureConfig
    {
        List<ProjectFolder> Build();
    }
}