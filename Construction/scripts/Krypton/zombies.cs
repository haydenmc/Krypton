//Variables...
//$Zombie::TurningSpeed = 100;
$Zombie::ForwardSpeed = 750; 
$Zombie::FForwardSpeed = 1500; 
$Zombie::LForwardSpeed = 4000; 
$Zombie::DForwardSpeed = 1200; 
$Zombie::RForwardSpeed = 1500;
$ZombieNum = 0; //Number of Zombies spawned
$MaxZombies = 30;
$ZombiesEnabled = 0;
$ZombieDelay = 4000;

function EndZombies()
{

%count = MissionCleanup.getCount();
for(%i = 0; %i < %count; %i++)
{
%obj = MissionCleanup.getObject(%i);
if(isObject(%obj))
{
if(%obj.infected || strstr(%obj.getDatablock().getName(), "Zombie") != -1)
{
%obj.infected = 0;
%obj.scriptkill($DamageType::Idiocy);
}
}
}
$ZombiesEnabled = 0;
messageAll('MsgAdminForce', "\c2Zombie attack stopped.");
}


function ZombieLoop()
{
if ($ZombieNum < $MaxZombies)
RandomZombieSpawn();

if ($ZombiesEnabled)
$ZSpawnLoop = schedule($ZombieDelay,0,ZombieLoop);
}

//-------------------
//Zombie Functions
//-------------------
function RandomZombieSpawn()
{
if (!$ZombiesEnabled)
	return;
$ZombieNum++;
			%play = RandomPlayer();
			%pos = pos(%play);
			%angle = getRandom() * 2 * $Pi;
			%dist = getRandom() * 500 + 500;
			%pos = VectorAdd(%pos,getTerrainHeight2( mCos(%angle) * %dist SPC mSin(%angle) * %dist SPC 0));


   %Cpos = %pos;

	%obj.ZType = 1;

   	%Zombie = new player(){
	   Datablock = "ZombieArmor";
   	};
//	%Cpos = vectoradd(vectoradd(vectorscale(%obj.getUpVector(),1.15),"0 0 -1.15"),%obj.getposition());
	%command = "Zombiemovetotarget";


   %zombie.type = %obj.Ztype;
   %Zombie.setTransform(%Cpos);
   %Zombie.team = 99;
   %zombie.canjump = 1;
   %zombie.HasCP = 1;
   %zombie.voice = "Derm1";
   %zombie.voiceTag = addTaggedString( "Derm1" );
   %zombie.player = %Zombie;
//   %zombie.target = createTarget(%zombie, addTaggedString("ZOMBIE!!"), addTaggedString( "Horde" ), addTaggedString( "Derm1" ), '', PlayerSensor, 1.0);
//   setTargetVoice(%zombie.target,addTaggedString( "Derm1" ));
   if(%obj.spawnTypeset == 1)
	%obj.numZ = 3;
   else
      %zombie.CP = %play;
   %zombie.hastarget = 1;
   MissionCleanup.add(%Zombie);
   schedule(1000, %zombie, %command, %zombie);
}

//----------------------------------------------------
// Zombie Spawn Point
//---------------------------------------------------------

$zombie::detectDist = 1000; //Eyes of a... bat...
$zombie::lungDist = 10;
$zombie::LKillDist = 5;
$zombie::Rupvec = 750;
$zombie::killpoints = 5;

datablock AudioProfile(ZombieMoan)
{
   filename    = "fx/environment/growl3.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ZombieHOWL)
{
   filename    = "fx/environment/Yeti_Howl1.wav";
   description = AudioBomb3d;
   preload = true;
};

datablock ParticleData(ZAcidBallParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.5;
   inheritedVelFactor   = 0.5;

   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 200;

   spinRandomMin = -200.0;
   spinRandomMax =  200.0;

   textureName          = "special/bubbles";

   colors[0]     = "0.0 1.0 0.5 0.3";
   colors[1]     = "0.0 1.0 0.4 0.2";
   colors[2]     = "0.0 1.0 0.3 0.1";

   sizes[0]      = 0.6;
   sizes[1]      = 0.3;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ZAcidBallExplosionEmitter)
{
   lifetimeMS       = 200;

   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 3.0;
   velocityVariance = 0.5;

   thetaMin         = 0.0;
   thetaMax         = 90.0;

   orientParticles  = false;
   orientOnVelocity = false;

   particles = "ZAcidBallParticle";
};

datablock ExplosionData(ZAcidBallExplosion)
{
   emitter[0]   = ZAcidBallExplosionEmitter;
   soundProfile = NerfBallExplosionSound;
};

datablock TracerProjectileData(LZombieAcidBall)
{
   doDynamicClientHits = true;

   projectileShapeName = "";
   directDamage        = 0.0;
   directDamageType    = $DamageType::ZAcid;
   hasDamageRadius     = true;
   indirectDamage      = 0.24;
   damageRadius        = 4.0;
   kickBackStrength    = 0.0;
   radiusDamageType    = $DamageType::ZAcid;
   sound          	   = BlasterProjectileSound;
   explosion           = ZAcidBallExplosion;

   dryVelocity       = 100.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 1000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;

   tracerLength    = 5;
   tracerAlpha     = false;
   tracerMinPixels = 3;
   tracerColor     = "0 1 0 1";
	tracerTex[0]  	 = "special/landSpikeBolt";
	tracerTex[1]  	 = "special/landSpikeBoltCross";
	tracerWidth     = 0.5;
   crossSize       = 0.79;
   crossViewAng    = 0.990;
   renderCross     = true;
   emap = true;
};

datablock ParticleData(DemonFBSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;

   lifetimeMS           = 2500;  
   lifetimeVarianceMS   = 500;

   textureName          = "particleTest";

   useInvAlpha =     true;

   spinRandomMin = -60.0;
   spinRandomMax = 60.0;

   colors[0]     = "0.5 0.5 0.5 0.5";
   colors[1]     = "0.4 0.4 0.4 0.2";
   colors[2]     = "0.3 0.3 0.3 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 1.75;
   sizes[2]      = 3.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(DemonFBSmokeEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;

   ejectionVelocity = 0.75;  // A little oomph at the back end
   velocityVariance = 0.2;

   thetaMin         = 0.0;
   thetaMax         = 180.0;

   particles = "DemonFBSmokeParticle";
};

datablock GrenadeProjectileData(DemonFireball)
{
   projectileShapeName = "plasmabolt.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.4;
   damageRadius        = 5.0; // z0dd - ZOD, 8/13/02. Was 20.0
   radiusDamageType    = $DamageType::zombie;
   kickBackStrength    = 1500;

   explosion           = "PlasmaBoltExplosion";
   underwaterExplosion = "PlasmaBoltExplosion";
   velInheritFactor    = 0;
   splash              = PlasmaSplash;
   depthTolerance      = 100.0;
   
   baseEmitter         = DemonFBSmokeEmitter;
   bubbleEmitter       = DemonFBSmokeEmitter;
   
   grenadeElasticity = 0;
   grenadeFriction   = 0.4;
   armingDelayMS     = -1; // z0dd - ZOD, 4/14/02. Was 2000

   gravityMod        = 0.4;  // z0dd - ZOD, 5/18/02. Make mortar projectile heavier, less floaty
   muzzleVelocity    = 50.0; // z0dd - ZOD, 8/13/02. More velocity to compensate for higher gravity. Was 63.7
   drag              = 0;
   sound	     = PlasmaProjectileSound;

   hasLight    = true;
   lightRadius = 10;
   lightColor  = "1 0.75 0.25";

   hasLightUnderwaterColor = true;
   underWaterLightColor = "1 0.75 0.25";
};

datablock StaticShapeData(DeployedZSpawnBase) : StaticShapeDamageProfile {
	className	= "lightbase";
	shapeFile	= "pack_deploy_sensor_motion.dts";

	maxDamage	= 2.5;
	destroyedLevel	= 2.5;
	disabledLevel	= 2.0;

	maxEnergy = 50;
	rechargeRate = 0.25;

	explosion	= HandGrenadeExplosion;
	expDmgRadius	= 1.0;
	expDamage	= 0.05;
	expImpulse	= 200;

	dynamicType			= $TypeMasks::StaticShapeObjectType;
	deployedObject		= true;
	cmdCategory			= "DSupport";
	cmdIcon			= CMDSensorIcon;
	cmdMiniIconName		= "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag		= 'Deployed Zombie Spawner';
	deployAmbientThread	= true;
	debrisShapeName		= "debris_generic_small.dts";
	debris			= DeployableDebris;
	heatSignature		= 0;
	needsPower 			= true;
};

datablock ShapeBaseImageData(ZSpawnDeployableImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = ZSpawnDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedZSpawnBase;
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

datablock ItemData(ZSpawnDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "ZSpawnDeployableImage";
	pickUpName = "a Zombie Spawn pack";
	heatSignature = 0;
	emap = true;
};

datablock ShapeBaseImageData(ZHead)
{
   shapeFile = "bioderm_heavy.dts";
   emap = false;
   mountPoint = 1;
   offset = "0 0.25 -1.5";
   rotation = "1 0 0 15";
};

datablock ShapeBaseImageData(ZBack)
{
   shapeFile = "bioderm_medium.dts";
   emap = false;
   mountPoint = 1;
   offset = "0 0.25 -1.25";
   rotation = "-1 0 0 10";
};

datablock ShapeBaseImageData(ZDummyslotImg)
{
   shapeFile = "turret_muzzlepoint.dts";
   emap = false;
};

datablock ShapeBaseImageData(ZDummyslotImg2)
{
   shapeFile = "turret_muzzlepoint.dts";
   emap = false;
   offset = "-1.5 0 0";
};

datablock ShapeBaseImageData(ZWingImage)
{ 
   shapeFile = "flag.dts"; 
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "0 1 0 90"; // L/R - F/B - T/B
}; 

datablock ShapeBaseImageData(ZWingImage2)
{ 
   shapeFile = "flag.dts"; 
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "0 -1 0 90"; // L/R - F/B - T/B
}; 

datablock ShapeBaseImageData(ZWingAltImage)
{ 
   shapeFile = "flag.dts"; 
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "-0.5 2 0 35"; // L/R - F/B - T/B
}; 

datablock ShapeBaseImageData(ZWingAltImage2)
{ 
   shapeFile = "flag.dts"; 
   mountPoint = 1;

   offset = "0 0 0"; // L/R - F/B - T/B
   rotation = "-0.5 -2 0 35"; // L/R - F/B - T/B
}; 

function ZSpawnDeployableImage::testObjectTooClose(%item) {
	return "";
}
function ZSpawnDeployableImage::testNoTerrainFound(%item) {}
function ZSpawnDeployable::onPickup(%this, %obj, %shape, %amount) {}

function ZSpawnDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "StaticShape";

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
		%item.surfaceNrm2 = %playerVector;
	else
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 -1"));

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		dataBlock = %item.deployed;
	};

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set the recharge rate right away
	if (%deplObj.getDatablock().rechargeRate)
		%deplObj.setRechargeRate(%deplObj.getDatablock().rechargeRate);

	// set team, owner, and handle
	%deplObj.team = %plyr.client.Team;
	%deplObj.setOwner(%plyr);
	%deplObj.light.lightBase = %deplObj;

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

	%deplObj.playThread($PowerThread,"Power");
	%deplObj.playThread($AmbientThread,"ambient");

	// take the deployable off the player's back and out of inventory
	%plyr.unmountImage(%slot);
	%plyr.decInventory(%item.item, 1);

	// set power frequency
	%deplObj.powerFreq = %plyr.powerFreq;

	// Power object
	checkPowerObject(%deplObj);

	if (%plyr.packset==0)
	   %deplobj.ZType=1;
	else if (%plyr.packset==1)
	   %deplobj.ZType=2;
	else if (%plyr.packset==2){
	   %deplobj.ZType=3;
	   %deplobj.numZ=2;
	}
	else if (%plyr.packset==3)
	   %deplobj.Ztype=4;
	else if (%plyr.packset==4)
	   %deplobj.Ztype=5;

	%deplobj.spawnTypeSet = %plyr.expertset;

	return %deplObj;
}

function DeployedZSpawnBase::onDestroyed(%this,%obj,%prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this,%obj,%prevState);
	$TeamDeployedCount[%obj.team, ZSpawnDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500, "delete");
      if (%obj.ZCloop !$= "")
   	   Cancel(%obj.ZCLoop);
}

function DeployedZSpawnBase::disassemble(%data,%plyr,%obj) {
   	if (%obj.ZCloop !$= "")
   	   Cancel(%obj.ZCLoop);
	disassemble(%data,%plyr,%obj);
}

function ZSpawnDeployableImage::onMount(%data, %obj, %node) {
   %obj.hasZSpawn = true;
   %obj.expertset = 0;
}

function ZSpawnDeployableImage::onUnmount(%data, %obj, %node) {
   %obj.hasZSpawn = "";	
}

function DeployedZSpawnBase::onGainPowerEnabled(%data,%obj) {
   if(%obj.spawnTypeSet == 1)
	%obj.numz = 0;
   if (%obj.ZCloop !$= "")
   	Cancel(%obj.ZCLoop);
   %obj.ZCLoop = schedule(1000, 0, "ZcreateLoop", %obj);
   Parent::onGainPowerEnabled(%data,%obj);
}

function DeployedZSpawnBase::onLosePowerDisabled(%data,%obj) {
   if (%obj.ZCloop !$= "")
   	Cancel(%obj.ZCLoop);
   Parent::onLosePowerDisabled(%data,%obj);
}

//************************************************************
//*****************Zomb Creation Stuff************************
//************************************************************

function ZcreateLoop(%obj) 
{ 
   if(isObject(%obj))
   {
	if(%obj.timedout == 0){
	   if(%obj.numZ <= 2 || %obj.numZ $= ""){
		ZPCreateZombie(%obj);
		if(%obj.numZ $= "")
		   %obj.numZ = 0;
		%obj.numZ++;
		%obj.timedout = 1;
		schedule(10000, %obj, "TimedInF", %obj);
	   }
	}
   	%obj.ZCLoop = schedule(2000, 0, "ZcreateLoop", %obj); 
   }
} 

//this is for when a ZSpawn spawns a zombie
function ZPCreateZombie(%obj){
if (!$ZombiesEnabled)
	return;
if ($ZombieNum > $MaxZombies)
return;
$ZombieNum++;
   %Cpos = vectorAdd(%obj.getposition() ,%obj.getUpVector());
   if(%obj.Ztype $= ""){
	%obj.ZType = 1;
   }
   if(%obj.ZType == 1){
   	%Zombie = new player(){
	   Datablock = "ZombieArmor";
   	};
	%Cpos = vectoradd(vectoradd(vectorscale(%obj.getUpVector(),1.15),"0 0 -1.15"),%obj.getposition());
	%command = "Zombiemovetotarget";
   }
   else if(%obj.ZType == 2){
   	%Zombie = new player(){
	   Datablock = "FZombieArmor";
   	};
	%Cpos = vectoradd(vectoradd(vectorscale(%obj.getUpVector(),1.15),"0 0 -1.15"),%obj.getposition());
	%command = "FZombiemovetotarget";
   }
   else if(%obj.ZType == 3){
   	%Zombie = new player(){
	   Datablock = "LordZombieArmor";
   	};
	%Cpos = vectoradd(vectoradd(vectorscale(%obj.getUpVector(),2.4),"0 0 -2.4"),%obj.getposition());
	%Zombie.mountImage(ZHead, 3);
	%Zombie.mountImage(ZBack, 4);
	%Zombie.mountImage(ZDummyslotImg, 5);
	%Zombie.mountImage(ZDummyslotImg2, 6);
	%zombie.firstFired = 0;
	%zombie.canmove = 1;
	%command = "LZombiemovetotarget";
   }
   else if(%obj.ZType == 4){
	%Zombie = new player(){
	   Datablock = "DemonZombieArmor";
	};
	%zombie.mountImage(ZdummyslotImg, 4);
	%Cpos = vectoradd(vectoradd(vectorscale(%obj.getUpVector(),1.3),"0 0 -1.3"),%obj.getposition());
	%command = "DZombiemovetotarget";
   }
   else if(%obj.ZType == 5){
	%Zombie = new player(){
	   Datablock = "RapierZombieArmor";
	};
	%Zombie.mountImage(ZWingImage, 3);
	%Zombie.mountImage(ZWingImage2, 4);
	%zombie.setActionThread("scoutRoot",true);
	%Cpos = vectoradd(vectoradd(vectorscale(%obj.getUpVector(),1),"0 0 -0.6"),%obj.getposition());
	%command = "RZombiemovetotarget";
   }
   %zombie.type = %obj.Ztype;
   %Zombie.setTransform(%Cpos);
   %Zombie.team = 99;
   %zombie.canjump = 1;
   %zombie.HasCP = 1;
   %zombie.voice = "Derm1";
   %zombie.voiceTag = addTaggedString( "Derm1" );
//   %zombie.target = createTarget(%zombie, addTaggedString("ZOMBIE!!"), addTaggedString( "Horde" ), addTaggedString( "Derm1" ), '', PlayerSensor, 1.0);
//   setTargetVoice(%zombie.target,addTaggedString( "Derm1" ));
   if(%obj.spawnTypeset == 1)
	%obj.numZ = 3;
   else
      %zombie.CP = %obj;
   %zombie.hastarget = 1;
   MissionCleanup.add(%Zombie);
   schedule(1000, %zombie, %command, %zombie);
}

//This is for creation of a zombie at a location using the console
function StartAZombie(%pos, %type){
   if(%Type == 1){
   	%Zombie = new player(){
	   Datablock = "ZombieArmor";
   	};
	%command = "Zombiemovetotarget";
   }
   if(%Type == 2){
   	%Zombie = new player(){
	   Datablock = "FZombieArmor";
   	};
	%command = "FZombiemovetotarget";
   }
   if(%Type == 3){
   	%Zombie = new player(){
	   Datablock = "LordZombieArmor";
   	};
	%zombie.client = $zombie::Lclient;
	%Zombie.mountImage(ZHead, 3);
	%Zombie.mountImage(ZBack, 4);
	%Zombie.mountImage(ZDummyslotImg, 5);
	%Zombie.mountImage(ZDummyslotImg2, 6);
	%zombie.firstFired = 0;
	%zombie.canmove = 1;
	%command = "LZombiemovetotarget";
   }
   if(%type == 4){
	%Zombie = new player(){
	   Datablock = "DemonZombieArmor";
	};
	%zombie.mountImage(ZdummyslotImg, 4);
	%command = "DZombiemovetotarget";
   }
   %zombie.type = %type;
   %Zombie.setTransform(%pos);
   %Zombie.team = 0;
   %zombie.canjump = 1;
   %zombie.hastarget = 1;
   MissionCleanup.add(%Zombie);
   schedule(1000, %zombie, %command, %zombie);
}

//This is for when someone is killed by a zombie and spawns a new one
function CreateZombie(%obj){
if (!$ZombiesEnabled)
	return;
   %Cpos = %obj.getposition();
   %Zombie = new player(){
	Datablock = "ZombieArmor";
   };
   %Zombie.setTransform(%Cpos);
   %zombie.type = 1;
   %Zombie.team = 0;
   %zombie.canjump = 1;
   %zombie.hastarget = 1;
   MissionCleanup.add(%Zombie);
   schedule(1000, %zombie, "Zombiemovetotarget", %zombie);
}

//************************************************************
//*******************Zomb AI Stuff****************************
//************************************************************

function Zombiemovetotarget(%zombie){
   if(!isobject(%Zombie))
	return;
   if(%Zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){ 
	if(%zombie.hastarget != 1){
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
	   ZDoMoan(%zombie);

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	%fmultiplier = $Zombie::ForwardSpeed;
	if(%closestDistance <= $zombie::lungDist && %zombie.canjump == 1)
	   %fmultiplier = (%fmultiplier * 4);
	%vector = vectorscale(%vector, %Fmultiplier);
	%upvec = "150";
	if(%closestDistance <= $zombie::lungDist && %zombie.canjump == 1){
	   %upvec = %upvec * 2;
	   %zombie.canjump = 0;
	   schedule(1500, %zombie, "Zsetjump", %zombie);
	}
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= 600)
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else {
	%zombie.getDataBlock().damageObject( %obj, 0,pos( %obj),999999,$DamageType::Explosion);
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "Zombiemovetotarget", %zombie);
}

function FZombiemovetotarget(%zombie){
   if(!isobject(%Zombie))
	return;
   if(%Zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){ 
	if(%zombie.hastarget != 1){
	   %zombie.hastarget = 1;
	}
	%zombie.setActionThread("scoutRoot",true);
	%upvec = "250";
	%fmultiplier = $Zombie::FForwardSpeed;

	//moanStuff
	%chance = (getrandom() * 50);
   	if(%chance >= 49)
	   ZDoMoan(%zombie);

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	//Move Stuff
	if(%closestDistance <= $zombie::lungDist && %zombie.canjump == 1 && getword(%vector, 2) <= "0.8" ){
	   %zombie.setvelocity("0 0 0");
	   %fmultiplier = (%fmultiplier * 2);
	   %upvec = (%upvec * 3.5);
	   %zombie.canjump = 0;
	   schedule(2000, %zombie, "Zsetjump", %zombie);
	}
	%vector = vectorscale(%vector, %Fmultiplier);
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= "1200" && %zombie.canjump == 1){
	   %zombie.setvelocity("0 0 0");
	   %upvec = (%upvec * 8);
	   %x = (%x * 0.5);
	   %y = (%y * 0.5);
	   %zombie.canjump = 0;
	   schedule(2500, %zombie, "Zsetjump", %zombie);
	}

	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   	%zombie.setActionThread("ski",true);
   }
   %zombie.moveloop = schedule(500, %zombie, "FZombiemovetotarget", %zombie);
}

function LZombiemovetotarget(%zombie){
   if(!isobject(%Zombie))
	return;
   if(%Zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist && %zombie.canmove != 0){ 
	if(%zombie.hastarget != 1){
	   LZDoYell(%zombie);
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
	   LZDoMoan(%zombie);

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);
	if(%zombie.ATCount >= 20){
	   %zombie.ATCount = 0;
	   %zombie.nomove = 1;
   	   %zombie.Fire = schedule(1250, %zombie, "ZFire", %zombie, %closestClient);
	   %zombie.Charge = schedule(500, %zombie, "ChargeEmitter", %zombie);
	   %zombie.chargecount = 0;
	}
	if(%zombie.nomove != 1) {
	%fmultiplier = $Zombie::LForwardSpeed;
	%upvec = "150";
	if(%closestDistance <= $zombie::LKillDist && %zombie.canjump == 1){
	   %vec = vectoradd(%pos,vectorScale(%vector,($zombie::LkillDist - 1.6)));
	   %mask = $TypeMasks::InteriorObjectType | $TypeMasks::StaticShapeObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::TerrainObjectType | $TypeMasks::PlayerObjectType;
	   %searchResult = containerRayCast(vectoradd(%pos,vectorScale(%vector,1.6)), %vec, %mask, %zombie);
	   if(%searchResult){
		%searchObj = getWord(%searchResult,0);
		if(%searchObj $= %closestClient){
		   %chance = getrandom(1,5);
		   if(%chance == 1){
			%dir = %zombie.getEyeVector();
			%dir = vectornormalize(getword(%dir,0)@" "@getword(%dir,1)@" 0");
			%dir = vectoradd(vectorscale(%dir,7500),"0 0 1000");
			%closestclient.applyimpulse(%clpos,%dir);
			%closestclient.damage(0, %clpos, 0.8, $DamageType::ZombieL);
		   }
		   else{
	   		%zombie.setvelocity("0 0 0");
	   		%zombie.canjump = 0;
	   		schedule(1000, %zombie, "Zsetjump", %zombie);
	   		Llifttarget(%zombie,%closestclient,0);
		   }
		}
	   }
	}
	else if(%closestDistance <= ($zombie::LKillDist + $zombie::lungDist)){
	   %zombie.setvelocity("0 0 0");
	   %fmultiplier = (%fmultiplier * 2.5);
	}
	%vector = vectorscale(%vector, %Fmultiplier);
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= 4000)
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
	%zombie.ATCount++;
	}
   }
   else if(%zombie.hastarget == 1 && %zombie.canmove != 0){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "LZombiemovetotarget", %zombie);
}

function DZombiemovetotarget(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){ 
	if(%zombie.hastarget != 1 && %closestdistance >= 10 && %closestdistance <= 150){
	   DzombieFire(%zombie,%closestclient);
	   %zombie.canjump = 0;
	   schedule(4000, %zombie, "Zsetjump", %zombie);
	}
	if(%zombie.hastarget != 1){
	   LZDoYell(%zombie);
	   %zombie.hastarget = 1;
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
	   LZDoMoan(%zombie);

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	if (%closestdistance >= 10 && %closestdistance <= 150 && %zombie.canjump == 1){
	   DzombieFire(%zombie,%closestclient);
	   %zombie.canjump = 0;
	   schedule(4000, %zombie, "Zsetjump", %zombie);
	}
	%vector = vectorscale(%vector, $Zombie::DForwardSpeed);
	%upvec = "150";
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%z = Getword(%vector,2);
	if(%z >= ($Zombie::DForwardSpeed / 3 * 2))
	   %upvec = (%upvec * 5);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
   }
   else if(%zombie.hastarget == 1){
	%zombie.hastarget = 0;
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
   }
   %zombie.moveloop = schedule(500, %zombie, "DZombiemovetotarget", %zombie);
}

function RZombiemovetotarget(%zombie){
   if(!isobject(%Zombie))
	return;
   if(%Zombie.getState() $= "dead")
	return;
   %pos = %zombie.getworldboxcenter();
   %zombie.setActionThread("scoutRoot",true);
   %closestClient = ZombieLookForTarget(%zombie);
   %closestDistance = getWord(%closestClient,1);
   %closestClient = getWord(%closestClient,0).Player;
   if(%closestDistance <= $zombie::detectDist){ 
	if(%zombie.wingset == 1){
	   %zombie.wingset = 0;
	   %Zombie.mountImage(ZWingImage, 3);
	   %Zombie.mountImage(ZWingImage2, 4);
	}
	else{
	   %zombie.wingset = 1;
	   %Zombie.mountImage(ZWingaltImage, 3);
	   %Zombie.mountImage(ZWingaltImage2, 4);
	}
	%chance = (getrandom() * 20);
   	if(%chance >= 19)
	   ZDoMoan(%zombie);
	if(%zombie.iscarrying == 1){
	   %vector = vectorscale(%zombie.getForwardVector(),($Zombie::RForwardSpeed / 2));
	   %vector = getword(%vector, 0)@" "@getword(%vector, 1)@" "@($zombie::Rupvec * 1.5);
	   %zombie.applyImpulse(%zombie.getposition(), %vector);
	}
     else{

      %vector = ZgetFacingDirection(%zombie,%closestClient,%pos);

	%z = Getword(%vector,2);
	%spd = vectorLen(%zombie.getVelocity());
	%fallpoint = 0.05 - (%spd / 630);
	if(%closestdistance <= 15 || %z > (0.25 + %fallpoint) || %z < (-1 * (0.25 + %fallpoint))){
	   if(%z < 0)
	      %upvec = ($zombie::Rupvec * (%z - (%spd / 130)));
	   if(%z >= 0)
		%upvec = ($zombie::Rupvec * (%z + 1));
	   if(%spd <= 5)
		%vector = vectorScale(%vector,3);
	}
	else{
	   %upvec = $zombie::Rupvec * (%z + 1.2);
	   %spdmod = 1;
	}
	if(%z < 0)
	   %z = %z * -1;

	%Zz = getWord(%zombie.getVelocity(),2);
	if(%Zz <= -40){
         %result = containerRayCast(%pos, vectoradd(%pos,vectorScale("0 0 1",%Zz * 2)), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType, %zombie);
	   if(%result)
		%upvec = $zombie::Rupvec * 5;
	}

	%vector = vectorscale(%vector, ($Zombie::RForwardSpeed * (1 - %z)));
	%x = Getword(%vector,0);
	%y = Getword(%vector,1);
	%vector = %x@" "@%y@" "@%upvec;
	%zombie.applyImpulse(%pos, %vector);
     }
   }
   %zombie.moveloop = schedule(500, %zombie, "RZombiemovetotarget", %zombie);
}

function ZombieLookforTarget(%zombie){
   %wbpos = %zombie.getworldboxcenter();
   %count = ClientGroup.getCount();
   %closestClient = -1;
   %closestDistance = 32767;
   for(%i = 0; %i < %count; %i++)
   {
	%cl = ClientGroup.getObject(%i);
	if(isObject(%cl.player)){
	   %testPos = %cl.player.getWorldBoxCenter();
	   %distance = vectorDist(%wbpos, %testPos);
	   if (%distance > 0 && %distance < %closestDistance && %cl.player.isFTD != 1 && %cl.player.iszombie != 1)
	   {
	   	%closestClient = %cl;
	   	%closestDistance = %distance;
	   }
	}
   }
   return %closestClient SPC %closestDistance;
}

function ZgetFacingDirection(%zombie,%closestClient,%pos){
   %clpos = %closestClient.getPosition(); 
   %vector = vectorNormalize(vectorSub(%clpos, %pos)); 
   %v1 = getword(%vector, 0);
   %v2 = getword(%vector, 1);
   %nv1 = %v2;
   %nv2 = (%v1 * -1);
   %none = 0;
   %vector2 = %nv1@" "@%nv2@" "@%none;
   %zombie.setRotation(fullrot("0 0 0",%vector2));

   return %vector;
}

//************************************************************
//*****************Zomb Attack Stuff**************************
//************************************************************

function ChargeEmitter(%zombie){
   if(!isobject(%zombie))
	return;
   if(%zombie.chargecount >= 2){
   	%charge2 = new ParticleEmissionDummy() 
   	{ 
   	   position = %zombie.getMuzzlePoint(6);
   	   dataBlock = "defaultEmissionDummy"; 
   	   emitter = "FlameEmitter"; 
      }; 
	MissionCleanup.add(%charge2);
	%charge2.schedule(100, "delete");
   }
   if(%zombie.chargecount <= 7){
   	%charge = new ParticleEmissionDummy() 
   	{ 
   	   position = %zombie.getMuzzlePoint(5);
   	   dataBlock = "defaultEmissionDummy"; 
   	   emitter = "FlameEmitter";  
      };
	MissionCleanup.add(%charge);
	%charge.schedule(100, "delete");
   }
   if(%zombie.chargecount <= 9){
      %zombie.Fire = schedule(100, %zombie, "ChargeEmitter", %zombie);
	%zombie.chargecount++;
   }
   else
	%zombie.chargecount = 0;
}

function ZFire(%zombie, %target){
   if(isobject(%zombie) && isobject(%target)){
   	if(%Zombie.firstFired == 1){
         %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(6));
	   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100)); 
	   %zombie.firstFired = 0;
   	   %zombie.nomove = 0;
   	   %p = new TracerProjectile() 
   	   { 
   	   	dataBlock        = LZombieAcidBall; 
   	   	initialDirection = %vec; 
   	   	initialPosition  = %zombie.getMuzzlePoint(6); 
   	   	sourceObject     = %zombie; 
   	   	sourceSlot       = 6; 
   	   };
   	}
   	else{
         %vec = vectorsub(%target.getworldboxcenter(),%zombie.getMuzzlePoint(5));
	   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100)); 
   	   %p = new TracerProjectile() 
   	   { 
   	   	dataBlock        = LZombieAcidBall; 
   	   	initialDirection = %vec; 
   	   	initialPosition  = %zombie.getMuzzlePoint(5); 
   	   	sourceObject     = %zombie; 
   	   	sourceSlot       = 5; 
   	   }; 
	   %zombie.firstFired = 1;
   	   %zombie.Fire = schedule(250, %zombie, "ZFire", %zombie, %target);
      }
   }
   else{
	%zombie.firstFired = 0;
	%zombie.nomove = 0;
   }
}

function Llifttarget(%zombie,%closestclient,%count){
   if(%count == 0)
	%zombie.canmove = 0;
   if(%closestclient.getState() $= "dead" || %Zombie.getState() $= "dead"){
	%zombie.canmove = 1;
	return;
   }
   %target = %closestclient;
   if(!isobject(%target)){
	%zombie.canmove = 1;
	return;
   }
   %pos = %zombie.getworldboxcenter();
   %Tpos = %target.getworldboxcenter();
   %ZtoT = vectorsub(%tpos,%pos);
   if (%count <= 12){
	%newpos = vectoradd(%ZtoT,vectoradd(%pos,"0 0 -0.6"));
	%target.setTransform(%newpos);
	%target.setvelocity("0 0 0");
   }
   else {
	%killtype = getrandom(1,2);
	if(%killtype == 1){
	   %closestwall = 20;
	   %nv2 = (getword(%ZtoT, 0) * -1);
	   %nv1 = getword(%ZtoT, 1);
	   %vector1 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"),20);
	   %nvector1 = vectoradd(%tpos,%vector1);
	   %nv2 = getword(%ZtoT, 0);
	   %nv1 = (getword(%ZtoT, 1) * -1);
	   %vector2 = vectorscale(vectornormalize(%nv1@" "@%nv2@" 0"),20);
	   %nvector2 = vectoradd(%tpos,%vector2);
	   %searchresultR = containerRayCast(%tpos, %nvector1, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
	   %searchresultL = containerRayCast(%tpos, %nvector2, $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::VehicleObjectType);
	   if(%searchresultR){
		%Hpos = getword(%searchresultR,1)@" "@getword(%searchresultR,2)@" "@getword(%searchresultR,3);
		%distance = vectordist(%Tpos, %Hpos);
		if(%distance <= %closestwall){
		   %closestwall = %distance;
		   %vector = vectoradd(vectorscale(%vector1,1500),"0 0 100");
		}
	   }
	   if(%searchresultL){
		%Hpos = getword(%searchresultL,1)@" "@getword(%searchresultL,2)@" "@getword(%searchresultL,3);
		%distance = vectordist(%Tpos, %Hpos);
		if(%distance <= %closestwall){
		   %closestwall = %distance;
		   %vector = vectoradd(vectorscale(%vector2,1500),"0 0 100");
		}
	   }
	   if(%closestwall == 20){
	   	%x = getword(%ZtoT, 0);
	   	%y = getword(%ZtoT, 1);
		%outvec = vectorscale(vectornormalize(%x@" "@%y@" 0"),50);
		%vector = vectoradd("0 0 -15000",%outvec);
	   }
	   %target.applyimpulse(%target.getposition(),%vector);
	}
	else if(%killtype == 2){
	   %target.infected = 1;
	   %target.damage(0, %target.getposition(), 10.0, $DamageType::ZombieL);
	}
	%count = 0;
	%zombie.canmove = 1;
	return;
   }
   %count++;
   %zombie.killingplayer = schedule(150, %zombie, "Llifttarget", %zombie, %closestclient, %count);
}


function DZombieFire(%zombie,%closestclient){
   %pos = %zombie.getMuzzlePoint(4);
   %tpos = %closestclient.getWorldBoxCenter();
   %tvel = %closestclient.getvelocity();
   %vec = vectorsub(%tpos,%pos);
   %dist = vectorlen(%vec);
   %velpredict = vectorscale(%tvel,(%dist / 50));
   %vector = vectoradd(%vec,%velpredict);
   %ndist = vectorlen(%vector);
   %upvec = "0 0 "@((%ndist / 50) * (%ndist / 50) * 2);
   %vector = vectornormalize(vectoradd(%vector,%upvec));
   %p = new GrenadeProjectile() 
   { 
	dataBlock        = DemonFireball; 
	initialDirection = %vector; 
	initialPosition  = %pos; 
	sourceObject     = %zombie; 
	sourceSlot       = 4; 
   };
}

function RkillLoop(%zombie,%target,%count){
   if(!isObject(%zombie)){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if(!isObject(%target)){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if (%Zombie.getState() $= "dead"){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if (%target.getState() $= "dead"){
	%zombie.iscarrying = 0;
	%target.grabbed = 0;
	return;
   }
   if(%count == 50){
	%chance = getRandom(1,3);
	if(%chance == 3)
	   %target.damage(0, %tpos, 10.0, $DamageType::Zombie);
	else{
	   %target.isFTD = 1;
	   if(%target.getMountedImage($Backpackslot) !$= "")
   	      %target.throwPack();
   	   %target.finishingfall = schedule(5000, 0, "stopFTD", %target);
	}
	%zombie.iscarrying = 0;
	return;
   }
   %target.setPosition(vectoradd(%zombie.getPosition(),"0 0 -4"));
   %target.setVelocity(%zombie.getVelocity());
   %count++;
   %zombie.killingplayer = schedule(100, 0, "RkillLoop", %zombie, %target, %count);
}

//************************************************************
//*******************MISC Zomb Functions**********************
//************************************************************

function ZSetRandomMove(%zombie){
   if (!isobject(%zombie))
	return;
   %RX = getrandom(-10, 10);
   %RY = getrandom(-10, 10);
   %RZ = "0";
   %vector = %RX@" "@%RY@" "@%RZ;
   %zombie.direction = vectornormalize(%vector);
   %zombie.Mnum = getrandom(1, 20);
   %zombie.zombieRmove = schedule(500, %zombie, "ZrandommoveLoop", %zombie);
}

function ZrandommoveLoop(%zombie){
   if (!isobject(%zombie))
	return;
   if (%Zombie.getState() $= "dead")
	return;
   if (%zombie.hastarget == 1){
	%zombie.direction = "";
	return;
   }
   if (%zombie.Mnum >= 1){
	%X = getword(%zombie.direction, 1);
	%Y = (getword(%zombie.direction, 0) * -1);
	%none = 0;
	%vector = %X@" "@%Y@" "@%none;
	%zombie.setRotation(fullrot("0 0 0",%vector));
	if(%zombie.type == 1)
	   %speed = ($zombie::forwardspeed);
	else if(%zombie.type == 2)
	   %speed = ($zombie::Fforwardspeed * 0.6);
	else if(%zombie.type == 4)
	   %speed = ($zombie::Dforwardspeed * 0.75);
	else if(%zombie.type == 3)
	   %speed = ($zombie::Lforwardspeed * 0.8);
	%vector = vectorscale(%zombie.direction, %speed);
	%zombie.applyimpulse(%zombie.getposition(), %vector);
	%zombie.Mnum = (%zombie.Mnum - 1);
	%zombie.zombieRmove = schedule(500, %zombie, "ZrandommoveLoop", %zombie);
   }
   else if(%zombie.Mnum == 0)
	%zombie.zombieRmove = schedule(100, %zombie, "ZSetRandomMove", %zombie);
}

function ZkillUpdateScore(%game, %client,%implement){
   if( %implement.getClassName() $= "Turret")
	%client = %implement.getControllingClient();
   else if(%implement.getDataBlock().catagory $= "Vehicles") 
	%client = %implement.getControllingClient(); 
   %client.Zkills++;
   %game.recalcScore(%client);
}

function zombieSpawnLoop(%pos, %radius, %freq){
   if(%freq > 10)
	%freq = 10;
   if(%freq < 1)
	%freq = 1;
   %chance = getRandom(1,50);
   if(%chance <= %freq && $numspawnedzombies < (%freq * 5)){
	%spawnPos = vectorAdd(%pos,(getRandom(0,%radius) - (%radius / 2))@" "@(getRandom(0,%radius) - (%radius / 2))@" getRandom(0,15)");
	%search = containerRayCast(%spawnPos, vectorAdd(%spawnPos,"0 0 -1000"), $TypeMasks::StaticShapeObjectType | $TypeMasks::InteriorObjectType | $TypeMasks::ForceFieldObjectType | $TypeMasks::TerrainObjectType);
	if(%search)
	   %spawnPos = getWord(%search,1)@" "@getWord(%search,2)@" "@getWord(%search,3);
	%chance = getRandom(1,(60 + %freq));
      if(%chance <= 25){
   	   %Zombie = new player(){
	      Datablock = "ZombieArmor";
   	   };
	   %Ztype = 1;
	   %command = "Zombiemovetotarget";
	}
      else if(%chance <= 35){
   	   %Zombie = new player(){
	      Datablock = "FZombieArmor";
   	   };
	   %Ztype = 2;
	   %command = "FZombiemovetotarget";
	}
      else if(%chance <= 50){
   	   %Zombie = new player(){
	      Datablock = "DemonZombieArmor";
   	   };
	   %zombie.mountImage(ZdummyslotImg, 4);
	   %Ztype = 4;
	   %command = "DZombiemovetotarget";
	}
      else if(%chance <= 60){
   	   %Zombie = new player(){
	      Datablock = "RapierZombieArmor";
   	   };
	   %Zombie.mountImage(ZWingImage, 3);
	   %Zombie.mountImage(ZWingImage2, 4);
	   %zombie.setActionThread("scoutRoot",true);
	   %Ztype = 5;
	   %command = "RZombiemovetotarget";
	}
      else if(%chance <= 66){
   	   %Zombie = new player(){
	      Datablock = "LordZombieArmor";
   	   };
	   %Zombie.mountImage(ZHead, 3);
	   %Zombie.mountImage(ZBack, 4);
	   %Zombie.mountImage(ZDummyslotImg, 5);
	   %Zombie.mountImage(ZDummyslotImg2, 6);
	   %zombie.firstFired = 0;
	   %zombie.canmove = 1;
	   %Ztype = 3;
	   %command = "LZombiemovetotarget";
	}
      if(%chance > 66)
	   DemonMotherCreate(%spawnpos);
	else {
         %zombie.type = %Ztype;
         %Zombie.setTransform(%spawnPos);
         %Zombie.team = 0;
         %zombie.canjump = 1;
         %zombie.hastarget = 1;
	   %zombie.randspawnerstarted = 1;
         MissionCleanup.add(%Zombie);
         schedule(1000, %zombie, %command, %zombie);
	   $numspawnedzombies++;
	}
   }
   $zombieloop = schedule(500, 0, "zombieSpawnLoop", %pos, %radius, %freq);
}

function InfectLoop(%obj){
   if (%obj.getState() $= "dead")
	return;
   if(isObject(%obj)){
	if(%obj.beats $= "")
	   zombieAttackImpulse(%obj,0);
	if(%obj.beats < 15)
         %obj.setWhiteOut(%obj.beats * 0.05);
	else
	   %obj.setDamageFlash(1);
	if(%obj.beats == 15){
	   %obj.canZkill = 1;
	}
	if(%obj.beats >=15)
	   serverPlay3d("ZombieMoan",%obj.getWorldBoxCenter()); 
	else if (%obj.beats >= 10)
	   playDeathCry(%obj);
	else
	   playPain(%obj);
	if(%obj.beats == 20){
	   if($host::canZombie $= "")
		$host::canZombie = 0;
	   if($host::canZombie == 1)
	      makePersonZombie(%obj.getTransform(),%obj.client);
	   else
		%obj.damage(0, %obj.getposition(), 10.0, $DamageType::Zombie);
	   return;
	}
      %obj.beats++;
	%obj.infectedDamage = schedule(2000, %obj, "InfectLoop", %obj); 
   }
}

function makePersonZombie(%trans, %client){
   %client.player.delete();

   %player = new Player() {
      dataBlock = "ZombieArmor";
   };

   %player.setTransform( %trans );
   MissionCleanup.add(%player);

   // setup some info
   %player.setOwnerClient(%client);
   %player.team = 0;
   %client.outOfBounds = false;
   %player.setEnergyLevel(0);
   %client.player = %player;

   // updates client's target info for this player
   %player.setTarget(%client.target);
   setTargetDataBlock(%client.target, %player.getDatablock());
   setTargetSensorData(%client.target, PlayerSensor);
   setTargetSensorGroup(%client.target, 1);
   %client.setSensorGroup(1);

   %client.setControlObject(%player);
   %player.client.clearBackpackIcon();

   %player.iszombie = 1;

   ZombieBloodLust(%player,0);
   zombieAttackImpulse(%player,80);
}

function ZombieBloodLust(%obj, %count){
   if(!isObject(%obj))
	return;
   if (%obj.getState() $= "dead")
	return;
   %obj.setDamageFlash(0.5);
   if(%count == 10){
	serverPlay3d("ZombieMoan",%obj.getWorldBoxCenter()); 
	%count = 0;
   }
   %count++;
   schedule(200, %obj, "ZombieBloodLust", %obj, %count); 
}

function zombieAttackImpulse(%obj, %count){
   if(!isObject(%obj))
	return;
   if (%obj.getState() $= "dead")
	return;
   %pos = %obj.getposition();
   InitContainerRadiusSearch(%pos, %count, $TypeMasks::PlayerObjectType);
   while ((%objtarget = containerSearchNext()) != 0){
	if(isObject(%objtarget) && %objtarget !$= %obj){
	   %objarmortype = %objtarget.getdatablock().getname();
	   if(%objarmortype $= "ZombieArmor" || %objarmortype $= "FZombieArmor" || %objarmortype $= "LordZombieArmor" || %objarmortype $= "DemonZombieArmor" || %objarmortype $= "RapierZombieArmor")
	      %objiszomb = 1;
	   if(!(%objiszomb) && %objtarget.iszombie != 1){
		%vec = vectorNormalize(vectorSub(%objtarget.getWorldBoxCenter(),%obj.getWorldBoxCenter()));
		if(vectorDist(%vec,%obj.getForwardVector()) <= 0.5){
		   if(%count <= 200){
		      %impulse = (%count / 20) * 90;
		      %up = (%count / 100) * 90;
		   }
		   else{
		      %impulse = 900;
		      %up = 180;
		   }
		   %vec = vectorScale(%vec,%impulse);
		   %vec = vectorAdd(%vec,"0 0 "@%up);
		   %obj.applyimpulse(%obj.getPosition(),%vec);
		   %count++;
   		   schedule(500, 0, "zombieAttackImpulse", %obj, %count);
		   return;
		}
         }
      }
	%objiszomb = 0;
   }
   %count++;
   schedule(500, 0, "zombieAttackImpulse", %obj, %count);
}

function TimedInF(%obj){
   %obj.timedout = 0;
}
function stopFTD(%target){
   %target.isFTD = 0;
   %target.grabbed = 0;
}
function StopZombieSpawnLoop(){
   cancel($zombieloop);
}
function ZDoMoan(%zombie){
   %chance = (getRandom() * 12);
   if(%chance <= 11)
	serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter()); 
   else
	serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter()); 
}
function LZDoMoan(%zombie){
   serverPlay3d("ZombieMoan",%zombie.getWorldBoxCenter()); 
}
function LZDoYell(%zombie){
   serverPlay3d("ZombieHOWL",%zombie.getWorldBoxCenter());  
}
function Zsetjump(%zombie){
   %zombie.canjump = 1;
}

//---------------------------------------------------------------------
//------------------------DEMON MOTHER STUFF---------------------------
//---------------------------------------------------------------------

datablock SeekerProjectileData(DMMissile)
{
   casingShapeName     = "weapon_missile_casement.dts";
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 5.0;
   radiusDamageType    = $DamageType::Zombie;
   kickBackStrength    = 2000;

   explosion           = "MissileExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = MortarSmokeEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 10000; // z0dd - ZOD, 4/14/02. Was 6000
   muzzleVelocity      = 10.0;
   maxVelocity         = 35.0; // z0dd - ZOD, 4/14/02. Was 80.0
   turningSpeed        = 23.0;
   acceleration        = 15.0;

   proximityRadius     = 2.5;

   terrainAvoidanceSpeed = 10;
   terrainScanAhead      = 7;
   terrainHeightFail     = 1;
   terrainAvoidanceRadius = 3; 
   
   flareDistance = 40;
   flareAngle    = 20;
   minSeekHeat   = 0.0;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";

   useFlechette = true;
   flechetteDelayMs = 250;
   casingDeb = FlechetteDebris;

   explodeOnWaterImpact = false;
};

datablock LinearFlareProjectileData(DMPlasma)
{
   doDynamicClientHits = true;

   directDamage        = 0;
   directDamageType    = $DamageType::Zombie;
   hasDamageRadius     = true;
   indirectDamage      = 0.8;  // z0dd - ZOD, 4/25/02. Was 0.5
   damageRadius        = 15.0;
   kickBackStrength    = 1500;
   radiusDamageType    = $DamageType::Zombie;
   explosion           = MortarExplosion;
   splash              = PlasmaSplash;

   dryVelocity       = 85.0; // z0dd - ZOD, 4/25/02. Was 50. Velocity of projectile out of water
   wetVelocity       = -1;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 2500; // z0dd - ZOD, 4/25/02. Was 6000
   explodeOnDeath    = true;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = -1;

   activateDelayMS = 100;

   scale             = "3.0 3.0 3.0";
   numFlares         = 30;
   flareColor        = "0.1 0.3 1.0";
   flareModTexture   = "flaremod";
   flareBaseTexture  = "flarebase";
};

function DemonMotherCreate(%pos){
   %obj = new player(){
	Datablock = "DemonMotherZombieArmor";
   };
   %obj.setTransform(%pos);
   %obj.team = 0;
   MissionCleanup.add(%obj);
   schedule(1000, 0, "DemonMotherInitiate", %obj);
}

function DemonMotherInitiate(%obj){
   if(!isObject(%obj))
	return;
   DemonMotherDefense(%obj);
   DemonMotherThink(%obj);
   %obj.mountImage(ZdummyslotImg, 4);
   %obj.justshot = 0;
   %obj.justmelee = 0;
}

function DemonMotherThink(%obj){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   %pos = %obj.getposition();
   %count = ClientGroup.getCount();
   %closestClient = -1;
   %closestDistance = 32767;
   for(%i = 0; %i < %count; %i++)
   {
	%cl = ClientGroup.getObject(%i);
	if(isObject(%cl.player)){
	   %testPos = %cl.player.getWorldBoxCenter();
	   %distance = vectorDist(%pos, %testPos);
	   if (%distance > 0 && %distance < %closestDistance && %cl.player.isFTD != 1 && %cl.player.iszombie != 1)
	   {
	   	%closestClient = %cl;
	   	%closestDistance = %distance;
	   }
	}
   }
   if(%closestClient != -1){
	%searchobject = %closestclient.player;
	%dist = vectorDist(%pos,%searchobject.getPosition());
	if(%dist <= 100){
	   if(%dist <= 50){		//ok were now in combat mode, lets decide on what we should do, move attack, or shoot.
		if(%obj.justmelee == 1){	 //if we just used a melee attack, maybe we should follow it up with a shot attack.
		   if(%dist >= 10){	//good were far enough away, lets shoot em.
			%rand = getrandom(1,3);
			if(%rand <= 2)
			   DemonMotherSpermAttack(%obj,%searchobject);
			else
			   DemonMotherFireRainAttack(%obj,%searchobject);
		   }
		   else			//damn, to close, ok lung at him
			DemonMotherLungAttack(%obj,%searchobject);
		}
		else{
		   %rand = getRandom(1,5); //ok so theres 3 good possible attacks here, so lets get a random variable and decide what to do.
		   if(%rand == 1)
			DemonMotherPlasmaAttack(%obj,%searchobject);
		   else if(%rand <= 3)
			DemonMotherStrafeAttack(%obj,%searchobject);
		   else
			DemonMotherFlyAttack(%obj,%searchobject);
		}
	   }
	   else{		//ok, were to far away, maybe we should shoot at them.
		if(%obj.justshot == 1)		//humm we just attacked, ok, let charge him, get in close
		   DemonMotherChargeIn(%obj,%searchobject);
		else{			//were good to fire, FIRE AWAY!
		   %rand = getRandom(1,5);	//ok so theres 3 good possible attacks here, so lets get a random variable and decide what to do.
		   if(%rand == 1)
			DemonMotherFireRainAttack(%obj,%searchobject);
		   else if(%rand <= 3)
			DemonMotherMissileAttack(%obj,%searchobject);
		   else
			DemonMotherSpermAttack(%obj,%searchobject);
		}
	   }
	}
	else if(%dist > 200){
	   %rand = getrandom(1,120);
	   if(%rand == 94)		//please, dont ask why i choose this number, it just poped in my head
		DemonMotherDemonSpawn(%obj);
	   else
		DemonMotherMoveToTarget(%obj,%searchobject);
	}
	else
	   DemonMotherMoveToTarget(%obj,%searchobject);

	%obj.justshot = 0;
	%obj.justmelee = 0;
   }
   else{
	schedule(500, 0, "DemonMotherThink", %obj);
   }
}

function DemonMotherDefense(%obj){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   %pos = %obj.getposition();
   InitContainerRadiusSearch(%pos, 250, $TypeMasks::ProjectileObjectType);
   while ((%searchObject = containerSearchNext()) != 0){
	%projpos = %searchobject.getPosition();
	%dist = vectorDist(%pos,%projpos);
	if(%dist <= 100){
	   if(%searchobject.lastpos)
		%searchobject.delete();
	}
	else
	   %searchobject.lastpos = %projpos;
   }
   schedule(50, "DemonMotherDefense", %obj);
}

function DemonMotherSpermAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(!isObject(%target)){
	DemonMotherThink(%obj);
	return;
   }
   FaceTarget(%obj,%target);
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   %charge = new ParticleEmissionDummy() 
   { 
   	position = %obj.getMuzzlePoint(4);
   	dataBlock = "defaultEmissionDummy"; 
   	emitter = "FlameEmitter"; 
   }; 
   MissionCleanup.add(%charge);
   %charge.schedule(100, "delete");

   	if(%obj.chargecount == 7){
         %vec = vectorsub(%target.getworldboxcenter(),%obj.getMuzzlePoint(4));
	   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100)); 
   	   %p = new TracerProjectile() 
   	   { 
   	   	dataBlock        = LZombieAcidBall; 
   	   	initialDirection = %vec; 
   	   	initialPosition  = %obj.getMuzzlePoint(4); 
   	   	sourceObject     = %obj; 
   	   	sourceSlot       = 6; 
   	   };
   	}
   	if(%obj.chargecount == 9){
         %vec = vectorsub(%target.getworldboxcenter(),%obj.getMuzzlePoint(4));
	   %vec = vectoradd(%vec, vectorscale(%target.getvelocity(),vectorlen(%vec)/100)); 
   	   %p = new TracerProjectile() 
   	   { 
   	   	dataBlock        = LZombieAcidBall; 
   	   	initialDirection = %vec; 
   	   	initialPosition  = %obj.getMuzzlePoint(4); 
   	   	sourceObject     = %obj; 
   	   	sourceSlot       = 6; 
   	   };
   	}

   if(%obj.chargecount <= 9){
      schedule(100, 0, "DemonMotherSpermAttack", %obj, %target);
	%obj.chargecount++;
   }
   else{
	%obj.chargecount = 0;
	%obj.justshot = 1;
	DemonMotherThink(%obj);
   }
}

function DemonMotherLungAttack(%obj,%target){
   FaceTarget(%obj,%target);
   %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition())); 
   %vector = vectorscale(%vector, 4000);
   %x = Getword(%vector,0);
   %y = Getword(%vector,1);
   %z = Getword(%vector,2);
   %vector = %x@" "@%y@" 400";
   %obj.applyImpulse(%obj.getPosition(), %vector);

   %obj.justmelee = 1;
   schedule(750, 0, "DemonMotherThink", %obj);
}

function DemonMotherStrafeAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 8){
	%obj.setVelocity("0 0 0");
      FaceTarget(%obj,%target);
      %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition())); 
      %vector = vectorscale(%vector, 3250);
      %x = Getword(%vector,0);
      %y = Getword(%vector,1);
      %nv1 = %y;
      %nv2 = (%x * -1);
      %vector = %nv1@" "@%nv2@" 0";
      %obj.applyImpulse(%obj.getPosition(), %vector);
   }
   else if(%obj.chargecount <= 11){
	%obj.setvelocity("0 0 0");
      FaceTarget(%obj,%target);
      %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition())); 
      %vector = vectorscale(%vector, 4500);
      %x = Getword(%vector,0);
      %y = Getword(%vector,1);
      %z = Getword(%vector,2);
      %vector = %x@" "@%y@" 150";
      %obj.applyImpulse(%obj.getPosition(), %vector);
   }
   else{
	%obj.chargecount = 0;
	%obj.justmelee = 1;
	schedule(250, 0, "DemonMotherThink", %obj);
	return;
   }
   schedule(250, 0, "DemonMotherStrafeAttack", %obj, %target);
   %obj.chargecount++;
}

function DemonMotherFlyAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 9){
	FaceTarget(%obj,%target);
	%obj.setvelocity("0 0 10");
	%obj.chargecount++;
	schedule(100, 0, "DemonMotherFlyAttack",%obj,%target);
   }
   else if(%obj.chargecount == 10){
	FaceTarget(%obj,%target);
	%obj.setvelocity("0 0 5");
	%vector = vectorSub(%target.getPosition(),%obj.getPosition());
	%nVec = vectorNormalize(%vector);
	%vector = vectorAdd(%vector,vectorscale(%nvec,-4));
	%obj.attackpos = vectorAdd(%obj.getPosition(),%vector);
	%obj.attackdir = %nVec;
	echo(%obj.getPosition() SPC %target.getPosition() SPC %obj.attackpos SPC %obj.attackdir);
	%obj.startFade(400, 0, true);
	%obj.chargecount++;
	schedule(400, 0, "DemonMotherFlyAttack",%obj,%target);
   }
   else if(%obj.chargecount >= 11){
	%obj.startFade(500, 0, false);
	%obj.setPosition(%obj.attackpos);
	%obj.setvelocity(vectorscale(%obj.attackdir,25));
	%obj.justmelee = 1;
	%obj.chargecount = 0;
	echo(%obj.getPosition() SPC %target.getPosition() SPC %obj.attackpos SPC %obj.attackdir);
	%obj.attackpos = "";
	%obj.attackdir = "";
	schedule(1000, 0, "DemonMotherThink",%obj);
   }
}

function DemonMotherFireRainAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount == 0){
	FaceTarget(%obj, %target);
	for(%i = 0; %i < 10; %i++){
	   %pos = %obj.getPosition();
         %x = getRandom(0,10) - 5;
         %y = getRandom(0,10) - 5;
         %vec = vectorAdd(%pos,%x SPC %y SPC "5");
	   %searchResult = containerRayCast(%vec, vectorAdd(%vec,"0 0 -10"), $TypeMasks::TerrainObjectType, %obj);

         %charge = new ParticleEmissionDummy() 
         { 
   	      position = posFromRaycast(%searchresult);
   	      dataBlock = "defaultEmissionDummy"; 
   	      emitter = "FlameEmitter"; 
         }; 
         MissionCleanup.add(%charge);
         %charge.schedule(1500, "delete");
	}
	%obj.chargecount++;
	schedule(1000, 0, "DemonMotherFireRainAttack",%obj,%target);
   }
   else{
	%x = (getRandom() * 2) - 1;
	%y = (getRandom() * 2) - 1;
	%z = getRandom();
	%vec = vectorNormalize(%x SPC %y SPC %z);
	%pos = vectorAdd(%target.getPosition(),vectorScale(%vec, 20));
	for(%i = 0;%i < 10;%i++){
	   %x = getRandom(0,14) - 7;
         %y = getRandom(0,14) - 7;
         %spwpos = vectorAdd(%pos,%x SPC %y SPC "2");
   	   %p = new GrenadeProjectile() 
   	   { 
		dataBlock        = DemonFireball; 
		initialDirection = vectorScale(%vec,-1); 
		initialPosition  = %spwpos; 
		sourceObject     = %obj; 
		sourceSlot       = 4; 
   	   };
	}
	%obj.justshot = 1;
	%obj.chargecount = 0;
	schedule(1000, 0, "DemonMotherThink",%obj);
   }
}

function DemonMotherMissileAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount == 0){
	%obj.chargecount++;
	schedule(1000, 0, "DemonMotherMissileAttack", %obj, %target);
   }
   else{
	%vec = vectorNormalize(vectorSub(%target.getPosition(),%obj.getPosition()));
   	%p = new SeekerProjectile() 
   	{ 
	   dataBlock        = DMMissile; 
	   initialDirection = %vec; 
	   initialPosition  = %obj.getMuzzlePoint(4); 
	   sourceObject     = %obj; 
	   sourceSlot       = 4; 
   	};
   	%beacon = new BeaconObject() {
         dataBlock = "SubBeacon";
         beaconType = "vehicle";
         position = %target.getWorldBoxCenter();
   	};
   	%beacon.team = 0;
   	%beacon.setTarget(0);                  
   	MissionCleanup.add(%beacon);
	%p.setObjectTarget(%beacon);
	DemonMotherMissileFollow(%target,%beacon,%p);

	%obj.justshot = 1;
	%obj.chargecount = 0;
	schedule(1000, 0, "DemonMotherThink", %obj);
   }
}

function DemonMotherMissileFollow(%target,%beacon,%missile){
   if(!isObject(%target)){
	%beacon.delete();
	return;
   }
   if(!isObject(%missile)){
	%beacon.delete();
	return;
   }
   %beacon.setPosition(%target.getWorldBoxCenter());
   schedule(100, 0, "DemonMotherMissileFollow", %target, %beacon, %missile);
}

function DemonMotherPlasmaAttack(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 9){
	%obj.setVelocity("0 0 10");
	%obj.chargecount++;
	schedule(100, 0, "DemonMotherPlasmaAttack", %obj, %target);
   }
   else{
	%obj.setVelocity("0 0 3");
	%vec = vectorNormalize(vectorSub(%target.getPosition(),%obj.getPosition()));
   	%p = new LinearFlareProjectile() 
   	{ 
	   dataBlock        = DMPlasma; 
	   initialDirection = %vec; 
	   initialPosition  = %obj.getMuzzlePoint(4); 
	   sourceObject     = %obj; 
	   sourceSlot       = 4; 
   	};
	%obj.chargecount = 0;
	%obj.justshot = 1;
	schedule(1000, 0, "DemonMotherThink", %obj);
   }
}

function DemonMotherChargeIn(%obj,%target){
   if(!isObject(%obj))
	return;
   if(%obj.getState() $= "dead")
	return;
   if(%obj.chargecount $= "")
	%obj.chargecount = 0;
   if(%obj.chargecount <= 4){
	FaceTarget(%obj, %target);
	%vec = vectorNormalize(vectorSub(%target.getPosition(),%obj.getPosition()));
	%obj.setvelocity(vectorScale(%vec,50));
	%obj.chargecount++;
	schedule(500, 0, "DemonMotherChargeIn", %obj, %target);
   }
   else{
	%obj.justmelee = 1;
	%obj.chargecount = 0;
	DemonMotherThink(%obj);
   }
}

function DemonMotherMoveToTarget(%obj,%target){
   FaceTarget(%obj,%target);
   %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition())); 
   %vector = vectorscale(%vector, 1200);
   %x = Getword(%vector,0);
   %y = Getword(%vector,1);
   %z = Getword(%vector,2);
   %vector = %x@" "@%y@" 150";
   %obj.applyImpulse(%obj.getPosition(), %vector);

   schedule(500, 0, "DemonMotherThink", %obj);
}

function DemonMotherDemonSpawn(%obj){
   for(%i = 0; %i < 5; %i++){
      %pos = %obj.getPosition();
      %x = getRandom(0,200) - 100;
      %y = getRandom(0,200) - 100;
      %vec = vectorAdd(%pos,%x SPC %y SPC "40");
	%searchResult = containerRayCast(%vec, vectorAdd(%vec,"0 0 -80"), $TypeMasks::TerrainObjectType, %obj);

      %charge = new ParticleEmissionDummy() 
      { 
   	   position = posFromRaycast(%searchresult);
   	   dataBlock = "defaultEmissionDummy"; 
   	   emitter = "FlameEmitter"; 
      }; 
      MissionCleanup.add(%charge);
      %charge.schedule(1100, "delete");
	schedule(1000, 0, "startAzombie", posFromRaycast(%searchresult), 4);
   }
   schedule(1500, 0, "DemonMotherThink", %obj);
}

function FaceTarget(%obj,%target){
   %vector = vectorNormalize(vectorSub(%target.getPosition(), %obj.getPosition())); 
   %v1 = getword(%vector, 0);
   %v2 = getword(%vector, 1);
   %nv1 = %v2;
   %nv2 = (%v1 * -1);
   %vector2 = %nv1@" "@%nv2@" 0";
   %obj.setRotation(fullrot("0 0 0",%vector2));
}
