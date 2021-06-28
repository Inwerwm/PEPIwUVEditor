if (-Not(Test-Path ../IwUVEditor/Licenses/)) {
    New-Item ../IwUVEditor/Licenses/ -ItemType Directory
}

Copy-Item ../../../LICENSE ../IwUVEditor/Licenses/IwUVEditor
Copy-Item ../../../ThirdPartyLicenses/* ../IwUVEditor/Licenses/

Get-ChildItem * -Include *.dll -Exclude PEPlugin*, SlimDX* | Copy-Item -Destination ../IwUVEditor/

Compress-Archive -Path ../IwUVEditor/ -DestinationPath ../IwUVEditor.zip -Force