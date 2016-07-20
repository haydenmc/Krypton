//**************************************************************
// KRYPTON BLADE FLIER
//**************************************************************
//**************************************************************
// SOUNDS
//**************************************************************
datablock EffectProfile(BladeThrustEffect)
{
   effectname = "vehicles/shrike_boost";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(BladeEngineEffect)
{
   effectname = "vehicles/shrike_engine";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock EffectProfile(BladeBlasterFireEffect)
{
   effectname = "vehicles/shrike_blaster";
   minDistance = 5.0;
   maxDistance = 10.0;
};

datablock AudioProfile(BladeThrustSound)
{
   filename    = "fx/vehicles/htransport_thrust.wav";
   description = AudioDefaultLooping3d;
   preload = true;
   effect = ScoutFlyerThrustEffect;
};

datablock AudioProfile(BladeEngineSound)
{
   filename    = "fx/environment/howlingwind3.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(BladeBlasterFire)
{
   filename    = "fx/vehicles/shrike_blaster.wav";
   description = AudioDefault3d;
   preload = true;
   effect = ScoutFlyerEngineEffect;
};

datablock AudioProfile(BladeBlasterProjectile)
{
   filename    = "fx/powered/turret_sentry_fire.wav";
   description = ProjectileLooping3d;
   preload = true;
   effect = ShrikeBlasterFireEffect;
};

datablock AudioProfile(BladeBlasterDryFireSound)
{
   filename    = "fx/powered/turret_light_reload.wav";
   description = AudioClose3d;
   preload = true;
};


datablock ParticleData(BladeContrailParticle)
{
   dragCoefficient      = 1; //Orig 2
   gravityCoefficient   = 0.15; //Orig 0.2
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000; //orig 750
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "0.3 0.4 1.0 1.0";
   colors[1]     = "1.0 1.0 0.3 1.0";
   colors[2]     = "0.3 0.4 1.0 1.0";
   colors[3]     = "0.3 0.4 1.0 0.0";
   sizes[0]      = 2;
   sizes[1]      = 1;
   sizes[2]      = 2;
   sizes[3]      = 3;
   times[0] = 0.0;
   times[1] = 0.3;
   times[2] = 0.6;
   times[3] = 1.0;
};

datablock ParticleEmitterData(BladeContrailEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 10;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "BladeContrailParticle";
};


//**************************************************************
// VEHICLE CHARACTERISTICS
//**************************************************************

datablock FlyingVehicleData(KryptonBlade) : BladeDamageProfile
{
   spawnOffset = "0 0 2";
   canControl = true;
   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   multipassenger = true;
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag    = 0.10;
   density = 1.0;

   canWarp = 1;

   mountPose[0] = sitting;
   numMountPoints = 2;
   isProtectedMountPoint[0] = true;
   isProtectedMountPoint[1] = true;
   cameraMaxDist = 15;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

   maxDamage = 1.80;
   destroyedLevel = 1.80;

   isShielded = true;
   energyPerDamagePoint = 80;
   maxEnergy = 280;      // Afterburner and any energy weapon pool
   rechargeRate = 0.8;

   minDrag = 30;           // Linear Drag (eventually slows you down when not thrusting...constant drag)
   rotationalDrag = 900;        // Anguler Drag (dampens the drift after you stop moving the mouse...also tumble drag)

   maxAutoSpeed = 5;       // Autostabilizer kicks in when less than this speed. (meters/second)
   autoAngularForce = 50;       // Angular stabilizer force (this force levels you out when autostabilizer kicks in)
   autoLinearForce = 300;        // Linear stabilzer force (this slows you down when autostabilizer kicks in)
   autoInputDamping = 0.95;      // Dampen control input so you don't` whack out at very slow speeds

   // Maneuvering
   maxSteeringAngle = 2;    // Max radiens you can rotate the wheel. Smaller number is more maneuverable.
   horizontalSurfaceForce = 6;   // Horizontal center "wing" (provides "bite" into the wind for climbing/diving and turning)
   verticalSurfaceForce = 8;     // Vertical center "wing" (controls side slip. lower numbers make MORE slide.)
   maneuveringForce = 5000;      // Horizontal jets (W,S,D,A key thrust)
   steeringForce = 2000;         // Steering jets (force applied when you move the mouse)
   steeringRollForce = 300;      // Steering jets (how much you heel over when you turn)
   rollForce = 70;                // Auto-roll (self-correction to right you after you roll/invert)
   hoverHeight = 4;        // Height off the ground at rest
   createHoverHeight = 3;  // Height off the ground when created
   maxForwardSpeed = 900;  // speed in which forward thrust force is no longer applied (meters/second)

   // Turbo Jet
   jetForce = 8000;      // Afterburner thrust (this is in addition to normal thrust)
   minJetEnergy = 1;     // Afterburner can't be used if below this threshhold.
   jetEnergyDrain = 0;       // Energy use of the afterburners (low number is less drain...can be fractional)                                                                                                                                                                                                                                                                                          // Auto stabilize speed
   vertThrustMultiple = 3.0;

   // Rigid body
   mass = 150;        // Mass of the vehicle
   bodyFriction = 0;     // Don't mess with this.
   bodyRestitution = 0.5;   // When you hit the ground, how much you rebound. (between 0 and 1)
   minRollSpeed = 0;     // Don't mess with this.
   softImpactSpeed = 14;       // Sound hooks. This is the soft hit.
   hardImpactSpeed = 25;    // Sound hooks. This is the hard hit.

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 18;      // If hit ground at speed above this then it's an impact. Meters/second
   speedDamageScale = 0.06;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 23.0;
   collDamageMultiplier   = 0.02;

   //
   minTrailSpeed = 5;      // The speed your contrail shows up at.
   trailEmitter = "BladeContrailEmitter";
   forwardJetEmitter = FlyerJetEmitter;
   downJetEmitter = FlyerJetEmitter;

   //
   jetSound = BladeThrustSound;
   engineSound = BladeEngineSound;
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
   targetNameTag = 'Blade';
   targetTypeTag = 'Turbograv';
   sensorData = AWACPulseSensor;
   sensorRadius = AWACPulseSensor.detectRadius;
   sensorColor = "255 194 9";

   checkRadius = 5.5;
   observeParameters = "1 10 10";

   runningLight[0] = ShrikeLight1;
//   runningLight[1] = ShrikeLight2;

   shieldEffectScale = "0.937 1.125 0.60";

};


datablock ShapeBaseImageData(BladeBlasterPairImage)
{
   className = WeaponImage;
//   shapeFile = "weapon_energy_vehicle.dts";
   shapeFile = "turret_muzzlepoint.dts";
   item      = Chaingun;
   ammo   = ChaingunAmmo;
   projectile = PhotonProjectile;
   projectileType = LinearProjectile;
   mountPoint = 10;
   offset = "3.30 -0.32 0.1";


   usesEnergy = true;
   useMountEnergy = true;
   // DAVEG -- balancing numbers below!
   minEnergy = 5;
   fireEnergy = 15;
   fireTimeout = 750;

   projectileSpread = 4.0 / 1000.0;
   muzzleFlash = ChaingunTurretMuzzleFlash;

   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateAllowImageChange[0] = false;
   stateTimeoutValue[0]        = 0.05;
   stateTransitionOnTimeout[0] = "Ready";
   stateTransitionOnNoAmmo[0]  = "NoAmmo";
   //--------------------------------------
   stateName[1]       = "Ready";
   stateSound[1]                    = PhotonIdleSound;
   stateSpinThread[1] = Stop;
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
   stateTimeoutValue[3]          = 0.01;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";
   //--------------------------------------
   stateName[4]             = "Fire";
   stateSpinThread[4]       = FullSpeed;
   stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateSound[4]            = PhotonFireSound;
   // IMPORTANT! The stateTimeoutValue below has been replaced by fireTimeOut
   // above.
   stateTimeoutValue[4]          = 1.3;
   stateTransitionOnTimeout[4]   = "checkState";
   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSpinThread[5] = SpinDown;
   stateTimeoutValue[5]            = 0.01;
   stateWaitForTimeout[5]          = false;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";
   //--------------------------------------
   stateName[6]       = "EmptySpindown";
//   stateSound[6]      = ChaingunSpindownSound;
   stateSpinThread[6] = SpinDown;
   stateTransitionOnAmmo[6]   = "Ready";
   stateTimeoutValue[6]        = 0.01;
   stateTransitionOnTimeout[6] = "NoAmmo";
   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ShrikeBlasterDryFireSound;
   stateTransitionOnTriggerUp[7] = "NoAmmo";
   stateTimeoutValue[7]        = 0.25;
   stateTransitionOnTimeout[7] = "NoAmmo";

   stateName[8] = "checkState";
   stateTransitionOnTriggerUp[8] = "Spindown";
   stateTransitionOnNoAmmo[8]    = "EmptySpindown";
   stateTimeoutValue[8]          = 0.01;
   stateTransitionOnTimeout[8]   = "ready";
};

datablock ShapeBaseImageData(BladeBlasterImage) : BladeBlasterPairImage
{
//**original**   offset = "-.73 0 0";
   offset = "-3.30 -0.32 0.1";
   stateScript[3]           = "onTriggerDown";
   stateScript[5]           = "onTriggerUp";
   stateScript[6]           = "onTriggerUp";
};

datablock ShapeBaseImageData(BladeBlasterParam)
{
   mountPoint = 2;
   shapeFile = "turret_muzzlepoint.dts";
   projectile = PhotonProjectile;
   projectileType = LinearProjectile;
}; 

datablock StaticShapeData(BladeDecal1)
{
   mountPoint = 1;
   shapeFile = "vehicle_air_scout.dts";
};

function KryptonBlade::onAdd(%data, %obj)
{
   Parent::onAdd(%data, %obj);
//   %obj.mountImage(ApacheDecal2, 6);
//   %obj.mountImage(BladeDecal1, 4);

%rw = new StaticShape()
    {
        scale = "3 0.7 0.5";
        dataBlock = "BladeDecal1";
    };
    %obj.mountObject(%rw, 5);
    %rw.setCloaked(true);
    %rw.schedule(4800, "setCloaked", false);
    %obj.mountImage(BladeBlasterParam, 0);
    %obj.mountImage(BladeBlasterImage, 2);
    %obj.mountImage(BladeBlasterPairImage, 3);
    %obj.nextWeaponFire = 2;
    $numVWeapons = 2;
schedule(10000,0,BladeRemount,%obj);
}

function KryptonBlade::deleteAllMounted(%data, %obj)
{
   %rw = %obj.getMountNodeObject(5);
   if(!%rw)
      return;
   %rw.delete();
}

function KryptonBlade::onRemove(%this, %obj)
{
   KryptonBlade::deleteAllMounted(%this, %obj);

   Parent::onRemove(%this, %obj);
}

function KryptonBlade::playerMounted(%data, %obj, %player, %node)
{

   if (%obj.clientControl)
       serverCmdResetControlObject(%obj.clientControl);

   commandToClient(%player.client, 'setHudMode', 'Pilot', "Shrike", %node);
   $numVWeapons = 2;
   if ( %player.client.observeCount > 0 )
      resetObserveFollow( %player.client, false );
}

function BladeRemount(%obj)
{
if (!isObject(%obj))
return;

   %rw = %obj.getMountNodeObject(5);
   if(isObject(%rw))
   %rw.delete();

%rw = new StaticShape(%obj)
    {
        scale = "3 0.7 0.5";
        dataBlock = "BladeDecal1";
    };
    %obj.mountObject(%rw, 5);
//echo("Remounting");
schedule(10000,0,BladeRemount,%obj);
}




function KryptonBlade::onTrigger(%data, %obj, %trigger, %state)
{
   // data = Sparrow datablock
   // obj = Sparrow object number
   // trigger = 0 for "fire", 1 for "jump", 3 for "thrust"
   // state = 1 for firing, 0 for not firing
   if(%trigger == 0)
   {
      switch (%state) {
         case 0:
            %obj.fireWeapon = false;
            %obj.setImageTrigger(2, false);
            %obj.setImageTrigger(3, false);
         case 1:
            %obj.fireWeapon = true;
            if(%obj.nextWeaponFire == 2) {
               %obj.setImageTrigger(2, true);
               %obj.setImageTrigger(3, false);
            }
            else {
               %obj.setImageTrigger(2, false);
               %obj.setImageTrigger(3, true);
            }
      }
   }
}

function KryptonBlade::playerDismounted(%data, %obj, %player)
{
   %obj.fireWeapon = false;
   %obj.setImageTrigger(2, false);
   %obj.setImageTrigger(3, false);
   setTargetSensorGroup(%obj.getTarget(), %obj.team);
}

function BladeBlasterImage::onFire(%data,%obj,%slot)
{
   // obj = SparrowFlyer object number
   // slot = 2

   %p = Parent::onFire(%data,%obj,%slot);
   MissileSet.add(%p);
   %obj.nextWeaponFire = 3;
   schedule(%data.fireTimeout, 0, "fireNextGun", %obj);
}

function BladeBlasterPairImage::onFire(%data,%obj,%slot)
{
   // obj = SparrowFlyer object number
   // slot = 3

   %p = Parent::onFire(%data,%obj,%slot);
   MissileSet.add(%p);
   %obj.nextWeaponFire = 2;
   schedule(%data.fireTimeout, 0, "fireNextGun", %obj);
}

function BladeBlasterImage::onTriggerDown(%this, %obj, %slot)
{
}

function BladeBlasterImage::onTriggerUp(%this, %obj, %slot)
{
}

function BladeBlasterImage::onMount(%this, %obj, %slot)
{
}

function BladeBlasterPairImage::onMount(%this, %obj, %slot)
{
}

function BladeBlasterImage::onUnmount(%this,%obj,%slot)
{
}

function BladeBlasterPairImage::onUnmount(%this,%obj,%slot)
{
}