# NewRelic.Synthetics.Api
MsBuild targets for working with NewRelic Synthetics API (disable monitor before deployment and enable it after)

## Usage

Could be used in a regular deployment as a standalone target with following modifications to your web app csproj file (after installation of Nuget package - https://www.nuget.org/packages/NewRelic.Synthetics.Api/ )

```xml
<PropertyGroup>
    <NewRelicSyntheticsApiPackageName>NewRelic.Synthetics.Api</NewRelicSyntheticsApiPackageName>
    <NewRelicSyntheticsApiPackageVersion>PACKAGE_VERSION</NewRelicSyntheticsApiPackageVersion>
    <NewRelicSyntheticsApiPackagePath>..\packages\$(NewRelicSyntheticsApiPackageName).$(NewRelicSyntheticsApiPackageVersion)\build\$(NewRelicSyntheticsApiPackageName).targets</NewRelicSyntheticsApiPackagePath>
    <SyntheticsApiKey>YOUR-ADMIN-API-KEY</SyntheticsApiKey>
</PropertyGroup>
 <Import Project="$(NewRelicSyntheticsApiPackagePath)" Condition="Exists('$(NewRelicSyntheticsApiPackagePath)')" />

 <Target Name="DisableSyntheticsMonitors" BeforeTargets="BeforeBuild">
    <MSBuild Projects="$(MSBuildProjectFile)" Targets="StartStopMonitors" Properties="SyntheticsApiKey=$(SyntheticsApiKey);EnableMonitors=False"/>
 </Target>
 <Target Name="EnableSyntheticsMonitors" BeforeTargets="BeforeBuild">
     <MSBuild Projects="$(MSBuildProjectFile)" Targets="StartStopMonitors" Properties="SyntheticsApiKey=$(SyntheticsApiKey);EnableMonitors=True"/>
 </Target>
```

Or invoke MsBuild task in your csproj directly:

```xml
<PropertyGroup>
    <NewRelicSyntheticsApiPackageName>NewRelic.Synthetics.Api</NewRelicSyntheticsApiPackageName>
    <NewRelicSyntheticsApiPackageVersion>PACKAGE_VERSION</NewRelicSyntheticsApiPackageVersion>
    <NewRelicSyntheticsApiTaskAssembly>..\packages\$(NewRelicSyntheticsApiPackageName).$(NewRelicSyntheticsApiPackageVersion)\tools\$(NewRelicSyntheticsApiPackageName).dll</NewRelicSyntheticsApiTaskAssembly>
    <SyntheticsApiKey>YOUR-ADMIN-API-KEY</SyntheticsApiKey>
</PropertyGroup>

<UsingTask TaskName="UpdateMonitorStatus" AssemblyFile="$(NewRelicSyntheticsApiTaskAssembly)"/>

 <Target Name="DisableSyntheticsMonitors" BeforeTargets="BeforeBuild">
   <UpdateMonitorStatus SyntheticsApiKey="$(SyntheticsApiKey)" EnableMonitors="False"/>
 </Target>
 <Target Name="EnableSyntheticsMonitors" BeforeTargets="BeforeBuild">
    <UpdateMonitorStatus SyntheticsApiKey="$(SyntheticsApiKey)" EnableMonitors="True"/>
 </Target>

```

Or use a Teamcity Metarunner from https://github.com/akuryan/Teamcity.Metarunners (when it will be published)

# NewRelic.Synthetics.Api.Tests
Tests is dumb - add your api key to app.config and observe, that api requests to real new relic endpoints are working

`src/NewRelic/NewRelic.Synthetics.Api.Tests/App.config` is ignored by command `git update-index --skip-worktree src/NewRelic/NewRelic.Synthetics.Api.Tests/App.config`, so, all changes there will be untracked.
If you feel urgent requirement to add something there - this ignore can be reverted by command `git update-index --no-skip-worktree src/NewRelic/NewRelic.Synthetics.Api.Tests/App.config`

You can view all skips by command `git ls-files -v . | grep ^S`
