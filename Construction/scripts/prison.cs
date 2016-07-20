// prison.cs

// Set up defaults for nonexisting vars
if ($Host::Prison::Enabled $= "")
	$Host::Prison::Enabled = 1; // Enable prison system
if ($Host::Prison::JailMode $= "")
	$Host::Prison::JailMode = 0; // Jailing mode
				     // 0 = prison building
				     // 1 = spawnsphere
				     // 2 = players current/last position (prison only affects use of items)
if ($Host::Prison::ReleaseMode $= "")
	$Host::Prison::ReleaseMode = 1; // Release mode - same as above

// Killing
if ($Host::Prison::Kill $= "")
	$Host::Prison::Kill = 0; // Enable killing punishment
if ($Host::Prison::TeamKill $= "")
	$Host::Prison::TeamKill = 1; // Enable teamkill punishment
if ($Host::Prison::KillTime $= "")
	$Host::Prison::KillTime = 2 * 60; // Time to punish for killing/teamkilling

// Deployables spamming
if ($Host::Prison::DeploySpam $= "")
	$Host::Prison::DeploySpam = 1; // Enable deployables spam punishment
if ($Host::Prison::DeploySpamTime $= "")
	$Host::Prison::DeploySpamTime = 60; // Time to punish for deployables spamming
if ($Host::Prison::DeploySpamMultiply $= "")
	$Host::Prison::DeploySpamMultiply = 1; // Enable punishment multiplier for repeat offenders
if ($Host::Prison::DeploySpamMaxTime $= "")
	$Host::Prison::DeploySpamMaxTime = 5 * 60; // Max time, if applying multiplier, to jail a player
if ($Host::Prison::DeploySpamCheckTimeMS $= "")
	$Host::Prison::DeploySpamCheckTimeMS = 1000; // Time in MS between deploying that is considered spam
if ($Host::Prison::DeploySpamWarnings $= "")
	$Host::Prison::DeploySpamWarnings = 10; // Number of warnings before punishment
						// This is a bit misleading. It is actually the number of spams
						// allowed before punishment. Warnings will be given for the last
						// half of them
if ($Host::Prison::DeploySpamResetWarnCountTime $= "")
	$Host::Prison::DeploySpamResetWarnCountTime = 30; // Reset warn counter after this many seconds of not deploying
if ($Host::Prison::DeploySpamRemoveRecentMS $= "")
	$Host::Prison::DeploySpamRemoveRecentMS = 1000 * 15; // Remove pieces deployed by offender within the last 15 seconds

if ($Prison::RemoveSpamTimer < 10000) // Remove spam around prison every 30 seconds, 10 seconds minimum
	$Prison::RemoveSpamTimer = 30000;

function jailPlayer(%cl,%release,%prisonTimeInSeconds,%jailThread) {
	%cl.jailThread++;
	if (!isObject(%cl)) {
		warn("-jailPlayer- no client: " @ %cl @ " (" @ (%release ? "release" : "jail") @ ")");
		return;
	}
	if (%release) {
		if (%jailThread == 0 || %cl.jailThread - 1 == %jailThread) {
			%cl.isJailed = false;
			if (($Host::Prison::ReleaseMode $= "0" || $Host::Prison::ReleaseMode $= "")
			    && $Prison::ReleasePos !$= "0" && $Prison::ReleasePos !$= "") {
				%cl.player.setVelocity("0 0 0");
				if ($Prison::ReleaseRad > 0) {
					%pi = 3.1415926535897932384626433832795; // Whoa..
					%vec = getRandom() * %pi * 2;
					%rad = getRandom() * $Prison::ReleaseRad;
					%x = %x + (mSin(%vec) * %rad);
					%y = %y + (mCos(%vec) * %rad);
					%cl.player.setPosition(VectorAdd(%x SPC %y SPC 0,$Prison::ReleasePos));
				}
				else
					%cl.player.setPosition($Prison::ReleasePos);
			}
			else if ($Host::Prison::ReleaseMode == 1) {
				%cl.player.setVelocity("0 0 0");
				%cl.player.setPosition(Game.pickPlayerSpawn(%cl,false));
			}
			else {
				// Make sure they still get released from prison building
				if (($Host::Prison::JailMode $= "0" || $Host::Prison::JailMode $= "")
				    && $Prison::JailPos !$= "0" && $Prison::JailPos !$= "") { // This could be handled nicer..
					%cl.player.setVelocity("0 0 0");
					%cl.player.setPosition(%cl.preJailPos);
//					%cl.player.setVelocity(%cl.preJailVel);
				}
				// Else, do nothing. Leave player at current position.
			}
			buyFavorites(%cl);
			if (%cl.player.weaponCount > 0)
				%cl.player.selectWeaponSlot(0);
			if (%jailThread) // Only show for timed releases
				messageAll('msgClient','\c2%1 has been released from jail.',%cl.name);
		}
		return;
	}
	if ($Host::Prison::Enabled != true) {
		warn("-jailPlayer- prison system is disabled.");
		return;
	}
	%cl.isJailed = true;
	%cl.jailTime = %prisonTimeInSeconds;
	if (isObject(%cl.player)) {
		if (%cl.player.getState() !$="Dead") {
			if(%cl.player.isMounted()) {
				if(%cl.player.vehicleTurret)
					%cl.player.vehicleTurret.getDataBlock().playerDismount(%cl.player.vehicleTurret);
				else {
					%cl.player.getDataBlock().doDismount(%cl.player,true);
					%cl.player.mountVehicle = false;
				}
			}
			serverCmdResetControlObject(%cl);
			%cl.player.setInventory(EnergyPack,1,1); // Fix Satchel Charge
			%cl.player.clearInventory();
			%cl.setWeaponsHudClearAll();
			%cl.player.setArmor("Light");
//			Game.dropFlag(%cl.player);
			%cl.preJailVel = %cl.player.getVelocity();
			%cl.preJailPos = %cl.player.getPosition();
			// If we have no prison to put them in, we'll let them run around without any weapons..
			if (($Host::Prison::JailMode $= "0" || $Host::Prison::JailMode $= "")
			    && $Prison::JailPos !$= "0" && $Prison::JailPos !$= "") {
				%cl.player.setVelocity("0 0 0");
				if ($Prison::JailRad > 0) {
					%pi = 3.1415926535897932384626433832795; // Whoa..
					%vec = getRandom() * %pi * 2;
					%rad = getRandom() * $Prison::JailRad;
					%x = %x + (mSin(%vec) * %rad);
					%y = %y + (mCos(%vec) * %rad);
					%cl.player.setPosition("-0.19715 2.69382 10011");
				}
				else
					%cl.player.setPosition($Prison::JailPos);
			}
			else if ($Host::Prison::JailMode == 1) {
				%cl.player.setVelocity("0 0 0");
				%cl.player.setPosition(Game.pickPlayerSpawn(%cl,false));
			}
			else {
				// Do nothing, leave player's current position
			}
		}
		}
			cancel(%cl.prisonReleaseSched);
			if (%prisonTimeInSeconds > 0)
				%cl.prisonReleaseSched = schedule(%prisonTimeInSeconds * 1000,0,jailPlayer,%cl,true,0,%cl.jailThread);
	
}

function prisonCreate() {
	%prisonBasePos = "0 0 10000";
	if (isObject(nameToID(PrisonGroup)))
		 // Note, this does not handle removal of the PhysicalZones
		PrisonGroup.delete();
	%group = nameToID(MissionCleanup);
	if(%group == -1)
		return;
	%p = new SimGroup(PrisonGroup) {
		providesPower = true;
		new InteriorInstance(PrisonMain) {
			position = %prisonBasePos;
//			rotation = "-1 0 0 90";
			rotation = "0 0 0 0";
			scale = "1 1 1";
//			interiorFile = "btowra.dif";
			interiorFile = "dbase_broadside_nef.dif";
		};
//		new ForceFieldBare(PrisonFF1) {
//			position = "-5.00913 -31.6636 10006";
//			rotation = "-0.578066 -0.575915 0.578068 120.123";
//			scale = "7.02 10.0201 0.48";
//			dataBlock = "defaultSolidFieldBare";
//		};
//		new ForceFieldBare(PrisonFF2) {
//			position = "-22.01 3.01 9976.49";
//			rotation = "1 0 -4.82798e-06 180";
//			scale = "6.02 6.02 0.48";
//			dataBlock = "defaultSolidFieldBare";
//		};
//		new ForceFieldBare(PrisonFF3) {
//			position = "22.01 -3.01 9976.52";
//			rotation = "-1.37092e-06 1 0 180";
//			scale = "6.02 6.02 0.48";
//			dataBlock = "defaultSolidFieldBare";
//		};
//		new ForceFieldBare(PrisonFF4) {
//			position = "-13.3587 3.99003 10062.5";
//			rotation = "-4.08576e-07 -1 -3.74186e-07 105.946";
//			scale = "6.02 6.01997 0.52";
//			dataBlock = "defaultSolidFieldBare";
//		};
//		new ForceFieldBare(PrisonFF5) {
//			position = "13.2985 10.01 10062.5";
//			rotation = "0.798348 4.05391e-06 -0.602196 180";
//			scale = "6.02 6.01997 0.52";
//			dataBlock = "defaultSolidFieldBare";
//		};

//Some lovely deployables...
//A generator
//%building = new Turret(StaticShape) () {datablock = "GeneratorLarge";position = "-7.11436 13.6861 10014";rotation = "0 0 1 146.294";scale = "1 1 1";team = "1";ownerGUID = "1";deployed = "1";powerFreq = "1";powerRadius = "400";}
//setTargetSensorGroup(%building.getTarget(),1);
//addToDeployGroup(%building);
//%building.setSelfPowered();setTargetName(%building.target,addTaggedString("Frequency" SPC %building.powerFreq));
//%building.playThread($AmbientThread,"ambient");

//A telepad..
//%building = new Turret(StaticShape) () {datablock = "TelePadDeployedBase";position = "6.84369 15.4784 10014.4";rotation = "0.637498 0.770452 9.76618e-07 180";scale = "1 1 1";team = "1";ownerGUID = "1";deployed = "1";powerFreq = "1";frequency = "1";teleMode = "3";}
//setTargetSensorGroup(%building.getTarget(),1);
//addToDeployGroup(%building);
//%beam = new StaticShape() {datablock = "TelePadBeam";position = "6.84369 15.4784 10014.4";rotation = "-2.6941e-06 1.15733e-13 -1 100.789";scale = "1 1 0.4";}
//%building.beam = %beam;%beam.playThread(0,"ambient");
//%beam.setThreadDir(0,true);
//%beam.flashThreadDir = true;
//setTargetName(%building.target,addTaggedString("Frequency" SPC %building.frequency));
new Turret(PrisonTurret1) {
position = "-2.46022 7.13686 10067";
rotation = "0 0 1 91.1278";
scale = "1 1 1";
nameTag = "Prison";
dataBlock = "TurretBaseLarge";
lockCount = "0";
homingCount = "0";
initialBarrel = "ChaingunBarrelLarge";
locked = "true";
team = "6";
};
new Turret(PrisonTurret2) {
position = "-22.0718 -17.4789 10057";
rotation = "0 0 1 42.3157";
scale = "1 1 1";
nameTag = "Prison";
dataBlock = "TurretBaseLarge";
lockCount = "0";
homingCount = "0";
initialBarrel = "ChaingunBarrelLarge";
locked = "true";
team = "6";
};

new Turret(PrisonTurret3) {
position = "22.1211 30.1111 10057";
rotation = "0 0 1 223.926";
scale = "1 1 1";
nameTag = "Prison";
dataBlock = "TurretBaseLarge";
lockCount = "0";
homingCount = "0";
initialBarrel = "ChaingunBarrelLarge";
locked = "true";
team = "6";
};

new Turret(PrisonTurret4) {
position = "-7.41957 -32.2994 10002.3";
rotation = "-0.331595 -0.619591 0.711443 221.689";
scale = "1 1 1";
nameTag = "Prison";
dataBlock = "TurretBaseLarge";
lockCount = "0";
homingCount = "0";
initialBarrel = "ChaingunBarrelLarge";
locked = "true";
team = "6";
};

new Turret(PrisonTurret5) {
position = "-15 0.239721 9957.62";
rotation = "-0.577351 0.577352 0.577348 120";
scale = "1 1 1";
nameTag = "Prison";
dataBlock = "TurretBaseLarge";
lockCount = "0";
homingCount = "0";
initialBarrel = "ChaingunBarrelLarge";
locked = "true";
team = "6";
};

new Turret(PrisonTurret6) {
position = "15 -0.219893 9957.45";
rotation = "0.57735 0.577353 0.577348 239.999";
scale = "1 1 1";
nameTag = "Prison";
dataBlock = "TurretBaseLarge";
lockCount = "0";
homingCount = "0";
initialBarrel = "ChaingunBarrelLarge";
locked = "true";
team = "6";
};


//Spider Clamps...
new Turret(PrisonClamp1) {
datablock = "TurretDeployedWallIndoor";
position = "-10.0705 3.5 10047.7";
rotation = "0 0.707107 0.707107 180";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp2) {
datablock = "TurretDeployedWallIndoor";
position = "-4.43628 3.5 10045.9";
rotation = "-1 -0 0 90";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp3) {
datablock = "TurretDeployedWallIndoor";
position = "4.6297 3.5 10045.8";
rotation = "-1 -0 0 90";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp4) {
datablock = "TurretDeployedWallIndoor";
position = "9.99698 3.5 10047.7";
rotation = "0 0.707107 0.707107 180";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp5) {
datablock = "TurretDeployedWallIndoor";
position = "-0.0249347 12.184 10039.2";
rotation = "0 0.544914 0.838492 180";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp6) {
datablock = "TurretDeployedWallIndoor";
position = "-5.48865 -13.5484 10028.7";
rotation = "-0.337066 0.665728 0.665728 142.746";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp7) {
datablock = "TurretDeployedWallIndoor";
position = "-6.1678 19.25 10015.9";
rotation = "0 0.707107 0.707107 180";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp8) {
datablock = "TurretDeployedWallIndoor";
position = "16 2.60423 9996.88";
rotation = "0.57735 -0.57735 -0.57735 240";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};

new Turret(PrisonClamp9) {
datablock = "TurretDeployedCeilingIndoor";
position = "10.9849 -8.11171 9988";
rotation = "0 1 0 180";
scale = "1 1 1";
team = "6";
ownerGUID = "0";
initialBarrel = "DeployableIndoorBarrel";
};



	};
	%group.add(%p);
	PrisonGroup.powerInit(0);
	setTargetSensorGroup(PrisonTurret1.getTarget(),6);
	PrisonTurret1.setRechargeRate(PrisonTurret1.getDatablock().rechargeRate);
	PrisonTurret1.setRepairRate(0.0005);
	setTargetSensorGroup(PrisonTurret2.getTarget(),6);
	PrisonTurret2.setRechargeRate(PrisonTurret2.getDatablock().rechargeRate);
	PrisonTurret2.setRepairRate(0.0005);
	setTargetSensorGroup(PrisonTurret3.getTarget(),6);
	PrisonTurret3.setRechargeRate(PrisonTurret3.getDatablock().rechargeRate);
	PrisonTurret3.setRepairRate(0.0005);
	setTargetSensorGroup(PrisonTurret4.getTarget(),6);
	PrisonTurret4.setRechargeRate(PrisonTurret4.getDatablock().rechargeRate);
	PrisonTurret4.setRepairRate(0.0005);
	setTargetSensorGroup(PrisonTurret5.getTarget(),6);
	PrisonTurret5.setRechargeRate(PrisonTurret5.getDatablock().rechargeRate);
	PrisonTurret5.setRepairRate(0.0005);
	setTargetSensorGroup(PrisonTurret6.getTarget(),6);
	PrisonTurret6.setRechargeRate(PrisonTurret6.getDatablock().rechargeRate);
	PrisonTurret6.setRepairRate(0.0005);

setTargetSensorGroup(PrisonClamp1.getTarget(),6);
PrisonClamp1.deploy();
PrisonClamp1.setSelfPowered();
PrisonClamp1.setRechargeRate(PrisonClamp1.getDatablock().rechargeRate);
PrisonClamp1.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp2.getTarget(),6);
PrisonClamp2.deploy();
PrisonClamp2.setSelfPowered();
PrisonClamp2.setRechargeRate(PrisonClamp2.getDatablock().rechargeRate);
PrisonClamp2.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp3.getTarget(),6);
PrisonClamp3.deploy();
PrisonClamp3.setSelfPowered();
PrisonClamp3.setRechargeRate(PrisonClamp3.getDatablock().rechargeRate);
PrisonClamp3.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp4.getTarget(),6);
PrisonClamp4.deploy();
PrisonClamp4.setSelfPowered();
PrisonClamp4.setRechargeRate(PrisonClamp4.getDatablock().rechargeRate);
PrisonClamp4.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp5.getTarget(),6);
PrisonClamp5.deploy();
PrisonClamp5.setSelfPowered();
PrisonClamp5.setRechargeRate(PrisonClamp5.getDatablock().rechargeRate);
PrisonClamp5.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp6.getTarget(),6);
PrisonClamp6.deploy();
PrisonClamp6.setSelfPowered();
PrisonClamp6.setRechargeRate(PrisonClamp6.getDatablock().rechargeRate);
PrisonClamp6.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp7.getTarget(),6);
PrisonClamp7.deploy();
PrisonClamp7.setSelfPowered();
PrisonClamp7.setRechargeRate(PrisonClamp7.getDatablock().rechargeRate);
PrisonClamp7.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp8.getTarget(),6);
PrisonClamp8.deploy();
PrisonClamp8.setSelfPowered();
PrisonClamp8.setRechargeRate(PrisonClamp8.getDatablock().rechargeRate);
PrisonClamp8.setRepairRate(0.0002);

setTargetSensorGroup(PrisonClamp9.getTarget(),6);
PrisonClamp9.deploy();
PrisonClamp9.setSelfPowered();
PrisonClamp9.setRechargeRate(PrisonClamp9.getDatablock().rechargeRate);
PrisonClamp9.setRepairRate(0.0002);


schedule(5000,0,prisonturretreploop);

	addPrisonCamera("-1.0 -7.5 10010.4","0 0.707105 0.707105 3.14159",0);
//	addPrisonCamera("1.638419 -7.5 10010.4",getWords(MatrixCreateFromEuler(mDegToRad(270)) SPC "0 0",3,6),1);
	addPrisonCamera("0.0 -7.5 10010.4","0 0.707105 0.707105 3.14159",1);
	addPrisonCamera("1.0 -7.5 10010.4","0 0.707105 0.707105 3.14159",2);
//	addPrisonCamera(VectorAdd("1 2 0.5",%prisonBasePos),getWords(MatrixCreateFromEuler(mDegToRad(90)) SPC "0 0",3,6),1);
//	addPrisonCamera(VectorAdd("-1 2 0.5",%prisonBasePos),getWords(MatrixCreateFromEuler(mDegToRad(90)) SPC "0 0",3,6),2);
	$Prison::JailRad = 3;
	$Prison::JailPos = VectorAdd("0 -8.5 0",%prisonBasePos);
	$Prison::ReleaseRad = 4;
	$Prison::ReleasePos = VectorAdd("0 -14.25 6.75",%prisonBasePos); // Release player on top of prison
	$Prison::NoBuildRadius = 0;
}

// Prevent spam in prison. If called outside prisonCreate(), needs to be passed $Prison::RemoveSpamThread++ as arg
function prisonRemoveSpamThread(%thread) {
	// Re-evaluate here, in case user has set it to an "illegal" value
	if ($Prison::RemoveSpamTimer < 10000) // 10 seconds
		$Prison::RemoveSpamTimer = 10000;
	// Thread cancels if prison is re-created, if PrisonGroup ceases to exist, or if prison system is disabled
	if (%thread != $Prison::RemoveSpamThread || !isObject(nameToID(PrisonGroup)) || $Host::Prison::Enabled != 1) {
		warn("prisonRemoveSpamThread #" @ mAbs(%thread) @ " stopped. Last started thread: " @ $Prison::RemoveSpamThread);
		return;
	}
	InitContainerRadiusSearch($Prison::JailPos,$Prison::NoBuildRadius,$TypeMasks::StaticShapeObjectType | $TypeMasks::ItemObjectType | $TypeMasks::ForceFieldObjectType);
	while((%obj = ContainerSearchNext()) != 0) {
		// Extra safety
		if (VectorDist($Prison::JailPos,%obj.getPosition()) < $Prison::NoBuildRadius) {
			%dataBlockName = %obj.getDataBlock().getName();
			if (saveBuildingCheck(%obj)) { // If it's handled by saveBuilding(), it must be a deployable
				%random = getRandom() * $Prison::RemoveSpamTimer-2000; // prevent duplicate disassemblies
				%obj.getDataBlock().schedule(%random,"disassemble",0,%obj); // Run Item Specific code.
			}
		}
	}
	schedule($Prison::RemoveSpamTimer,0,prisonRemoveSpamThread,%thread);
}

function prisonEnable() {
	$Host::Prison::Enabled = 1;
	%pgroup = nameToID(PrisonGroup);
	if(isObject(%pgroup)) {
		%pgroup.providesPower = true;
		%pgroup.powerInit(0);
	}
	else
		prisonCreate();
	prisonRemoveSpamThread($Prison::RemoveSpamThread++);
}

function prisonDisable() {
	$Host::Prison::Enabled = 0;
	%pgroup = nameToID(PrisonGroup);
	if(isObject(%pgroup)) {
		%pgroup.providesPower = false;
		%pgroup.powerInit(0);
	}
	// Release jailed players
	%count = ClientGroup.getCount();
	for(%i = 0; %i < %count; %i++) {
		%cl = ClientGroup.getObject(%i);
		if (%cl.isJailed)
			jailPlayer(%cl,true);
	}
}

datablock SensorData(PrisonCameraSensorObject) {
	detects = false;
};

datablock TurretData(TurretPrisonCamera) {
	className = PrisonCameraTurret;
	shapeFile = "camera.dts";

	thetaMin = 50;
	thetaMax = 130;
//	thetaNull = 90;

	cameraDefaultFov = 120;
	cameraMinFov = 120;
	cameraMaxFov = 120;

	neverUpdateControl = false;

	canControl = true;
	canObserve = true;
	observeThroughObject = true;
	cmdCategory = "Support";
	cmdIcon = CMDCameraIcon;
	cmdMiniIconName = "commander/MiniIcons/com_camera_grey";
	targetNameTag = 'Prison';
	targetTypeTag = 'Camera';
	sensorData = PrisonCameraSensorObject;
	sensorRadius = PrisonCameraSensorObject.detectRadius;

	firstPersonOnly = true;
	observeParameters = "0.5 4.5 4.5";
};

datablock TurretImageData(PrisonCameraBarrel) {
	shapeFile = "turret_muzzlepoint.dts";
	usesEnergy = false;

	// Turret parameters
	activationMS      = 100;
	deactivateDelayMS = 100;
	thinkTimeMS       = 100;
	degPerSecTheta    = 180;
	degPerSecPhi      = 360;
};

function addPrisonCamera(%pos,%rot,%team) {
	%group = nameToID(PrisonGroup);
	if (!isObject(%group))
		return;
	%pCam = new Turret(PrisonCamera) {
		dataBlock = "TurretPrisonCamera";
		position = %pos;
		rotation = %rot;
		team = %team;
		needsNoPower = true;
	};
	%pCam.setRotation(%rot); // Gah!
	%group.add(%pCam);

	if(%pCam.getTarget() != -1)
		setTargetSensorGroup(%pCam.getTarget(),%team);
	%pCam.deploy();
}

function TurretPrisonCamera::damageObject(%data,%targetObject,%sourceObject,%position,%amount,%damageType,%momVec,%mineSC) {
	// Do nothing
}

function TurretPrisonCamera::onAdd(%this, %obj) {
	Parent::onAdd(%this, %obj);
	%obj.mountImage(PrisonCameraBarrel, 0, true);
		%obj.setRechargeRate(%this.rechargeRate);
	%obj.setAutoFire(false); // z0ddm0d: Server crash fix related to controlable cameras
}


function prisonturretreploop()
{
if (isObject(PrisonTurret1)) {
PrisonTurret1.setRepairRate(0.0005);
PrisonTurret2.setRepairRate(0.0005);
PrisonTurret3.setRepairRate(0.0005);
PrisonTurret4.setRepairRate(0.0005);
PrisonTurret5.setRepairRate(0.0005);
PrisonTurret6.setRepairRate(0.0005);

if (isObject(PrisonClamp1)) {
PrisonClamp1.setDamageState(Enabled);
PrisonClamp2.setDamageState(Enabled);
PrisonClamp3.setDamageState(Enabled);
PrisonClamp4.setDamageState(Enabled);
PrisonClamp5.setDamageState(Enabled);
PrisonClamp6.setDamageState(Enabled);
PrisonClamp7.setDamageState(Enabled);
PrisonClamp8.setDamageState(Enabled);
PrisonClamp9.setDamageState(Enabled);
}

schedule(5000,0,prisonturretreploop);
}
}