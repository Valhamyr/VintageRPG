# VintageRPG

This repository hosts a Vintage Story mod that extends the [XSkills](https://mods.vintagestory.at/xskills) system.
The initial goal is to add a **Flight** ability to the existing XSkills `survival` skill. When a player
unlocks this ability they gain access to the creative flight controls from the base game without any
additional requirements.

## Interaction with XSkills and XLib

The mod does not modify the XSkills or XLib mods themselves. Instead it interacts with their APIs:

* XLib exposes utility types that XSkills uses for skills and abilities. By referencing the XLib API we can
  implement new ability classes that follow the same interfaces used by XSkills.
* XSkills provides an API (`IXSkillsAPI`) that allows third-party mods to register new abilities at runtime.
  During `Start` we obtain this API from the mod loader and register our `FlightAbility` class.
* The ability is injected into the `survival` skill via a JSON patch (`mods/vintagerpg/assets/vintagerpg/patches/xskills/survival-flight.json`).
  This patch appends a new ability definition with code `vintagerpg:flight` and links it to our C# class.

Because the interaction happens through public APIs and JSON patches, XSkills and XLib remain untouched
and can still be updated independently.

## Mod structure

The mod files live under `mods/vintagerpg`:

```
mods/vintagerpg/modinfo.json                  # Mod metadata and dependencies
mods/vintagerpg/src/FlightAbility.cs          # C# ability implementation and registration
mods/vintagerpg/assets/vintagerpg/patches/... # JSON patch adding the ability to XSkills
mods/vintagerpg/assets/vintagerpg/lang/...    # Localized ability name/description
mods/vintagerpg/libs/                         # Place XLib and XSkills DLLs here (not tracked)
```

## Building

From the `mods/vintagerpg` directory compile the project against the Vintage Story game assemblies.
Download the XSkills and XLib DLLs separately and place them under `libs/` before building.
Place the resulting DLL and the `assets` folder into a `.zip` to distribute the mod.
