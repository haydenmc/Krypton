//client.player.setInventory(TdoorDeployable,1,true);
$TeamDeployableMax[TdoorDeployable]      = 1000;

//---------------------------------------------------------
// Deployable mspine, Code by Parousia
//---------------------------------------------------------

datablock StaticShapeData(DeployedTdoor) : StaticShapeDamageProfile {
	className = "Tdoor";
	shapeFile = "dmiscf.dts";

	maxDamage      = 5.0;
	destroyedLevel = 5.0;
	disabledLevel  = 2.5;

	isShielded = true;
	energyPerDamagePoint = 240;
	maxEnergy = 50;
	rechargeRate = 0.25;

	explosion    = HandGrenadeExplosion;
	expDmgRadius = 5.0;
	expDamage    = 0.5;
	expImpulse   = 200.0;

	dynamicType = $TypeMasks::StaticShapeObjectType;
	deployedObject = true;
	cmdCategory = "DSupport";
	cmdIcon = CMDSensorIcon;
	cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";
	targetNameTag = 'MTest Door';
	deployAmbientThread = true;
	debrisShapeName = "debris_generic_small.dts";
	debris = DeployableDebris;
	heatSignature = 0;
    needsPower = true;
deployable = true;
};

datablock ShapeBaseImageData(TdoorDeployableImage) {
	mass = 20;
	emap = true;
	shapeFile = "stackable1s.dts";
	item = TdoorDeployable;
	mountPoint = 1;
	offset = "0 0 0";
	deployed = DeployedTdoor;
	heatSignature = 0;

	stateName[0] = "Idle";
	stateTransitionOnTriggerDown[0] = "Activate";

	stateName[1] = "Activate";
	stateScript[1] = "onActivate";
	stateTransitionOnTriggerUp[1] = "Idle";

	isLarge = true;
	maxDepSlope = 360;
	deploySound = ItemPickupSound;

	minDeployDis = 0.1;
	maxDeployDis = 50.0;
};

datablock ItemData(TdoorDeployable) {
	className = Pack;
	catagory = "Deployables";
	shapeFile = "stackable1s.dts";
	mass = 5.0;
	elasticity = 0.2;
	friction = 0.6;
	pickupRadius = 1;
	joint = "1 1 1";
	rotate = true;
	image = "TdoorDeployableImage";
	pickUpName = "a Test Door pack";
	heatSignature = 0;
	emap = true;
};

function TdoorDeployableImage::testObjectTooClose(%item) {
	return "";
}

function TdoorDeployableImage::testNoTerrainFound(%item) {
	// don't check this for non-Landspike turret deployables
}

function TdoorDeployable::onPickup(%this, %obj, %shape, %amount) {
	// created to prevent console errors
}

function TdoorDeployableImage::onMount(%data, %obj, %node) {
%obj.hasDoor = true; // set for blastcheck
%obj.packSet = 0;
%obj.expertSet = 3;
displayPowerFreq(%obj);

}

function TdoorDeployableImage::onUnmount(%data, %obj, %node) {
%obj.hasDoor = "";
%obj.packSet = 0;
%obj.expertSet = 0;
}

function TdoorDeployableImage::onDeploy(%item, %plyr, %slot) {
 %className = "StaticShape";
	%grounded = 0;
	if (%item.surface.getClassName() $= TerrainBlock)
		%grounded = 1;

	%playerVector = vectorNormalize(-1 * getWord(%plyr.getEyeVector(),1) SPC getWord(%plyr.getEyeVector(),0) SPC "0");

	if (%item.surfaceinher == 0) {
		if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1"){
			%item.surfaceNrm2 = %playerVector;
            }
		else{
			%item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 1"));
            }
    }

	%rot1    = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

    %scale1 = "0.5 6 160";
    %scale2 = "0.5 8 160";
	%dataBlock = %item.deployed;

		%space = rayDist(%item.surfacePt SPC %item.surfaceNrm,%scale1,$AllObjMask);
		if (%space != getWord(%scale1,1))
			%type  = true;
		%scale1 = getWord(%scale1,0) SPC getWord(%scale1,0) SPC %space;


        %mCenter = "0 0 -0.5";
		%pad = pad(%item.surfacePt SPC %item.surfaceNrm SPC %item.surfaceNrm2,%scale2,%mCenter);
		%scale2 = getWords(%pad,0,2);
		%item.surfacePt2 = getWords(%pad,3,5);
  
        //%vec1 = realVec(getWord(%item.surface,0),%item.surfaceNrm);
        //%vec1 = realVec(%pad,%item.surfaceNrm);
        %vec1 =validateVal(MatrixMulVector("0 0 0",%item.surfaceNrm));
        
	if (!%scaleIsSet){
		%scale1 = vectorMultiply(%scale1,1/4 SPC 1/3 SPC 2);
        %scale2 = vectorMultiply(%scale2,1/4 SPC 1/3 SPC 2);
        %x = (getWord(%scale2,1)/0.166666)*0.125;
        %scale3 = %x SPC 0.166666 SPC getWord(%scale1,2);
        }



	%dir1 = VectorNormalize(vectorSub(%item.surfacePt,%item.surfacePt2));
	%adjust1 = vectorNormalize(vectorProject(%dir1,vectorCross(%item.surfaceNrm,%item.surfaceNrm2)));
    %offset1 = -0.5;
    %adjust1 = vectorScale(%adjust1,-0.5 * %offset1);

    %deplObj = new (%className)() {
		dataBlock = %dataBlock;
		scale = %scale3;
	};
    %deplObj.state = "closed";
    %deplObj.closedscale = %scale3;
    %deplObj.openedscale = getwords(%scale3,0,1) SPC 0.1;
//////////////////////////Apply settings//////////////////////////////

	// exact:
	//%deplObj.setTransform(%item.surfacePt SPC %rot1);
    %deplObj.setTransform(vectorAdd(VectorAdd(%item.surfacePt2, VectorScale(%vec1,getword(%scale3,2)/-4)),%adjust1) SPC %rot1);

	// misc info
	addDSurface(%item.surface,%deplObj);
	// [[Settings]]:

	%deplObj.grounded = %grounded;
	%deplObj.needsFit = 1;
    %deplObj.isdoor   = 1;
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

	%deplObj.deploy();

// Power object
%deplobj.timeout=getWord($expertSetting["Door",%plyr.expertSet],0);
%deplobj.hasslided=0;

if (%plyr.packset==0)
%deplobj.toggletype=0;

if (%plyr.packset==1)
%deplobj.toggletype=1;

if (%plyr.packset==2) {
%deplobj.toggletype=0;
%deplobj.powercontrol=1; //staticshape.cs function StaticShapeData::onGainPowerEnabled |and| function StaticShapeData::onLosePowerDisabled
}                        //for power togle code
//"closed when powered";  2
if (%plyr.packset==3) {
%deplobj.toggletype=1;
%deplobj.powercontrol=2;
}
//"opened when powered";  3
if (%plyr.packset==4) {
%deplobj.toggletype=1;
%deplobj.powercontrol=3;
}
//collision door
if (%plyr.packset==5){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =0;
}
//lv 1 door
if (%plyr.packset==6){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =1;
}
//lv 2 door
if (%plyr.packset==7){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =2;
}
//lv 3 door
if (%plyr.packset==8){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =3;
}
//owner door
if (%plyr.packset==9){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =4;
}
//admin door
if (%plyr.packset==10){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =5;
}
//super admin door
if (%plyr.packset==11){
%deplobj.toggletype=0;
%deplobj.Collision = true;
%deplobj.lv =6;
}
%deplobj.canmove = true;
checkPowerObject(%deplObj);
	return %deplObj;
}

function DeployedTdoor::onDestroyed(%this, %obj, %prevState) {
	if (%obj.isRemoved)
		return;
	%obj.isRemoved = true;
	Parent::onDestroyed(%this, %obj, %prevState);
	$TeamDeployedCount[%obj.team, TdoorDeployable]--;
	remDSurface(%obj);
	%obj.schedule(500,"delete");
	cascade(%obj);
	fireBallExplode(%obj,1);
}

function DeployedTdoor::disassemble(%data,%plyr,%hTgt) {
	disassemble(%data,%plyr,%hTgt);
}

function TdoorDeployableImage::onMount(%data,%obj,%node) {
	%obj.hasdoor = true; // set for mspinecheck
	%obj.packSet = 0;
	%obj.expertSet = 0;
	displayPowerFreq(%obj);
}

function TdoorDeployableImage::onUnmount(%data,%obj,%node) {
	%obj.hasdoor = "";
	%obj.packSet = 0;
	%obj.expertSet = 0;
}
////////////////////
////////////////////
function open(%obj){
if (!isObject(%obj))
   return;
if (%obj.issliding !=1 && %obj.canmove == true){
   %obj.issliding = 1;
   %obj.moving = "open";
   %obj.dist   = getWord(%obj.scale,2);
   %amount1    = %obj.dist*100;
   %amount2    = mfloor(%amount1);
   %obj.amount = %amount2/5;
   %obj.oldscale  = %obj.scale;
   %obj.prevscale=%obj.scale;
%obj.canmove = false;
}
if (%obj.hasmoved !=1)
   %obj.closedscale = %obj.scale;
if (getWord(%obj.scale,2)<0.4){
   %obj.issliding = 0;
   %obj.scale =  getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC 0.1;
   %obj.settransform(%obj.gettransform());
   %obj.state = "opened";
   %obj.openedscale  = %obj.scale;
   %obj.hasmoved=0;
   if (%obj.toggletype ==0)
      schedule(%obj.timeout*1000,0,"close",%obj,1);
   else
       %obj.canmove = true;
   if (%obj.powercontrol==2 && %obj.powercount >0)//"closed when powered";  2
      schedule(200,0,"close",%obj);
   if (%obj.powercontrol==3 && %obj.powercount ==0)//"opened when powered";  3
      schedule(200,0,"close",%obj);
   return;
   }
%obj.hasmoved=1;
//%obj.scale = getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.prevscale,2)-0.4;
%obj.scale = getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.prevscale,2)-2.0;
%obj.settransform(%obj.gettransform());
%obj.prevscale=%obj.scale;
schedule(200,0,"open",%obj);
}
/////////////////////
/////////////////////
function close(%obj,%timeout){
if (!isObject(%obj))
   return;
if (%obj.issliding !=1){
   %obj.issliding = 1;
   %obj.moving = "close";
   %obj.dist      = getWord(%obj.oldscale,2);
   %amount1       = %obj.dist*100;
   %amount2       = mfloor(%amount1);
   %obj.amount    = %amount2/10;
   %obj.prevscale=%obj.scale;
%obj.canmove = false;
}
if (%obj.hasmoved !=1)
   %obj.openedscale = %obj.scale;
if (getWord(%obj.scale,2)>getWord(%obj.closedscale,2)-0.2){
   %obj.issliding = 0;
   %obj.scale =getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.closedscale,2);
   %obj.settransform(%obj.gettransform());
   %obj.state = "closed";
   %obj.closedscale = %obj.scale;
   %obj.canmove = true;
   %obj.hasmoved=0;
   if (%obj.powercontrol==3 && %obj.powercount >0)//"opened when powered";  3
      schedule(200,0,"open",%obj);
   if (%obj.powercontrol==2 && %obj.powercount ==0)//"closed when powered";  2
      schedule(200,0,"open",%obj);
   return;
   }
%obj.hasmoved=1;
//%obj.scale = getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.prevscale,2)+0.4;
%obj.scale = getWord(%obj.scale,0) SPC getWord(%obj.scale,1) SPC getWord(%obj.prevscale,2)+2.0;
%obj.settransform(%obj.gettransform());
%obj.prevscale=%obj.scale;
schedule(200,0,"close",%obj);
}
