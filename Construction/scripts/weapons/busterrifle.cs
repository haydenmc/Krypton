//--------------------------------------
// Krypton Buster Rifle
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds
//--------------------------------------
datablock AudioProfile(BusterSwitchSound)
{
   filename    = "fx/weapons/mine_switch.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(BusterFireSound)
{
   filename    = "fx/weapons/cg_hard3.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(BusterDryFireSound)
{
   filename    = "fx/weapons/plasma_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};

datablock AudioProfile(BusterExplodeSound)
{
   filename    = "fx/weapons/grenade_flash_explode.wav";
   description = AudioClose3d;
   preload = true;
};

//--------------------------------------------------------------------------
// Explosion
//--------------------------------------
datablock ParticleData(BusterExplosionParticle)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.25;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.25;
   lifetimeMS           = 2000;
   lifetimeVarianceMS   = 750;
   useInvAlpha          = false;
   textureName          = "special/LensFlare/Flare00";

   spinRandomMin        = -100.0;
   spinRandomMax        =  100.0;

   colors[0]     = "0.7 0.8 1.0 0.0";
   colors[1]     = "0.7 0.8 1.0 0.4";
   colors[2]     = "0.7 0.8 1.0 0.0";
   sizes[0]      = 1.0;
   sizes[1]      = 1.0;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.3;
   times[2]      = 1.0;
};
datablock ParticleEmitterData(BusterExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 1.0;
   ejectionOffset   = 3.0;
   velocityVariance = 0.5;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "BusterExplosionParticle";
};

datablock ExplosionData(BusterExplosion)
{
   explosionShape = "disc_explosion.dts";
   soundProfile   = BusterExplodeSound;

   faceViewer     = true;

   sizes[0]      = "1.3 1.3 1.3";
   sizes[1]      = "0.75 0.75 0.75";
   sizes[2]      = "0.4 0.4 0.4";
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;

   emitter[0] = "BusterExplosionEmitter";

   shakeCamera = true;
   camShakeFreq = "10.0 11.0 10.0";
   camShakeAmp = "20.0 20.0 20.0";
   camShakeDuration = 0.5;
   camShakeRadius = 10.0;
};




//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock DecalData(BusterDecal0)
{
   sizeX       = 0.4;
   sizeY       = 0.4;
   textureName = "skins/bullethole3";
};

datablock TracerProjectileData(BusterBullet)
{
   doDynamicClientHits = true;

   directDamage        = 0.0225;		////v2 was .165
   directDamageType    = $DamageType::Bullet;
   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 0.0;
   sound 				= ChaingunProjectile;

   dryVelocity       = 6000;		//g=425.0;
   wetVelocity       = 2000;	//v2 100.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 1300;
   lifetimeMS        = 1300;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 45.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 30.0;
   fizzleUnderwaterMS        = 1000;	//v2 3000;

   tracerLength    = 50.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.055;		//v2 0.10;
   crossSize       = 0.1;		//v2 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = BusterDecal0;
};

datablock TracerProjectileData(BusterExplodeProjectile)
{
projectileShapeName = "turret_muzzlepoint.dts";
scale               = "0.0 0.0 0.0";
Explosion = "BusterExplosion";
radiusDamageType    = $DamageType::Plasma;

   hasDamageRadius     = true;
   indirectDamage      = 0.6;
   kickBackStrength    = 0.1;
   directDamage        = 0.0;
   damageRadius        = 8.0;
   dryVelocity       = 0.0;
   wetVelocity       = 0.0;
   velInheritFactor  = 0.0;
   lifetimeMS        = 10;
   explodeOnDeath    = true;
};

//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(BusterAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_plasma.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some buster rifle ammo";
	computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(BusterSecondImage)
{
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   offset = "0.0 0.0 0.0";

}; 

datablock ItemData(Buster)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_shocklance.dts";
   image = BusterImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "a buster rifle";
	computeCRC = true;
};

datablock ShapeBaseImageData(BusterImage)
{
   className = WeaponImage;
   shapeFile = "weapon_shocklance.dts";
   item = Buster;
   ammo = BusterAmmo;
   offset = "0 0 0";

   casing              = MiniAAShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 30.0;
   shellVelocity       = 10.0;

   projectile = BusterBullet;
   projectileType = TracerProjectile;

	firetime = 0.7;		//v2
	
   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = BusterSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateEjectShell[3]       = true;
   stateTimeoutValue[3] = 0.5;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = true;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.1;
   stateSound[3] = BusterFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTriggerDown[4] = "Fire";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.5;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = BusterDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
   
};

function BusterImage::onMount(%this,%obj,%slot)
{
   %obj.mountImage(BusterSecondImage, 4);
displaywepstat(%obj.client,"Krypton Buster Rifle",%mode1,%mode2,"Fires highly explosive shells at an extremely fast velocity.");
   Parent::onMount(%this,%obj,%slot);
}

function BusterImage::onUnmount(%this, %obj, %slot)
{
%obj.unmountImage(4);
Parent::onUnmount(%this,%obj,%slot);
}

function BusterBullet::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal)
{
//schedule(1000,0,RadiusExplosion,%projectile, %pos, %data.damageRadius, %data.indirectDamage, %data.kickBackStrength, %projectile.sourceObject, %data.radiusDamageType);
//schedule(1000,0,PlayExplosion,%position,BusterExplodeProjectile,"0 0 1");
//    %p = new LinearProjectile() 
//         {
//         dataBlock        = BusterExplodeProjectile;
//         initialPosition  = %position;
//         initialDirection = "0 0 1";
//         sourceObject     = %projectile.sourceObject;
//         vehicleObject    = 0;
//         };
//%p.setTransform(%position);

%obj=%projectile.sourceObject;
//%projectile.delete();
//%projectile = "";

schedule(1000,0,makebusterexplode,%position,%obj,%projectile);

//%p.sourceObject     = %projectile.sourceObject;
Parent::onCollision(%data, %projectile, %targetObject, %modifier, %position, %normal);
}

function makebusterexplode(%position,%obj,%projectile)
{
   %p = new LinearProjectile() {
      dataBlock        = BusterExplodeProjectile;
      initialDirection = "0 0 1";
      initialPosition  = VectorAdd(%position,VectorScale("0 0 1",1));
      sourceObject     = %obj;
      sourceSlot       = %slot;
   };
//   %obj.lastProjectile = %p;
   MissionCleanup.add(%p);
//   if(%obj.client)
//      %obj.client.projectile = %p; 
}

//function BusterBullet::onExplode(%data, %proj, %pos, %mod)
//{
//%p = new LinearProjectile() 
//         {
//         dataBlock        = BusterExplodeProjectile;
//         initialPosition  = %pos;
//         };
//}