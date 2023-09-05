using System.Collections.Generic;

namespace TemplateBuilder.Runtime
{
    public interface IProjectStructureConfig
    {
        List<ProjectFolder> Build();
    }
}