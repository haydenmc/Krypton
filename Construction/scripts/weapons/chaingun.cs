//--------------------------------------
// Chaingun
//--------------------------------------


//--------------------------------------------------------------------------
// Sounds
//--------------------------------------

datablock AudioProfile(ChaingunSwitchSound)
{
   filename    = "fx/weapons/chaingun_activate.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunFireSound)
{
   filename    = "fx/weapons/chaingun_fire.wav";
   description = AudioDefaultLooping3d;
   preload = true;
};

datablock AudioProfile(ChaingunProjectile)
{
   filename    = "fx/weapons/chaingun_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(ChaingunImpact)
{
   filename    = "fx/weapons/cg_soft3.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunSpinDownSound)
{
   filename    = "fx/weapons/chaingun_spindown.wav";		//chaingun_off.wav";		
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunSpinUpSound)
{
   filename    = "fx/weapons/chaingun_spinup.wav";		//chaingun_start.wav";		
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(ChaingunDryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(ShrikeBlasterProjectileSound)
{
   filename    = "fx/vehicles/shrike_blaster_projectile.wav";
   description = ProjectileLooping3d;
   preload = true;
};

datablock AudioProfile(ScoutChaingunExpSound)
{
   filename    = "fx/weapons/plasma_rifle_projectile_die.wav";
   description = AudioClose3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Splash
//--------------------------------------------------------------------------

datablock ParticleData( ChaingunSplashParticle )
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -1.4;
   lifetimeMS           = 300;
   lifetimeVarianceMS   = 0;
   textureName          = "special/droplet";
   colors[0]     = "0.7 0.8 1.0 1.0";
   colors[1]     = "0.7 0.8 1.0 0.5";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 0.05;
   sizes[1]      = 0.2;
   sizes[2]      = 0.2;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( ChaingunSplashEmitter )
{
   ejectionPeriodMS = 4;
   periodVarianceMS = 0;
   ejectionVelocity = 3;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 50;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 100;
   particles = "ChaingunSplashParticle";
};


datablock SplashData(ChaingunSplash)
{
   numSegments = 10;
   ejectionFreq = 10;
   ejectionAngle = 20;
   ringLifetime = 0.4;
   lifetimeMS = 400;
   velocity = 3.0;
   startRadius = 0.0;
   acceleration = -3.0;
   texWrap = 5.0;

   texture = "special/water2";

   emitter[0] = ChaingunSplashEmitter;

   colors[0] = "0.7 0.8 1.0 0.0";
   colors[1] = "0.7 0.8 1.0 1.0";
   colors[2] = "0.7 0.8 1.0 0.0";
   colors[3] = "0.7 0.8 1.0 0.0";
   times[0] = 0.0;
   times[1] = 0.4;
   times[2] = 0.8;
   times[3] = 1.0;
};

//--------------------------------------------------------------------------
// Particle Effects
//--------------------------------------
datablock ParticleData(ChaingunFireParticle)
{
   dragCoefficient      = 2.75;
   gravityCoefficient   = 0.1;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 550;
   lifetimeVarianceMS   = 0;
   textureName          = "particleTest";
   colors[0]     = "0.46 0.36 0.26 1.0";
   colors[1]     = "0.46 0.36 0.26 0.0";
   sizes[0]      = 0.25;
   sizes[1]      = 0.20;
};

datablock ParticleEmitterData(ChaingunFireEmitter)
{
   ejectionPeriodMS = 6;
   periodVarianceMS = 0;
   ejectionVelocity = 10;
   velocityVariance = 1.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 12;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance  = true;
   particles = "ChaingunFireParticle";
};

//--------------------------------------------------------------------------
// Explosions
//--------------------------------------
datablock ParticleData(ChaingunExplosionParticle1)
{
   dragCoefficient      = 0.65;
   gravityCoefficient   = 0.3;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 500;
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "0.56 0.36 0.26 1.0";
   colors[1]     = "0.56 0.36 0.26 0.0";
   sizes[0]      = 0.0625;
   sizes[1]      = 0.2;
};

datablock ParticleEmitterData(ChaingunExplosionEmitter)
{
   ejectionPeriodMS = 15;		//v2 change, was 10
   periodVarianceMS = 0;
   ejectionVelocity = 0.75;
   velocityVariance = 0.25;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "ChaingunExplosionParticle1";
};




datablock ParticleData(ChaingunImpactSmokeParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.2;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 600;		//v2 change was 1000
   lifetimeVarianceMS   = 200;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 90.0;
   textureName          = "particleTest";
   colors[0]     = "0.6 0.6 0.6 0.0";
   colors[1]     = "0.8 0.8 0.8 0.3";
   colors[2]     = "0.8 0.8 0.8 0.0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.5;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ChaingunImpactSmoke)
{
   ejectionPeriodMS = 10;		//v2 change, was 8
   periodVarianceMS = 1;
   ejectionVelocity = 1.0;
   velocityVariance = 0.5;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 35;
   overrideAdvances = false;
   particles = "ChaingunImpactSmokeParticle";
   lifetimeMS       = 50;
};


datablock ParticleData(ChaingunSparks)
{
   dragCoefficient      = 1;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 300;
   lifetimeVarianceMS   = 100;		//v2 0
   textureName          = "special/spark00";
   colors[0]     = "0.84 0.74 0.39 1.0";		//v2 brightened
   colors[1]     = "0.84 0.64 0.39 1.0";
   colors[2]     = "1.0 0.54 0.39 0.0";
   sizes[0]      = 1.2;		//v2 0.6;			//v2 enlarged
   sizes[1]      = 0.4;		//v2 0.2;
   sizes[2]      = 0.1;		//v2 0.05;
   times[0]      = 0.0;
   times[1]      = 0.2;
   times[2]      = 1.0;

};

datablock ParticleEmitterData(ChaingunSparkEmitter)
{
   ejectionPeriodMS = 8;	//v2 change, was 4
   periodVarianceMS = 0;
   ejectionVelocity = 4;
   velocityVariance = 2.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 50;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 80;		//v2 100
   particles = "ChaingunSparks";
};


datablock ExplosionData(ChaingunExplosion)
{
   soundProfile   = ChaingunImpact;
   emitter[0] = ChaingunImpactSmoke;
   emitter[1] = ChaingunSparkEmitter;
   faceViewer           = false;
};


datablock ShockwaveData(ScoutChaingunHit)
{
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


   width = 1;
   numSegments = 13;
   numVertSegments = 1;
   velocity = 0.5;
   acceleration = 2.0;
   lifetimeMS = 900;
   height = 0.1;
   verticalCurve = 0.5;

   mapToTerrain = false;
   renderBottom = false;
   orientToNormal = true;

   texture[0] = "special/shockwave5";
   texture[1] = "special/gradient";
   texWrap = 3.0;

   times[0] = 0.0;
   times[1] = 0.5;
   times[2] = 1.0;

   colors[0] = "1.0 1.0 0.6 1.0";
   colors[1] = "0.6 0.6 0.3 0.5";
   colors[2] = "0.0 0.0 1.0 0.0";
};

datablock ParticleData(ScoutChaingunExplosionParticle)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent4";
   colors[0] = "1.0 1.0 0.0 1.0";
   colors[1] = "0.6 0.6 0.0 1.0";
   colors[2] = "1 0 0 0";
   sizes[0]      = 0.25;
   sizes[1]      = 0.5;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(ScoutChaingunExplosionEmitter)
{
   ejectionPeriodMS = 10;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.5;
   ejectionOffset   = 0.0;
   thetaMin         = 80;
   thetaMax         = 90;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 200;
   particles = "ScoutChaingunExplosionParticle";
};

datablock ExplosionData(ScoutChaingunExplosion)
{
   soundProfile   = ScoutChaingunExpSound;
   shockwave = ScoutChaingunHit;
   emitter[0] = ScoutChaingunExplosionEmitter;
};

//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------


//datablock DebrisData( ShellDebris )
//{
//   shapeName = "weapon_chaingun_ammocasing.dts";
//   lifetime = 3.0;
//   minSpinSpeed = 300.0;
//   maxSpinSpeed = 400.0;
//   elasticity = 0.5;
//   friction = 0.2;
//   numBounces = 3;
//   fade = true;
//   staticOnMaxBounce = true;
//   snapOnMaxBounce = true;
//};             


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock DecalData(ChaingunDecal0)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/bullethole3";
};

datablock DecalData(ChaingunDecal1)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/bullethole3";
};
datablock DecalData(ChaingunDecal2)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/station_damageM2";
};
datablock DecalData(ChaingunDecal3)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/station_damageM2";
};
datablock DecalData(ChaingunDecal4)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/bullethole2";
};
datablock DecalData(ChaingunDecal5)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/bullethole5";
};
datablock DecalData(ChaingunDecal6)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/bullethole1";
};


datablock TracerProjectileData(ChaingunBullet)
{
   doDynamicClientHits = true;

   directDamage        = 0.0825;		////v2 was .165
   directDamageType    = $DamageType::Bullet;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 0.0;
   sound 				= ChaingunProjectile;

   dryVelocity       = 560;		//g=425.0;
   wetVelocity       = 325;	//v2 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 1300;
   lifetimeMS        = 1300;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 1000;	//v2 3000;

   tracerLength    = 75.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.055;		//v2 0.10;
   crossSize       = 0.2;		//v2 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal0;
};


datablock TracerProjectileData(ChaingunBullet1):ChaingunBullet
{
   decalData[0] = ChaingunDecal1;
};
datablock TracerProjectileData(ChaingunBullet2):ChaingunBullet
{
   decalData[0] = ChaingunDecal2;
};
datablock TracerProjectileData(ChaingunBullet3):ChaingunBullet
{
   decalData[0] = ChaingunDecal3;
};
datablock TracerProjectileData(ChaingunBullet4):ChaingunBullet
{
   decalData[0] = ChaingunDecal4;
};
datablock TracerProjectileData(ChaingunBullet5):ChaingunBullet
{
   decalData[0] = ChaingunDecal5;
};
datablock TracerProjectileData(ChaingunBullet6):ChaingunBullet
{
   decalData[0] = ChaingunDecal6;
};




//--------------------------------------------------------------------------
// Scout Projectile
//--------------------------------------
datablock TracerProjectileData(ScoutChaingunBullet)
{
   doDynamicClientHits = true;

   directDamage        = 0.125;
   explosion           = "ScoutChaingunExplosion";
   splash              = ChaingunSplash;
   directDamageType    = $DamageType::ShrikeBlaster;
   kickBackStrength    = 30;	//v2h changed from 300 to 30

//v2 mods, radius damage
   hasDamageRadius     = true;
   indirectDamage      = 0.125;
   damageRadius        = 1;
   radiusDamageType    = $DamageType::ShrikeBlaster;

   sound = ShrikeBlasterProjectileSound;

   dryVelocity       = 425.0;
   wetVelocity       = 325;	//v2 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 1000;
   lifetimeMS        = 1000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 1000;	//v2 3000;

   tracerLength    = 50.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = "1 0.8 0.5 0.5";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
   tracerWidth     = 0.35;
   crossSize       = 0.99;
   crossViewAng    = 0.990;
   renderCross     = true;

};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(ChaingunAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some chaingun ammo";

   computeCRC = true;

};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(ChaingunImage)
{
   className = WeaponImage;
   shapeFile = "weapon_chaingun.dts";
   item      = Chaingun;
   ammo 	 = ChaingunAmmo;
   projectile = ChaingunBullet;
	projectilecount = 6;
	projectile[1] = ChaingunBullet1;
	projectile[2] = ChaingunBullet2;
	projectile[3] = ChaingunBullet3;
	projectile[4] = ChaingunBullet4;
	projectile[5] = ChaingunBullet5;
	projectile[6] = ChaingunBullet6;
   
   projectileType = TracerProjectile;
   emap = true;

//   casing              = ShellDebris;		//v2 change, no casing
//   shellExitDir        = "1.0 0.3 1.0";
//   shellExitOffset     = "0.15 -0.56 -0.1";
//   shellExitVariance   = 15.0;
//   shellVelocity       = 3.0;

   projectileSpread = 8.0 / 1000.0;		//v2 change, was 8, then 7, then 7.5, changed to 8 with conical spread
	choke = 1;		//v2g

	firetime = 0.15;		//v2
	
   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateSound[0]            = ChaingunSwitchSound;
   stateAllowImageChange[0] = false;
   //
   stateTimeoutValue[0]        = 0.5;
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
   stateSound[3]        = ChaingunSpinupSound;
   //
   stateTimeoutValue[3]          = 0.25;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   stateSound[4]            = ChaingunFireSound;
   stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = false;		//v2 true
   stateTimeoutValue[4]          = 0.15;
   stateTransitionOnTimeout[4]   = "Fire";
   stateTransitionOnTriggerUp[4] = "Spindown";
   stateTransitionOnNoAmmo[4]    = "EmptySpindown";

   //--------------------------------------
   stateName[5]       = "Spindown";
   stateSound[5]      = ChaingunSpinDownSound;
   stateSpinThread[5] = SpinDown;
   //
   stateTimeoutValue[5]            = 0.5;
   stateWaitForTimeout[5]          = true;
   stateTransitionOnTimeout[5]     = "Ready";
   stateTransitionOnTriggerDown[5] = "Spinup";

   //--------------------------------------
   stateName[6]       = "EmptySpindown";
   stateSound[6]      = ChaingunSpinDownSound;
   stateSpinThread[6] = SpinDown;
   //
   stateTimeoutValue[6]        = 0.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
   
   //--------------------------------------
   stateName[7]       = "DryFire";
   stateSound[7]      = ChaingunDryFireSound;
   stateTimeoutValue[7]        = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";
};

datablock ItemData(Chaingun)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_chaingun.dts";
   image        = ChaingunImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a chaingun";

   computeCRC = true;
   emap = true;
};

