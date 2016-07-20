//--------------------------------------
// Plasma rifle
//--------------------------------------

//--------------------------------------------------------------------------
// Sounds
//--------------------------------------
datablock AudioProfile(AssaultSwitchSound)
{
   filename    = "fx/weapons/plasma_rifle_activate.wav";
   description = AudioClosest3d;
   preload = true;
};

datablock AudioProfile(AssaultFireSound)
{
   filename    = "fx/weapons/cg_soft2.wav";
   description = AudioDefault3d;
   preload = true;
};

datablock AudioProfile(AssaultDryFireSound)
{
   filename    = "fx/weapons/plasma_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};


//--------------------------------------------------------------------------
// Projectile
//--------------------------------------
datablock DecalData(AssaultDecal0)
{
   sizeX       = 0.2;
   sizeY       = 0.2;
   textureName = "skins/bullethole3";
};

datablock TracerProjectileData(AssaultBullet)
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

   tracerLength    = 300.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.055;		//v2 0.10;
   crossSize       = 0.1;		//v2 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal0;
};


//--------------------------------------------------------------------------
// Ammo
//--------------------------------------

datablock ItemData(AssaultAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_plasma.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some assault rifle ammo";
	computeCRC = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------
datablock ShapeBaseImageData(AssaultSecondImage)
{
   className = WeaponImage;

   shapeFile = "weapon_targeting.dts";
   offset = "0.0 0.0 0.0";

}; 

datablock ItemData(Assault)
{
   className = Weapon;
   catagory = "Spawn Items";
   shapeFile = "weapon_plasma.dts";
   image = AssaultImage;
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "an assault rifle";
	computeCRC = true;
};

datablock ShapeBaseImageData(AssaultImage)
{
   className = WeaponImage;
   shapeFile = "weapon_sniper.dts";
   item = Assault;
   ammo = AssaultAmmo;
   offset = "0 0 0";

   casing              = MiniAAShellDebris;
   shellExitDir        = "1.0 0.3 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 30.0;
   shellVelocity       = 10.0;

   projectile = AssaultBullet;
   projectileType = TracerProjectile;

	firetime = 0.7;		//v2
	
   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = AssaultSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateEjectShell[3]       = true;
   stateTimeoutValue[3] = 0.03;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = true;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateEmitterTime[3] = 0.1;
   stateSound[3] = AssaultFireSound;

   stateName[4] = "Reload";
   stateTransitionOnNoAmmo[4] = "NoAmmo";
   stateTransitionOnTriggerDown[4] = "Fire";
   stateTransitionOnTimeout[4] = "Ready";
   stateTimeoutValue[4] = 0.03;
   stateAllowImageChange[4] = false;
   stateSequence[4] = "Reload";

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = AssaultDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
   
};

function AssaultImage::onMount(%this,%obj,%slot)
{
   %obj.mountImage(AssaultSecondImage, 4);
displaywepstat(%obj.client,"Krypton Assault Rifle",%mode1,%mode2,"Also known as \"KAR Rifle\"\nFires 30 light weight rounds per second.");
   Parent::onMount(%this,%obj,%slot);
}

function AssaultImage::onUnmount(%this, %obj, %slot)
{
%obj.unmountImage(4);
Parent::onUnmount(%this,%obj,%slot);
}