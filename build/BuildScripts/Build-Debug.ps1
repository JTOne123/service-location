# requires -Version 5.1

<#
  .SYNOPSIS
    Builds the solution as debug, and all of its dependencies.
  .EXAMPLE
    Build-Debug.ps1
  .EXAMPLE
    Build-Debug.ps1 -SolutionFileName 'HelloWorld.sln'
  .INPUTS
    (None)
  .OUTPUTS
    (None)
#>
[CmdletBinding()]
param
(
  [Parameter(Position = 0,HelpMessage = 'The solution file to use (needed when more than one solution file exists).')]
  [System.String]$SolutionFileName
)
begin
{
  Set-StrictMode -Version Latest
  $started = [datetime]::UtcNow
  $thisScriptDirectory = Split-Path $script:MyInvocation.MyCommand.Path

  if ($null -eq (Get-Module -Name 'CSharpBuild'))
  {
    Import-Module -Name (Join-Path -Path $thisScriptDirectory -ChildPath '../CSharpBuild')
  }
  Use-CallerPreference -Cmdlet $PSCmdlet -SessionState $ExecutionContext.SessionState
  $buildCleanScript = Join-Path -Path $thisScriptDirectory -ChildPath 'Build-Clean.ps1'
}
process
{
  Set-BuildVariable -SolutionFileName $SolutionFileName
  try
  {
    & $buildCleanScript -SolutionFileName $SolutionFileName
    Write-Debug "Build-Debug.ps1 $buildSetVarInvocationCount"

    if ($null -eq $buildSolution)
    {
      Write-Error 'No Visual Studio solution found.'
      return 1
    }

    if ($buildSolution -is [array])
    {
      Write-Error 'Multiple Visual Studio solutions found; this script expects a single solution file.'
      return 2
    }

    # Use this syntax if a .Net Framework is added (dotnet build fails on .Net Framework projects with NuGet packages)
    # Verbosity: q[uiet], m[inimal], n[ormal], d[etailed], and diag[nostic].
    # MSbuild.exe /property:Configuration=Release /verbosity:normal /restore /detailedsummary $buildSolution
    dotnet build $buildSolution -c debug
  }
  finally
  {
    Clear-BuildVariable
  }
}
end
{
  $completed = [datetime]::UtcNow
  $elapsed = $completed - $started
  'Build-Debug:'
  "  Elapsed        := $elapsed"
  "  Started   (UTC):= $started"
  "  Completed (UTC):= $completed"
}
