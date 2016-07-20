datablock AudioDescription(Apachethrust)
{
   volume   = 3.0;
   isLooping= true;

   is3D     = true;
   minDistance= 80.0;
   MaxDistance= 150.0;
   type     = $EffectAudioType;
   environmentLevel = 3.0;
};
datablock AudioProfile(ApacheFlyerEngineSound)
{
   filename    = "fx/vehicles/wake_wildcat.wav";
   description = Apachethrust;
   preload = true;
   //effect = BomberFlyerEngineEffect;
};
datablock AudioProfile(ApacheVehicleSound)
{
   filename    = "fx/vehicles/wake_wildcat.wav";
   description = Apachethrust;
   preload = true;
   //effect = BomberFlyerEngineEffect;
};
datablock AudioProfile(ApacheRocketFireSound)
{
   filename    = "fx/powered/turret_mortar_fire.wav";
   description = AudioDefault3d;
   preload = true;
   effect = MortarFireEffect;
};
datablock AudioProfile(ApacheFlyerThrustSound)
{
   filename    = "fx/weapons/htransport_thrust.wav";
   description = Apachethrust;
   preload = true;
   //effect = BomberFlyerEngineEffect;
};
datablock AudioProfile(ApacheChaingunFireSound)
{
   filename    = "fx/vehicles/tank_chaingun.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = ChaingunFireEffect;
};
datablock AudioProfile(ApacheChaingunSpinUpSound)
{
   filename    = "fx/weapons/chaingun_spinup.wav";
   description = AudioClosest3d;
   preload = true;
   effect = ChaingunSpinUpEffect;
};
datablock AudioProfile(ApacheChaingunSpinDownSound)
{
   filename    = "fx/weapons/chaingun_spindown.wav";
   description = AudioClosest3d;
   preload = true;
   effect = ChaingunSpinDownEffect;
};
datablock ParticleData(TMStreakParticle)
{
   dragCoefficient      = 1.25;
   gravityCoefficient   = 0;
   inheritedVelFactor   = 0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 300;
   textureName          = "particleTest";
   colors[0]     = "0.9 0.9 0.9 1";
   colors[1]     = "0.1 0.1 0.1 0";
};

datablock ParticleEmitterData(TMStreakEmitter)
{
   ejectionPeriodMS = 12;
   periodVarianceMS = 2;
   ejectionVelocity = 1;
   velocityVariance = 0.5;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 8;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "TMStreakParticle";
};
datablock SeekerProjectileData(StreakRocket)
{
   projectileShapeName = "weapon_missile_projectile.dts";
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 25.0;
   radiusDamageType    = $DamageType::Blaster;
   kickBackStrength    = 2000;
   explosion           = "GrenadeExplosion";
   underwaterExplosion = "UnderwaterMortarExplosion";
   splash              = MissileSplash;
   velInheritFactor    = 1.0;    // to compensate for slow starting velocity, this value
                                 // is cranked up to full so the missile doesn't start
                                 // out behind the player when the player is moving
                                 // very quickly - bramage

   baseEmitter         = TMStreakEmitter;
   delayEmitter        = MissileFireEmitter;
   puffEmitter         = MissilePuffEmitter;
   bubbleEmitter       = MortarBubbleEmitter;
   bubbleEmitTime      = 1.0;

   exhaustEmitter      = MissileLauncherExhaustEmitter;
   exhaustTimeMs       = 300;
   exhaustNodeName     = "muzzlePoint1";

   lifetimeMS          = 6000;
   muzzleVelocity      = 370.0;
   maxVelocity         = 180.0;
   turningSpeed        = 110.0;
   acceleration        = 200.0;

   proximityRadius     = 3;

   terrainAvoidanceSpeed         = 180;
   terrainScanAhead              = 25;
   terrainHeightFail             = 12;
   terrainAvoidanceRadius        = 100;

   flareDistance = 200;
   flareAngle    = 30;

   sound = MissileProjectileSound;

   hasLight    = true;
   lightRadius = 5.0;
   lightColor  = "0.2 0.05 0";


   explodeOnWaterImpact = false;
};

datablock FlyingVehicleData(ApacheFlyer) : ShrikeDamageProfile
{
   spawnOffset = "0 0 2";

   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   multipassenger = false;
   computeCRC = true;
   numWeapons              = 2;
   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.25;
   density = 1.0;

   mountPose[0] = sitting;
   numMountPoints = 1;
   isProtectedMountPoint[0] = true;
   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = ShrikeExplosion;
	explosionDamage = 1.25;
	explosionRadius = 12.5;

   keepReticle = true;

   maxDamage = 12.40;
   destroyedLevel = 12.40;

   isShielded = true;
   energyPerDamagePoint = 75;
   maxEnergy = 290;      // Afterburner and any energy weapon pool
   minDrag = 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)
   rechargeRate = 0.825;

   maxAutoSpeed = 8;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 350;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 215;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 5;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 3;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 1;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 8200;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 1250;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 475;      // Steering jets (how much you heel over when you turn)
   rollForce = 4;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 10;        // Height off the ground at rest
   createHoverHeight = 5;  // Height off the ground when created
   maxForwardSpeed = 8500;
   strafeThrustForce  = 80;
   // Turbo Jet
   jetForce = 2000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 16;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0.2;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 14.15;

   // Rigid body
   mass = 138;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 23;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.06;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 100;      // The speed your contrail shows up at.
   trailEmitter = ContrailEmitter;
   //forwardJetEmitter = ShrikeEngineEmitter;
   //downJetEmitter = TankDustEmitter;

   //
   jetSound = ApacheFlyerThrustSound;
   engineSound = ApacheFlyerEngineSound;
   softImpactSound = SoftImpactSound;
   hardImpactSound = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0;
   mediumSplashSoundVelocity = 15.0;
   hardSplashSoundVelocity = 20.0;
   exitSplashSoundVelocity = 10.0;

   exitingWater      = VehicleExitWaterMediumSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterMediumSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeMediumSplashSound;

   dustEmitter = VehicleLiftoffDustEmitter;
   triggerDustHeight = 4.0;
   dustHeight = 1.0;

   damageEmitter[0] = LightDamageSmoke;
   damageEmitter[1] = HeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -3.0 0.0 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   //
   max[chaingunAmmo] = 1000;

   minMountDist = 4;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   cmdCategory = "Tactical";
   cmdIcon = CMDFlyingScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_scout_grey";
   targetNameTag = 'Apache';
   targetTypeTag = 'Gunship';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "255 194 9";



   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

};
datablock ShapeBaseImageData(ApacheDecal)
{
   shapeFile = "vehicle_grav_tank.dts";
   offset = "0 -2.44 -0.95";
   mountPoint = 10;
};
datablock ShapeBaseImageData(ApacheDecal2)
{
   shapeFile = "vehicle_grav_scout.dts";
   offset = "0 -6.8 1.0";
   rotation = "0 1 0 180";
};

datablock ShapeBaseImageData(ApacheRocketPairImage)
{
   className = WeaponImage;
   shapeFile = "TR2weapon_grenade_launcher.dts";
   item      = Chaingun;
   ammo   = ChaingunAmmo;
   projectile = StreakRocket;
   projectileType = SeekerProjectile;
   projectileSpread = 3.0 / 1000.0;
   isSeeker     = true;
   seekRadius   = 400;
   maxSeekAngle = 18;
   seekTime     = 0.3;
   minSeekHeat  = 0.3;  // the heat that must be present on a target to lock it.
   mountPoint = 2;
   offset = "3.4 2.05 -0.5";
   emap = true;
   usesEnergy = true;
   useMountEnergy = true;

   // DAVEG -- balancing numbers below!
   minEnergy = 50;
   fireEnergy = 10;
   fireTimeout = 1000;
   projectileSpread = 8.0 / 1000.0;
   muzzleFlash = ChaingunTurretMuzzleFlash;
   //--------------------------------------
   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateAllowImageChange[0] = false;
   stateTimeoutValue[0]        = 0.05;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";
   stateSound[0] = MILSwitchSound;
   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1]      = "NoAmmo";
   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "Ready";
   stateTransitionOnTriggerDown[2] = "DryFire";
   //--------------------------------------
   stateName[3]         = "Spinup";
   stateTimeoutValue[3]          = 0.01;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";
   //--------------------------------------
   stateName[4]             = "Fire";
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateSound[4]            = ApacheRocketFireSound;
   // IMPORTANT! The stateTimeoutValue below has been replaced by fireTimeOut
   // above.
   stateTimeoutValue[4]          = 2.0;
   stateTransitionOnTimeout[4]   = "checkState";
   stateEmitter[4]                     = StarburstFlameEmitter;
   stateEmitterTime[4]                 = 0.05;
   stateEmitterNode[4]                 = "muzzlePoint1";
   //--------------------------------------
   stateName[5]       = "Spindown";
   stateTimeoutValue[5]            = 0.01;
   stateWaitForTimeout[5]          = false;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";
   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateTransitionOnAmmo[6]   = "Ready";
   stateTimeoutValue[6]        = 0.01;
   stateTransitionOnTimeout[6] = "NoAmmo";
   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = MissileDryFireSound;
   stateTransitionOnTriggerUp[7] = "NoAmmo";
   stateTimeoutValue[7]        = 0.25;
   stateTransitionOnTimeout[7] = "NoAmmo";

   stateName[8] = "checkState";
   stateTransitionOnTriggerUp[8] = "Spindown";
   stateTransitionOnNoAmmo[8]    = "EmptySpindown";
   stateTimeoutValue[8]          = 0.01;
   stateTransitionOnTimeout[8]   = "ready";
};

datablock ShapeBaseImageData(ApacheRocketImage) : ApacheRocketPairImage
{
   offset = "-3.4 2.05 -0.5";//"-.95 0.6 -0.1";
   stateScript[3]           = "onTriggerDown";
   stateScript[5]           = "onTriggerUp";
   stateScript[6]           = "onTriggerUp";

};
function ApacheRocketImage::onTriggerDown(%this, %obj, %slot)
{
}

function ApacheRocketImage::onTriggerUp(%this, %obj, %slot)
{
}

function ApacheRocketImage::onMount(%this, %obj, %slot)
{
//   %obj.setImageAmmo(%slot,true);
}

function ApacheRocketImage::onMount(%this, %obj, %slot)
{
//   %obj.setImageAmmo(%slot,true);
}

function ApacheRocketPairImage::onUnmount(%this,%obj,%slot)
{
}

function ApacheRocketPairImage::onUnmount(%this,%obj,%slot)
{
}
datablock ShapeBaseImageData(ApacheChaingunPairImage)
{
   className = WeaponImage;
   shapeFile = "TR2weapon_chaingun.dts";
   item      = Chaingun;
//   ammo   = ChaingunAmmo;
   projectile = AssaultChaingunBullet;
   projectileType = TracerProjectile;
   mountPoint = 10;
   offset = "1.88 2.1 -0.42";
   projectileSpread = 4.0 / 1000.0;
   usesEnergy = true;
   useMountEnergy = true;
   // DAVEG -- balancing numbers below!
   minEnergy = 1.0;
   fireEnergy = 1.0;
   fireTimeout = 10;

   // State transitions
   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateSound[0]            = ChaingunSwitchSound;
   stateAllowImageChange[0] = false;
   //
   stateTimeoutValue[0]        = 0.2;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";

   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   //
   stateTransitionOnTriggerDown[1] = "Spinup";
   stateTransitionOnNoAmmo[1]      = "NoAmmo";

   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "Ready";
   stateSpinThread[2]         = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";

   //--------------------------------------
   stateName[3]         = "Spinup";
   stateSpinThread[3]   = SpinUp;
   stateSound[3]        = ApacheChaingunSpinUpSound;
   //
   stateTimeoutValue[3]          = 0.15;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   stateSound[4]            = ApacheChaingunFireSound;
   //stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;

   //
   stateTimeoutValue[4]          = 0.2;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = ApacheChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   //
   stateTimeoutValue[5]            = 0.15;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateSound[6]      = ApacheChaingunSpinDownSound;
   stateSpinThread[6] = SpinDown;
   //
   stateTimeoutValue[6]        = 0.35;
   stateTransitionOnTimeout[6] = "NoAmmo";

   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.35;
   stateTransitionOnTimeout[7] = "NoAmmo";
};

datablock ShapeBaseImageData(ApacheChaingunImage) : ApacheChaingunPairImage
{
   rotation = ("0 1 0 180");
   offset = "-1.88 2.1 -0.42";
   stateScript[3]           = "onTriggerDown";
   stateScript[8]           = "onTriggerUp";
   stateScript[9]           = "onTriggerUp";
};
////////////////////////Functions///////////////////////////////////////////////
function ApacheFlyer::playerMounted(%data, %obj, %player, %node)
{
   // scout flyer == SUV (single-user vehicle)
   commandToClient(%player.client, 'setHudMode', 'Pilot', "Shrike", %node);
   commandToClient(%player.client,'SetWeaponryVehicleKeys', true);


   %obj.owner = %player.client;

      // update observers who are following this guy...
   if( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
  parent::playerMounted(%data, %obj, %player, %node);
}
function ApacheFlyer::onAdd(%data, %obj)
{
   Parent::onAdd(%data, %obj);
//   %obj.mountImage(ApacheDecal2, 6);
   %obj.mountImage(ApacheRocketImage, 4);
   %obj.mountImage(ApacheRocketPairImage, 5);
   %obj.mountImage(ApacheChaingunImage, 2);
   %obj.mountImage(ApacheChaingunPairImage, 3);
   %obj.mountImage(ApacheDecal, 6);
   //%obj.ApacheDecal = new StaticShape()
//{
//scale = "0.9 0.58 0.615";
//dataBlock = "ApacheDecal";
//lockCount = "0";
//homingCount = "0";
//team = %obj.team;
//};
//%decal = %obj.ApacheDecal;
//%obj.turretObject = %decal;
//%decal.vehicleMounted = %obj;
//%obj.mountObject(%obj.ApacheDecal, 10);
//setTargetSensorGroup(%decal.getTarget(), %decal.team);
//setTargetNeverVisMask(%decal.getTarget(), 0xffffffff);
//%decal.setThreadDir($DeployThread, true);
//%decal.playThread($DeployThread,"deploy");

   %obj.selectedWeapon = 1;
   %obj.veh_weapon[0] = "Apache Gatling Laser";
   %obj.veh_weapon_ammo[0] = "inf";

   %obj.veh_weapon[1] = "HE Rockets";
   %obj.veh_weapon_ammo[1] = "inf";

   %obj.weapon[1, Display] = true;
   %obj.weapon[1, Name] = "Apache Gatling Laser";
   %obj.weapon[1, Description] = "Pulse Laser rounds";
   %obj.weapon[1, Hilite] = "gui/hud_blaster";
   
   %obj.weapon[2, Display] = true;
   %obj.weapon[2, Name] = "HE Rockets";
   %obj.weapon[2, Description] = "High Explosive Non-guided rockets";
   %obj.weapon[2, Hilite] = "gui/hud_missiles";
   $numVWeapons = 2;
   serverPlay3D(ApacheVehicleSound, %obj.getTransform());
   %obj.veh_description = "Apache Warbird. Grenade key activates countermeasures";
   %obj.schedule(5400, "playShieldEffect", "0.0 0.0 1.0");
   %obj.schedule(3750, "playThread", $ActivateThread, "activate");

}
function ApacheFlyer::deleteAllMounted(%data, %obj)
{
   %decal = %obj.getMountNodeObject(10);
   if(!%decal)
      return;
   %decal.killit();


}
function ApacheFlyer::onRemove(%this, %obj)
{
   ApacheFlyer::deleteAllMounted(%this, %obj);

   Parent::onRemove(%this, %obj);
}
function ApacheFlyer::onTrigger(%data, %obj, %trigger, %state)
{
     %client = %obj.getControllingClient();
     if(%trigger == 0)
     {
          if(%obj.selectedWeapon == 1)
          {
               %obj.setImageTrigger(2, false);
               %obj.setImageTrigger(3, false);
               %obj.setImageTrigger(4, false);
               %obj.setImageTrigger(5, false);


               switch(%state)
               {
                   case 0:
                   %obj.fireWeapon = false;
                   %obj.setImageTrigger(2, false);
                   %obj.setImageTrigger(3, false);
                   case 1:
                   %obj.fireWeapon = true;
                   %obj.setImageTrigger(2, true);
                   %obj.setImageTrigger(3, true);


               }
          }
          else if(%obj.selectedWeapon == 2)
          {
               %obj.setImageTrigger(2, false);
               %obj.setImageTrigger(3, false);
               %obj.setImageTrigger(4, false);
               %obj.setImageTrigger(5, false);


               switch(%state)
               {
                   case 0:
                   %obj.fireWeapon = false;
                   %obj.setImageTrigger(4, false);
                   %obj.setImageTrigger(5, false);
                   case 1:
                   %obj.fireWeapon = true;
                   if(%obj.nextWeaponFire == 2)
                   {
                         %obj.setImageTrigger(4, true);
                         %obj.setImageTrigger(5, false);
                   }
                   else
                   {
                         %obj.setImageTrigger(4, false);
                         %obj.setImageTrigger(5, true);
                   }
               }


          }



     }
    if (%trigger ==4)    // Throw flare
      {
      if (%state == 1)
      {
      %p = new FlareProjectile()
      {
         dataBlock        = FlareGrenadeProj;
         initialDirection = "0 -1 0";
         initialPosition  = getBoxCenter(%obj.getWorldBox());
         sourceObject     = %obj;
         sourceSlot       = 0;
      };

      FlareSet.add(%p);
      MissionCleanup.add(%p);
      serverPlay3D(GrenadeThrowSound, getBoxCenter(%obj.getWorldBox()));
      %p.schedule(6000, "killit");
      // miscellaneous grenade-throwing cleanup stuff
      %obj.lastThrowTime[%data] = getSimTime();
      %obj.throwStrength = 0;
      return %p;
      }
      else
      {
      messageClient(%obj.client, 'MsgNoFlare', 'c2You have no flares left.~wfx/misc/misc.error.wav');
      }
   }

}

function ApacheFlyer::playerDismounted(%data, %obj, %player)
{
   %obj.fireWeapon = false;
   %obj.setImageTrigger(2, false);
   %obj.setImageTrigger(3, false);
   %obj.setImageTrigger(4, false);
   %obj.setImageTrigger(5, false);


   if(%player.isCloaked())
      %player.setCloaked(false);

   Parent::playerDismounted( %data, %obj, %player );
}
function ApacheChaingunImage::onFire(%data, %obj, %slot)
{
   Parent::onFire(%data, %obj, %slot);
   %obj.setImageTrigger(3, true);

}
function ApacheChaingunPairImage::onFire(%data, %obj, %slot)
{
  Parent::onFire(%data, %obj, %slot);

}
function ApacheRocketPairImage::onFire(%data, %obj, %slot)
{
      Parent::onFire(%data, %obj, %slot);

   %obj.nextWeaponFire = 2;
   schedule(%data.fireTimeout, 0, "fireNextGun", %obj);

    %vec = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);

      %p = new (%data.projectileType)() {
            dataBlock        = %data.projectile;
            initialDirection = %obj.getMuzzleVector(%slot);
            initialPosition  = %mp;
            sourceObject     = %obj;
			damageFactor	 = 1;
            sourceSlot       = %slot;
        };
    if (isObject(%p))
   %p.schedule(%p.getDataBlock().lifetimeMS + 500, "killit");
             %p = new (%data.projectileType)() {
            dataBlock        = %data.projectile;
            initialDirection = %obj.getMuzzleVector(%slot);
            initialPosition  = %mp;
            sourceObject     = %obj;
			damageFactor	 = 1;
            sourceSlot       = %slot;
        };

   if (isObject(%obj.lastProjectile) && %obj.deleteLastProjectile)
      %obj.lastProjectile.killit();

   %obj.lastProjectile = %p;
   %obj.deleteLastProjectile = %data.deleteLastProjectile;
   MissionCleanup.add(%p);
    if (isObject(%p))
   %p.schedule(%p.getDataBlock().lifetimeMS + 500, "killit");
return %p;
}
function ApacheRocketImage::onFire(%data, %obj, %slot)
{
      Parent::onFire(%data, %obj, %slot);

   %obj.nextWeaponFire = 3;
   schedule(%data.fireTimeout, 0, "fireNextGun", %obj);
    %vec = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);


      %p = new (%data.projectileType)() {
            dataBlock        = %data.projectile;
            initialDirection = %obj.getMuzzleVector(%slot);
            initialPosition  = %mp;
            sourceObject     = %obj;
			damageFactor	 = 1;
            sourceSlot       = %slot;
        };
    if (isObject(%p))
   %p.schedule(%p.getDataBlock().lifetimeMS + 500, "killit");
             %p = new (%data.projectileType)() {
            dataBlock        = %data.projectile;
            initialDirection = %obj.getMuzzleVector(%slot);
            initialPosition  = %mp;
            sourceObject     = %obj;
			damageFactor	 = 1;
            sourceSlot       = %slot;
        };
    if (isObject(%p))
   %p.schedule(%p.getDataBlock().lifetimeMS + 500, "killit");
   if (isObject(%obj.lastProjectile) && %obj.deleteLastProjectile)
      %obj.lastProjectile.killit();

   %obj.lastProjectile = %p;
   %obj.deleteLastProjectile = %data.deleteLastProjectile;
   MissionCleanup.add(%p);
return %p;
}
function ApacheChaingunImage::onTriggerDown(%this, %obj, %slot)
{
}

function ApacheChaingunImage::onTriggerUp(%this, %obj, %slot)
{
}

function ApacheChaingunImage::onMount(%this, %obj, %slot)
{
//   %obj.setImageAmmo(%slot,true);
}

function ApacheChaingunPairImage::onMount(%this, %obj, %slot)
{
//   %obj.setImageAmmo(%slot,true);
}
