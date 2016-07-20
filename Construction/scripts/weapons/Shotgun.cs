//--------------------------------------------------------------------------
// Double Barrel Shotgun
// Built by Defender
// onFire code by Team hammer
//--------------------------------------------------------------------------


//--------------------------------------------------------------------------
// Sounds
//--------------------------------------

datablock AudioProfile(ShotgunSwitchSound)
{
   filename    = "fx/weapons/chaingun_activate.wav";
   description = AudioClosest3d;
   preload = true;
   effect = TargetingLaserSwitchEffect;
};

//datablock AudioProfile(ShotgunProjectile)
//{
//   filename    = "fx/weapons/chaingun_projectile.wav";
//   description = ProjectileLooping3d;
//   preload = true;
//};

//datablock AudioProfile(ShotgunImpactSound)
//{
//   filename    = "fx/weapons/cg_metal3.wav";
//   description = AudioClosest3d;
//   preload = true;
//};

datablock AudioProfile(ShotgunDryFireSound)
{
   filename    = "fx/weapons/chaingun_dryfire.wav";
   description = AudioClose3d;
   preload = true;
};




datablock AudioProfile(ShotGunFireSound1)
{
   filename    = "fx/weapons/cg_metal1.wav";
   description = AudioDefault3d;
   preload = true;
   effect = TargetingLaserPaintEffect;
};
datablock AudioProfile(ShotGunFireSound2)
{
   filename    = "fx/weapons/cg_metal2.wav";
   description = AudioDefault3d;
   preload = true;
   effect = TargetingLaserPaintEffect;
};
datablock AudioProfile(ShotGunFireSound3)
{
   filename    = "fx/weapons/cg_metal3.wav";
   description = AudioDefault3d;
   preload = true;
   effect = TargetingLaserPaintEffect;
};
datablock AudioProfile(ShotGunFireSound4)
{
   filename    = "fx/weapons/cg_metal4.wav";
   description = AudioDefault3d;
   preload = true;
   effect = TargetingLaserPaintEffect;
};

//--------------------------------------

datablock TracerProjectileData(ShotgunBullet)
{
   doDynamicClientHits = true;
   directDamage          = 0.085;
   directDamageType    = $DamageType::Bullet;
   //directDamageType    = $DamageType::Shotgun;
   //Add a shotgun damage type for this gun, thin remove the base type..
   closeRangeMultiplier  = 1.5;
   rifleHeadMultiplier = 1.5;

   explosion           = "ChaingunExplosion";
   splash              = ChaingunSplash;

   kickBackStrength  = 0.0;
//   sound 				= ShotgunImpactSound;

   dryVelocity       = 625.0; // was 425.0
   wetVelocity       = 525.0;
   velInheritFactor  = 1.0;
   fizzleTimeMS      = 4000;
   lifetimeMS        = 4000;
   explodeOnDeath    = false;
   reflectOnWaterImpactAngle = 0.0;
   explodeOnWaterImpact      = false;
   deflectionOnWaterImpact   = 0.0;
   fizzleUnderwaterMS        = 4000;

   tracerLength    = 15.0;
   tracerAlpha     = false;
   tracerMinPixels = 6;
   tracerColor     = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
	tracerTex[0]  	 = "special/tracer00";
	tracerTex[1]  	 = "special/tracercross";
	tracerWidth     = 0.10;
   crossSize       = 0.20;
   crossViewAng    = 0.990;
   renderCross     = true;

   decalData[0] = ChaingunDecal1;
   decalData[1] = ChaingunDecal2;
   decalData[2] = ChaingunDecal3;
   decalData[3] = ChaingunDecal4;
   decalData[4] = ChaingunDecal5;
   decalData[5] = ChaingunDecal6;
};


//--------------------------------------------------------------------------

datablock ItemData(ShotgunAmmo)
{
   className = Ammo;
   catagory = "Ammo";
   shapeFile = "ammo_chaingun.dts";
   mass = 1;
   elasticity = 0.2;
   friction = 0.6;
   pickupRadius = 2;
	pickUpName = "some Shotgun ammo";
computeCRC = true;
   emap = true;
};

//--------------------------------------------------------------------------
// Weapon
//--------------------------------------------------------------------------

datablock DebrisData(ShotgunShellDebris)
{
   //shapeName = "weapon_chaingun_ammocasing.dts";
   shapeName = "weapon_missile_casement.dts";
   scale = "2.5 2.5 3.5";

   lifetime = 4.0;

   minSpinSpeed = 100.0;
   maxSpinSpeed = 200.0;

   elasticity = 0.5;
   friction = 0.2;

   numBounces = 3;

   fade = true;
   staticOnMaxBounce = true;
   snapOnMaxBounce = true;
};

//--------------------------------------
// Shotgun
//--------------------------------------

datablock ItemData(Shotgun)
{
   className	= Weapon;
   catagory		= "Spawn Items";
   shapeFile	= "weapon_targeting.dts";
   image		= ShotgunImage;
   ammo		= ShotgunAmmo;
   mass		= 1;
   elasticity	= 0.2;
   friction		= 0.6;
   pickupRadius	= 2;
   pickUpName = "a Shotgun";
computeCRC = true;
   emap = true;
};

datablock ShapeBaseImageData(ShotgunImage)
{
	className = WeaponImage;
   shapeFile = "weapon_targeting.dts";
   item = Shotgun;
   ammo = ShotgunAmmo;
   offset = "0.08 0 0.0";  // offsets to the right
   emap = true;

   projectile = ShotgunBullet;
   projectileType = TracerProjectile;

   casing              = ShotgunShellDebris;
   shellExitDir        = "0.3 1.0 1.0";
   shellExitOffset     = "0.15 -0.56 -0.1";
   shellExitVariance   = 15.0;
   shellVelocity       = 3.0;

   projectileSpread = 14.0 / 1000.0;

   // State Data
   stateName[0] = "Activate";
   stateTransitionOnTimeout[0] = "ActivateReady";
   stateTimeoutValue[0] = 0.5;
   stateSequence[0] = "Activate";
   stateSound[0] = ChaingunSwitchSound;

   stateName[1] = "ActivateReady";
   stateTransitionOnLoaded[1] = "Ready";
   stateTransitionOnNoAmmo[1] = "NoAmmo";

   stateName[2] = "Ready";
   stateTransitionOnNoAmmo[2] = "NoAmmo";
   stateTransitionOnTriggerDown[2] = "Fire";

   stateName[3] = "Fire";
   stateTransitionOnTimeout[3] = "Reload";
   stateTimeoutValue[3] = 0.085;
   stateFire[3] = true;
   stateRecoil[3] = LightRecoil;
   stateAllowImageChange[3] = false;
   stateSequence[3] = "Fire";
   stateScript[3] = "onFire";
   stateSound[3] = ShotgunFireSound;

   stateName[4]                  = "Reload";
   stateTransitionOnNoAmmo[4]    = "NoAmmo";
   stateTransitionOnTimeout[4]   = "Ready";
   stateTimeoutValue[4]          = 1.25;
   stateAllowImageChange[4]      = false;
   stateSequence[4]              = "Reload";
   stateScript[4] = "onReload";
   stateEjectShell[4]            = true;

   stateName[5] = "NoAmmo";
   stateTransitionOnAmmo[5] = "Reload";
   stateSequence[5] = "NoAmmo";
   stateTransitionOnTriggerDown[5] = "DryFire";

   stateName[6]       = "DryFire";
   stateSound[6]      = GrenadeDryFireSound;
   stateTimeoutValue[6]        = 1.5;
   stateTransitionOnTimeout[6] = "NoAmmo";
};

datablock ShapeBaseImageData(Shotgun2Image) : ShotgunImage
{
    offset = "0 0 0";
    stateScript[3] = "onFire";
};
datablock ShapeBaseImageData(Shotgun3Image)
{
    emap = true;
   shapeFile = "ammo_disc.dts";
   offset = "0.02 0.28 -0.09";
   rotation = "1 0 0 90";
};
function ShotgunImage::onReload(%data, %obj, %slot)
{
   %obj.setImageTrigger(4, false);
}
function ShotgunImage::onMount(%this,%obj,%slot)
{
   Parent::onMount(%this, %obj, %slot);
   %obj.mountImage(Shotgun2Image, 4);
   %obj.mountImage(Shotgun3Image, 5);
displaywepstat(%obj.client,"Krypton Shotgun",%mode1,%mode2,"Also known as \"Shotfun\"\nFires a large quantity of chaingun rounds at once.\nCreated by Dark Order of Chaos clan.");
//  %obj.client.setWeaponsHudActive("Chaingun");
//  %obj.client.setWeaponsHudHighLightBmp("Chaingun");
//  %obj.client.setWeaponsHudBackGroundBmp("Chaingun");
}

function ShotgunImage::onUnmount(%this,%obj,%slot)
{
   Parent::onUnmount(%this, %obj, %slot);
   %obj.unmountImage(4);
    %obj.unmountImage(5);
}

function ShotgunImage::onFire(%data, %obj, %slot)
{
if (%obj.inpeacesphere !$= "") //No firing in peace spheres.
return;

	// cloaking
	if( %obj.station $= "" && %obj.isCloaked() )
    {
		if( %obj.respawnCloakThread !$= "" )
		{
			cancel(%obj.respawnCloakThread);
			%obj.setCloaked( false );
			%obj.respawnCloakThread = "";
		}
		else
		{
			if( %obj.getEnergyLevel() > 20 )
			{
				%obj.setCloaked( false );
				%obj.reCloak = %obj.schedule( 500, "setCloaked", true );

			}
		}
	}

	// ammo
    //%obj.applyKick(-400);
    %obj.decInventory(%data.ammo,1);
    %obj.setImageTrigger(4, true);
    %vec = %obj.getMuzzleVector(%slot);
    %mp = %obj.getMuzzlePoint(%slot);


	for (%i=0; %i < 12; %i++)
	{
      %x = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
      %y = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
      %z = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vector = MatrixMulVector(%mat, %vec);

      %p = new (%data.projectileType)() {
            dataBlock        = %data.projectile;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
			damageFactor	 = 1;
            sourceSlot       = %slot;
        };
     }
%which = getRandom(0,3);
if (%which == 0)
serverPlay3D(ShotGunFireSound1, %obj.getTransform());
if (%which == 1)
serverPlay3D(ShotGunFireSound2, %obj.getTransform());
if (%which == 2)
serverPlay3D(ShotGunFireSound3, %obj.getTransform());
if (%which == 3)
serverPlay3D(ShotGunFireSound4, %obj.getTransform());


   if (isObject(%obj.lastProjectile) && %obj.deleteLastProjectile)
      %obj.lastProjectile.killit();

   %obj.lastProjectile = %p;
   %obj.deleteLastProjectile = %data.deleteLastProjectile;
   MissionCleanup.add(%p);

   // AI hook
   if(%obj.client)
      %obj.client.projectile = %p;
    if (isObject(%p))
   %p.schedule(%p.getDataBlock().lifetimeMS + 500, "killit");
   return %p;
}

function Shotgun2Image::onFire(%data, %obj, %slot)
{
if (%obj.inpeacesphere !$= "") //No firing in peace spheres.
return;

	// cloaking
	if( %obj.station $= "" && %obj.isCloaked() )
    {
		if( %obj.respawnCloakThread !$= "" )
		{
			cancel(%obj.respawnCloakThread);
			%obj.setCloaked( false );
			%obj.respawnCloakThread = "";
		}
		else
		{
			if( %obj.getEnergyLevel() > 20 )
			{
				%obj.setCloaked( false );
				%obj.reCloak = %obj.schedule( 500, "setCloaked", true );

			}
		}
	}

	// ammo
    //%obj.applyKick(-400);
    %obj.decInventory(%data.ammo,1);
    %mp = %obj.getMuzzlePoint(%slot);
    %vec = %obj.getMuzzleVector(%slot);
	for (%i=0; %i < 12; %i++)
	{
      %x = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
      %y = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
      %z = (getRandom() - 0.5) * 2 * 3.1415926 * %data.projectileSpread;
      %mat = MatrixCreateFromEuler(%x @ " " @ %y @ " " @ %z);
      %vector = MatrixMulVector(%mat, %vec);

      %p = new (%data.projectileType)() {
            dataBlock        = %data.projectile;
            initialDirection = %vector;
            initialPosition  = %mp;
            sourceObject     = %obj;
			damageFactor	 = 1;
            sourceSlot       = %slot;
        };
    }
   if (isObject(%obj.lastProjectile) && %obj.deleteLastProjectile)
      %obj.lastProjectile.killit();

   %obj.lastProjectile = %p;
   %obj.deleteLastProjectile = %data.deleteLastProjectile;
   MissionCleanup.add(%p);

   // AI hook
   if(%obj.client)
      %obj.client.projectile = %p;
    if (isObject(%p))
   %p.schedule(%p.getDataBlock().lifetimeMS + 500, "killit");
   return %p;
}
