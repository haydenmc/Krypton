function saveundofile(%client)
{
if (!isObject(%client)) //Client doesn't exist?
return;

%client.undodelpieces = ""; //Reset delete file

//echo("DEBUG: Saving undo file for " @ %client.nameBase);
//Create the undo save file
%filepath = "playerprefs/undo/" @ %client.guid @ ".cs";
new FileObject("UndoFile"); //create file object (player's save file)
UndoFile.openForWrite(%filepath); //open it up, and create it if it isn't there
UndoFile.writeLine("// KRYPTON MOD UNDO FILE -- Last pieces modified by this user that can be undone.");
UndoFile.writeLine("// Saved by \"" @ getField(%client.nameBase,0) @ "\"");
UndoFile.writeLine("");

   %count = getFieldCount( %client.undopieces );

   for ( %i = 0; %i < %count; %i++ )
   {
	%piece = getField( %client.undopieces, %i );
//	echo("DEBUG: Saving a piece..." @ %piece.getDatablock().getName());
	if (SavePiece(%piece)) {
	new FileObject("UndoTemp"); //Open the temporary save file..
	UndoTemp.openForRead("piecetemp.txt"); //open it up, and create it if it isn't there
		while (!UndoTemp.isEOF()) {
			%currentline = UndoTemp.readLine();
			UndoFile.writeLine(%currentline); //Read and copy to player save.
		}
	UndoTemp.close(); //close the file
	UndoTemp.delete(); //delete the object (not the file)

	addDeletePiece(%client,%piece);

	} else {
//		echo("DEBUG: Piece rejected.");
	}
   }

UndoFile.close(); //Close file
UndoFile.delete(); //Delete object.


%client.undopieces = ""; //Reset client's undo piece list.
}

function addUndoPiece(%client,%obj)
{
%count = getFieldCount( %client.undopieces );

   if ( %count == 0 )
      %client.undopieces = %obj;
   else
      %client.undopieces = %client.undopieces TAB %obj;

}

function addDeletePiece(%client,%obj)
{
%count = getFieldCount( %client.undodelpieces );

   if ( %count == 0 )
      %client.undodelpieces = %obj;
   else
      %client.undodelpieces = %client.undodelpieces TAB %obj;

}

function loadUndoFile(%client)
{
%filepath = "playerprefs/undo/" @ %client.guid @ ".cs";
exec(%filepath);
schedule(10000,0,delDupPieces,0,0,true);
schedule(10000,0,clientPowerCheck,%client);


%count = getFieldCount( %client.undodelpieces );
for ( %i = 0; %i < %count; %i++ )
   {
	%piece = getField( %client.undodelpieces, %i );
	if (isObject(%piece))
	%piece.getDataBlock().disassemble(%client.player,%piece);
   }
%client.undodelpieces = "";
//Clear the undo file
new FileObject("UndoDelCheck"); //create file object
UndoDelCheck.openForWrite(%filepath); //open it up, and create it if it isn't there
UndoDelCheck.close(); //Close file
UndoDelCheck.delete(); //Delete object.
}