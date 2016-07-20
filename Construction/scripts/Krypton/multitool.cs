//--------------------------------------
// Krypton Multi-Tool
// Coded by Sloik for Krypton Construct
// Puh-LEEZE don't just steal this -- It shall remain associated with Krypton.
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds
//--------------------------------------
datablock AudioProfile(MultiToolSwitchSound)
{
   filename    = "fx/powered/turret_light_reload.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(MultiToolFireSound)
{
   filename    = "fx/weapons/cg_hard3.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(MultiToolDryFireSound)
{
   filename    = "fx/weapons/plasma_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};


//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(MultiToolSecondImage)
{
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   offset = "0.0 0.0 0.1";

}; 

datablock ItemData(MultiTool)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = MultiToolImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a Krypton multi tool";
	computeCRC = true;
};

datablock ShapeBaseImageData(MultiToolImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy.dts";
   item = MultiTool;
   offset = "0 0 0";

   usesEnergy = true;
   minEnergy = 0;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "Ready";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = MultiToolSwitchSound;

   stateName[1] = "Ready";
   stateTransitionOnTriggerDown[1] = "Fire";

   stateName[2] = "Fire";
   stateTransitionOnTimeout[2] = "Reload";
   stateEjectShell[2]       = false;
   stateTimeoutValue[2] = 0.5;
   stateFire[2] = true;
   stateAllowImageChange[2] = true;
   stateSequence[2] = "Fire";
   stateScript[2] = "onFire";
   stateEmitterTime[2] = 0.1;
   stateSound[2] = MultiToolFireSound;

   stateName[3] = "Reload";
   stateTransitionOnTimeout[3] = "Ready";
   stateTimeoutValue[3] = 0.05;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Reload";
   
};

function MultiToolImage::onMount(%this,%obj,%slot)
{
   %obj.mountImage(MultiToolSecondImage, 4);
   Parent::onMount(%this,%obj,%slot);
   %obj.multitoolon = 1;
   %obj.multiobj = "";
   %obj.multighost = "";
   %obj.multivecref = "";
   %obj.multivec = "";

if (%obj.multimode $= "")
   %obj.multimode = 1;
if (%obj.multisubmode $= "")
%obj.multisubmode = 0;


displaywepstat(%obj.client,"Krypton Multi-Tool",$WeaponSetting["MultiTool",%obj.multimode],$WeaponSetting["MultiTool" @ %obj.multimode,%obj.multisubmode],"Property of Krypton Construction");
}

function MultiToolImage::onFire(%data,%obj,%slot)
{

	%pos = %obj.getMuzzlePoint($WeaponSlot);
	%vec = %obj.getMuzzleVector($WeaponSlot);
	%targetpos = VectorAdd(%pos, VectorScale(%vec, 200));

if (%obj.multiobj $= "") { //Don't have a piece selected?

	%scanTarg = containerRaycast(%pos, %targetpos, $TypeMasks::StaticShapeObjectType, %obj);
	%piece = getWord(%scanTarg, 0);
	%dataBlockName = %piece.getDataBlock().getName();
%obj.multivec1 = posFromRaycast(%scanTarg);
%obj.multivec2 = normalFromRaycast(%scanTarg);

	if (!isObject(%piece)) {
		messageclient(%obj.client,0,"\c2No object found.");
		return;
	}
	if (%piece.getOwner() != %obj.client) {
		messageclient(%obj.client,0,"\c2This piece is not yours.");
		return;
	}

%obj.multiobj = %piece;

%piece.startFade(1000,0,true);

%obj.multivecref = VectorScale(VectorNormalize(%obj.getMuzzleVector($WeaponSlot)),VectorLen(%obj.getMuzzleVector($WeaponSlot))); //Store the player vector for reference.

messageclient(%obj.client,0,"\c2Object " @ %dataBlockName @ " selected.");

%obj.duplicate = createduplicate(%piece);
multiloop(%obj.duplicate,%obj);
cloakingloop(%obj.duplicate,true);

} else { //We've already got a piece selected.

if (%obj.multimode == 0) { // Mode 1 -- Moving Objects.
%surface = Deployables::searchView(%obj,
					400,
					($TypeMasks::TerrainObjectType |
					$TypeMasks::InteriorObjectType |
					$TypeMasks::StaticShapeObjectType));
		%surfacePt  = posFromRaycast(%surface);
		%surfaceNrm = normalFromRaycast(%surface);

//%playerVector = vectorNormalize(-1 * getWord(%obj.getEyeVector(),1) SPC getWord(%obj.getEyeVector(),0) SPC "0");
//%surfaceNrm2 = %playerVector;
//%surfaceNrm2 = vectorNormalize(vectorCross(%surfaceNrm,"0 0 1"));
		%mask = invFace(%surfaceNrm);
                %narrower = vectorMultiply(%mask,%surface.getRealSize());
		%subject = vectorNormalize(topVec(%narrower));
                %surfaceNrm2 = realVec(%surface,%subject);
%rot = fullRot(%surfaceNrm,%surfaceNrm2);

%obj.multiobj.setTransform(%surfacePt SPC %rot);
%obj.scale = vectorMultiply(%obj.scale,1/4 SPC 1/3 SPC 2);
messageclient(%obj.client,0,"\c2Object moved.");

} else if (%obj.multimode == 1) { //Mode 2 -- Axis Move

%obj.multivec = VectorScale(VectorNormalize(%obj.getMuzzleVector($WeaponSlot)),VectorLen(%obj.getMuzzleVector($WeaponSlot))); //Store the player vector for reference.

%xval = mAbs(getWord(%obj.multivecref, 0) - getWord(%obj.multivec, 0));
%yval = mAbs(getWord(%obj.multivecref, 1) - getWord(%obj.multivec, 1));
%zval = mAbs(getWord(%obj.multivecref, 2) - getWord(%obj.multivec, 2));
%globalval = ((%xval+%yval+%zval)/3)*vectorDist(%obj.multiobj.getPosition(),%obj.getPosition());

if (%obj.multisubmode)
%globalval *= -1;

//Save to undo file...
addUndoPiece(%obj.client,%obj.multiobj);
saveundofile(%obj.client);

moveobject(%obj.multiobj,%obj.multivec1,%obj.multivec2,%obj,%globalval*2);
messageclient(%obj.client,0,"\c2Object moved.");


} else if (%obj.multimode == 2) { // Mode 3 -- Scaling Objects.

%obj.multivec = VectorScale(VectorNormalize(%obj.getMuzzleVector($WeaponSlot)),VectorLen(%obj.getMuzzleVector($WeaponSlot))); //Store the player vector for reference.

%xval = mAbs(getWord(%obj.multivecref, 0) - getWord(%obj.multivec, 0));
%yval = mAbs(getWord(%obj.multivecref, 1) - getWord(%obj.multivec, 1));
%zval = mAbs(getWord(%obj.multivecref, 2) - getWord(%obj.multivec, 2));
%globalval = ((%xval+%yval+%zval)/3)*vectorDist(%obj.multiobj.getPosition(),%obj.getPosition());


%vec = VectorNormalize(%obj.multivec2);
%vec = vectorScale(%vec,%globalval);

%vec = mAbs(getword(%vec,0)) SPC mAbs(getword(%vec,1)) SPC mAbs(getword(%vec,2));

if (%obj.multisubmode)
%vec=VectorScale(%vec,-1);    //invert

%size=Vectoradd(%obj.multiobj.getScale(),%vec);

//Save to undo file...
addUndoPiece(%obj.client,%obj.multiobj);
saveundofile(%obj.client);


%obj.multiobj.setScale(mAbs(getword(%size,0)) SPC mAbs(getword(%size,1)) SPC mAbs(getword(%size,2)));

messageclient(%obj.client,0,"\c2Object scaled.");

} else {
messageclient(%obj.client,0,"\c2Mode error. Mode set to " @ %obj.multimode);
}

%obj.multiobj.startFade(1000,0,false);
%obj.duplicate.delete();
%obj.multiobj = "";
%obj.multivecref = "";
%obj.multivec = "";


}
}

function moveobject(%obj,%vec1,%vec2,%plyr,%scale) { //Special thanks to Quantium for this.
   if (!isObject(%obj)) {
error("No object selected to move.");
      return;
}
   %pos = %obj.getPosition();
   %rot = %obj.getRotation();
   %vec = realVec(%obj,%vec2);
   if (%obj.getType() & $TypeMasks::ForceFieldObjectType)
      return;
   %dir = 1;
   %vec = vectorScale(%vec,%scale * %dir);
   %newpos = vectorAdd(%pos,%vec);
   %obj.setPosition(%newpos);
   checkAfterRot(%obj);

	%dataBlockName = %obj.getDataBlock().getName();
echo("Moved the " @ %dataBlockName);
}

function relmoveobject(%obj,%vec1,%vec2,%plyr,%scale,%movobj) { //Special thanks to Quantium for this.
   if (!isObject(%obj)) {
error("No object selected to move.");
      return;
}
   %pos = %obj.getPosition();
   %rot = %obj.getRotation();
   %vec = realVec(%obj,%vec2);
   if (%obj.getType() & $TypeMasks::ForceFieldObjectType)
      return;
   %dir = 1;
   %vec = vectorScale(%vec,%scale * %dir);
   %newpos = vectorAdd(%pos,%vec);
   %movobj.setPosition(%newpos);
   checkAfterRot(%movobj);

}

function MultiToolImage::onUnmount(%this, %obj, %slot)
{
%obj.unmountImage(4);
Parent::onUnmount(%this,%obj,%slot);
if (isObject(%obj.duplicate)) {
%obj.duplicate.delete();
}
if (isObject(%obj.multiobj)) {
%obj.multiobj.startFade(1000,0,false);
}
   %obj.multitoolon = 0;
   %obj.multiobj = "";
   %obj.multighost = "";
   %obj.multivecref = "";
   %obj.multivec = "";
}


function createduplicate(%obj) {
	new FileObject("MultiFile"); //create file object (player's save file)
	MultiFile.openForWrite("multifile.cs"); //open it up, and create it if it isn't there
	MultiFile.writeLine("// Krypton Multi-tool temporary save file.");

		%obj.save("multitemp.txt"); //Save the piece to a temporary file...

		new FileObject("MultiTemp"); //Open the temporary save file..
		MultiTemp.openForRead("MultiTemp.txt"); //open it up, and create it if it isn't there

		while (!MultiTemp.isEOF()) {
			%currentline = MultiTemp.readLine();
			if (getSubStr( %currentline, 0, 3 ) $= "new")
			%currentline = "%thedup = " @ %currentline; //Make sure we can run stuff on this deployable later.
			MultiFile.writeLine(%currentline); //Read and copy to player save.
		}
		MultiTemp.close(); //close the file
		MultiTemp.delete(); //delete the object (not the file)

	MultiFile.close(); //close the file
	MultiFile.delete(); //delete the object (not the file)
compile("multifile.cs");
exec("multifile.cs");

return %thedup;
}

function multiloop(%obj,%plyr) {
if (!isObject(%obj))
return;

if (!isObject(%plyr.multiobj))
return;

if (%plyr.multimode == 1) { //Mode 2 -- Axis Move
%plyr.multivec = VectorScale(VectorNormalize(%plyr.getMuzzleVector($WeaponSlot)),VectorLen(%plyr.getMuzzleVector($WeaponSlot))); //Store the player vector for reference.

%xval = mAbs(getWord(%plyr.multivecref, 0) - getWord(%plyr.multivec, 0));
%yval = mAbs(getWord(%plyr.multivecref, 1) - getWord(%plyr.multivec, 1));
%zval = mAbs(getWord(%plyr.multivecref, 2) - getWord(%plyr.multivec, 2));
%globalval = ((%xval+%yval+%zval)/3)*vectorDist(%plyr.multiobj.getPosition(),%plyr.getPosition());

if (%plyr.multisubmode)
%globalval *= -1;


%obj.setPosition(%plyr.multiobj.getPosition());
relmoveobject(%plyr.multiobj,%plyr.multivec1,%plyr.multivec2,%plyr,%globalval*2,%obj);
}


if (%plyr.multimode == 2) { //Mode 3 -- Axis Scale
%plyr.multivec = VectorScale(VectorNormalize(%plyr.getMuzzleVector($WeaponSlot)),VectorLen(%plyr.getMuzzleVector($WeaponSlot))); //Store the player vector for reference.

%xval = mAbs(getWord(%plyr.multivecref, 0) - getWord(%plyr.multivec, 0));
%yval = mAbs(getWord(%plyr.multivecref, 1) - getWord(%plyr.multivec, 1));
%zval = mAbs(getWord(%plyr.multivecref, 2) - getWord(%plyr.multivec, 2));
%globalval = ((%xval+%yval+%zval)/3)*vectorDist(%plyr.multiobj.getPosition(),%plyr.getPosition());

//%vec = realVec(%plyr.multiobj,%plyr.multivec2);
%vec = VectorNormalize(%plyr.multivec2);
%vec = vectorScale(%vec,%globalval);

%vec = mAbs(getword(%vec,0)) SPC mAbs(getword(%vec,1)) SPC mAbs(getword(%vec,2));

if (%plyr.multisubmode)
%vec=VectorScale(%vec,-1);    //invert

//messageclient(%plyr.client,0,%vec);
%size=Vectoradd(%plyr.multiobj.getScale(),%vec);
%obj.setScale(mAbs(getword(%size,0)) SPC mAbs(getword(%size,1)) SPC mAbs(getword(%size,2)));


}

schedule(128,0,multiloop,%obj,%plyr);
}

function cloakingloop(%obj,%stat) {
if (!isObject(%obj))
return;

%obj.setcloaked(%stat);

schedule(500,0,cloakingloop,%obj,!%stat);
}