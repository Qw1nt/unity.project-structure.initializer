# Project Initializer
Создание структуры проекта в 2 клика 🖱️

## Использование

#### 1) Открыть панель инструмента

> Панель инструмента находится по пути Tools -> Qw1nt -> ProjectStructure -> Initializer

![App Screenshot](https://github.com/Qw1nt/unity.project-structure.initializer/blob/screenshots/Screenshots/1.png?raw=true)

#### 2) Выбрать нужную конфигурацию

> Для исключение ненужной нужно снять с неё выделение

![App Screenshot](https://github.com/Qw1nt/unity.project-structure.initializer/blob/screenshots/Screenshots/2.png?raw=true)

#### 3) Нажать на кнопку «Инициализировать»

![App Screenshot](https://github.com/Qw1nt/unity.project-structure.initializer/blob/screenshots/Screenshots/3.png?raw=true)

## Создание собственной конфигурации
 
Для этого необходимо унаследовать класс от ```IProjectStructureConfig```

```csharp
public class CustomConfig : IProjectStructureConfig
{
    public void Setup(ProjectBuilder builder)
    {
        // Добавление папки с названием "MyFolder"
        builder.AddFolder("MyFolder");

        // | MyFolder2
        // | - SimpleSubfolder
        builder.AddFolder("MyFolder2", folder => folder.AddSubfolder("SimpleSubfolder"));
        
        // | Fluent
        // | - FluentSub
        // | - - FluentSubSub
        // | NewRoot
        builder
            .AddFolder("Fluent", fluent => fluent
                .AddSubfolder("FluentSub", fluentSub => fluentSub
                    .AddSubfolder("FluentSubSub")))
            .AddFolder("NewRoot");
    }
}
```