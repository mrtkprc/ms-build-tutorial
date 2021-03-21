This file is inspired by Tarik Guney's youtube video. [The video](https://www.youtube.com/watch?v=gdhwnhKBTho)


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

## To Build Target

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

### Vectorial values can be shown using character `@`.

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

## Sample Project
```xml
<Project DefaultTargets="Clean;Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
    <PropertyGroup>
        <AssemblyName>MSBuildSample</AssemblyName>
        <OutputPath>Outputs\</OutputPath>
    </PropertyGroup>

    <ItemGroup>
        <Compile Include="helloworld.cs">
            <Culture>TR</Culture>
        </Compile>
        <Compile Include="MyMath.cs" />
    </ItemGroup>

    <Target Name="Build">
        <Message Text="Your Operating System: $(OS)" />
        <Message Text="MSBuildToolsPath: $(MSBuildToolsPath)" />
        <Message Text="Project Directory: $(MSBuildProjectDirectory)" />
        <Message Text="Compiling Files: @(Compile)" />

        <!-- User defined metadata -->
        <Message Text="Compile.Culture: %(Compile.Culture)" />
        <!-- Well-known metadata -->
        <Message Text="Compile.Filename: %(Compile.Filename)" />

        <MakeDir Directories="$(OutputPath)" Condition="!Exists('$(OutputPath)')" />
        <Csc Sources="@(Compile)" OutputAssembly="$(OutputPath)$(AssemblyName).exe" />
        <!-- If you use Unix-based Operating system, executable permission will be added. -->
        <Exec Command="chmod +x $(OutputPath)$(AssemblyName).exe" Condition=" '$(OS)' == 'Unix' " />
    </Target>

    <Target Name="Clean">
        <RemoveDir Condition="Exists('$(OutputPath)')" Directories="$(OutputPath)" />
    </Target>

    <!-- You can combine other targets into current build -->
    <Target Name="Rebuild" DependsOnTargets="Clean;Build" />

</Project>

```

### Building the project

`msbuild helloworld.csproj -t:Build`


### Typing `msbuild` without target name

`msbuild`
Although a project file is not specified, MSBuild builds the helloworld.csproj file because there is only one project file in the current folder and default target is `Rebuild`


### Overriding property

`msbuild helloworld.csproj -t:Build -p:AssemblyName=Mert`

### Item metadata

Items may contain metadata in addition to the information gathered from the Include and Exclude attributes. This metadata can be used by tasks that require more information about items than just the item value.

