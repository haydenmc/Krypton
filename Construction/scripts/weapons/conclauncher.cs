//--------------------------------------
// Grenade launcher
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds
//--------------------------------------


datablock AudioProfile(ConcGrenadeExplosionSound)
{
   filename    = "fx/weapons/grenade_explode.wav";
   description = AudioExplosion3d;
   preload = true;
   effect = GrenadeExplosionEffect;
};

datablock AudioProfile(ConcUnderwaterGrenadeExplosionSound)
{
   filename    = "fx/weapons/grenade_explode.wav";
   description = AudioExplosion3d;
   preload = true;
   effect = GrenadeExplosionEffect;
};




//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------
datablock ParticleData(ConcGrenadeSmokeParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;   // DOES NOT rise slowly
   inheritedVelFactor   = 0.00;

   lifetimeMS           = 1500;  // lasts 2 second
   lifetimeVarianceMS   = 300;   // ...more or less

   textureName          = "particleTest";

   useInvAlpha = true;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;

   colors[0]     = "1.0 0.0 1.0 1.0";
   colors[1]     = "1.0 0.5 1.0 1.0";
   colors[2]     = "1.0 1.0 1.0 0.0";

   sizes[0]      = 2.25;
   sizes[1]      = 0.5;
   sizes[2]      = 0.1;

   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ConcGrenadeSmokeEmitter)
{
   ejectionPeriodMS = 15;
   periodVarianceMS = 5;

   ejectionVelocity = 1.25;
   velocityVariance = 0.50;

   thetaMin         = 0.0;
   thetaMax         = 90.0;  

   particles = "ConcGrenadeSmokeParticle";
};

//----------------------------------------------------
//  Explosion
//----------------------------------------------------
datablock ExplosionData(ConcGrenadeExplosion)
{
   soundProfile   = ConcussionGrenadeExplosionSound;
   shockwave =  ConcussionGrenadeShockwave;

   faceViewer           = true;
   explosionScale = "0.8 0.8 0.8";

   debris = GrenadeDebris;
   debrisThetaMin = 10;
   debrisThetaMax = 50;
   debrisNum = 8;
   debrisVelocity = 26.0;
   debrisVelocityVariance = 7.0;

   emitter[0] = ConcussionGrenadeSparkEmitter;
   emitter[1] = ConcussionGrenadeCrescentEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 20.0;
};


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock GrenadeProjectileData(BasicConcGrenade)
{
   projectileShapeName = "grenade_projectile.dts";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.5;
   damageRadius        = 30.0;
   radiusDamageType    = $DamageType::Grenade;
   kickBackStrength    = 4500;
   bubbleEmitTime      = 1.0;

   sound               = GrenadeProjectileSound;
   explosion           = "ConcGrenadeExplosion";
   underwaterExplosion = "ConcGrenadeExplosion";
   velInheritFactor    = 0.5;
   splash              = GrenadeSplash;

   baseEmitter         = ConcGrenadeSmokeEmitter;
   bubbleEmitter       = GrenadeBubbleEmitter;

   grenadeElasticity = 0.35;
   grenadeFriction   = 0.1;
   armingDelayMS     = 3000;
   muzzleVelocity    = 70.00;
   drag = 0.05;
};


//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(ConcGrenadeLauncherAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_grenade.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some concussion grenade launcher ammo";

   computeCRC = true;
   emap = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ItemData(ConcGrenadeLauncher)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_grenade_launcher.dts";
   image = ConcGrenadeLauncherImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a concussion grenade launcher";

   computeCRC = true;

};

datablock ShapeBaseImageData(ConcGrenadeLauncherImage)
{
   className = WeaponImage;
   shapeFile = "weapon_grenade_launcher.dts";
   item = ConcGrenadeLauncher;
   ammo = ConcGrenadeLauncherAmmo;
   offset = "0 0 0";
   emap = true;

   projectile = BasicConcGrenade;
   projectileType = GrenadeProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = GrenadeSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.4;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateSound[3] = GrenadeFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.8;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";
   stateSound[4] = GrenadeReloadSound;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = GrenadeDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};