$os = "win";
$platforms = "AnyCPU", "x64", "x86";
$configurations = "Release";
$csproj = "$PSScriptRoot\Source\Annex\Annex.csproj";
$unitTestsPath = "$PSScriptRoot\Source\Tests";
$unitTestsCsProj = "$unitTestsPath\Tests.csproj";
$unitTestsBin = "$unitTestsPath\bin";
$nuspec = "$PSScriptRoot\nuspec.nuspec";
$err = $false;
$test = $true;
$build = $true;

$xml = [Xml] (Get-Content $csproj);
$framework = $xml.Project.PropertyGroup[0].TargetFramework;
Write-Host "Found framework as: $framework";
Write-Host "";

foreach ($platform in $platforms) {
    foreach ($configuration in $configurations) {
        $buildConfiguration = "$os $framework $platform-$configuration";
        Write-Host $buildConfiguration;

        if ($test) {
            # Build tests
            Write-Host "Building tests...";
            $res = Invoke-MsBuild $unitTestsCsProj -MsBuildParameters "/target:Clean;Build /property:Configuration=$configuration;Platform=$platform;BuildInParallel=true /verbosity:Detailed /maxcpucount";
            if ($res.BuildSucceeded -eq $true)
            {
                Write-Host ("Build completed successfully in {0:N1} seconds." -f $res.BuildDuration.TotalSeconds)
            } else {
                Write-Host ("Build failed in {0:N1} seconds." -f $res.BuildDuration.TotalSeconds);
                $err = $true;
            }
            Write-Host $res.Message;

            # Validation
            Write-Host "Validating tests...";
            $testPlatform = $platform + "\";
            if ($platform -eq "AnyCPU") {
                $testPlatform = "";
            }
            $testsPath = "$unitTestsBin\$testPlatform$configuration\$framework";
            $tests = "$testsPath\Tests.dll";
            $results = "$PSScriptRoot\TestResults\output.xml";
            
            if ([System.IO.File]::Exists($results) -eq $true) {
                [System.IO.File]::Delete($results);
            }

            & dotnet test --logger "trx;LogFileName=output.xml" $tests;

            $xml = [xml] (Get-Content $results);
            $summary = $xml.TestRun.ResultSummary;
            if ($summary.Counters.failed -ne "0") {
                $err = $true;
            }
            Remove-Item "$PSScriptRoot\TestResults\" -Recurse;
        }

        if ($build) {
            # Building Annex
            Write-Host "Building Annex...";
            
            $res = Invoke-MsBuild $csproj -MsBuildParameters "/target:Clean;Build /property:Configuration=$configuration;Platform=$platform;BuildInParallel=true /verbosity:Detailed /maxcpucount";
            if ($res.BuildSucceeded -eq $true)
            {
                Write-Host ("Build completed successfully in {0:N1} seconds." -f $res.BuildDuration.TotalSeconds)
            } else {
                Write-Host ("Build failed in {0:N1} seconds." -f $res.BuildDuration.TotalSeconds);
                $err = $true;
            }
            $message = $res.Message;
            Write-Host "Message: $message";
        }
        Write-Host "";
    }
}

# NOTE: Currently non-functional.
# # Create targets file
# $targetsSource = Get-Content "$PSScriptRoot\template.targets";
# $xml = [xml] (Get-Content "$PSScriptRoot\nuspec.nuspec");
# $annexVersion = $xml.package.metadata.version;
# $annexId = $xml.package.metadata.id;
# $targets = $targetsSource.Replace("__version__", $annexVersion);
# $targets = $targets.Replace("__app_id__", $annexId);
# $targets = $targets.Replace("__framework__", $framework);
# Set-Content -path "$annexId.targets" -Value $targets;

if ($err -eq $true) {
    Write-Host "Errors found. Canceling pack.";
} else {
    Write-Host "Packing...";
    nuget pack $nuspec;
}

Write-Host -NoNewLine 'Press any key to continue...';
$null = $Host.UI.RawUI.ReadKey('NoEcho,IncludeKeyDown');