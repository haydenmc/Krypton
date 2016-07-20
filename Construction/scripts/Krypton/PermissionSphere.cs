//--------------------------------------------------------------------------
// Deployable Permission Sphere
//--------------------------------------

datablock ItemData(PermSphereDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "deploy_sensor_pulse.dts";
	scale = 2.5 / 3.85 @ " " @ 2.5 / 3.1 @ " " @ 5 / 3.1;
 mass = 1;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "500";
	lightRadius = "3";

	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = false;
	image = "PermSphereDeployableImage";
	pickUpName = "a Permissions Sphere pack";
	emap = true;
};


datablock StaticShapeData(DeployedOOSphere) : StaticShapeDamageProfile { //Deployed owner-only sphere
	className = "oosphere";
	shapeFile = "deploy_sensor_pulse.dts";

	maxDamage      = 1.0;
	destroyedLevel = 1.0;
	disabledLevel  = 1.0;

	isShielded = true;
	energyPerDamagePoint = 30;
	maxEnergy = 50;
	rechargeRate = 0.05;

	explosion = SatchelMainExplosion;
	underwaterExplosion = UnderwaterSatchelMainExplosion;

	expDmgRadius = 20.0;
	expDamage    = 1.25;
	expImpulse   = 1500.0;

	dynamicType = $TypeMasks::StationObjectType;
	renderWhenDestroyed = true;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

//	humSound = GeneratorHumSound;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSwitchIcon;
	cmdMiniIconName = "commander/MiniIcons/com_switch_grey";
	targetNameTag = 'Deployed';
	targetTypeTag = 'Permission Sphere';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock StaticShapeData(DeployedPeaceSphere) : StaticShapeDamageProfile { //Deployed Peace sphere
	className = "oosphere";
	shapeFile = "deploy_sensor_pulse.dts";

	maxDamage      = 1.0;
	destroyedLevel = 1.0;
	disabledLevel  = 1.0;

	isShielded = true;
	energyPerDamagePoint = 30;
	maxEnergy = 50;
	rechargeRate = 0.05;

	explosion = SatchelMainExplosion;
	underwaterExplosion = UnderwaterSatchelMainExplosion;

	expDmgRadius = 20.0;
	expDamage    = 1.25;
	expImpulse   = 1500.0;

	dynamicType = $TypeMasks::StationObjectType;
	renderWhenDestroyed = true;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

//	humSound = GeneratorHumSound;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSwitchIcon;
	cmdMiniIconName = "commander/MiniIcons/com_switch_grey";
	targetNameTag = 'Deployed';
	targetTypeTag = 'Permission Sphere';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock StaticShapeData(DeployedCombatSphere) : StaticShapeDamageProfile { //Deployed Combat sphere
	className = "oosphere";
	shapeFile = "deploy_sensor_pulse.dts";

	maxDamage      = 1.0;
	destroyedLevel = 1.0;
	disabledLevel  = 1.0;

	isShielded = true;
	energyPerDamagePoint = 30;
	maxEnergy = 50;
	rechargeRate = 0.05;

	explosion = SatchelMainExplosion;
	underwaterExplosion = UnderwaterSatchelMainExplosion;

	expDmgRadius = 20.0;
	expDamage    = 1.25;
	expImpulse   = 1500.0;

	dynamicType = $TypeMasks::StationObjectType;
	renderWhenDestroyed = true;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

//	humSound = GeneratorHumSound;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSwitchIcon;
	cmdMiniIconName = "commander/MiniIcons/com_switch_grey";
	targetNameTag = 'Deployed';
	targetTypeTag = 'Permission Sphere';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock StaticShapeData(DeployedSpawnSphere) : StaticShapeDamageProfile { //Deployed Spawn sphere
	className = "oosphere";
	shapeFile = "turret_Muzzlepoint.dts";

	maxDamage      = 1.0;
	destroyedLevel = 1.0;
	disabledLevel  = 1.0;

	isShielded = true;
	energyPerDamagePoint = 30;
	maxEnergy = 50;
	rechargeRate = 0.05;

	explosion = SatchelMainExplosion;
	underwaterExplosion = UnderwaterSatchelMainExplosion;

	expDmgRadius = 20.0;
	expDamage    = 1.25;
	expImpulse   = 1500.0;

	dynamicType = $TypeMasks::StationObjectType;
	renderWhenDestroyed = true;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

//	humSound = GeneratorHumSound;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSwitchIcon;
	cmdMiniIconName = "commander/MiniIcons/com_switch_grey";
	targetNameTag = 'Deployed';
	targetTypeTag = 'Spawn Sphere';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock ShapeBaseImageData(PermSphereDeployableImage) {
 mass = 1;
	shapeFile = "deploy_sensor_pulse.dts";
	scale = 2.5 / 3.85 @ " " @ 2.5 / 3.1 @ " " @ 5 / 3.1;
	item = PermSphereDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = PermSphereDeployed;
	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.1 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	emap = true;
	maxDepSlope = 360;
	deploySound = TurretDeploySound;
	minDeployDis =  0.5;
	maxDeployDis =  5.0;
};

function PermSphereDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasPermSphere = true; // set for PermSpherecheck
	%obj.packSet = 0;
	%obj.expertSet = 0;
}

function PermSphereDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasPermSphere = "";
	%obj.packSet = 0;
	%obj.expertSet = 0;
}

function PermSphereDeployableImage::onDeploy(%item, %plyr, %slot) {
	//Object
	%className = "StaticShape";

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (%item.surfaceinher == 0) {
		if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
			%item.surfaceNrm2 = %playerVector;
		else
			%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));
	}

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);
	%scale = "2 2 2";


if (%plyr.packSet == 0) { //Owner-Only Construct Zone Sphere!
	%deplObj = new (%className)() {
		dataBlock = "DeployedOOSphere";
		scale = %scale;
	};
}

if (%plyr.packSet == 1) { //Peace Zone Sphere!
	%deplObj = new (%className)() {
		dataBlock = "DeployedPeaceSphere";
		scale = %scale;
	};
}

if (%plyr.packSet == 2) { //Combat Zone Sphere!
	%deplObj = new (%className)() {
		dataBlock = "DeployedCombatSphere";
		scale = %scale;
	};
}

//Set the range on this baby
	if (%plyr.expertSet == 0)
	%deplObj.range = 50; //50 meter range
	if (%plyr.expertSet == 1)
	%deplObj.range = 100; //100 meter range
	if (%plyr.expertSet == 2)
	%deplObj.range = 300; //300 meter range


	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);


	%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);
	addDSurface(%item.surface,%deplObj);

	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	addToDeployGroup(%deplObj);

	AIDeployObject(%plyr.client, %deplObj);

	serverPlay3D(%item.deploySound, %deplObj.getTransform());
	if ($SphereCount[%plyr.client.guid] < 0) //If it's somehow in the negatives...
	$SphereCount[%plyr.client.guid] = 0; //Set it to zero.
	$SphereCount[%plyr.client.guid]++;
	%deplObj.deploy();

	%deplObj.playThread($AmbientThread, "ambient");


//echo("Expert Setting: " @ %plyr.expertSet);
//echo("Pack Setting: " @ %plyr.packSet);


if (%plyr.packSet == 0) { //Owner-Only Construct Zone Sphere!
	if (%plyr.expertSet == 0)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "OOSphereEmitter50");
	if (%plyr.expertSet == 1)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "OOSphereEmitter100");
	if (%plyr.expertSet == 2)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "OOSphereEmitter300");
	schedule(5000,0,OwnerSphereCheck,%deplObj); //Begin the loop!
}

if (%plyr.packSet == 1) { //Peace Zone Sphere!
	if (%plyr.expertSet == 0)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "PeaceSphereEmitter50");
	if (%plyr.expertSet == 1)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "PeaceSphereEmitter100");
	if (%plyr.expertSet == 2)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "PeaceSphereEmitter300");
	schedule(5000,0,PeaceSphereCheck,%deplObj); //Begin the loop!
}

if (%plyr.packSet == 2) { //Combat Zone Sphere!
	if (%plyr.expertSet == 0)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "CombatSphereEmitter50");
	if (%plyr.expertSet == 1)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "CombatSphereEmitter100");
	if (%plyr.expertSet == 2)
	%deplObj.emitter = createEmitter(%deplObj.getPosition(), "CombatSphereEmitter300");
	schedule(5000,0,CombatSphereCheck,%deplObj); //Begin the loop!
}

	%plyr.unmountImage(%slot);
	%plyr.decInventory(%item.item, 1);

	return %deplObj;
}

function DeployedOOSphere::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$SphereCount[%obj.getOwner().guid]--;
	remDSurface(%obj);
	%obj.emitter.delete();
	%obj.schedule(500, "delete");

	RadiusExplosion(%obj, %obj.getWorldBoxCenter(), %obj.expDmgRadius, %obj.expDamage, %obj.expImpulse, %obj, $DamageType::Explosion);
	fireBallExplode(%obj,10);
}

function DeployedPeaceSphere::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$SphereCount[%obj.getOwner().guid]--;
	remDSurface(%obj);
	%obj.emitter.delete();
	%obj.schedule(500, "delete");

	RadiusExplosion(%obj, %obj.getWorldBoxCenter(), %obj.expDmgRadius, %obj.expDamage, %obj.expImpulse, %obj, $DamageType::Explosion);
	fireBallExplode(%obj,10);
}

function DeployedCombatSphere::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$SphereCount[%obj.getOwner().guid]--;
	remDSurface(%obj);
	%obj.emitter.delete();
	%obj.schedule(500, "delete");

	RadiusExplosion(%obj, %obj.getWorldBoxCenter(), %obj.expDmgRadius, %obj.expDamage, %obj.expImpulse, %obj, $DamageType::Explosion);
	fireBallExplode(%obj,10);
}

function OwnerSphereCheck(%thesphere) {

if (!isObject(%thesphere)) //Do we still exist?
	return;

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::StaticShapeObjectType | $TypeMasks::ItemObjectType | $TypeMasks::ForceFieldObjectType);
	while((%obj = ContainerSearchNext()) != 0) {
		// Extra safety
		if (%obj.getOwner().guid !$= %thesphere.getOwner().guid) {
			%dataBlockName = %obj.getDataBlock().getName();
			if (saveBuildingCheck(%obj)) { // If it's handled by saveBuilding(), it must be a deployable
//				%random = getRandom() * $Prison::RemoveSpamTimer-2000; // prevent duplicate disassemblies -- DISASSEMBLE IT NOW!
				%obj.getDataBlock().schedule(10,"disassemble",0,%obj); // Run Item Specific code.
			}
		}
	}
	schedule(5000,0,OwnerSphereCheck,%thesphere);
}

function PeaceSphereCheck(%thesphere) {

if (!isObject(%thesphere)) //Do we still exist?
	return;

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::ProjectileObjectType);
	while((%obj = ContainerSearchNext()) != 0) {
		%dataBlockName = %obj.getDataBlock().getName();
		if (%dataBlockName !$= "DeathBeam" && %dataBlockName !$= "BasicTargeter" && %dataBlockName !$= "ConstructionTargeter" && %dataBlockName !$= "ConstructionToolBeam" && %dataBlockName !$= "AimingLaser0" && %dataBlockName !$= "AimingLaser1" && %dataBlockName !$= "AimingLaser2" && %dataBlockName !$= "AimingLaser3" && %dataBlockName !$= "AimingLaser4" && %dataBlockName !$= "AimingLaser5") {
		//Delete projectiles...
		%obj.delete();
		}
		
	}
	
InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::VehicleObjectType);	 //Look for MPBs
while((%obj = ContainerSearchNext()) != 0) {
		%dataBlockName = %obj.getDataBlock().getName();
		if (%dataBlockName !$= "EscapePodVehicle" && %dataBlockName !$= "AdminATV") {
		//Apple ungodly amounts of damage
		%obj.getDataBlock().damageObject( %obj, 0,pos( %obj),999999,$DamageType::Explosion);
//		%obj.delete();
		}
		
	}

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::ItemObjectType);	 //Look for nades
while((%obj = ContainerSearchNext()) != 0) {
		%dataBlockName = %obj.getDataBlock().getName();
		if (%dataBlockName $= "GrenadeThrown" || %dataBlockName $= "FlashGrenadeThrown" || %dataBlockName $= "ConcussionGrenadeThrown") {
		%obj.delete();
//		%obj.delete();
		}
		
	}

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::PlayerObjectType);
while((%obj = ContainerSearchNext()) != 0) {
	if (%obj.inpeacesphere != %thesphere) {
	%obj.inpeacesphere = %thesphere;
	messageClient(%obj.client,0,"~wgui/launchMenuOver.wav");
	commandToClient( %obj.client, 'bottomPrint', "<color:1dcf00><font:Impact:19>[ Sphere Alert ]\n<color:00ff8a><font:Arial:18>You have entered a peace sphere.", 4, 2 );
//No zombies allowed...
   %objarmortype = %obj.getdatablock().getname();
   if(%objarmortype $= "ZombieArmor" || %objarmortype $= "FZombieArmor" || %objarmortype $= "LordZombieArmor" || %objarmortype $= "DemonZombieArmor" || %objarmortype $= "RapierZombieArmor")
   %obj.getDataBlock().damageObject( %obj, 0,pos( %obj),999999,$DamageType::Explosion);

	}
//	if (isEventPending(%obj.spherecancel))
	cancel(%obj.spherecancel);
	%obj.spherecancel = schedule(256,0,"eval",%obj @ ".inpeacesphere=\"\";");
	}
	schedule(128,0,PeaceSphereCheck,%thesphere);
}


function CombatSphereCheck(%thesphere) {

if (!isObject(%thesphere)) //Do we still exist?
	return;

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::PlayerObjectType);
while((%obj = ContainerSearchNext()) != 0) {
	if (%obj.incombatsphere != %thesphere) {
	%obj.incombatsphere = %thesphere;
	messageClient(%obj.client,0,"~wgui/launchMenuOver.wav");
	commandToClient( %obj.client, 'bottomPrint', "<color:e50000><font:Impact:19>[ Sphere Alert ]\n<color:ff6000><font:Arial:18>You have entered a combat sphere.\nMurder is now tolerated.", 4, 3 );
	}
//	if (isEventPending(%obj.spherecancel))
	cancel(%obj.combatspherecancel);
	%obj.combatspherecancel = schedule(256,0,"eval",%obj @ ".incombatsphere=\"\";");
	}
	schedule(128,0,CombatSphereCheck,%thesphere);
}


function SpawnSphereCheck(%thesphere) {

if (!isObject(%thesphere)) //Do we still exist?
	return;

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::ProjectileObjectType);
	while((%obj = ContainerSearchNext()) != 0) {
		%dataBlockName = %obj.getDataBlock().getName();
		if (%dataBlockName !$= "DeathBeam" && %dataBlockName !$= "BasicTargeter" && %dataBlockName !$= "ConstructionTargeter" && %dataBlockName !$= "ConstructionToolBeam" && %dataBlockName !$= "AimingLaser0" && %dataBlockName !$= "AimingLaser1" && %dataBlockName !$= "AimingLaser2" && %dataBlockName !$= "AimingLaser3" && %dataBlockName !$= "AimingLaser4" && %dataBlockName !$= "AimingLaser5") {
		//Delete projectiles...
		%obj.delete();
		}
		
	}

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::PlayerObjectType);
while((%obj = ContainerSearchNext()) != 0) {
	if (%obj.inpeacesphere != %thesphere) {
	%obj.inpeacesphere = %thesphere;
	messageClient(%obj.client,0,"~wgui/launchMenuOver.wav");
	commandToClient( %obj.client, 'bottomPrint', "<color:ffde00><font:Impact:19>[ Sphere Alert ]\n<color:dddd1e><font:Arial:18>You have entered a spawn sphere.", 4, 2 );
//No zombies allowed...
   %objarmortype = %obj.getdatablock().getname();
   if(%objarmortype $= "ZombieArmor" || %objarmortype $= "FZombieArmor" || %objarmortype $= "LordZombieArmor" || %objarmortype $= "DemonZombieArmor" || %objarmortype $= "RapierZombieArmor")
   %obj.getDataBlock().damageObject( %obj, 0,pos( %obj),999999,$DamageType::Explosion);

	}
//	if (isEventPending(%obj.spherecancel))
	cancel(%obj.spherecancel);
	%obj.spherecancel = schedule(256,0,"eval",%obj @ ".inpeacesphere=\"\";");
	}

InitContainerRadiusSearch(%thesphere.getPosition(),%thesphere.range,$TypeMasks::StaticShapeObjectType | $TypeMasks::ItemObjectType | $TypeMasks::ForceFieldObjectType);
	while((%obj = ContainerSearchNext()) != 0) {
		// Extra safety
		if (%obj.getOwner().guid !$= %thesphere.getOwner().guid) {
			%dataBlockName = %obj.getDataBlock().getName();
			if (saveBuildingCheck(%obj)) { // If it's handled by saveBuilding(), it must be a deployable
//				%random = getRandom() * $Prison::RemoveSpamTimer-2000; // prevent duplicate disassemblies -- DISASSEMBLE IT NOW!
				%obj.getDataBlock().schedule(10,"disassemble",0,%obj); // Run Item Specific code.
			}
		}
	}

	schedule(128,0,SpawnSphereCheck,%thesphere);

}

datablock ParticleData(OOSphereSmoke)
{
   dragCoefficient      = 0.0;
   windCoefficient      = 0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0;
   lifetimeMS           = 5000;
   lifetimeVarianceMS   = 0;
   useInvAlpha          = false;
   spinRandomMin        = -200.0;
   spinRandomMax        = 200.0;
   textureName          = "special/lightFalloffMono";
   colors[0]     = "0.01 0.01 0.01 1.0";
   colors[1]     = "0 1 1 1.0";
   colors[2]     = "0.01 0.01 0.01 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 3.0;
   sizes[2]      = 0.1;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleData(PeaceSphereSmoke)
{
   dragCoefficient      = 0.0;
   windCoefficient      = 0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0;
   lifetimeMS           = 5000;
   lifetimeVarianceMS   = 0;
   useInvAlpha          = false;
   spinRandomMin        = -200.0;
   spinRandomMax        = 200.0;
   textureName          = "special/lightFalloffMono";
   colors[0]     = "0.01 0.01 0.01 1.0";
   colors[1]     = "0 1 0 1.0";
   colors[2]     = "0.01 0.01 0.01 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 3.0;
   sizes[2]      = 0.1;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleData(CombatSphereSmoke)
{
   dragCoefficient      = 0.0;
   windCoefficient      = 0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0;
   lifetimeMS           = 5000;
   lifetimeVarianceMS   = 0;
   useInvAlpha          = false;
   spinRandomMin        = -200.0;
   spinRandomMax        = 200.0;
   textureName          = "special/lightFalloffMono";
   colors[0]     = "0.01 0.01 0.01 1.0";
   colors[1]     = "1 0 0 1.0";
   colors[2]     = "0.01 0.01 0.01 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 3.0;
   sizes[2]      = 0.1;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleData(SpawnSphereSmoke)
{
   dragCoefficient      = 0.0;
   windCoefficient      = 0;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0;
   lifetimeMS           = 5000;
   lifetimeVarianceMS   = 0;
   useInvAlpha          = false;
   spinRandomMin        = -200.0;
   spinRandomMax        = 200.0;
   textureName          = "special/lightFalloffMono";
   colors[0]     = "0.01 0.01 0.01 1.0";
   colors[1]     = "1 1 0 1.0";
   colors[2]     = "0.01 0.01 0.01 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 3.0;
   sizes[2]      = 0.1;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};


datablock ParticleEmitterData(SpawnSphereEmitter70) //owner-only 50 radius.
{
   ejectionPeriodMS = 8;
   ejectionOffset = 70;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "SpawnSphereSmoke";
};

//--------------------------
//Owner-Only sphere emitters.
//--------------------------
datablock ParticleEmitterData(OOSphereEmitter50) //owner-only 50 radius.
{
   ejectionPeriodMS = 5;
   ejectionOffset = 50;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "OOSphereSmoke";
};

datablock ParticleEmitterData(OOSphereEmitter100) //owner-only 100 radius.
{
   ejectionPeriodMS = 3;
   ejectionOffset = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "OOSphereSmoke";
};

datablock ParticleEmitterData(OOSphereEmitter300) //owner-only 300 radius.
{
   ejectionPeriodMS = 1;
   ejectionOffset = 300;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "OOSphereSmoke";
};

//--------------------------
//Peace sphere emitters.
//--------------------------
datablock ParticleEmitterData(PeaceSphereEmitter50) //Peace 50 radius.
{
   ejectionPeriodMS = 5;
   ejectionOffset = 50;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "PeaceSphereSmoke";
};

datablock ParticleEmitterData(PeaceSphereEmitter100) //Peace 100 radius.
{
   ejectionPeriodMS = 3;
   ejectionOffset = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "PeaceSphereSmoke";
};

datablock ParticleEmitterData(PeaceSphereEmitter300) //Peace 300 radius.
{
   ejectionPeriodMS = 1;
   ejectionOffset = 300;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "PeaceSphereSmoke";
};

//--------------------------
//Combat sphere emitters.
//--------------------------
datablock ParticleEmitterData(CombatSphereEmitter50) //Combat 50 radius.
{
   ejectionPeriodMS = 5;
   ejectionOffset = 50;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "CombatSphereSmoke";
};

datablock ParticleEmitterData(CombatSphereEmitter100) //Combat 100 radius.
{
   ejectionPeriodMS = 3;
   ejectionOffset = 100;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "CombatSphereSmoke";
};

datablock ParticleEmitterData(CombatSphereEmitter300) //Combat 300 radius.
{
   ejectionPeriodMS = 1;
   ejectionOffset = 300;
   periodVarianceMS = 0;
   ejectionVelocity = 0.0;
   velocityVariance = 0.0;
//   ejectionOffset   = 0.0;
   thetaMin         = 5;
   thetaMax         = 175;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
//   lifetimeMS       = 30000; To infitinity! and beyond!
   particles = "CombatSphereSmoke";
};