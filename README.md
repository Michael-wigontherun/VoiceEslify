# VoiceEslify
Respitory for VoiceEslify intended to help turning skyrim mods with voice files into eslified plugins

Important Note: This only helps and fixes a specific bug. This will not fix anything other then the formIDs for voice files when eslified this will not fix any other type of eslifing problems.

Overview

When using xEdit to compact formIDs to flag as esl mods with voice files will not be changed to the new formIDs. This is a problem because Skyrim's engine stores the voice files using a the formID. Hense the purpose of this using xEdit's scripting function will out put all voice files and there formIDs before and after compacting for the esl flag then reading those files and changing names of the voice files to that of the new formIDs of attached to the voice forms.

While this is not a advanced tool it is important to know what to eslify and what not to eslify. Obscene911 made a great guide for eslifing here.
A shortened version of it is do not eslify plugins with .seq files, scripts that call hard coded formIDs from that plugin you want to eslify (See bottom for extra information), and facegen files. This how ever will help fix the problem with facegen renumbering.

Installation

Make sure all dependencies are installed
Drop my two folders into the root of SSEEdit folder ensuring "_1PreEslifyVoice.pas" and "_2PostEslifyVoice.pas" are inside the Edit Scripts folder for SSEEdit.
Now open "config.json" inside of VoiceEslify folder and change "xEditFolder" header's value to where ever your SSEEdit is.
    Example: "C:\\Modding\\SkyrimSE\\sseedit" ensuring to add the double backslash \.
Next change "SkyrimDataFolder" header's to the Skyrim Special Edition's data folder.
    Example: "C:\\Steam\\steamapps\\common\\Skyrim Special Edition\\Data"

Usage

If using MO2 you need to run this and SSEEdit through MO2.

1. First, Identify what mod you want to turn into an esl check to see if it has voice data it will be located under "sound\voice\{Whatever the plugin name is}\" as loose files or using the same path inside the BSA attached to the mod. If it does not have voice files attached to the plugin then this is nonaplicable to the problem with eslifying a plugin. But if you didn't know that read to step 4 because it is a useful thing to know when eslifying plugins.
2. Second, Identify if the mod ever has hard coded form calls in any of their scripts. If it does this plugin can not be eslify, or if you do not have source code and you do not know how to decompile Bethesda scripts skip the plugin aswell or See bottom for extra information.
3. Load SSEEdit, though MO2 if your using it, Load all plugins.
4. Right click on a plugin tree inside of xEdit and click apply script - "Find ESP plugins which could be turned into ESL.pas" and wait for it to finish and then scroll till you get to the plugin you wish to eslify and if it has any kind of warning DO NOT COMPACT AT ALL it is not a mod you can eslify.
This on "Can be turned into ESL by compacting FormIDs first, then adding ESL flag in TES4 header" means its potentially ok to eslify while "Warning: Plugin has new CELL(s) which won't work when turned into ESL and overridden by other mods due to the game bug" is not because it adds new cells. there is another comment from the script that is "Can be turned into ESL by adding ESL flag in TES4 header" this means all you need to do is add the esl flag to the header file.
5. Now we can start using this utilities functions, first locate the applicable plugin you want to eslify in the load order inside of SSEEdit. Right click apply script "_1PreEslifyVoice.pas". Once done move on.
6. Now compact that plugin using xEdit's feature, right click on the plugin "Compact FormIDs for ESL." Once done go to the plugins header double click on record flag and check ESL. Then save.
7. Now Right click on the plugin then apply script "_2PostEslifyVoice.pas". Once this script is done you can close xEdit.
8. Now run "VoiceEslify.exe", though MO2 if your using it, from this page and your done.

Extra Notes

If your using MO2 you can trick my utility into thinking the mod folder that contains the facegen data you need changed is the data folder by making that folders path to the value of "SkyrimDataFolder" in the config file.

If you do not have the source code of a script from a mod you can use the "PapyrusAssembler.exe" to decompile the script. You need to install the creation kit that comes from Bethesda net launcher. Located inside of Skyrim Special Edition\Papyrus Compiler. copy the script you want decompiled into the folder. Select and create a shortcut for "PapyrusAssembler.exe". Open the shortcuts properties. Inside the target box, type 
( "PapyrusAssembler.exe" "{Name of script without file extension}" -D ) no parenthesis and no squiggly bracket where the script name goes.

This may potentially work for Fallout 4 but I am not sure if its the same file pathing or system I do not know enough about Fallout 4 modding.

My other utilities

Skyrim New Game Plus esk Batch Builder : https://www.nexusmods.com/skyrimspecialedition/mods/44325

FaceGen Eslify : https://www.nexusmods.com/skyrimspecialedition/mods/46208


