# BPSum
Space Engineers blueprint component summary calculator with mod support

### Requires .NET Core 2.0 or newer.

## Usage
```
dotnet BPSum.dll [options] <blueprint file>
```
The program requires you to specify the path to a blueprint file.

Local blueprints can be found in subdirectories inside `%appdata%\SpaceEngineers\Blueprints\local`.

The required file is usually `bp.sbc`.

You can view the full usage with `dotnet BPSum.dll -h`.

### Example
```
dotnet BPSum.dll "C:\Users\?\AppData\Roaming\SpaceEngineers\Blueprints\local\Small Welder\bp.sbc"
```

## Mods
If a blueprint contains unknown blocks, a message will be printed with the missing blocks.
If the missing blocks are from a mod, you can use the option `-m <modid>` to load the mod.
```
dotnet BPSum.dll -m 1732471843 "C:\Users\?\AppData\Roaming\SpaceEngineers\Blueprints\local\Modded Grinder\bp.sbc"
```
You can determine the mod id by going to the mod's Steam Workshop page. The end of the URL has a number, which is the mod id.

Alternatively, you can also specify the path to the mod directory.


If you have a lot of mods, you can load all mods from a world with the option `-w <world path>`.
You need to specify the path of the Sandbox.sbc file.
```
dotnet BPSum.dll -w "C:\Users\?\AppData\Roaming\SpaceEngineers\Saves\?\Modded Star System\Sandbox.sbc" "C:\Users\?\AppData\Roaming\SpaceEngineers\Blueprints\local\Heavily Modded Ship\bp.sbc"
```

World files can be found in `%appdata%\SpaceEngineers\Saves`.
## Sample Output
```
dotnet BPSum.dll "C:\Users\?\AppData\Roaming\SpaceEngineers\Blueprints\local\Small Welder\bp.sbc"
Loading data files...
Calculating...
Component summary:
  Bulletproof Glass: 32
  Computer: 60
  Construction Comp.: 440
  Display: 6
  Interior Plate: 120
  Large Steel Tube: 37
  Metal Grid: 73
  Motor: 763
  Radio-comm Comp.: 4
  Reactor Comp.: 95
  Small Steel Tube: 69
  Steel Plate: 410
```
