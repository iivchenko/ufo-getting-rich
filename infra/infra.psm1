$gVersion = "3.5.1"

$url = "https://downloads.tuxfamily.org/godotengine/$gVersion/mono/Godot_v$gVersion-stable_mono_win64.zip"
$out = ".tools"

function Download-Godot {

    New-Item -ItemType Directory -Force -Path $out
    Invoke-WebRequest -Uri $url -OutFile "$out/godot.zip"
    expand-archive -path "$out/godot.zip" -DestinationPath "$out/"
    Remove-Item "$out/godot.zip"
}

function Run-Editor {
    
    param()

    if (-not (Test-Path -Path "$out/Godot_v$gVersion-stable_mono_win64/"))
    {
        Download-Godot
    }

    & "$out/Godot_v$gVersion-stable_mono_win64/Godot_v$gVersion-stable_mono_win64.exe" --editor --path ./src
}

Export-ModuleMember -Function * -Alias *