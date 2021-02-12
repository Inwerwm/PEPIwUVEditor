if (-Not(Test-Path ./Licenses/)) {
    New-Item ./Licenses/ -ItemType Directory
}

Copy-Item ../../../LICENSE Licenses/IwUVEditor
Copy-Item ../../../ThirdPartyLicenses/* Licenses/
