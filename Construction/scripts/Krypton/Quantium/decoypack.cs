//---------------------------------------------------------
// Deployable Decoy
//---------------------------------------------------------

$expertSettings[DecoyDeployableImage] = "5 -1 Decoy Pack:[Options]";
$expertSetting[DecoyDeployableImage,0] = "Select Armor";
$expertSetting[DecoyDeployableImage,1] = "Select Skin";
$expertSetting[DecoyDeployableImage,2] = "Select Pack mounted";
$expertSetting[DecoyDeployableImage,3] = "Select Weapon mounted";
$expertSetting[DecoyDeployableImage,4] = "Select Power options";
$expertSetting[DecoyDeployableImage,5] = "Select Emote";

$packSettings[DecoyDeployableImage] = "8 -1 Decoy Pack:[Armor]";
$packSetting[DecoyDeployableImage,0] = "LightMaleHumanArmor | Light Male Armor";
$packSetting[DecoyDeployableImage,1] = "MediumMaleHumanArmor | Medium Male Armor";
$packSetting[DecoyDeployableImage,2] = "HeavyMaleHumanArmor | Large Male Armor";
$packSetting[DecoyDeployableImage,3] = "LightFemaleHumanArmor | Light Female Armor";
$packSetting[DecoyDeployableImage,4] = "MediumFemaleHumanArmor  | Medium Female Armor";
$packSetting[DecoyDeployableImage,5] = "HeavyFemaleHumanArmor | Large Female Armor";
$packSetting[DecoyDeployableImage,6] = "LightMaleBiodermArmor | Light Bioderm Armor";
$packSetting[DecoyDeployableImage,7] = "MediumMaleBiodermArmor | Medium Bioderm Armor";
$packSetting[DecoyDeployableImage,8] = "HeavyMaleBiodermArmor | Large Bioderm Armor";

$packSettings[DecoyDeployableImage,1] = "10 -2 Decoy Pack: [Skin]";
$packSetting[DecoyDeployableImage,0,1] = "BEagle Blood eagle";
$packSetting[DecoyDeployableImage,1,1] = "COTP Inferno";
$packSetting[DecoyDeployableImage,2,1] = "DSword Diamond Sword";
$packSetting[DecoyDeployableImage,3,1] = "Swolf Starwolf";
$packSetting[DecoyDeployableImage,4,1] = "BaseB Base1";
$packSetting[DecoyDeployableImage,5,1] = "Base Base2";
$packSetting[DecoyDeployableImage,6,1] = "basebbot Bot1 (MALE ONLY)";
$packSetting[DecoyDeployableImage,7,1] = "basebot Bot2 (MALE ONLY)";
$packSetting[DecoyDeployableImage,8,1] = "TR2-1 Tr2 1 (FEMALE ONLY)";
$packSetting[DecoyDeployableImage,9,1] = "TR2-2 Tr2 2 (FEMALE ONLY)";
$packSetting[DecoyDeployableImage,10,1] = "horde Horde (BIODERM ONLY)";

$packSettings[DecoyDeployableImage,5] = "15 -1 Decoy Pack: [Emote]";
$packSetting[DecoyDeployableImage,0,5] = "Standing";
$packSetting[DecoyDeployableImage,1,5] = "Sniper rifle pose";
$packSetting[DecoyDeployableImage,2,5] = "Death1";
$packSetting[DecoyDeployableImage,3,5] = "Death2";
$packSetting[DecoyDeployableImage,4,5] = "Death3";
$packSetting[DecoyDeployableImage,5,5] = "Death4";
$packSetting[DecoyDeployableImage,6,5] = "Looking";
$packSetting[DecoyDeployableImage,7,5] = "Standjump";
$packSetting[DecoyDeployableImage,8,5] = "Missile launcher pose";
$packSetting[DecoyDeployableImage,9,5] = "Peeing pose =)";
$packSetting[DecoyDeployableImage,10,5] = "Hopping pose";
$packSetting[DecoyDeployableImage,11,5] = "Crawl";
$packSetting[DecoyDeployableImage,12,5] = "Sitting";
$packSetting[DecoyDeployableImage,13,5] = "Headside";
$packSetting[DecoyDeployableImage,14,5] = "Dancing";
$packSetting[DecoyDeployableImage,15,5] = "Prox. Wave";

$packSettings[DecoyDeployableImage,2] = "7 -2 Decoy Pack: [Pack]";
$packSetting[DecoyDeployableImage,0,2] = "CloakPack Cloak pack";
$packSetting[DecoyDeployableImage,1,2] = "RepairPack Repair pack";
$packSetting[DecoyDeployableImage,2,2] = "CrateDeployable Bigbox";
$packSetting[DecoyDeployableImage,3,2] = "AmmoPack Ammo pack";
$packSetting[DecoyDeployableImage,4,2] = "ShieldPack Shield pack";
$packSetting[DecoyDeployableImage,5,2] = "spineDeployable2 LSB pack";
$packSetting[DecoyDeployableImage,6,2] = "E Energy pack";
$packSetting[DecoyDeployableImage,7,2] = "E Empty";

$packSettings[DecoyDeployableImage,3] = "8 -2 Decoy Pack: [Weapon]";
$packSetting[DecoyDeployableImage,0,3] = "Plasma Plasma rifle";
$packSetting[DecoyDeployableImage,1,3] = "Disc Spinfusor";
$packSetting[DecoyDeployableImage,2,3] = "SniperRifle Sniper rifle";
$packSetting[DecoyDeployableImage,3,3] = "Mortar Fusion mortar";
$packSetting[DecoyDeployableImage,4,3] = "Shocklance Shocklance";
$packSetting[DecoyDeployableImage,5,3] = "ElfGun ELF";
$packSetting[DecoyDeployableImage,6,3] = "Chaingun Chaingun";
$packSetting[DecoyDeployableImage,7,3] = "ElfGun Empty";
$packSetting[DecoyDeployableImage,8,3] = "MS Missile Launcher";

$packSettings[DecoyDeployableImage,4] = "1 -1 Decoy Pack: [Power Options]";
$packSetting[DecoyDeployableImage,0,4] = "Showing when powered";
$packSetting[DecoyDeployableImage,1,4] = "Always showing";


datablock StaticShapeData(BiodermPlayerProjection)
{
   shapeFile = "bioderm_light.dts";
   skin = "";
   isStatic = true;
collideable = 0;
};
datablock StaticShapeData(HumanPlayerProjection)
{
   shapeFile = "light_male.dts";
   isStatic = true;
   skin = "";
collideable = 0;
};
datablock StaticShapeData(HumanFemalePlayerProjection)
{
   shapeFile = "light_female.dts";
   isStatic = true;
   skin = "";
collideable = 0;
};






datablock StaticShapeData(DeployedDecoy) : StaticShapeDamageProfile {
	className	= "lightbase";
	shapeFile	= "camera.dts";

	maxDamage	= 0.5;
	destroyedLevel	= 0.5;
	disabledLevel	= 0.3;

	maxEnergy = 50;
	rechargeRate = 0.25;

	explosion	= HandGrenadeExplosion;
	expDmgRadius	= 1.0;
	expDamage	= 0.05;
	expImpulse	= 200;

	dynamicType		= $TypeMasks::StaticShapeObjectType;
	deployedObject		= true;
	cmdCategory		= "DSupport";
	cmdIcon			= CMDSensorIcon;
	cmdMiniIconName		= "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag		= 'Deployed Decoy';
	deployAmbientThread	= true;
	debrisShapeName		= "debris_generic_small.dts";
	debris			= DeployableDebris;
	heatSignature		= 0;
	needsPower = true;
};

datablock ShapeBaseImageData(DecoyDeployableImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = DecoyDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedDecoy;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = false;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 50.0;
};

datablock ItemData(DecoyDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "DecoyDeployableImage";
	pickUpName = "a deployable decoy pack";
	heatSignature = 0;
	emap = true;
};

function DecoyDeployableImage::testObjectTooClose(%item) {
	return "";
}

function DecoyDeployableImage::testNoTerrainFound(%item) {
	// don't check this for non-Landspike turret deployables
}

//function DecoyDeployable::onPickup(%this, %obj, %shape, %amount) {
//	// created to prevent console errors
//}
function DecoyDeployableImage::onDeploy(%item, %plyr, %slot) {
if(%plyr.decoyCount == 4 && !%plyr.client.isAdmin) {
messageClient(%plyr.client,0,'\c2Too many decoys deployed');
return;
}
%plyr.decoyCount++;
	%className = "StaticShape";
    %pos2 = rayDist(%item.surfacePt SPC %item.surfaceNrm);
	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
		%item.surfaceNrm2 = %playerVector;
	else
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 -1"));

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		dataBlock = %item.deployed;
	};

    %mode = %plyr.packSet[4];
	if (%mode $= "2")
		%deplObj.mode = 1;
	else
		%deplObj.mode = %mode;

    %deplObj.decoyType = firstWord($packsetting[DecoyDeployableImage,%plyr.packset]);
    %deplObj.decoySkin = %plyr.packSet[1];
    %deplObj.decoyPack = %plyr.packSet[2];
    %deplObj.decoyWeapon = %plyr.packSet[3];
    %deplObj.decoyEmote = %plyr.packSet[5];
    %deplObj.decoyName = %plyr.decoyName;

// this is such a pain in the ass....
    if(%deplObj.decoyPack == 0)
			%logo23 = "CloakPack";
    else if(%deplObj.decoyPack == 1)
			%logo23 = "RepairPack";
    else if(%deplObj.decoyPack == 2)
			%logo23 = "CrateDeployable";
    else if(%deplObj.decoyPack == 3)
			%logo23 = "AmmoPack";
    else if(%deplObj.decoyPack == 4)
			%logo23 = "ShieldPack";
    else if(%deplObj.decoyPack == 5)
			%logo23 = "spineDeployable2";
    else if(%deplObj.decoyPack == 6)
			%logo23 = "EnergyPack";
    else if(%deplObj.decoyPack == 7)
			%logo23 = "";
    else
			%logo23 = "0";

    %deplObj.decoyPack = %logo23;

    if(%deplObj.decoyWeapon == 0)
			%logo2 = "Plasma";
    else if(%deplObj.decoyWeapon == 1)
			%logo2 = "Disc";
    else if(%deplObj.decoyWeapon == 2)
			%logo2 = "SniperRifle";
    else if(%deplObj.decoyWeapon == 3)
			%logo2 = "Mortar";
    else if(%deplObj.decoyWeapon == 4)
			%logo2 = "Shocklance";
    else if(%deplObj.decoyWeapon == 5)
			%logo2 = "ELFGun";
    else if(%deplObj.decoyWeapon == 6)
			%logo2 = "Chaingun";
    else if(%deplObj.decoyWeapon == 7)
			%logo2 = "";
    else if(%deplObj.decoyWeapon == 8)
			%logo2 = "MissileLauncher";
    else
			%logo2 = "0";

    %deplObj.decoyWeapon = %logo2;
    
    if(%deplObj.decoySkin == 0)
			%logo = "BEagle";
    else if(%deplObj.decoySkin == 1)
			%logo = "COTP";
    else if(%deplObj.decoySkin == 2)
			%logo = "DSword";
    else if(%deplObj.decoySkin == 3)
			%logo = "Swolf";
    else if(%deplObj.decoySkin == 4)
			%logo = "BaseB";
    else if(%deplObj.decoySkin == 5)
			%logo = "Base";
    else if(%deplObj.decoySkin == 6)
			%logo = "basebbot";
    else if(%deplObj.decoySkin == 7)
			%logo = "basebot";
    else if(%deplObj.decoySkin == 8)
			%logo = "TR2-1";
    else if(%deplObj.decoySkin == 9)
			%logo = "TR2-2";
    else if(%deplObj.decoySkin == 10)
			%logo = "Horde";
    else
			%logo = "0";

    %deplObj.decoySkin = addTaggedString(%logo);
    %logo = "";
  
  if(%delObj.decoyEmote < 14) {
    if(%deplObj.decoyEmote == 0)
			%logoa = "root";
    else if(%deplObj.decoyEmote == 1)
			%logoa = "looksn";
    else if(%deplObj.decoyEmote == 2)
			%logoa = "Death9";
    else if(%deplObj.decoyEmote == 3)
			%logoa = "Death3";
    else if(%deplObj.decoyEmote == 4)
			%logoa = "Death8";
    else if(%deplObj.decoyEmote == 5)
			%logoa = "Death11";
    else if(%deplObj.decoyEmote == 6)
			%logoa = "look";
    else if(%deplObj.decoyEmote == 7)
			%logoa = "standjump";
    else if(%deplObj.decoyEmote == 8)
			%logoa = "lookms";
    else if(%deplObj.decoyEmote == 9)
			%logoa = "looknw";
    else if(%deplObj.decoyEmote == 10)
			%logoa = "ski";
    else if(%deplObj.decoyEmote == 11)
			%logoa = "scoutroot";
    else if(%deplObj.decoyEmote == 12)
			%logoa = "sitting";
    else if(%deplObj.decoyEmote == 13)
			%logoa = "headside";
    else {
			%logoa = "0";
   if(%deplObj.decoyEmote == 14) 
      %deplObj.decoyEmote = 14; 
   else if(%deplObj.decoyEmote == 15) 
     %deplObj.decoyEmote = 15;
   else
      %deplObj.decoyEmote = %logoa;
      
      }
    }


	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.setOwner(%plyr);
	%deplObj.team = %plyr.client.Team;
	%deplObj.owner = %plyr.client;
	%deplObj.powerFreq = %plyr.powerFreq;
    %deplObj.decoy = createNewDecoy(%deplObj);

    if(%deplObj.decoyEmote == 14 || %deplObj.decoyEmote == 15)  {
      if(%deplObj.decoyEmote == 14) {
         decoyRepeat(%deplObj,"cel5");
         %deplObj.decoyEmote = 14;
      }
      if(%deplObj.decoyEmote == 15) {
         decoyRepeat(%deplObj,"cel2");
         %deplObj.decoyEmote = 15;
      }
    }
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

	addDSurface(%item.surface,%deplObj);

    if(%deplObj.mode == 2)
       %deplObj.startFade(1000,0,true);


 	checkPowerObject(%deplObj);
	// take the deployable off the player's back and out of inventory
//	%plyr.unmountImage(%slot);
//	%plyr.decInventory(%item.item, 1);

	return %deplObj;
}

function DeployedDecoy::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
if (isObject(%obj.decoy))
		%obj.decoy.delete();
%this.decoyCount--;
	Parent::onDestroyed(%this,%obj,%prevState);
	$TeamDeployedCount[%obj.team, DecoyDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");

}
function DeployedDecoy::disassemble(%data,%plyr,%obj) {
//%obj.decoy.target.delete();
freeClientTarget(%obj.decoy);
%plyr.decoyCount--;
		%obj.decoy.delete();
	disassemble(%data,%plyr,%obj);
}
function createNewDecoy(%obj) {
echo("CREATINGDECOY");
%mountimage = %obj.decoyType;
%mountweapon = %obj.decoyWeapon;
%mountpack = %obj.decoyPack;
%mountskin = %obj.decoySkin;
%mountname = %obj.decoyName;
%mountemote = %obj.decoyEmote;

        %ABCCount = 11;
        %ABC[0] = "Boo Boo the boomer";
        %ABC[1] = "????????";
        %ABC[2] = "Amanda hugnkiss";
        %ABC[3] = "Ivana tinkle";
        %ABC[4] = "Jack Goff";
        %ABC[5] = "Willy Leak";
        %ABC[6] = "Use /decoyname and replace me";
        %ABC[7] = "Santa claws";
        %ABC[8] = "Seemore Butts";
        %ABC[9] = "Nauq Nus";
        %ABC[10] = "Cereal killer";
        %ABC[11] = "Saddam whosayin";
        %name = %ABC[mFloor(getRandom()*%ABCCount)];

if(%mountname $= "")
   %mountname = %name;
%objdecoy = new Player()
     {
      dataBlock = %mountimage;
   //   rotation = %obj.getRotation();
      Position = %obj.getPosition();
      };
%objdecoy.target = createTarget(%objdecoy, %mountname, %mountskin, "Male1", '', 0, PlayerSensor);
   setTargetDataBlock(%objdecoy.target, %objdecoy.getDatablock());
   setTargetSensorData(%objdecoy.target, PlayerSensor);
   setTargetSensorGroup(%objdecoy.target, 0);
    setTargetSkin(%objdecoy.target, %mountskin);
    setTargetName(%objdecoy.target, addtaggedstring(%mountname));
    if(%obj.decoyEmote != 15 || %obj.decoyEmote != 14)
       %objdecoy.setActionThread(%mountemote,true);
       
    %objdecoy.setInventory(%mountweapon,1,1);
    %objdecoy.setInventory(%mountweapon.ammo,0,555);
    %objdecoy.setInventory(%mountpack,1,1);
    %objdecoy.use(%mountweapon);
    %objdecoy.isStatic = true;
    %obj.decoy = %objdecoy;  
    %objdecoy.setTransform(VectorAdd(%obj.getPosition(),"0 0 0") SPC rot(%obj));
    %objdecoy.disableMove(true);
       
    return %objdecoy;
}
function DecoyDeployableImage::onMount(%data, %obj, %node) {
	//%obj.hasDecoy = true; // set for lightcheck
	%obj.packSet[0] = 0;
 	%obj.packSet[1] = 0;
  	%obj.packSet[2] = 0;
   	%obj.packSet[3] = 0;
   	%obj.packSet[4] = 0;
   	%obj.packSet[5] = 0;
	%obj.expertSet = 0;
	displayPowerFreq(%obj);
}

function DecoyDeployableImage::onUnmount(%data, %obj, %node) {
	//%obj.hasDecoy = "";
	%obj.packSet = 0;
 	%obj.packSet[0] = 0;
 	%obj.packSet[1] = 0;
  	%obj.packSet[2] = 0;
   	%obj.packSet[3] = 0;
   	%obj.packSet[4] = 0;
   	%obj.packSet[5] = 0;
 	%obj.expertSet = 0;
}
function DeployedDecoy::onGainPowerEnabled(%data,%obj) {
	if (shouldChangePowerState(%obj,true)) {
    if(%obj.mode == 0) {
  	if (isObject(%obj.decoy))
	    %obj.decoy.delete();
    %obj.decoy = createNewDecoy(%obj);
    %obj.startFade(1000,0,true);
    if(%obj.decoyEmote == 15)
       decoyRepeat(%obj,"cel2");
    if(%obj.decoyEmote == 14)
       decoyRepeat(%obj,"cel5");
    %obj.decoy.setTransform(%obj.getPosition() SPC %obj.getRotation());
    }
    else
    {
    }
	Parent::onGainPowerEnabled(%data,%obj);
     }
   }


function DeployedDecoy::onLosePowerDisabled(%data,%obj) {
	if (shouldChangePowerState(%obj,false)) {
    if(%obj.mode == 0) {
    %obj.decoy.startFade(1000,0,true);
    %obj.startFade(1000,0,false);
     cancel(%obj.decoyLoop);
   %obj.decoy.setTransform("0 0 -10000 0 0 0 0");
    }
    else
    {
    }
    Parent::onLosePowerDisabled(%data,%obj);
     }
   }
function decoyRepeat(%obj,%cel) {
   cancel(%obj.decoyLoop);
   
   if(!isObject(%obj.decoy))
      return;
     
   if(%cel $= "cel2") {
	%pos=%obj.decoy.getMuzzlePoint(0);
	%vec = %obj.decoy.getMuzzleVector(0);
	%targetpos = vectoradd(%pos,vectorscale(%vec,5));
	%newobj=containerraycast(%pos,%targetpos,$typemasks::playerobjecttype,%obj.decoy);
    %newobj = getWord(%newobj,0);
   if(isObject(%newObj))
    if(%newobj != %obj.decoy)      
   %obj.decoy.setActionThread("cel2",0);
   

   %obj.decoyLoop = schedule(1000,0,decoyRepeat,%obj,%cel);
   }
   else {
   %obj.decoy.setActionThread("cel5",0);
   %obj.decoyLoop = schedule(3000,0,decoyRepeat,%obj,%cel);
   }
}
function DecoyDeployableImage::ChangeMode(%data,%plyr,%val,%level)
{
if (%level == 0)
   {
   //Selecting Detonation
   if (!%plyr.expertSet)
      {
      Parent::ChangeMode(%data,%plyr,%val,%level);
      %plyr.packset[0] = GetWord($packSetting[DecoyDeployableImage,%plyr.packset],0);
      }
    //Selecting selection mode/PowerLogic/CloakLogic
     if (%plyr.expertSet > 0)
      {
      %set = %plyr.expertSet;
      %image = %data.getName();
      %settings = $packSettings[%image,%set];

      %plyr.packSet[%set] = %plyr.packSet[%set] + %val;
      if (%plyr.packSet[%set] > getWord(%settings,0))
	  %plyr.packSet[%set] = 0;
      if (%plyr.packSet[%set] < 0)
 	  %plyr.packSet[%set] = getWord(%settings,0);

      %packname = GetWords(%settings,2,getWordCount(%settings));
      %curset = $PackSetting[%image,%plyr.packSet[%set],%set];
      if (getWord(%settings,1) == -1)
	   %line = GetWords(%curset,0,getWordCount(%curset));
    else if(getWord(%settings,1) == -2)
     %line= getWords(%curset,1,getWordCount(%curset));
      else
	   %line = GetWords(%curset,0,getWord(%settings,1));
      bottomPrint(%plyr.client,%packname SPC "set to"SPC %line,2,1);
      }
   }
else
   {
   Parent::ChangeMode(%data,%plyr,%val,%level);
   }
}
