function KryptonClientSave(%cl,%name) {

if (%name $= "")
%name = "default";

echo("Krypton Save System -- Saving for GUID: " @ %cl.guid);

	%origCl = %cl;
	if (!isObject(%cl)) {
		if (isObject(nameToID(LocalClientConnection))) {
			%cl = nameToID(LocalClientConnection);
		}
		else {
			if ($CurrentClientId) {
				%cl = $CurrentClientId;
			}
		}
	}

//There has to be a better way to get all pieces than to have a huge radius....
		%rad = 99999999;

	%buildingCount = 0;

//Is the client alive? Save relative to their position.
	if (isObject(%cl.getControlObject())) {
		%pos = %cl.getControlObject().getPosition();
	}

//No? Just use the center of the map.
	if (!%pos) {
		%pos = "0 0 0";
	}
%filepath = "Saves" @ "/" @ %cl.guid @ "/" @ %name @ ".cs";
	new FileObject("SaveFile"); //create file object (player's save file)
	SaveFile.openForWrite(%filepath); //open it up, and create it if it isn't there
	SaveFile.writeLine("// KRYPTON MOD SAVE FILE -- This probably isn't compatible with other mods without intense modification.");
	SaveFile.writeLine("// Saved by \"" @ getField(%cl.nameBase,0) @ "\"");
	SaveFile.writeLine("// Created in mission \"" @ $MissionName @ "\"");
	SaveFile.writeLine("// Krypton Advanced Save System");
	SaveFile.writeLine("");

	initContainerRadiusSearch(%pos,%rad,$TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::ItemObjectType);
	while((%obj = containerSearchNext()) != 0) {
		if (%obj.getowner() != %cl) //We only want THIS guy's pieces.
		continue;

//		echo("DEBUG: Saving " @ %obj.getDatablock().getName());
		if (SavePiece(%obj)) {

		new FileObject("SaveTemp2"); //Open the temporary save file..
		SaveTemp2.openForRead("piecetemp.txt"); //open it up, and create it if it isn't there

		while (!SaveTemp2.isEOF()) {
			%currentline = SaveTemp2.readLine();
			SaveFile.writeLine(%currentline); //Read and copy to player save.
		}
		SaveTemp2.close(); //close the file
		SaveTemp2.delete(); //delete the object (not the file)

		}

	}

	SaveFile.close(); //close the file
	SaveFile.delete(); //delete the object (not the file)

compile(%filepath);
	return %file;
}

function SavePiece(%obj) //Code for saving a specific piece to a temporary location.
{
	new FileObject("PieceTemp"); //create file object (player's save file)
	PieceTemp.openForWrite("piecetemp.txt"); //open it up, and create it if it isn't there

		%datablockname = %obj.getDatablock().getName();
		if (%datablockname $= "TelePadBeam" || %datablockname $= "DeployedLTarget" || %datablockname $= "DeployedOOSphere" || %datablockname $= "DeployedPeaceSphere" || %datablockname $= "DeployedCombatSphere" || %datablockname $= "DeployableVehiclePadBottom" || %datablockname $= "DeployableVehiclePad" || %datablockname $= "DeployableVehiclePad2" || %datablockname $= "EnergizerLight" || %datablockname $= "DeployedEnergizer" || %datablockname $= "Mpm_Beacon_Ghost" || %datablockname $= "Mpm_Beacon") { //The ignore list.
			PieceTemp.close(); //close the file
			PieceTemp.delete(); //delete the object (not the file)
		return false; //We don't save these.
		}

		%obj.nameTag = ""; //This variable causes problems... so we just remove it.
		%obj.save("savetemp.txt"); //Save the piece to a temporary file...

		new FileObject("SaveTemp"); //Open the temporary save file..
		SaveTemp.openForRead("savetemp.txt"); //open it up, and create it if it isn't there

		while (!SaveTemp.isEOF()) {
			%currentline = SaveTemp.readLine();
			if (getSubStr( %currentline, 0, 3 ) $= "new")
			%currentline = "%piece = " @ %currentline; //Make sure we can run stuff on this deployable later.
			if (getSubStr( %currentline, 0, 2 ) $= "//")
			%currentline = "//-----------------"; //Change comments into lines...
			if (strstr(strlwr(%currentline),"tagged") >= 0)
			%currentline = ""; //Remove these weird "tagged" variables that break things.
			PieceTemp.writeLine(%currentline); //Read and copy to player save.
		}
		PieceTemp.writeLine("%piece.onLoad();"); //Read and copy to player save.
		SaveTemp.close(); //close the file
		SaveTemp.delete(); //delete the object (not the file)

//TIME FOR SOME EXCEPTIONS!
	if (%datablockname $= "DeployableVehicleStation") {
	//Save the pad
		%obj.pad.nameTag = ""; //This variable causes problems... so we just remove it.
		%obj.pad.save("savetemp.txt"); //Save the piece to a temporary file...
		new FileObject("SaveTemp"); //Open the temporary save file..
		SaveTemp.openForRead("savetemp.txt"); //open it up, and create it if it isn't there

		while (!SaveTemp.isEOF()) {
			%currentline = SaveTemp.readLine();
			if (getSubStr( %currentline, 0, 3 ) $= "new")
			%currentline = "%piece.pad = " @ %currentline; //Make sure we can run stuff on this deployable later.
			if (getSubStr( %currentline, 0, 2 ) $= "//")
			%currentline = "//-----------------"; //Change comments into lines...
			PieceTemp.writeLine(%currentline); //Read and copy to player save.
		}
		PieceTemp.writeLine("%piece.pad.onLoad();"); //Read and copy to player save.
		PieceTemp.writeLine("%piece.pad.station = %piece;"); //Associate the pad with the station.
		PieceTemp.writeLine("%piece.trigger.mainObj = %piece.pad;"); //Associate the pad with the station.
		PieceTemp.writeLine("%piece.trigger.disableObj = %piece;"); //Associate the pad with the station.


		SaveTemp.close(); //close the file
		SaveTemp.delete(); //delete the object (not the file)

	//Save the main pad... "back"
		%obj.pad.back.nameTag = ""; //This variable causes problems... so we just remove it.
		%obj.pad.back.save("savetemp.txt"); //Save the piece to a temporary file...
		new FileObject("SaveTemp"); //Open the temporary save file..
		SaveTemp.openForRead("savetemp.txt"); //open it up, and create it if it isn't there

		while (!SaveTemp.isEOF()) {
			%currentline = SaveTemp.readLine();
			if (getSubStr( %currentline, 0, 3 ) $= "new")
			%currentline = "%piece.pad.back = " @ %currentline; //Make sure we can run stuff on this deployable later.
			if (getSubStr( %currentline, 0, 2 ) $= "//")
			%currentline = "//-----------------"; //Change comments into lines...
			PieceTemp.writeLine(%currentline); //Read and copy to player save.
		}
		PieceTemp.writeLine("%piece.pad.back.onLoad();"); //Read and copy to player save.
		PieceTemp.writeLine("%piece.pad.back.station = %piece;"); //Associate the pad with the station.
		PieceTemp.writeLine("adjustTrigger(%sv);");
		SaveTemp.close(); //close the file
		SaveTemp.delete(); //delete the object (not the file)
	}

//Target thing, too? Decoration packs should work with this...
	if (isobject(%obj.lTarget) && %datablockname !$= "DeployedEscapePod") {
	//Save the target pad
		%obj.lTarget.nameTag = ""; //This variable causes problems... so we just remove it.
		%obj.lTarget.save("savetemp.txt"); //Save the piece to a temporary file...
		new FileObject("SaveTemp"); //Open the temporary save file..
		SaveTemp.openForRead("savetemp.txt"); //open it up, and create it if it isn't there

		while (!SaveTemp.isEOF()) {
			%currentline = SaveTemp.readLine();
			if (getSubStr( %currentline, 0, 3 ) $= "new")
			%currentline = "%piece.lTarget = " @ %currentline; //Make sure we can run stuff on this deployable later.
			if (getSubStr( %currentline, 0, 2 ) $= "//")
			%currentline = "//-----------------"; //Change comments into lines...
			PieceTemp.writeLine(%currentline); //Read and copy to player save.
		}
		PieceTemp.writeLine("%piece.lTarget.onLoad();"); //Read and copy to player save.
		PieceTemp.writeLine("%piece.lTarget.lMain = %piece;"); //Associate the pad with the deployable.

		SaveTemp.close(); //close the file
		SaveTemp.delete(); //delete the object (not the file)


	}

	PieceTemp.close(); //close the file
	PieceTemp.delete(); //delete the object (not the file)
return true;
} //End function


function automagicBackup(%client)
{

%counter=deployables.getcount();
for (%n=0;%n<%counter;%n++) {
%obj = deployables.getObject(%n);
if (%obj.getOwner() == %client)
%piececount++;
}

if (%piececount > 1) {
KryptonClientSave(%client,"backup");
messageclient(%client,'MsgClient',"Automagic backup file saved.");
}

cancel(%client.automagicschedule);
%client.automagicschedule = schedule(3*60*1000,0,automagicBackup,%client);
}