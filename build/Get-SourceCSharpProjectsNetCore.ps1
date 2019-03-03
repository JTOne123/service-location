# requires -Version 5.1

<#
  .SYNOPSIS
    Gets each .Net Core C# project in the source directory
  .EXAMPLE
    & Get-SourceCSharpProjectsNetCore.ps1
  .INPUTS
    (None)
  .OUTPUTS
    [System.String[]]
#>
[CmdletBinding()]
param()
begin
{
  Set-StrictMode -Version Latest

  if ($null -eq (Get-Module -Name 'mwp.utilities'))
  {
    $ConfirmPreference = "High" #([High], Medium, Low, None)
    $DebugPreference = "Continue" #([SilentlyContinue], Continue, Inquire, Stop)
    $ErrorActionPreference = "Continue" #(SilentlyContinue, [Continue], Suspend <!--NOT ALLOWED -->, Inquire, Stop)
    $InformationPreference = "Continue" #(SilentlyContinue, Continue, Inquire, Stop)
    $VerbosePreference = "Continue" #([SilentlyContinue], Continue, Inquire, Stop)
    $WarningPreference = "Inquire" #(SilentlyContinue, [Continue], Inquire, Stop)
  }
  else
  {
    Use-CallerPreference -Cmdlet $PSCmdlet -SessionState $ExecutionContext.SessionState
  }

  $thisScriptDirectory = Split-Path $script:MyInvocation.MyCommand.Path
  $getSourceScript = Join-Path -Path $thisScriptDirectory -ChildPath 'Get-SourceCSharpProjects.ps1'
}
process
{
  $sources = & $getSourceScript
  foreach ($source in $sources)
  {
    if ($source.ContainsKey('TargetFramework'))
    {
      $value = $source['TargetFramework']
      if ($value.StartsWith('netcore'))
      {
        Write-Output $source['Project']
      }
      else
      {
        Write-Debug "$source ''TargetFramework'' value does not start with ''netcore''"
      }
    }
    else
    {
      Write-Debug "$source does not have a ''TargetFramework'' key"
    }
  }
}
