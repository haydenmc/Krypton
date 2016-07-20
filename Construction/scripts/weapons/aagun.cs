//--------------------------------------
// Mini AA Gun, thrown together by sloik for Krypton
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds
//--------------------------------------
datablock AudioProfile(MiniAASwitchSound)
{
   filename    = "fx/powered/turret_aa_activate.wav";
   description = AudioClosest3d;
   preload = true;
   effect = ChaingunSwitchEffect;
};

datablock AudioProfile(MiniAAFireSound)
{
   filename    = "fx/powered/turret_aa_fire.wav";
   description = AudioDefault3d;
   preload = true;
   effect = ChaingunFireEffect;
};

datablock AudioProfile(MiniAASpinDownSound)
{
   filename    = "fx/weapons/chaingun_off.wav";
   description = AudioClosest3d;
   preload = true;
   effect = ChaingunSpinDownEffect;
};

datablock AudioProfile(MiniAASpinUpSound)
{
   filename    = "fx/weapons/chaingun_start.wav";
   description = AudioClosest3d;
   preload = true;
   effect = ChaingunSpinUpEffect;
};

datablock AudioProfile(MiniAADryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
   effect = ChaingunDryFire;
};


//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------
//--------------------------------------------------------------------------
// Explosion
//--------------------------------------
datablock ParticleData(MiniAABulletExplosionParticle1)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/crescent4";
   colors[0] = "0.57 0.41 1.0 1.0";
   colors[1] = "0.57 0.41 1.0 1.0";
   colors[2] = "0.57 0.41 0.0 0.0";
   sizes[0]      = 0.25;
   sizes[1]      = 0.5;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(MiniAABulletExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 2;
   velocityVariance = 1.5;
   ejectionOffset   = 0.0;
   thetaMin         = 70;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = true;
   lifetimeMS       = 200;
   particles = "MiniAABulletExplosionParticle1";
};

datablock ParticleData(MiniAABulletExplosionParticle2)
{
   dragCoefficient      = 2;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.2;
   constantAcceleration = -0.0;
   lifetimeMS           = 600;
   lifetimeVarianceMS   = 000;
   textureName          = "special/blasterHit";
   colors[0] = "0.57 0.41 1.0 0.6";
   colors[1] = "0.57 0.41 1.0 0.6";
   colors[2] = "0.57 0.41 0.0 0.0";
   sizes[0]      = 0.3;
   sizes[1]      = 0.90;
   sizes[2]      = 1.50;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

datablock ParticleEmitterData(MiniAABulletExplosionEmitter2)
{
   ejectionPeriodMS = 30;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 0.0;
   ejectionOffset   = 0.0;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   orientParticles  = false;
   lifetimeMS       = 200;
   particles = "MiniAABulletExplosionParticle2";
};

datablock ExplosionData(MiniAABulletExplosion)
{
   soundProfile   = blasterExpSound;
   emitter[0]     = MiniAABulletExplosionEmitter;
   emitter[1]     = MiniAABulletExplosionEmitter2;
};


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock TracerProjectileData(MiniAABullet)
{
   doDynamicClientHits = true;

   projectileShapeName = "energy_bolt.dts";
   directDamage        = 0.25;
   directDamageType    = $DamageType::Bullet;
   explosion           = "MiniAABulletExplosion";
   splash              = ChaingunSplash;

   dryVelocity       = 250.0;
   wetVelocity       = 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 3000;
   lifetimeMS        = 3000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 3000;

   tracerLength    = 20;
   tracerAlpha     = false;
   tracerMinPixels = 3;
   tracerColor     = "1 1 1 1";
	tracerTex[0]  	 = "special/shrikeBolt";
	tracerTex[1]  	 = "special/shrikeBoltCross";
	tracerWidth     = 0.55;
   crossSize       = 0.99;
   crossViewAng    = 0.990;
   renderCross     = true;
   emap = true;

   hasLight    = true;
   lightRadius = 6;		//v2 3.0;
   lightColor  = "0.57 0.41 1.0";
};


//--------------------------------------------------------------------------
// Particle effects
//--------------------------------------


datablock DebrisData( MiniAAShellDebris )
{
   shapeName = "weapon_chaingun_ammocasing.dts";

   lifetime = 3.0;

   minSpinSpeed = 300.0;
   maxSpinSpeed = 400.0;

   elasticity = 0.5;
   friction = 0.01;

   numBounces = 3;

   fade = true;
   staticOnMaxBounce = true;
   snapOnMaxBounce = true;
};             

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(MiniAAImage)
{
   className = WeaponImage;
   shapeFile = "weapon_chaingun.dts";
   item      = MiniAA;
   projectile = MiniAABullet;
   projectileType = TracerProjectile;
   emap = true;
   usesEnergy = true;
   fireEnergy = 0.1;
   minEnergy = 0.1;

   casing              = MiniAAShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 30.0;
   shellVelocity       = 10.0;

   projectileSpread = 1.0 / 1000.0;

   //--------------------------------------
   stateName[0]             = "Activate";
   stateSequence[0]         = "Activate";
   stateSound[0]            = MiniAASwitchSound;
   stateAllowImageChange[0] = false;
   //
   stateTimeoutValue[0]        = 0.2;
   stateTransitionOnTimeout[0] = "Ready";
//   stateTransitionOnNoAmmo[0]  = "NoAmmo";

   //--------------------------------------
   stateName[1]       = "Ready";
   stateSpinThread[1] = Stop;
   //
   stateTransitionOnTriggerDown[1] = "Spinup";
//   stateTransitionOnNoAmmo[1]      = "NoAmmo";

   //--------------------------------------
   stateName[2]               = "NoAmmo";
   stateTransitionOnAmmo[2]   = "Ready";
   stateSpinThread[2]         = Stop;
   stateTransitionOnTriggerDown[2] = "DryFire";

   //--------------------------------------
   stateName[3]         = "Spinup";
   stateScript[3]       = "onSpinup";
   stateSpinThread[3]   = SpinUp;
   stateSound[3]        = MiniAASpinupSound;
   //
   stateTimeoutValue[3]          = 0.30;
   stateWaitForTimeout[3]        = false;
   stateTransitionOnTimeout[3]   = "Fire";
   stateTransitionOnTriggerUp[3] = "Spindown";

   //--------------------------------------
   stateName[4]             = "Fire";
   stateSequence[4]            = "Fire";
   stateSequenceRandomFlash[4] = true;
   stateSpinThread[4]       = FullSpeed;
   stateSound[4]            = MiniAAFireSound;
   stateRecoil[4]           = LightRecoil;
   stateAllowImageChange[4] = false;
   stateScript[4]           = "onFire";
   stateFire[4]             = true;
   stateEjectShell[4]       = true;
   //
   stateTimeoutValue[4]          = 0.22;
   stateTransitionOnTimeout[4]   = "FireDelay";
   stateTransitionOnTriggerUp[4] = "Spindown";
//   stateTransitionOnNoAmmo[4]    = "EmptySpindown";
//----------------------------
   stateName[5]             = "FireDelay";
   stateSequence[5]         = "FireDelay";
   stateSpinThread[5]       = FullSpeed;
   stateAllowImageChange[5] = false;
   stateTimeoutValue[5]          = 0.01;
   stateTransitionOnTimeout[5]   = "Fire";
//----------------------------
   //--------------------------------------
   stateName[6]       = "Spindown";
   stateScript[6]     = "onSpindown";
   stateSound[6]      = MiniAASpinDownSound;
   stateSpinThread[6] = SpinDown;
   //
   stateTimeoutValue[6]            = 0.3;
   stateWaitForTimeout[6]          = true;
   stateTransitionOnTimeout[6]     = "Ready";
   stateTransitionOnTriggerDown[6] = "Spinup";

   //--------------------------------------
   stateName[7]       = "EmptySpindown";
   stateSound[7]      = MiniAASpinDownSound;
   stateSpinThread[7] = SpinDown;
   stateScript[7]     = "onSpindown";
   //
   stateTimeoutValue[7]        = 0.5;
   stateTransitionOnTimeout[7] = "NoAmmo";
   
   //--------------------------------------
   stateName[8]       = "DryFire";
   stateSound[8]      = MiniAADryFireSound;
   stateTimeoutValue[8]        = 0.5;
   stateTransitionOnTimeout[8] = "NoAmmo";
};

datablock ItemData(MiniAA)
{
   className    = Weapon;
   catagory     = "Spawn Items";
   shapeFile    = "weapon_chaingun.dts";
   image        = MiniAAImage;
   mass         = 1;
   elasticity   = 0.2;
   friction     = 0.6;
   pickupRadius = 2;
   pickUpName   = "a miniature AA gun";

   computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(MiniAASecondImage)
{
   shapeFile = "weapon_repair.dts";
   offset = "0.07 0 -0.19";
  emap = true;


   stateName[0] = "Activate";
   stateSpinThread[0] = Stop;
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.2;

   stateName[1] = "ActivateReady";
   stateSpinThread[1] = Stop;
   stateTransitionOnAmmo[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "Ready";

   stateName[2] = "Ready";
   stateSpinThread[2] = Stop;
//   stateTransitionOnNoAmmo[2] = "Deactivate";
   stateTransitionOnTriggerDown[2] = "Validate";

   stateName[3] = "Validate";
//   stateTransitionOnTimeout[3] = "Validate";
   stateTimeoutValue[3] = 0.30;
   stateEnergyDrain[3] = 0;
   stateSpinThread[3] = SpinUp;
   stateTransitionOnTimeout[3] = "Repair";
   stateTransitionOnTriggerUp[3] = "Deactivate";

   stateName[4] = "Repair";
   stateSound[4] = RepairPackFireSound;
   stateSpinThread[4] = FullSpeed;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "activate";
   stateFire[4] = true;
   stateEnergyDrain[4] = 0;
   stateTimeoutValue[4] = 0.23;
   stateTransitionOnTimeOut[4] = "Repair";
//   stateTransitionOnNoAmmo[4] = "Deactivate";
   stateTransitionOnTriggerUp[4] = "Deactivate";
   stateTransitionOnNotLoaded[4] = "Validate";

   stateName[5] = "Deactivate";
   stateSpinThread[5] = SpinDown;
   stateSequence[5] = "activate";
   stateDirection[5] = false;
   stateTimeoutValue[5] = 0.3;
   stateTransitionOnTimeout[5] = "ActivateReady";
}; 

function MiniAAImage::onMount(%this,%obj,%slot)
{
   Parent::onMount(%this,%obj,%slot);
   %obj.mountImage(MiniAASecondImage, 4);
displaywepstat(%obj.client,"Krypton Handheld Anti-Air Weapon",%mode1,%mode2,"Also known as \"AA Gun\"\nFires high powered energy slugs at a relatively high speed.");
}

//I think those labels are reversed below, but whateva meng.
//Fire All At Once
function MiniAAImage::onSpindown(%this,%obj,%slot)
{
   %obj.setImageTrigger(4, false);
}

//Stop All At Once
function MiniAAImage::onSpinup(%this,%obj,%slot)
{
   %obj.setImageTrigger(4, true);
}

function MiniAAImage::onUnmount(%this, %obj, %slot)
{
%obj.unmountImage(4);
Parent::onUnmount(%this,%obj,%slot);
//%obj.unmountImage($butts);
}