# MSBuild Tutorial

## Give a message simply

```c#
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <FullName>Mert Koprucu</FullName>
  </PropertyGroup>

  <Target Name="GiveFullName">
    <Message Text="$(FullName)" />
  </Target>
</Project>
```

To Build Target

``dotnet build -t GiveFullName -v n``

## Difference of Property and Item

Property is like a scalar value. On the other hand, property is such as vectorial value.    

```c#
  <ItemGroup>
    <CompilingFiles Include="src/*.cs" />
    <CompilingFiles Remove="src/Tar.cs" />
  </ItemGroup>
```

```c#
  <Target Name="CompilingAllFiles">
    <Message Text="@(CompilingFiles)" />
  </Target>
```

Vectorial values can be shown using character `@`.

## To add reference into project

```c#
  <ItemGroup>
    <PackageReference Include="Microsoft.Build.Utilities.Core" Version="16.9.0" />
  </ItemGroup>
```

## Using Task:

`.csproj side`

You should compile the project before using task with msbuild.

```c#
  <UsingTask TaskName="VeryUsefulTask" AssemblyFile="bin\Debug\net5.0\msbuildtest.dll"/>

  <Target Name="FirstTask" AfterTargets="AfterBuild">
    <VeryUsefulTask Name="Mert" />
  </Target>
```

`Task implementation side`
```c#
public class VeryUsefulTask : Microsoft.Build.Utilities.Task
    {
        [Required]
        public string Name { get; set; }
        
        public override bool Execute()
        {
            Log.LogMessage($"You Provide for Property Name: {Name}");
            return true;
        }
    }
```