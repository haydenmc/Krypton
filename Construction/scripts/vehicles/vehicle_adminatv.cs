datablock ParticleData(AdminATVSmallRedFlameParticle)
{
    dragCoefficient = 0.0487805;
    gravityCoefficient = -0.0;
    windCoefficient = 0;
    inheritedVelFactor = 0;//0.362903;
    constantAcceleration = 0;
    lifetimeMS = 10177;//2177
    lifetimeVarianceMS = 443;
    useInvAlpha = 0;
    spinRandomMin = -180.161;
    spinRandomMax = 180.065;
    textureName = "special/BlueImpact.PNG";
    times[0] = 0;
    times[1] = 1;
    colors[0] = "0.1000 0.304000 0.850394 0.451613";
    colors[1] = "0.000000 0.000000 0.000000 0.709677";
    sizes[0] = 2.5;
    sizes[1] = 2.0;
    sizes[2] = 1.5;
    sizes[3] = 0.5;
};

datablock ParticleEmitterData(AdminATVSmallRedFlameEmitter)
{
    ejectionPeriodMS = 4;
    periodVarianceMS = 0;
    ejectionVelocity = 0.1;
    velocityVariance = 0.0;
    ejectionOffset =   0;
    thetaMin = 0;
    thetaMax = 14.5161;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvances = 0;
    orientParticles= 0;
    orientOnVelocity = 1;
    particles = "AdminATVSmallRedFlameParticle";
};

//**************************************************************
// LIGHTS
//**************************************************************
//**************************************************************
// VEHICLE CHARACTERISTICS
//**************************************************************

datablock HoverVehicleData(AdminATV) : AdminATVDamageProfile
{
   spawnOffset = "0 0 2";

   floatingGravMag = 10.5;

   catagory = "Vehicles";
   shapeFile = "vehicle_air_scout.dts";
   computeCRC = true;

   debrisShapeName = "vehicle_air_scout_debris.dts";
   debris = ShapeDebris;
   renderWhenDestroyed = false;

   drag = 0.0;
   density = 0.9;

   mountPose[0] = sitting;
   cameraMaxDist = 15.0;
   cameraOffset = 2.5;
   cameraLag = 0.9;
   numMountPoints = 1;
   isProtectedMountPoint[0] = true;
   explosion = VehicleExplosion;
	explosionDamage = 0.5;
	explosionRadius = 5.0;

  // lightOnly = 1;
adminOnly = 1;

   maxDamage = 99.00;
   destroyedLevel = 99.00;

   isShielded = true;
   rechargeRate = 0.99;
   energyPerDamagePoint = 0;
   maxEnergy = 250;
   minJetEnergy = 0;
   jetEnergyDrain = 0.0;

   // Rigid Body
   mass = 25;
   bodyFriction = 0.1;
   bodyRestitution = 0.5;
   softImpactSpeed = 20;       // Play SoftImpact Sound
   hardImpactSpeed = 28;      // Play HardImpact Sound

   // Ground Impact Damage (uses DamageType::Ground)
   minImpactSpeed = 0;
   speedDamageScale = 0.00;

   // Object Impact Damage (uses DamageType::Impact)
   collDamageThresholdVel = 0;
   collDamageMultiplier   = 0.00;

   dragForce            = 26 / 45.0;
   vertFactor           = 0.4;
   floatingThrustFactor = 1.0;

   mainThrustForce    = 40;
   reverseThrustForce = 40;
   strafeThrustForce  = 20;
   turboFactor        = 5.5;

   brakingForce = 100;
   brakingActivationSpeed = 40;

   stabLenMin = 3.25;
   stabLenMax = 4.75;
   stabSpringConstant  = 50;
   stabDampingConstant = 20;

   gyroDrag = 15;
   normalForce = 40;
   restorativeForce = 100;
   steeringForce = 25;
   rollForce  = 15;
   pitchForce = 20;

   dustEmitter = TankDustEmitter;
   triggerDustHeight = 2.5;
   dustHeight = 1.0;
   dustTrailEmitter = TireEmitter;
   dustTrailOffset = "0.0 -1.0 0.5";
   triggerTrailHeight = 3.6;
   dustTrailFreqMod = 15.0;

   jetSound         = NuclearFireSound;
   engineSound      = ATVEngineSound;
   floatSound       = ATVThrustSound;
   softImpactSound  = GravSoftImpactSound;
   hardImpactSound  = HardImpactSound;
   //wheelImpactSound = WheelImpactSound;

   //
   softSplashSoundVelocity = 10.0;
   mediumSplashSoundVelocity = 20.0;
   hardSplashSoundVelocity = 30.0;
   exitSplashSoundVelocity = 10.0;

   exitingWater      = VehicleExitWaterSoftSound;
   impactWaterEasy   = VehicleImpactWaterSoftSound;
   impactWaterMedium = VehicleImpactWaterSoftSound;
   impactWaterHard   = VehicleImpactWaterMediumSound;
   waterWakeSound    = VehicleWakeSoftSplashSound;

   minMountDist = 4;

   damageEmitter[0] = SmallLightDamageSmoke;
   damageEmitter[1] = SmallHeavyDamageSmoke;
   damageEmitter[2] = DamageBubbles;
   damageEmitterOffset[0] = "0.0 -1.5 0.5 ";
   damageLevelTolerance[0] = 0.3;
   damageLevelTolerance[1] = 0.7;
   numDmgEmitterAreas = 1;

   splashEmitter[0] = VehicleFoamDropletsEmitter;
   splashEmitter[1] = VehicleFoamEmitter;

   shieldImpact = VehicleShieldImpact;

   forwardJetEmitter = AdminATVSmallRedFlameEmitter;

   cmdCategory = Tactical;
   cmdIcon = CMDHoverScoutIcon;
   cmdMiniIconName = "commander/MiniIcons/com_landscout_grey";
   targetNameTag = 'Hover';
   targetTypeTag = 'ATV';
   sensorData = VehiclePulseSensor;

   checkRadius = 1.7785;
   observeParameters = "1 10 10";

   runningLight[0] = WildcatLight1;
   runningLight[1] = WildcatLight2;
   runningLight[2] = WildcatLight3;

   shieldEffectScale = "0.9375 1.125 0.6";
};

