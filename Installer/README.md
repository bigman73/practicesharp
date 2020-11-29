# Practice# installater build

## Steps
1. Get `Wix Installer` zip
http://wixtoolset.org/

As of Nov 2020, Wix 3.11.2 is available
https://github.com/wixtoolset/wix3/releases/tag/wix3112rtm

2. Extract the zip into the Installer folder
There should be a folder under the Installer folder named wix311-binaries

```
practicesharp
  Installer
    wix311-binaries
```

3. Rebuild Practice# solution in `Release` mode

4. Run the batch file `PracticeShare\Installer\CompilePracticeSharpWix.bat`

Expected Result: An MSI file `PracticeSharp.msi` is generated under the `\MSI` folder 