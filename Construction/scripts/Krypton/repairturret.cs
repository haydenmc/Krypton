// --------------------------------------------------------------
// Repair Deployable Turret barrel
// --------------------------------------------------------------

datablock AudioProfile(LifeTakerSwitchSound)
{
   filename    = "fx/weapons/generic_switch.wav";
   description = AudioClosest3d;
   preload = true;
   effect = LifeTakerSwitchEffect;
};

datablock AudioProfile(LifeTakerFireSound)
{
   filename    = "fx/packs/repair_use.wav";
   description = CloseLooping3d;
   preload = true;
   effect = LifeTakerFireEffect;
};

//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
$TeamDeployableMax[repairTurretDeployable]      = 100;

datablock ELFProjectileData(repairTurretBolt)
{
   beamRange         = 25; // z0dd - ZOD, 5/18/02. WHAT?? INCREASE ELF RANGE?!!? was 37
   numControlPoints  = 8;
   restorativeFactor = 3.75;
   dragFactor        = 4.5;
   endFactor         = 2.25;
   randForceFactor   = 2;
   randForceTime     = 0.125;
	drainEnergy			= 1.0;
	drainHealth			= 0;
    directDamageType  = $DamageType::repair;
    radiusDamageType    = $DamageType::repair;
	mainBeamWidth     = 0.1;           // width of blue wave beam
	mainBeamSpeed     = 9.0;            // speed that the beam travels forward
	mainBeamRepeat    = 0.25;           // number of times the texture repeats
   lightningWidth    = 1.1;
   lightningDist      = 0.1;           // distance of lightning from main beam

   fireSound    = LifeTakerFireSound;
   wetFireSound = LifeTakerFireSound;

	textures[0] = "special/redbump2";
   textures[1] = "special/flare3";
   textures[2] = "special/BlueImpact";

   emitter = TakerSparksEmitter;
};

datablock TurretData(TurretDeployedrepair) : TurretDamageProfile
{
   className = DeployedTurret;
   shapeFile = "turret_outdoor_deploy.dts";

   rechargeRate = 0.15;

   mass = 5.0;
   maxDamage = 3;
   destroyedLevel = 3;
   disabledLevel = 2.8;
   explosion      = HandGrenadeExplosion;
      expDmgRadius = 5.0;
      expDamage = 0.3;
      expImpulse = 500.0;
   repairRate = 10;

	deployedObject = true;

   thetaMin = 0;
   thetaMax = 145;
   thetaNull = 90;

   yawVariance          = 30.0; // these will smooth out the elf tracking code.
   pitchVariance        = 30.0; // more or less just tolerances

   isShielded = true;
   energyPerDamagePoint = 110;
   maxEnergy = 60;
   renderWhenDestroyed = true;
   barrel = repairBarrelLarge;
   heatSignature = 0;

   canControl = true;
   cmdCategory = "DTactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turret_grey";
   targetNameTag = 'repair';
   targetTypeTag = 'Turret';
   sensorData = DeployedOutdoorTurretSensor;
   sensorRadius = DeployedOutdoorTurretSensor.detectRadius;
   sensorColor = "191 0 226";

   firstPersonOnly = true;

   debrisShapeName = "debris_generic_small.dts";
   debris = TurretDebrisSmall;
};
//--------------------------------------------------------------------------
// ELF Turret Image
//--------------------------------------------------------------------------

datablock TurretImageData(repairBarrelLarge){
   shapeFile = "turret_elf_large.dts";//"weapon_elf.dts";
   // ---------------------------------------------
   // z0dd - ZOD, 5/8/02. Incorrect parameter value
   //item      = ELFBarrelLargePack;
   	offset = "-0.1 -0.5 0";
	rotation = "0 1 0 90";
   item = TurretDeployedrepair;

   projectile = BasicSniperShot;
   projectileType = SniperProjectile;
   deleteLastProjectile = true;
   usesEnergy = true;
   fireEnergy = 0.000001;
   minEnergy = 0.0000001;

   // Turret parameters
   activationMS      = 100;
   deactivateDelayMS = 100;
   thinkTimeMS       = 100;
   degPerSecTheta    = 580;
   degPerSecPhi      = 960;
   attackRadius      = 50;

   yawVariance          = 50.0; // these will smooth out the elf tracking code.
   pitchVariance        = 50.0; // more or less just tolerances

   // State transiltions
   stateName[0]                        = "Activate";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";

   stateName[1]                        = "ActivateReady";
   stateSequence[1]                    = "Activate";
   stateSound[1]                       = LifeTakerSwitchSound;
   stateTimeoutValue[1]                = 1;
   stateTransitionOnTimeout[1]         = "Ready";
   stateTransitionOnNotLoaded[1]       = "Deactivate";
   stateTransitionOnNoAmmo[1]          = "NoAmmo";

   stateName[2]                        = "Ready";
   stateTransitionOnNotLoaded[2]       = "Deactivate";
   stateTransitionOnTriggerDown[2]     = "Fire";
   stateTransitionOnNoAmmo[2]          = "NoAmmo";

   stateName[3]                        = "Fire";
   stateFire[3]                        = true;
   stateRecoil[3]                      = LightRecoil;
   stateAllowImageChange[3]            = false;
   stateSequence[3]                    = "Fire";
   stateSound[3]                       = LifeTakerFireSound;
   stateScript[3]                      = "";
   stateTransitionOnTriggerUp[3]       = "Deconstruction";
   stateTransitionOnNoAmmo[3]          = "Deconstruction";

   stateName[4]                        = "Reload";
   stateTimeoutValue[4]                = 0.01;
   stateAllowImageChange[4]            = false;
   stateSequence[4]                    = "Reload";
   stateTransitionOnTimeout[4]         = "Ready";
   stateTransitionOnNotLoaded[4]       = "Deactivate";
   stateTransitionOnNoAmmo[4]          = "NoAmmo";

   stateName[5]                        = "Deactivate";
   stateSequence[5]                    = "Activate";
   stateDirection[5]                   = false;
   stateTimeoutValue[5]                = 1;
   stateTransitionOnLoaded[5]          = "ActivateReady";
   stateTransitionOnTimeout[5]         = "Dead";

   stateName[6]                        = "Dead";
   stateTransitionOnLoaded[6]          = "ActivateReady";

   stateName[7]                        = "NoAmmo";
   stateTransitionOnAmmo[7]            = "Reload";
   stateSequence[7]                    = "NoAmmo";

   stateName[8]                       = "Deconstruction";
   stateScript[8]                     = "deconstruct";
   stateTransitionOnNoAmmo[8]         = "NoAmmo";
   stateTransitionOnTimeout[8]        = "Reload";
   stateTimeoutValue[8]               = 0.1;
};

//////////////////////////////////////////////////////////////
datablock ShapeBaseImageData(repairTurretDeployableImage){
   mass = 15;
   shapeFile = "pack_deploy_turreti.dts";
   item = repairTurretDeployable;
   mountPoint = 1;
   offset = "0 0 0";
   deployed = TurretDeployedrepair;
   stateName[0] = "Idle";
   stateTransitionOnTriggerDown[0] = "Activate";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateTransitionOnTriggerUp[1] = "Idle";

   isLarge = true;
   emap = true;
   maxDepSlope = 360;
   deploySound = TurretDeploySound;
   minDeployDis = 0.5;
   maxDeployDis = 5.0;
};

datablock ItemData(repairTurretDeployable){
   className = Pack;
   catagory = "Deployables";
   shapeFile = "pack_deploy_turreti.dts";
   mass = 3.0;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 1;
   rotate = false;
   image = "repairTurretDeployableImage";
   pickUpName = "a repair turret pack";
   emap = true;
};
//////extra images
datablock TurretImageData(repairBarrelLarge2) {
	shapeFile = "pack_upgrade_repair.dts";
	rotation = "0 1 0 90";
	offset = "-0.05 -0.95 0";

   // State transiltions
   stateName[0] = "Idle";
   stateTransitionOnNotLoaded[0]       = "Dead";
   stateTransitionOnLoaded[0]          = "ActivateReady";

   stateName[1] = "Activate";
   stateScript[1] = "onActivate";
   stateSequence[1] = "fire";
   stateSound[1] = RepairPackActivateSound;
   stateTransitionOnTriggerUp[1] = "Deactivate";

   stateName[2] = "Deactivate";
   stateScript[2] = "onDeactivate";
   stateTransitionOnTimeout[2] = "Idle";
};

datablock TurretImageData(repairBarrelLarge3) {
	shapeFile = "repair_kit.dts";
 rotation = "1 0 0 90";
	offset = "-0.26 -0.25 0.23";
};

datablock TurretImageData(repairBarrelLarge4) {
	shapeFile = "repair_kit.dts";
 rotation = "1 0 0 90";
	offset = "-0.26 -0.25 -0.23";
};
datablock TurretImageData(repairBarrelLarge5) {
	shapeFile = "repair_patch.dts";
	rotation = "0 0 0";
	offset = "0.2 0.50 -0.185";
};

////////////////////////////////////////////////////////////////
//functions
////////////////////////////////////////////////////////////////
function repairBarrelLarge::onMount(%this,%obj,%slot) {
	%obj.currentMuzzleSlot = 0;
	%obj.schedule(100,"mountImage",repairBarrelLarge2,1,true);
	%obj.schedule(100,"mountImage",repairBarrelLarge3,2,true);
	%obj.schedule(100,"mountImage",repairBarrelLarge4,3,true);
	%obj.schedule(100,"mountImage",repairBarrelLarge5,4,true);
}
function repairTurretDeployableImage::testNoTerrainFound(%item){
// created to prevent console errors
}

function repairTurretDeployableImage::testNoInteriorFound(%item){
// created to prevent console errors
}

function repairTurretDeployableImage::onDeploy(%item, %plyr, %slot){

%searchRange = 5.0;
%mask = $TypeMasks::TerrainObjectType | $TypeMasks::InteriorObjectType;
%eyeVec = %plyr.getEyeVector();
%eyeTrans = %plyr.getEyeTransform();
%eyePos = posFromTransform(%eyeTrans);
%nEyeVec = VectorNormalize(%eyeVec);
%scEyeVec = VectorScale(%nEyeVec, %searchRange);
%eyeEnd = VectorAdd(%eyePos, %scEyeVec);
//%searchResult = containerRayCast(%eyePos, %eyeEnd, %mask, 0);
//if(!%searchResult ) {
//messageClient(%plyr.client, 'MsgBeaconNoSurface', 'c2Cannot place turret. You are too far from surface.');
//return 0;
//}
%terrPt = %item.surfacept;
%terrNrm = %item.surfacenrm;
%intAngle = getTerrainAngle(%terrNrm);
%rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 0 1"));
if ((getWord(%terrNrm, 2) == 1) || (getWord(%terrNrm, 2) == -1))
%rotAxis = vectorNormalize(vectorCross(%terrNrm, "0 1 0"));
%rotation = %rotAxis @ " " @ %intAngle;
%plyr.unmountImage(%slot);
%plyr.decInventory(%item.item, 1);

%deplObj = new Turret() {
dataBlock = %item.deployed;
position = VectorAdd(%terrPt, VectorScale(%terrNrm, 0.03));
rotation = %rotation;
};

	addDSurface(%item.surface,%deplObj);
%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);
%deplObj.team = %plyr.client.team;

%deplObj.isrepair =1; //this is used whit the chek for target in turret.cs
                      //to look for damaged stuf instead of enemy

	%deplObj.setOwner(%plyr);
%deplObj.setOwnerClient(%plyr.client);
if(%deplObj.getTarget() != -1)
setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

addToDeployGroup(%deplObj);
AIDeployObject(%plyr.client, %deplObj);
serverPlay3D(%item.deploySound, %deplObj.getTransform());
$TeamDeployedCount[%plyr.team, %item.item]++;
%deplObj.deploy();
%deplObj.playThread($AmbientThread, "ambient");
return %deplObj;
}

function TurretDeployedrepair::onPickup(%this, %obj, %shape, %amount){
// created to prevent console errors
}

function TurretDeployedrepair::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
stopRepairing(%obj);
   %obj.errMsgSent = false;
   %obj.selfRepairing = false;
   %obj.repairing = 0;
   %obj.setImageLoaded(%slot, false);
if (isobject(%obj.repairProjectile)){
   %obj.repairProjectile.schedule(100, "delete");
   %obj.repairProjectile=0;
   }
	if ($Host::InvincibleDeployables != 1 || %obj.damageFailedDecon) {
		%obj.isRemoved = true;
		$TeamDeployedCount[%obj.team, DiscTurretDeployable]--;
		remDSurface(%obj);
		%obj.schedule(500, "delete");
	}
	Parent::onDestroyed(%this, %obj, %prevState);
}
//function Deployedwaypoint::disassemble(%data, %plyr, %obj){ //What in the HELL is this doing here?
// 	if (%obj.isRemoved)
//		return;
//stopRepairing(%obj);
//   %obj.errMsgSent = false;
//   %obj.selfRepairing = false;
//   %obj.repairing = 0;
//   %obj.setImageLoaded(%slot, false);
//if (isobject(%obj.repairProjectile)){
//   %obj.repairProjectile.schedule(100, "delete");
//   %obj.repairProjectile=0;
//   }
//	if ($Host::InvincibleDeployables != 1 || %obj.damageFailedDecon) {
//		%obj.isRemoved = true;
//		$TeamDeployedCount[%obj.team, DiscTurretDeployable]--;
//		remDSurface(%obj);
//		%obj.schedule(500, "delete");
//	}
//	Parent::onDestroyed(%this, %obj, %prevState);
//}
function validatetarget(%obj,%slot,%target){
//used to make shure thers a clean line of sight betwin the object adn the target
         %muzVec      = %obj.getMuzzleVector(%slot);
         %muzNVec     = VectorNormalize(%muzVec);
         %repairRange = 30;
         %muzScaled   = VectorScale(%muzNVec, %repairRange);
         %muzPoint    = %obj.getMuzzlePoint(%slot);
         %rangeEnd    = VectorAdd(%muzPoint, %muzScaled);

         %searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType |
                        $TypeMasks::StaticShapeObjectType | $TypeMasks::TurretObjectType |
                        $TypeMasks::ItemObjectType | $TypeMasks::ForceFieldObjectType;

         //AI hack to help "fudge" the repairing stuff...
         %scanTarg = ContainerRayCast(%muzPoint, %target.getposition(), %searchMasks, %obj);
if (%scanTarg == %target ||%scanTarg ==0)
   return 1;
else{
    return 0;
    }
}
function startRepairing1(%obj,%slot)
{
   // this = repairgunimage datablock
   // obj = player wielding the repair gun
   // slot = weapon slot

   if(%obj.getEnergyLevel() <= 0)
   {
      stopRepairing(%obj);
      %obj.repairing="";
      return;
   }
   // reset the flag that indicates an error message has been sent
   %obj.errMsgSent = false;
   %target = %obj.repairing;
   if(!%target)
   {
      // no target -- whoops! never mind
      stopRepairing(%obj);
   }
   else
   {
      //%target.repairedBy = %obj.client;  //keep track of who last repaired this item
      if(%obj.repairing == %obj)
      {
         // player is self-repairing
         if(%obj.getDamageLevel())
         {
            if(!%obj.selfRepairing)
            {
               // no need for a projectile, just send a message and up the repair rate
               %obj.selfRepairing = true;
               startRepairing3(%obj, true);
            }
         }
         else
         {
            stopRepairing(%obj);
            %obj.errMsgSent = true;
         }
      }
      else
      {
         // make sure we still have a target -- more vector fun!!!
         %muzVec      = %obj.getMuzzleVector(%slot);
         %muzNVec     = VectorNormalize(%muzVec);
         %repairRange = 30;
         %muzScaled   = VectorScale(%muzNVec, %repairRange);
         %muzPoint    = %obj.getMuzzlePoint(%slot);
         %rangeEnd    = VectorAdd(%muzPoint, %muzScaled);

         %searchMasks = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType |
                        $TypeMasks::StaticShapeObjectType | $TypeMasks::TurretObjectType |
                        $TypeMasks::ItemObjectType | $TypeMasks::ForceFieldObjectType;

         //AI hack to help "fudge" the repairing stuff...
            %scanTarg = ContainerRayCast(%muzPoint, %rangeEnd, %searchMasks, %obj);
         if (%scanTarg)
         {
            if (%scanTarg.getType() & $TypeMasks::ForceFieldObjectType)
            {
               if (%scanTarg.getDataBlock().getName() $= "DeployedForceField2")
               {
                  %pos = getWords(%scanTarg, 1, 3);
                  %scanTarg = %scanTarg.parent SPC %pos;
               }
               else
                  %scanTarg = "0";
            }
         }
         if (%scanTarg)
         {
            %pos = getWords(%scanTarg, 1, 3);
            %obstructMask = $TypeMasks::InteriorObjectType | $TypeMasks::TerrainObjectType;
            %obstruction = ContainerRayCast(%muzPoint, %pos, %obstructMask, %obj);
            if (%obstruction)
               %scanTarg = "0";
         }
         if (%scanTarg && %scanTarg2)
            %scanTarg = %scanTarg2;

         if(%scanTarg)
         {
            // there's still a target out there
            %repTgt = firstWord(%scanTarg);
            // is the target damaged?
            if(%repTgt.getDamageLevel())
            {
               if(%repTgt != %obj.repairing)
               {
                  // the target is not the same as the one we were just repairing
                  // stop repairing old target, start repairing new target
                  stopRepairing(%obj);
                  if(isObject(%obj.repairing))
                     stopRepairing(%obj);
                  %obj.repairing = %repTgt;
                  // extract the name of what player is repairing based on what it is
                  // if it's a player, it's the player's name (duh)
                  // if it's an object, look for a nametag
                  // if object has no nametag, just say what it is (e.g. generatorLarge)
                  if(%repTgt.getClassName() $= Player)
                     %tgtName = getTaggedString(%repTgt.client.name);
                  else if(%repTgt.getGameName() !$= "")
                     %tgtName = %repTgt.getGameName();
                  else
                     %tgtName = %repTgt.getDatablock().getName();
                  startRepairing3(%obj, false);
               }
               else
               {
                  // it's the same target as last time
                  // changed to fix "2 players can't repair same object" bug
                  if (%obj.repairProjectile!=0){
                     if (isobject(%obj.repairProjectile))
                        %obj.repairProjectile.schedule(100, "delete");
                        %obj.repairProjectile=0;
                     }
                  if(%obj.repairProjectile == 0)
                  {
                     if(%repTgt.getClassName() $= Player)
                        %tgtName = getTaggedString(%repTgt.client.name);
                     else if(%repTgt.getGameName() !$= "")
                        %tgtName = %repTgt.getGameName();
                     else
                        %tgtName = %repTgt.getDatablock().getName();
                     startRepairing3(%obj, false);
                  }
               }
            }
            else
            {
              %rateOfRepair = %this.repairFactorObject;
               if(%repTgt.getClassName() $= Player)
               {
                  %tgtName = getTaggedString(%repTgt.client.name);
                  %rateOfRepair = %this.repairFactorPlayer;
               }
               else if(%repTgt.getGameName() !$= "")
                  %tgtName = %repTgt.getGameName();
               else
                  %tgtName = %repTgt.getDatablock().getName();
               if(%repTgt == %obj.repairing)
               {
                  // same target, but not damaged -- we must be done
                  Game.objectRepaired(%repTgt, %tgtName);
               }
               %obj.errMsgSent = true;
               stopRepairing(%obj);
            }
         }
         else
             stopRepairing(%obj);
      }
   }
}

function startRepairing3(%player, %self)
{
   // %player = the player who was using the repair pack
   // %self = boolean -- is player repairing him/herself?

   if(%self)
   {
      // one repair, hold the projectile
      %player.setRepairRate(%player.getRepairRate() + 0.0025);
      %player.selfRepairing = true;
      %player.repairingRate = 0.0025;
   }
   else
   {

         %initialDirection = %player.getMuzzleVector(0);
         %initialPosition  = %player.getMuzzlePoint(0);

         %repRate = 0.005;
      %player.repairing.setRepairRate(%repRate);

      %player.repairingRate = %repRate;

      if (%player.repairing.getDataBlock().getName() $= "DeployedForceField")
         %targetObject = %player.repairing.field;
      else
         %targetObject = %player.repairing;
       %player.repairProjectile = new RepairProjectile() {
         dataBlock = DefaultRepairBeam;
         initialDirection = %initialDirection;
         initialPosition  = %initialPosition;
         sourceObject     = %player;
         sourceSlot       = 0;
         targetObject     = %targetObject;
      };
      MissionCleanup.add(%player.repairProjectile);
   }
}
