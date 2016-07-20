//--------------------------------------------------------------------------
// waypoint dep
//--------------------------------------------------------------------------
//client.player.setInventory(waypointDeployable,1,true);
$TeamDeployableMax[waypointDeployable]      = 100;

datablock StaticShapeData(Deployedwaypoint) : StaticShapeDamageProfile {
	className = "waypoint";
	shapeFile = "nexuscap.dts"; // dmiscf.dts, alternate
	maxDamage = 2.0;
	destroyedLevel = 2.0;
	disabledLevel = 2.0;
	mass = 1.2;
	elasticity = 0.1;
	friction = 0.9;
	collideable = 1;
	pickupRadius = 1;
	sticky=false;

	hasLight = true;
	lightType = "PulsingLight";
	lightColor = "0.1 0.8 0.8 1.0";
	lightTime = "100";
	lightRadius = "3";

	explosion      = HandGrenadeExplosion;
	expDmgRadius = 1.0;
	expDamage = 0.1;
	expImpulse = 200.0;
	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";

	targetNameTag = 'waypoint';
	targetTypeTag = 'marker';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
deployable = true;
};

datablock ShapeBaseImageData(waypointDeployableImage) {
	mass = 10;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = waypointDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = Deployedwaypoint;
	heatSignature = 0;
	collideable = 1;
	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360; // 30
	deploySound = ItemPickupSound;

	minDeployDis = 0.5;
	maxDeployDis = 5.0;
};

datablock ItemData(waypointDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	rotate = true;
	image = "waypointDeployableImage";
	pickUpName = "a waypoint pack";
	heatSignature = 0;
	emap = true;
};

function waypointDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

 
function waypointDeployableImage::onDeploy(%item, %plyr, %slot) {
	%className = "StaticShape";

	%playerVector = vectorNormalize(getWord(%plyr.getEyeVector(),1) SPC -1 * getWord(%plyr.getEyeVector(),0) SPC "0");
	%item.surfaceNrm2 = %playerVector;

	if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
		%item.surfaceNrm2 = %playerVector;
	else
		%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 -1"));

	%rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

	%deplObj = new (%className)() {
		dataBlock = %item.deployed;
        scale = "0.5 0.5 0.5";
	};

     %deplObj.wp = new  (WayPoint)(){
         dataBlock        = WayPointMarker;
         name             = %plyr.client.namebase@"'s waypoint";
         base             = %deplObj;
         scale            = "0.1 0.1 0.1";
       };

     %deplObj.wpname = %plyr.client.namebase@"'s waypoint";

      MissionCleanup.add(%deplObj.wp);

	// set orientation
	%deplObj.setTransform(%item.surfacePt SPC %rot);
	%deplObj.wp.setTransform(%item.surfacePt SPC %rot);

	// set team, owner, and handle
    %deplObj.deploy();
	%deplObj.team = %plyr.client.Team;
	%deplObj.wp.team = %plyr.client.Team;
    %deplObj.setOwner(%plyr);
	%deplObj.wp.setOwner(%plyr);

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
//	%plyr.unmountImage(%slot);
//	%plyr.decInventory(%item.item, 1);

	return %deplObj;
}

function Deployedwaypoint::disassemble(%data,%plyr,%obj) {
disassemble(%data,%plyr,%obj);
}

//function Deployedwaypoint::onDestroyed(%this, %obj, %prevState) {
//	if (%obj.isRemoved)
//		return;
//	%obj.isRemoved = true;
//	Parent::onDestroyed(%this, %obj, %prevState);
//	$TeamDeployedCount[%obj.team, waypointDeployable]--;
//	remDSurface(%obj);
//	%obj.wp.schedule(500, "delete");
//	%obj.schedule(500, "delete");
//	fireBallExplode(%obj,1);
//}

function waypointDeployableImage::onMount(%data, %obj, %node) {
	%obj.haswaypoint = true; // set for jumpadcheck
	%obj.packSet = 0;
}

function waypointDeployableImage::onUnmount(%data, %obj, %node) {
	%obj.haswaypoint = "";
	%obj.packSet = 0;
}
