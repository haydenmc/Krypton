//Promixity Switch
//Coded for Krypton Construct by Sloik

datablock ItemData(ProximitySwitch) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "deploy_sensor_pulse.dts";
	scale = 2.5 / 3.85 @ " " @ 2.5 / 3.1 @ " " @ 5 / 3.1;
 mass = 1;

	hasLight = false;

	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = false;
	image = "ProximitySwitchDeployableImage";
	pickUpName = "a Proximity Switch pack";
	emap = true;
};

datablock StaticShapeData(DeployedProximitySwitch) : StaticShapeDamageProfile { //Deployed owner-only sphere
	className = "proxswitch";
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
	targetTypeTag = 'Proximity Switch';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
};

datablock ShapeBaseImageData(ProximitySwitchDeployableImage) {
 mass = 1;
	shapeFile = "deploy_sensor_pulse.dts";
	scale = 2.5 / 3.85 @ " " @ 2.5 / 3.1 @ " " @ 5 / 3.1;
	item = ProximitySwitch;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedProximitySwitch;
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

function ProximitySwitchDeployableImage::onMount(%data, %obj, %node) {
	%obj.hasProxSwitch = true; // set for PermSpherecheck
	%obj.packSet = 0;
	%obj.expertSet = 0;
}

function ProximitySwitchDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.hasProxSwitch = "";
	%obj.packSet = 0;
	%obj.expertSet = 0;
}

function ProximitySwitchDeployableImage::onDeploy(%item, %plyr, %slot) {

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
	%scale = "1 1 1";

	%deplObj = new (%className)() {
		dataBlock = "DeployedProximitySwitch";
		scale = %scale;
	};

//Set the range on this baby
	if (%plyr.packSet == 0)
	%deplObj.range = 2; //2 meter range
	if (%plyr.packSet == 1)
	%deplObj.range = 5; //5 meter range
	if (%plyr.packSet == 2)
	%deplObj.range = 10; //10 meter range
	if (%plyr.packSet == 3)
	%deplObj.range = 20; //20 meter range
	if (%plyr.packSet == 4)
	%deplObj.range = 40; //40 meter range
	if (%plyr.packSet == 5)
	%deplObj.range = 80; //80 meter range
	if (%plyr.packSet == 6)
	%deplObj.range = 160; //160 meter range

//Set the mode -- On or off?
	if (%plyr.expertSet == 0) {
	%deplObj.pwrmode = 0; //Turn generator on on entry
	%deplObj.buddyonly = 0;
}
	if (%plyr.expertSet == 1) {
	%deplObj.pwrmode = 1; //Turn generator off on entry
	%deplObj.buddyonly = 0;
}
	if (%plyr.expertSet == 2) {
	%deplObj.pwrmode = 0; //Turn generator on on entry
	%deplObj.buddyonly = 1;
}
	if (%plyr.expertSet == 3) {
	%deplObj.pwrmode = 1; //Turn generator off on entry
	%deplObj.buddyonly = 1;
}

	if (%plyr.expertSet == 4) {
	%deplObj.pwrmode = 0; //Turn generator on on entry
	%deplObj.enemyonly = 1;
}
	if (%plyr.expertSet == 5) {
	%deplObj.pwrmode = 1; //Turn generator off on entry
	%deplObj.enemyonly = 1;
}

%deplObj.powerrange = 100;
%deplObj.triggered = 0; //Nobody's in range.


	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);

	// set power frequency
	%deplObj.powerFreq = %plyr.powerFreq;
	setTargetName(%deplObj.target,addTaggedString("Frequency" SPC %deplObj.powerFreq));

	%deplObj.team = %plyr.client.team;
	%deplObj.setOwner(%plyr);
	addDSurface(%item.surface,%deplObj);

	if (%deplObj.getTarget() != -1)
		setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

	addToDeployGroup(%deplObj);

	AIDeployObject(%plyr.client, %deplObj);

	serverPlay3D(%item.deploySound, %deplObj.getTransform());

	%deplObj.deploy();

	%deplObj.playThread($AmbientThread, "ambient");

//	%plyr.unmountImage(%slot);
//	%plyr.decInventory(%item.item, 1);
schedule(128,0,ProximityCheck,%deplObj);
	return %deplObj;
}

function ProximityCheck(%theswitch) {
//echo("TEST");
if (!isObject(%theswitch)) //Do we still exist?
	return;

InitContainerRadiusSearch(%theswitch.getPosition(),%theswitch.range,$TypeMasks::PlayerObjectType);
if (%theswitch.buddyonly == 1) {
//echo("TEST");
//OWNER ONLY
%personcount = 0;
	while((%obj = ContainerSearchNext()) != 0) {
		// Extra safety
		if (isbuddy(%theswitch.getOwner(),%obj.client) || %theswitch.getOwner() == %obj.client) {
		%personcount += 1;
		}
	}
//echo(%personcount);

if (%personcount > 0 && !%theswitch.triggered) { //We have an owner here... (and we haven't already been triggered)
	%count = getWordCount($PowerList);
	for(%i=0;%i<%count;%i++) {
		%powerObj = getWord($PowerList,%i);
		if (vectorDist(%theswitch.getPosition(),%powerObj.getPosition()) < %theswitch.powerrange
		&& !%powerObj.isRemoved && %theswitch.powerFreq == %powerObj.powerFreq
		&& %theswitch.team == %powerObj.team) {
			%r = forcePower(%powerObj,!%theswitch.pwrmode);
		}
	}
	%theswitch.triggered = 1;
} else if (%personcount <= 0 && %theswitch.triggered) { //No owners around... and we're still triggered.
	%count = getWordCount($PowerList);
	for(%i=0;%i<%count;%i++) {
		%powerObj = getWord($PowerList,%i);
		if (!isObject(%powerObj))
		continue;
		if (vectorDist(%theswitch.getPosition(),%powerObj.getPosition()) < %theswitch.powerrange
		&& !%powerObj.isRemoved && %theswitch.powerFreq == %powerObj.powerFreq
		&& %theswitch.team == %powerObj.team) {
			%r = forcePower(%powerObj,%theswitch.pwrmode);
		}
	}
	%theswitch.triggered = 0;
}

} else if (%theswitch.enemyonly == 1) {


//echo("TEST");
//OWNER ONLY
%personcount = 0;
	while((%obj = ContainerSearchNext()) != 0) {
		// Extra safety
		if (isenemy(%theswitch.getOwner(),%obj.client)) {
		%personcount += 1;
		}
	}
//echo(%personcount);

if (%personcount > 0 && !%theswitch.triggered) { //We have an enemy here... and we're not triggered.
	%count = getWordCount($PowerList);
	for(%i=0;%i<%count;%i++) {
		%powerObj = getWord($PowerList,%i);
		if (vectorDist(%theswitch.getPosition(),%powerObj.getPosition()) < %theswitch.powerrange
		&& !%powerObj.isRemoved && %theswitch.powerFreq == %powerObj.powerFreq
		&& %theswitch.team == %powerObj.team) {
			%r = forcePower(%powerObj,!%theswitch.pwrmode);
		}
	}
	%theswitch.triggered = 1;
} else if (%personcount <= 0 && %theswitch.triggered) { //No owners around... but we're still triggered.
	%count = getWordCount($PowerList);
	for(%i=0;%i<%count;%i++) {
		%powerObj = getWord($PowerList,%i);
		if (!isObject(%powerObj))
		continue;
		if (vectorDist(%theswitch.getPosition(),%powerObj.getPosition()) < %theswitch.powerrange
		&& !%powerObj.isRemoved && %theswitch.powerFreq == %powerObj.powerFreq
		&& %theswitch.team == %powerObj.team) {
			%r = forcePower(%powerObj,%theswitch.pwrmode);
		}
	}
	%theswitch.triggered = 0;
}

} else {

//NON OWNER ONLY
%containersearch = ContainerSearchNext();
	if (%containersearch != 0 && !%theswitch.triggered) { //There's at least one person within range and we haven't been triggered... no need to loop with non-owneronly.
//	if (%theswitch.triggered != 1) { //If we're not already active....

	%count = getWordCount($PowerList);
	for(%i=0;%i<%count;%i++) {
		%powerObj = getWord($PowerList,%i);
		if (vectorDist(%theswitch.getPosition(),%powerObj.getPosition()) < %theswitch.powerrange
		&& !%powerObj.isRemoved && %theswitch.powerFreq == %powerObj.powerFreq
		&& %theswitch.team == %powerObj.team) {
			%r = forcePower(%powerObj,!%theswitch.pwrmode);
		}
	}
	%theswitch.triggered = 1;

//	}
	} else if (%containersearch == 0 && %theswitch.triggered) { //Nobody's around, but we're still triggered.
//	if (%theswitch.triggered != 0) { //If we're not already deactivated...

	%count = getWordCount($PowerList);
	for(%i=0;%i<%count;%i++) {
		%powerObj = getWord($PowerList,%i);
		if (!isObject(%powerObj))
		continue;
		if (vectorDist(%theswitch.getPosition(),%powerObj.getPosition()) < %theswitch.powerrange
		&& !%powerObj.isRemoved && %theswitch.powerFreq == %powerObj.powerFreq
		&& %theswitch.team == %powerObj.team) {
			%r = forcePower(%powerObj,%theswitch.pwrmode);
		}
	}
	%theswitch.triggered = 0;

//	}
	}
}
	schedule(128,0,ProximityCheck,%theswitch);
}