//---------------------------------------------------------
// Deployable Force Field
//---------------------------------------------------------

// Translucencies
%noPassTrans = 1.0;
%teamPassTrans = 0.6;
%allPassTrans = 0.2;

%dimDiv = 4;

// RGB
%colourOn = 0.3;
%colourOff = 0.0;

datablock ForceFieldBareData(DeployedForceField) {
	className = "forcefield";
	fadeMS = 1000;
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "1.0 1.0 1.0";
	powerOffColor = "0.0 0.0 0.0";
	targetNameTag = 'Force Field';
	targetTypeTag = 'ForceField';
	texture[0] = "skins/forcef1";
	texture[1] = "skins/forcef2";
	texture[2] = "skins/forcef3";
	texture[3] = "skins/forcef4";
	texture[4] = "skins/forcef5";
	framesPerSec = 1; // 10
	numFrames = 5;
	scrollSpeed = 15;
	umapping = 0.01; // 1.0
	vmapping = 0.01; // 0.15
	deployedFrom = ForceFieldDeployable;
	needsPower = true;
};

// No pass
datablock ForceFieldBareData(DeployedForceField0) : DeployedForceField {
	baseTranslucency = %noPassTrans;
	powerOffTranslucency = %noPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = %colourOn SPC %colourOn SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField1) : DeployedForceField {
	baseTranslucency = %noPassTrans;
	powerOffTranslucency = %noPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = %colourOn SPC %colourOff SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField2) : DeployedForceField {
	baseTranslucency = %noPassTrans;
	powerOffTranslucency = %noPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = %colourOff SPC %colourOn SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField3) : DeployedForceField {
	baseTranslucency = %noPassTrans;
	powerOffTranslucency = %noPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = %colourOff SPC %colourOff SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField4) : DeployedForceField {
	baseTranslucency = %noPassTrans;
	powerOffTranslucency = %noPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = %colourOff SPC %colourOn SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField5) : DeployedForceField {
	baseTranslucency = %noPassTrans;
	powerOffTranslucency = %noPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = %colourOn SPC %colourOff SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField6) : DeployedForceField {
	baseTranslucency = %noPassTrans;
	powerOffTranslucency = %noPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = %colourOn SPC %colourOn SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

// Team pass
datablock ForceFieldBareData(DeployedForceField7) : DeployedForceField {
	baseTranslucency = %teamPassTrans;
	powerOffTranslucency = %teamPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = %colourOn SPC %colourOn SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField8) : DeployedForceField {
	baseTranslucency = %teamPassTrans;
	powerOffTranslucency = %teamPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = %colourOn SPC %colourOff SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField9) : DeployedForceField {
	baseTranslucency = %teamPassTrans;
	powerOffTranslucency = %teamPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = %colourOff SPC %colourOn SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField10) : DeployedForceField {
	baseTranslucency = %teamPassTrans;
	powerOffTranslucency = %teamPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = %colourOff SPC %colourOff SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField11) : DeployedForceField {
	baseTranslucency = %teamPassTrans;
	powerOffTranslucency = %teamPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = %colourOff SPC %colourOn SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField12) : DeployedForceField {
	baseTranslucency = %teamPassTrans;
	powerOffTranslucency = %teamPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = %colourOn SPC %colourOff SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField13) : DeployedForceField {
	baseTranslucency = %teamPassTrans;
	powerOffTranslucency = %teamPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = %colourOn SPC %colourOn SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

// All pass
datablock ForceFieldBareData(DeployedForceField14) : DeployedForceField {
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = %colourOn SPC %colourOn SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField15) : DeployedForceField {
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = %colourOn SPC %colourOff SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField16) : DeployedForceField {
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = %colourOff SPC %colourOn SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField17) : DeployedForceField {
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = %colourOff SPC %colourOff SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField18) : DeployedForceField {
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = %colourOff SPC %colourOn SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField19) : DeployedForceField {
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = %colourOn SPC %colourOff SPC %colourOn;
	powerOffColor = "0.0 0.0 0.0";
};

datablock ForceFieldBareData(DeployedForceField20) : DeployedForceField {
	baseTranslucency = %allPassTrans;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = %colourOn SPC %colourOn SPC %colourOff;
	powerOffColor = "0.0 0.0 0.0";
};

//ADDITIONAL XENON FORCE FIELDS

// pure glass
  datablock ForceFieldBareData(DeployedForceField21) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.9 0.9 0.8";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "skins/screenstatic5";
  framesPerSec = 1;
  numFrames = 1;
  scrollSpeed = 0.00;
  umapping = 1.9;
  vmapping = 1.9;
};

// picture tube
  datablock ForceFieldBareData(DeployedForceField22) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.5 0.5 0.5";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "skins/billboard_1";
  texture[1] = "skins/billboard_2";
  texture[2] = "skins/billboard_3";
  texture[3] = "skins/billboard_4";
  framesPerSec = 1;
  numFrames = 4;
  scrollSpeed = 0.0;
  umapping = 0.1;
  vmapping = 0.1;
};

// tartgeting field all pass
datablock ForceFieldBareData(DeployedForceField23) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = "0.9 0.0 0.0";
	powerOffColor = "0.0 0.0 0.0";
   texture[0] = "skins/hud_ret_bomber3";     // pack_rep05
  framesPerSec = 10;                    // plrec04
  numFrames = 1;                       // beampulse
  scrollSpeed = 0;
  umapping = 1;
  vmapping = 1;
};

// static wall no pass
datablock ForceFieldBareData(DeployedForceField24) : DeployedForceField {
	baseTranslucency = 0.8;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.5 0.5 0.5";
	powerOffColor = "0.0 0.0 0.0";
   texture[0] = "special/grainy";     // pack_rep05
  framesPerSec = 10;                    // plrec04
  numFrames = 1;                       // beampulse
  scrollSpeed = 4;
  umapping = 0.8;
  vmapping = 0.8;
};

// lightning wall no pass
datablock ForceFieldBareData(DeployedForceField25) : DeployedForceField {
	baseTranslucency = 1.0;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.5 0.5 0.7";
	powerOffColor = "0.0 0.0 0.0";
   texture[0] = "special/lightning2frame1";
   texture[1] = "special/lightning2frame2";
   texture[2] = "special/lightning2frame3";
 //  texture[3] = "special/lightning1frame1";
 //  texture[4] = "special/lightning1frame2";
 //  texture[5] = "special/lightning1frame3";
  framesPerSec = 25;                    // plrec04
  numFrames = 3;                       // beampulse
  scrollSpeed = 5;
  umapping = 0.6;
  vmapping = 0.6;
};

// fake cloak no pass
datablock ForceFieldBareData(DeployedForceField26) : DeployedForceField {
	baseTranslucency = 0.7;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.7 0.7 0.7";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "special/cloakTexture";     // pack_rep05
  framesPerSec = 10;                    // plrec04
  numFrames = 1;                       // beampulse
  scrollSpeed = 12;
  umapping = 0.6;
  vmapping = 0.6;
};

// lava wall all pass
datablock ForceFieldBareData(DeployedForceField27) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = "0.7 0.4 0.1";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "liquidTiles/Lava";
  texture[1] = "liquidTiles/LavaPool01";
  texture[2] = "liquidTiles/LavaPool02";
  texture[3] = "liquidTiles/LavaPool03";
  texture[4] = "liquidTiles/LavaPool04";
  framesPerSec = 6;
  numFrames = 5;
  scrollSpeed = 0.05;
  umapping = 0.3;
  vmapping = 0.3;
};

// icky water all pass
datablock ForceFieldBareData(DeployedForceField28) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = "0.1 0.6 0.4";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "liquidTiles/LushWater01_Algae";
  framesPerSec = 6;
  numFrames = 1;
  scrollSpeed = 0.05;
  umapping = 0.3;
  vmapping = 0.3;
};

// inviso-ff no pass
datablock ForceFieldBareData(DeployedForceField29) : DeployedForceField {
	baseTranslucency = 0.0;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.0 0.0 0.0";
	powerOffColor = "0.0 0.0 0.0";
};
// inviso-ff team pass
datablock ForceFieldBareData(DeployedForceField30) : DeployedForceField {
	baseTranslucency = 0.0;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = false;
	color         = "0.0 0.0 0.0";
	powerOffColor = "0.0 0.0 0.0";
};

// laser beams no pass
datablock ForceFieldBareData(DeployedForceField31) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "1.0 0.05 0.05";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "special/LightningBlur";     // pack_rep05
  framesPerSec = 10;                    // plrec04
  numFrames = 1;                       // beampulse
  scrollSpeed = 0.0;
  umapping = 1.5;
  vmapping = 1.5;
};

// energy beams no pass
datablock ForceFieldBareData(DeployedForceField32) : DeployedForceField {
	baseTranslucency = 0.9;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.4 0.4 0.9";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "special/Shocklance_effect01";    //dcase01-05
  texture[1] = "special/Shocklance_effect02";    //dcase01-05
  framesPerSec = 20;
  numFrames = 2;
  scrollSpeed = 12;
  umapping = 0.5;
  vmapping = 0.5;
};

// water (thx Crazed Weazel)
datablock ForceFieldBareData(DeployedForceField33) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = "0.1 0.4 0.6";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "liquidTiles/BlueWater";
  framesPerSec = 6;
  numFrames = 1;
  scrollSpeed = 0.05;
  umapping = 0.3;
  vmapping = 0.3;
};

// ionic-field
datablock ForceFieldBareData(DeployedForceField34) : DeployedForceField {
	baseTranslucency = 1;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.1 0.1 1";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "skins/beampulse";
  framesPerSec = 10;
  numFrames = 1;
  scrollSpeed = -4;
  umapping = 0.5;
  vmapping = 0.5;
};

//  fire wall  no pass
datablock ForceFieldBareData(DeployedForceField35) : DeployedForceField {
	baseTranslucency = 0.7;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	color         = "0.7 0.3 0";
	powerOffColor = "0.0 0.0 0.0";
  texture[0] = "special/redbump2";
  framesPerSec = 10;
  numFrames = 1;
  scrollSpeed = -6;
  umapping = 1;
  vmapping = 1;
};

// ice cube ff    no pass
datablock ForceFieldBareData(DeployedForceField36) : DeployedForceField {
	baseTranslucency = 0.9;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
   color = "0.6 0.6 0.9";
  powerOffColor = "0.0 0 0";
	texture[0] = "liquidTiles/IslandWater01";
//  texture[0] = "liquidTiles/IslandWater01";
  framesPerSec = 10;
  numFrames = 1;
  scrollSpeed = 0.07;
  umapping = 0.07;
  vmapping = 0.07;
};

// oob ff  all pass
datablock ForceFieldBareData(DeployedForceField37) : DeployedForceField {
	baseTranslucency = 0.6;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = true;
	otherPermiable = true;
	color         = "0.1 0.5 0.1";
	powerOffColor = "0.0 0.0 0.0";
    texture[0] = "special/GameGrid";
  framesPerSec = 10;
  numFrames = 1;
  scrollSpeed = 0.0;
  umapping = 1;
  vmapping = 1;

};

// better matrix grid      no pass
datablock ForceFieldBareData(DeployedForceField38) : DeployedForceField {
	baseTranslucency = 0.9;
	powerOffTranslucency = %allPassTrans / %dimDiv;
	teamPermiable = false;
	otherPermiable = false;
	texture[0] = "small_cross";
	color         = "0.1 0.6 0.1";
	powerOffColor = "0.0 0.0 0.0";
  framesPerSec = 10;
  numFrames = 1;
  scrollSpeed = 2;
  umapping = 3;
  vmapping = 3;
};

//END ADDITIONAL FORCE FIELDS

datablock ShapeBaseImageData(ForceFieldDeployableImage) {
 mass = 1;
	emap = true;
	shapeFile = "ammo_chaingun.dts";
	item = ForceFieldDeployable;
	mountPoint = 1;
	offset = "-0.2 -0.125 0";
	rotation = "0 -1 0 90";
	deployed = DeployedForceField;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.1;
	maxDeployDis = 50.0;

	modhudenabled = 1;
};

datablock ItemData(ForceFieldDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
 mass = 1;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "ForceFieldDeployableImage";
	pickUpName = "a force field pack";
	heatSignature = 0;
	emap = true;
};

function ForceFieldDeployableImage::testObjectTooClose(%item) {
	return;
}

function ForceFieldDeployableImage::testNoTerrainFound(%item) {
	// don't check this for non-Landspike turret deployables
}

function ForceFieldDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function ForceFieldDeployableImage::onDeploy(%item, %plyr, %slot) {
	//Object
	%className = "ForceFieldBare";

	%grounded = 0;
	if (%item.surface.getClassName() $= TerrainBlock)
		%grounded = 1;

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (%item.surfaceinher == 0) {
		if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
			%item.surfaceNrm2 = %playerVector;
		else
			%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));
	}

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
	%scale = getWords($packSetting["forcefield",%plyr.packSet],0,2);
	%mCenter = "-0.5 -0.5 -0.5";
	%pad = pad(%item.surfacePt SPC %item.surfaceNrm SPC %item.surfaceNrm2,%scale,%mCenter);
	%scale = getWords(%pad,0,2);
	%item.surfacePt = getWords(%pad,3,5);
	%rot = getWords(%pad,6,9);

	// Add padding
	%padSize = 0.01;
	%scale = vectorAdd(%scale,%padSize * 2 SPC %padSize * 2 SPC -%padSize * 2);
	%item.surfacePt = vectorSub(%item.surfacePt,vectorScale(vectorNormalize(%item.surfaceNrm),%padSize));
	%item.surfacePt = vectorSub(%item.surfacePt,vectorScale(vectorNormalize(%item.surfaceNrm2),%padSize));
	%item.surfacePt = vectorSub(%item.surfacePt,vectorScale(vectorNormalize(vectorCross(%item.surfaceNrm,%item.surfaceNrm2)),-%padSize));

	if ($Host::ExpertMode == 1) {
		if (isCubic(%item.surface) && (%plyr.expertSet == 1 || %plyr.expertSet == 3) && %plyr.team == %item.surface.team
		&& %item.surface.getType() & $TypeMasks::StaticShapeObjectType
		&& (($Host::OnlyOwnerCubicReplace == 0) || (%plyr.client == %item.surface.getOwner()))) {
			%scale = vectorAdd(realSize(%item.surface),%padSize * 2 SPC %padSize * 2 SPC %padSize * 2);
			%center = realVec(%item.surface,vectorScale(getWords(%scale,0,1) SPC "0",-0.5));
			%item.surfacePt = vectorAdd(pos(%item.surface),%center);
			%rot = rot(%item.surface);
			%mod = vectorScale(matrixMulVector("0 0 0" SPC %rot ,"0 0 1"),-%padSize);
			%item.surfacePt = vectorAdd(%item.surfacePt,%mod);
			%item.surface.getDataBlock().disassemble(%plyr, %item.surface);
		}
	}

	// TODO - temporary test fix - remove?
	%scale = vectorAdd(%scale,"0 0 0");
	%x = getWord(%scale,0);
	%y = getWord(%scale,1);
	%z = getWord(%scale,2);
	if (%x <= 0)
		%x = 0.001;
	if (%y <= 0)
		%y = 0.001;
	if (%z <= 0)
		%z = 0.001;
	%scale = %x SPC %y SPC %z;

	%deplObj = new (%className)() {
		dataBlock = %item.deployed @ %plyr.packSet;
		scale = %scale;
	};

	// Take the deployable off the player's back and out of inventory
	if ($Host::ExpertMode == 0) {
		%plyr.unMountImage(%slot);
		%plyr.decInventory(%item.item,1);
	}

////////////////////////Apply settings//////////////////////////////

	// [[Location]]:

	// exact:
	%deplObj.setTransform(%item.surfacePt SPC %rot);
	%deplObj.pzone.setTransform(%item.surfacePt SPC %rot);

	if ($Host::ExpertMode == 1) {
		if (%plyr.expertSet == 2 || %plyr.expertSet == 3) {
			%deplObj.noSlow = true;
			%deplObj.pzone.delete();
			%deplObj.pzone = "";
		}
	}

	// misc info
	addDSurface(%item.surface,%deplObj);

	// [[Settings]]:

	%deplObj.grounded = %grounded;
	%deplObj.needsFit = 1;

	// [[Normal Stuff]]:

	// set team, owner, and handle
	%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);

	// set power frequency
	%deplObj.powerFreq = %plyr.powerFreq;

	// set the sensor group if it needs one
	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	// place the deployable in the MissionCleanup/Deployables group (AI reasons)
	addToDeployGroup(%deplObj);

	//let the AI know as well...
	AIDeployObject(%plyr.client, %deplObj);

	// play the deploy sound
	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	// increment the team count for this deployed object
	$TeamDeployedCount[%plyr.team, %item.item]++;

	// Power object
	checkPowerObject(%deplObj);

	if (!%deplObj.powerCount > 0) {
		%deplObj.getDataBlock().disassemble(0,%deplObj); // Run Item Specific code.
		messageClient(%plyr.client,'MsgDeployFailed','\c2Force field lost - no power source found!%1','~wfx/misc/misc.error.wav');
	}

	return %deplObj;
}

/////////////////////////////////////

function ForceFieldDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasForceField = true; // set for forcefieldcheck
	%obj.packSet = 0;
	%obj.expertSet = 0;
	displayPowerFreq(%obj);
}

function ForceFieldDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasForceField = "";
	%obj.packSet = 0;
	%obj.expertSet = 0;
}

function DeployedForceField::disassemble(%data,%plyr,%obj) {
	if (isObject(%obj.pzone))
		%obj.pzone.delete();
	disassemble(%data,%plyr,%obj);
}

function DeployedForceField0::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField1::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField2::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField3::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField4::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField5::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField6::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField7::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField8::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField9::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField10::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField11::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField12::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField13::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField14::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField15::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField16::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField17::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField18::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField19::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField20::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}

function DeployedForceField21::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField22::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField23::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField24::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField25::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField26::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField27::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField28::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField29::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField30::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField31::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField32::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField33::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField34::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField35::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField36::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField37::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}
function DeployedForceField38::disassemble(%data,%plyr,%obj) {
	DeployedForceField::disassemble(%data,%plyr,%obj);
}


function ForceFieldBare::onLoad(%this)
{
setTargetSensorGroup(%this.getTarget(),mAbs(%this.team));
addToDeployGroup(%this);
}



function ForceFieldDeployableImage::setModHud(%data,%client) {
%heightlist = "";
%currentsetting = 0;
%totalsettings = $packSettings["forcefield"];
%loopnum = 0;
%maxreached = 0;
%currentsetting = 0;

while (!%maxreached) {
%heightlist = "";
for (%x=0;%x<10;%x++) {
if (%currentsetting > %totalsettings) {
%maxreached = true;
break;
}

%setting = $packSetting["forcefield",%currentsetting];
%setting = getSubStr(%setting,strstr(%setting,">")+1,(strlen(%setting)-strstr(%setting,">")+1));
echo(%setting);
%color2 = getSubStr(%setting,strstr(%setting,"<"),(strstr(%setting,">")-strstr(%setting,"<")+1));
echo(%color2);
%setting = strreplace(%setting, %color2, "");
//%setting = getSubStr(%setting,strstr(%setting,">")+1,strlen(%setting)); //Probably a better way to do this.
//%setting = getWords(%setting,3,getWordCount(%setting));
%heightlist = %heightlist @ "\"" @ %setting @ "\",";
%currentsetting++;
}
%heightlist = getSubStr(%heightlist, 0, strlen(%heightlist)-1); //Remove the last comma.
%thecommand = "commandToClient(" @ %client @ ", 'ModHudPopulate', " @ "\"Forcefield Type-" @ %loopnum @ "\", " @ %heightlist @ ");";
eval(%thecommand);
echo(%thecommand);
%loopnum++;
}
}

function ForceFieldDeployableImage::getModHud(%data,%client,%option,%value) {

%packset = ((%option-1)*10)+%value-1;
%client.player.packSet = %packset;
}