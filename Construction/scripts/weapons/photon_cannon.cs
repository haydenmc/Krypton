//Krypton Photon Cannon
//Coded by Sloik for Krypton


//WEAPON SOUNDS
datablock AudioProfile(PhotonSwitchSound)
{
   filename    = "fx/Bonuses/Nouns/moon.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(PhotonFireSound)
{
   filename    = "fx/powered/turret_plasma_fire.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(PhotonDryFireSound)
{
   filename    = "fx/powered/vehicle_screen_on.wav";
   description = AudioClose3d;
   preload = true;
}; 

datablock AudioProfile(PhotonProjectileSound)
{
   filename    = "fx/Bonuses/low-level1-sharp.wav";
   description = ProjectileLooping3d;
   preload = true;
}; 

datablock AudioProfile(PhotonExplodeSound)
{
   filename    = "fx/Bonuses/down_passback3_rocket.wav";
   description = AudioDefault3d;
   preload = true;
}; 

datablock AudioProfile(PhotonIdleSound)
{
   filename    = "fx/powered/turret_heavy_idle.wav";
   description = ClosestLooping3d;
   effect = DiscIdleEffect;
};


//WEAPON EFFECTS

datablock ParticleData( PhotonCrescentParticle )
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 1000; //orig 600
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent3";

   colors[0]     = "0.3 0.4 1.0 1.0";
   colors[1]     = "0.3 0.4 1.0 0.5";
   colors[2]     = "0.3 0.4 1.0 0.0";
   sizes[0]      = 2.0;
   sizes[1]      = 4.0;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData( PhotonCrescentEmitter )
{
   ejectionPeriodMS = 10; //Orig 25
   periodVarianceMS = 0;
   ejectionVelocity = 12; //Orig 20
   velocityVariance = 5.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 400; //Orig 200
   particles = "PhotonCrescentParticle";
};

datablock ParticleData(PhotonExplosionParticle)
{
   dragCoefficient      = 1; //Orig 2
   gravityCoefficient   = 0.15; //Orig 0.2
   inheritedVelFactor   = 0.2;
   constantAcceleration = 0.0;
   lifetimeMS           = 1250; //orig 750
   lifetimeVarianceMS   = 150;
   textureName          = "particleTest";
   colors[0]     = "0.3 0.4 1.0 1.0";
   colors[1]     = "1.0 1.0 0.3 1.0";
   colors[2]     = "0.3 0.4 1.0 1.0";
   colors[3]     = "0.3 0.4 1.0 0.0";
   sizes[0]      = 3;
   sizes[1]      = 1;
   sizes[2]      = 3;
   sizes[3]      = 3;
   times[0] = 0.0;
   times[1] = 0.3;
   times[2] = 0.6;
   times[3] = 1.0;
};

datablock ParticleEmitterData(PhotonExplosionEmitter)
{
   ejectionPeriodMS = 3; //Orig 7
   periodVarianceMS = 0;
   ejectionVelocity = 16; //Orig 12
   velocityVariance = 1.75;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "PhotonExplosionParticle";
};

datablock ParticleData(PhotonDust)
{
   dragCoefficient      = 1.0;
   gravityCoefficient   = -0.01;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 1000;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = true;
   spinRandomMin        = -90.0;
   spinRandomMax        = 500.0;
   textureName          = "particleTest";
   colors[0]     = "1.0 1.0 0.3 0.8";
   colors[1]     = "0.31 0.31 0.47 0.3";
   colors[2]     = "0.31 0.31 0.47 0.0";
   sizes[0]      = 3.2;
   sizes[1]      = 4.6;
   sizes[2]      = 5.0;
   times[0]      = 0.0;
   times[1]      = 0.7;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(PhotonDustEmitter)
{
   ejectionPeriodMS = 5;
   periodVarianceMS = 0;
   ejectionVelocity = 19.0;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 85;
   thetaMax         = 85;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   lifetimeMS       = 1000;
   particles = "PhotonDust";
};

//Explosion Data
//----------------------------------------------------
//  Explosion
//----------------------------------------------------
datablock ExplosionData(PhotonExplosion)
{
   soundProfile   = PhotonExplodeSound;

   emitter[0] = PhotonExplosionEmitter;
   emitter[1] = PhotonCrescentEmitter;
   emitter[2] = PhotonDustEmitter;

   shakeCamera = true;
   camShakeFreq = "10.0 6.0 9.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 1.2;
   camShakeRadius = 32.0;
};


//Projectile Data
datablock LinearProjectileData(PhotonProjectile)
{
   className = "LinearProjectileData";
   projectileShapeName = "chaingun_shot.dts";
   scale = "5 10 5";
   emitterDelay        = -1;
   directDamage        = 0.0;
   hasDamageRadius     = true;
   indirectDamage      = 0.7;
   damageRadius        = 8.5;
   radiusDamageType    = $DamageType::Plasma;
   kickBackStrength    = 1600;

   sound 				= PhotonProjectileSound;
   explosion           = "PhotonExplosion";
   underwaterExplosion = "PhotonExplosion";
//   splash              = DiscSplash;

   dryVelocity       = 160;
   wetVelocity       = 160;
   velInheritFactor  = 0.0;
   fizzleTimeMS      = 5000;
   lifetimeMS        = 8000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 90.0;
   explodeOnWaterImpact      = true;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 5000;

   hasLight    = true;
   lightRadius = 8.0;
   lightColor  = "0.3 0.4 1.0";
};

// Ammo Data
datablock ItemData(PhotonAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_disc.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some photon cells";
};



//Weapon Data
datablock ShapeBaseImageData(PhotonImage)
{
   className = WeaponImage;
   shapeFile = "weapon_energy.dts";
   item = Photon;
   ammo = PhotonAmmo;
   projectile = PhotonProjectile;
   projectileType = LinearProjectile;

   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = PhotonSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";
   stateSound[2]                    = PhotonIdleSound;

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 1.0;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateSound[3] = PhotonFireSound;
   stateScript[3] = "onFire";

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTimeout[4] = "Ready";
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";
   
   stateName[6] = "DryFire";
   stateTimeoutValue[6] = 0.3;
   stateSound[6] = PhotonDryFireSound;
   stateTransitionOnTimeout[6] = "Ready";

};

datablock ItemData(Photon)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_energy.dts";
   image = PhotonImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a photon cannon";
   emap = true;
};