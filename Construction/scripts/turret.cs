// sounds and effects
///////////////////////
datablock EffectProfile(DeployableExplosionEffect)
{
   effectname = "explosions/explosion.xpl10";
   minDistance = 10;
   maxDistance = 50;
};

datablock AudioProfile(DeployablesExplosionSound)
{
   filename = "fx/explosions/deployables_explosion.wav";
   description = AudioExplosion3d;
   preload = true;
   effect = DeployableExplosionEffect;
};

//--------------------------------------------------------------------------
// Shockwave
//--------------------------------------------------------------------------
datablock ShockwaveData(TurretShockwave)
{
   width = 6.0;
   numSegments = 20;
   numVertSegments = 2;
   velocity = 8;
   acceleration = 20.0;
   lifetimeMS = 1500;
   height = 1.0;
   verticalCurve = 0.5;

   mapToTerrain = false;
   renderBottom = true;

   texture[0] = "special/shockwave4";
   texture[1] = "special/gradient";
   texWrap = 6.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "0.8 0.8 0.8 1.00";
   colors[1] = "0.8 0.5 0.2 0.20";
   colors[2] = "1.0 0.5 0.5 0.0";
};

//--------------------------------------------------------------------------
// Explosion
//--------------------------------------------------------------------------
datablock ExplosionData(TurretExplosion)
{
   explosionShape = "effect_plasma_explosion.dts";
   soundProfile = ShapeExplosionSound;
   faceViewer = true;
   shockwave = TurretShockwave;
};

datablock ExplosionData(SmallTurretExplosion)
{
   soundProfile = DeployablesExplosionSound;
   faceViewer = true;

   explosionShape = "effect_plasma_explosion.dts";
   sizes[0] = "0.3 0.3 0.3";
   sizes[1] = "0.3 0.3 0.3";
   times[0] = 0;
   times[1] = 1;
};


//--------------------------------------------------------------------------
// Turret Debris
//--------------------------------------------------------------------------
datablock DebrisData( TurretDebris )
{
   explodeOnMaxBounce = false;

   elasticity = 0.20;
   friction = 0.5;

   lifetime = 17.0;
   lifetimeVariance = 0.0;

   minSpinSpeed = 60;
   maxSpinSpeed = 600;

   numBounces = 10;
   bounceVariance = 0;

   staticOnMaxBounce = true;

   useRadiusMass = true;
   baseRadius = 0.4;

   velocity = 9.0;
   velocityVariance = 4.5;
};

datablock DebrisData( TurretDebrisSmall )
{
   explodeOnMaxBounce = false;

   elasticity = 0.20;
   friction = 0.5;

   lifetime = 17.0;
   lifetimeVariance = 0.0;

   minSpinSpeed = 60;
   maxSpinSpeed = 600;

   numBounces = 10;
   bounceVariance = 0;

   staticOnMaxBounce = true;

   useRadiusMass = true;
   baseRadius = 0.2;

   velocity = 5.0;
   velocityVariance = 2.5;
};


//--------------------------------------------------------------------------
// Turret base class functionality.  Barrels are in scripts/weapons/*.cs
//
//
//--------------------------------------------------------------------------

function GameBaseData::hasLOS() {
	return 1;
}

function TurretData::create(%block)
{
   %obj = new Turret() {
      dataBlock = %block;
   };

   return %obj;
}

datablock SensorData(TurretBaseSensorObj)
{
   detects = true;
   detectsUsingLOS = true;
   detectsPassiveJammed = false;
   detectsActiveJammed = false;
   detectsCloaked = false;
   detectionPings = true;
   detectRadius = 80;
};


datablock TurretData(TurretBaseLarge) : TurretDamageProfile
{
   className      = TurretBase;
   catagory       = "Turrets";
   shapeFile      = "turret_base_large.dts";
   preload        = true;

   mass           = 1.0;  // Not really relevant

   maxDamage      = 2.25;
   destroyedLevel = 2.25;
   disabledLevel  = 1.35;
   explosion      = TurretExplosion;
	expDmgRadius = 15.0;
	expDamage = 0.66;
	expImpulse = 2000.0;
   repairRate     = 0.5;
   emap = true;

   thetaMin = 15;
   thetaMax = 140;

   isShielded           = true;
   energyPerDamagePoint = 50;
   maxEnergy = 150;
   rechargeRate = 0.31;
   humSound = SensorHumSound;
   pausePowerThread = true;

   canControl = true;
   cmdCategory = "Tactical";
   cmdIcon = CMDTurretIcon;
   cmdMiniIconName = "commander/MiniIcons/com_turretbase_grey";
   targetNameTag = 'Base';
   targetTypeTag = 'Turret';
   sensorData = TurretBaseSensorObj;
   sensorRadius = TurretBaseSensorObj.detectRadius;
   sensorColor = "0 212 45";
   heatSignature = 1.0;
   firstPersonOnly = true;

   debrisShapeName = "debris_generic.dts";
   debris = TurretDebris;
};

function TurretData::onGainPowerEnabled(%data, %obj) {
	if (shouldChangePowerState(%obj,true))
		setTargetSensorData(%obj.target, %data.sensorData);
	Parent::onGainPowerEnabled(%data, %obj);
}

function TurretData::onLosePowerDisabled(%data, %obj) {
	if (shouldChangePowerState(%obj,false)) {
		// Must kick players out of turret
		%obj.clearTarget();
		setTargetSensorData(%obj.target, 0);
	}
	Parent::onLosePowerDisabled(%data, %obj);
}

//------------------------------------------------------------------------------

function TurretData::CanSeeTarget(%this, %target, %turret)
{


//  %vec = %sender.player.getEyeVector();

//  %targetPos = vectorAdd(%pos, vectorScale(%vec, 50));

%targetPos = %target.getPosition();
//%targetPos = PosFromTransform(%target.getEyeTransform());

//%pos = getWord(%turret.getPosition(),0) SPC getWord(%turret.getPosition(),1) SPC (getWord(%turret.getPosition(),2)+1);
//%pos = %turret.getPosition();
%pos = %turret.getMuzzlePoint(0);
//%pos = PosFromTransform(%turret.getEyeTransform());
%vec = VectorNormalize(VectorAdd(%turret.getMuzzlePoint(0),%targetPos));
%srchRange = VectorScale(%vec,5);
%srchEnd = VectorAdd(%targetPos, %srchRange);
//%tpos = VectorAdd(%pos,%targetPos);

  %damageMasks = $TypeMasks::InteriorObjectType | $TypeMasks::StaticShapeObjectType | $TypeMasks::PlayerObjectType | $TypeMasks::ForceFieldObjectType;

//%damageMasks = $TypeMasks::StaticShapeObjectType;

  %player = containerRaycast(%pos, %srchEnd, %damageMasks, %turret);


	if ((%player == %target.player))
		return true;
	else
		return false;


}

//------------------------------------------------------------------------------

function TurretData::selectTarget(%this, %turret) {
	%turretTarg = %turret.getTarget();
	if(%turretTarg == -1)
		return;

	// if the turret isn't on a team, don't fire at anyone
	if(getTargetSensorGroup(%turretTarg) == 0) {
		%turret.clearTarget();
		return;
	}

	// stop firing if turret is disabled or if it needs power and isn't powered
	if((!%turret.isPowered()) && (!%turret.needsNoPower)) {
		%turret.clearTarget();
		return;
	}

//	if ($Host::Purebuild == 1 && $TurretEnableOverride != 1) {
//		%turret.clearTarget();
//		return;
//	}

//////////////////////
//start turret repair
if(%turret.isrepair == 1){ //start turret repair
if (%turret.repairing!=0 && %turret.repairing !$=""){
   if (%turret.repairing.getDamageLevel()<=0){
      %turret.setTargetObject("");
      stopRepairing(%turret);
      %turret.repairing=0;
      }
   }
	%TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType |
	$TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType | $TypeMasks::StaticObjectType | $TypeMasks::ItemObjectType; //$TypeMasks::StaticObjectType;

	InitContainerRadiusSearch(%turret.getMuzzlePoint(0),
				              %turret.getMountedImage(0).attackRadius,
			                  %TargetSearchMask);

	// TODO - clean up this mess + GameBaseData::hasLOS()

   while ((%potentialTarget = ContainerSearchNext()) != 0){
       if (%turret.team == %potentialTarget.team){
          if (%potentialTarget.getDamageLevel() != 0 && %potentialTarget.isDestroyed() != 1){
          if (%turret.repairing==0 || %turret.repairing$=""){
                %turret.repairing=%potentialTarget;
                %turret.setTargetObject(%potentialTarget);
                %val=validatetarget(%turret,0,%turret.repairing);
                if (%val==0){
                  %turret.setTargetObject("");
                  stopRepairing(%turret);
                  %turret.repairing=0;
                  }
                else if (%val==1){
                     schedule(100,0,"startRepairing1",%turret,0);
                     return;
                     }
             }
          else {
               %val=validatetarget(%turret,0,%turret.repairing);
               if (%val==0){
                  %turret.setTargetObject("");
                  stopRepairing(%turret);
                  %turret.repairing=0;
                  }
               %turret.setTargetObject(%turret.repairing);
               }
             }
          }
       }
}
//end repair turret
//////////////////


    %TargetSearchMask = $TypeMasks::PlayerObjectType | $TypeMasks::VehicleObjectType | $TypeMasks::StationObjectType | $TypeMasks::GeneratorObjectType | $TypeMasks::SensorObjectType | $TypeMasks::TurretObjectType; //$TypeMasks::StaticObjectType;
    InitContainerRadiusSearch(%turret.getMuzzlePoint(0),
				%turret.getMountedImage(0).attackRadius,
				%TargetSearchMask);

	// TODO - clean up this mess + GameBaseData::hasLOS()
//Fire mode 0 -- Normal
//Fire mode 1 -- Engage all except buddies and self.
	while ((%potentialTarget = ContainerSearchNext()) != 0) {
		if (%potentialtarget) {
			%potTargTarg = %potentialTarget.getTarget();
		if( %this.CanSeeTarget(%potentialTarget,%turret)
			&& (%turret.isValidTarget(%potentialTarget) //Fire mode 0 -- Normal firing mode
			&& (getTargetSensorGroup(%turretTarg) != getTargetSensorGroup(%potTargTarg))
			&& (getTargetSensorGroup(%potTargTarg) != 0)
			&& ((%potentialTarget.getType() & $TypeMasks::PlayerObjectType) || !$TurretOnlyTargetPlayers))

		 || (%turret.firemode == 1 && (%potentialTarget.getType() & $TypeMasks::PlayerObjectType) && !isbuddy(%turret.getOwner(),%potentialTarget.client) && %turret.getOwner() != %potentialTarget.client) || (%turret.firemode == 2 && (%potentialTarget.getType() & $TypeMasks::PlayerObjectType) && isenemy(%turret.getOwner(),%potentialTarget.client) && %turret.getOwner() != %potentialTarget.client)) {
    if (%potentialTarget.homingCount > 0 && !%secondTarg) {
					if (!%firstTarg)
						%firstTarg = %potentialTarget;
					else
						%secondTarg = %potentialTarget;
				}
				else {
					%turret.setTargetObject(%potentialTarget);
					%turret.aquireTime = getSimTime();
					return;
				}
			}
		}
	}
	if (%secondTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
  return;
	}
	if (%firstTarg) {
		%turret.setTargetObject(%firstTarg);
		%turret.aquireTime = getSimTime();
		return;
	}
}

function TurretData::replaceCallback(%this, %turret, %engineer)
{
   // This is a valid replacement.  First, let's see if the engineer
   //  still has the correct pack in place...
   if (%engineer.getMountedImage($BackPackSlot) != 0)
   {
      %barrel = %engineer.getMountedImage($BackPackSlot).turretBarrel;
      if (%barrel !$= "")
      {
         // if there was a barrel there before, get rid of it
         %turret.unmountImage(0);
         // remove the turret barrel pack
         %engineer.setInventory(%engineer.getMountedImage($BackPackSlot).item, 0);
         // mount new barrel on base
         %turret.mountImage(%barrel, 0, false);
      }
      else
      {
         // Player doesn't have the correct pack on...
      }
   }
   else
   {
      // Player doesn't have any pack on...
   }
}

function TurretData::onDestroyed(%this, %turret, %prevState)
{
   if( isObject( %turret.lastProjectile ) )
      %turret.lastProjectile.delete();

   Parent::onDestroyed(%this, %turret, %prevState);
}

function checkTurretMount(%data, %obj, %slot)
{
   // search for a turret base in player's LOS
   %eyeVec = VectorNormalize(%obj.getEyeVector());
   %srchRange = VectorScale(%eyeVec, 5.0); // look 5m for a turret base
   %plTm = %obj.getEyeTransform();
   %plyrLoc = firstWord(%plTm) @ " " @ getWord(%plTm, 1) @ " " @ getWord(%plTm, 2);
   %srchEnd = VectorAdd(%plyrLoc, %srchRange);
   %potTurret = ContainerRayCast(%obj.getEyeTransform(), %srchEnd, $TypeMasks::TurretObjectType);
   if(%potTurret != 0)
   {
      %otherMountObj = "foo";

	if(%potTurret.getDatablock().getName() $= "TurretBaseLarge"
	|| %potTurret.getDatablock().getName() $= %otherMountObj
	|| %potTurret.getDatablock().getName() $= "TurretDeployedBase")
      {
         // found a turret base, what team is it on?
         if(%potTurret.team == %obj.client.team)
         {
				if(%potTurret.getDamageState() !$= "Enabled")
				{
					// the base is destroyed
					messageClient(%obj.client, 'MsgBaseDestroyed', "\c2Turret base is disabled, cannot mount barrel.");
					%obj.setImageTrigger($BackpackSlot, false);
				}
				else
				{
		         // it's a functional turret base on our team! stick the barrel on it
		         messageClient(%obj.client, 'MsgTurretMount', "\c2Mounting barrel pack on turret base.");
		         serverPlay3D(TurretPackActivateSound, %potTurret.getTransform());
		         %potTurret.initiateBarrelSwap(%obj);
				}
         }
         else
         {
            // whoops, wrong team
            messageClient(%obj.client, 'MsgTryEnemyTurretMount', "\c2Cannot mount a barrel on an enemy turret base!");
            %obj.setImageTrigger($BackpackSlot, false);
         }
      }
      else
      {
         // tried to mount barrel on some other turret type
         messageClient(%obj.client, 'MsgNotTurretBase', "\c2Can only mount a barrel on a turret base.");
         %obj.setImageTrigger($BackpackSlot, false);
      }
   }
   else
   {
      // I don't see any turret
      messageClient(%obj.client, 'MsgNoTurretBase', "\c2No turret within range.");
      %obj.setImageTrigger($BackpackSlot, false);
   }
}

//-------------------------------------- Load Barrel Images
//
exec("scripts/turrets/mortarBarrelLarge.cs");
exec("scripts/turrets/aaBarrelLarge.cs");
exec("scripts/turrets/chaingunBarrelLarge.cs");
exec("scripts/turrets/missileBarrelLarge.cs");
exec("scripts/turrets/plasmaBarrelLarge.cs");
exec("scripts/turrets/ELFBarrelLarge.cs");
exec("scripts/turrets/outdoorDeployableBarrel.cs");
exec("scripts/turrets/indoorDeployableBarrel.cs");
exec("scripts/turrets/sentryTurret.cs");
exec("scripts/Krypton/repairturret.cs");
