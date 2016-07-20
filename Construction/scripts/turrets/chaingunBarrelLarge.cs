//--------------------------------------------------------------------------
// Chaingun Turret
//
// Tip for players: 2-3 of these can chew up light to medium armors in an instant.
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
//
//

datablock ParticleData(ChaingunBarrelSparks) // This sets sparks flying when the bullet projectiles hit anything.
{
dragCoefficient = 1;
gravityCoefficient = 0.0;
inheritedVelFactor = 0.2;
constantAcceleration = 0.0;
lifetimeMS = 300;
lifetimeVarianceMS = 0;
textureName = "special/spark00";
colors[0] = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
colors[1] = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
colors[2] = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
sizes[0] = 1.2;
sizes[1] = 0.4;
sizes[2] = 0.1;
times[0] = 0.0;
times[1] = 0.2;
times[2] = 1.0;

};

datablock ParticleEmitterData(ChaingunBarrelSparkEmitter)
{
ejectionPeriodMS = 4;
periodVarianceMS = 0;
ejectionVelocity = 6;
velocityVariance = 4.0;
ejectionOffset = 0.0;
thetaMin = 0;
thetaMax = 50;
phiReferenceVel = 0;
phiVariance = 360;
overrideAdvances = false;
orientParticles = true;
lifetimeMS = 100;
particles = "ChaingunBarrelSparks";
};


datablock ExplosionData(ChaingunBarrelExplosion)
{
soundProfile = ChaingunImpact;

//emitter[0] = ChaingunBarrelImpactSmoke;
emitter[0] = ChaingunBarrelSparkEmitter;

faceViewer = false;
};

//---------------------------

datablock ExplosionData(ChaingunBarrelBoltExplosion)
{
soundProfile = PlasmaBarrelExpSound;
particleEmitter = PlasmaBarrelExplosionEmitter;
particleDensity = 100;
particleRadius = 0.6;
faceViewer = true;

emitter[0] = PlasmaBarrelCrescentEmitter;

subExplosion[0] = PlasmaBarrelSubExplosion1;
subExplosion[1] = PlasmaBarrelSubExplosion2;
subExplosion[2] = PlasmaBarrelSubExplosion3;

shakeCamera = true;
camShakeFreq = "10.0 9.0 9.0";
camShakeAmp = "1.0 1.0 1.0";
camShakeDuration = 0.1;
camShakeRadius = 5.0;
};
//--------------------------------------------------------------------------
// Projectile
//--------------------------------------

datablock TracerProjectileData(ChaingunTurretBullet)
{
doDynamicClientHits = true;

directDamage = 0.05;
directDamageType = $DamageType::Bullet;
explosion = "ChaingunBarrelExplosion";
splash = ChaingunBarrelSplash;

kickBackStrength = 0.0;
sound = ChaingunProjectile;

dryVelocity       = 560;		//g=425.0;
wetVelocity = 100.0;
velInheritFactor = 1.0;
fizzleTimeMS = 3000;
lifetimeMS = 3000;
explodeOnDeath = false;
reflectOnWaterImpactAngle = 0.0;
explodeOnWaterImpact = false;
deflectionOnWaterImpact = 0.0;
fizzleUnderwaterMS = 3000;


tracerLength    = 75.0;
tracerAlpha = false;
tracerMinPixels = 6;
tracerColor = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ " 0.75";
tracerTex[0] = "special/tracer00";
tracerTex[1] = "special/tracercross";
tracerWidth     = 0.055;		//v2 0.10;
crossSize       = 0.1;		//v2 0.20;
crossViewAng = 0.990;
renderCross = true;
};

datablock ShockwaveData(ChaingunTurretMuzzleFlash) // Since I don`t make custom skins, this is a muzzle flash, rendered in real-time, in 3d from within the game.
{
width = 0.5;
numSegments = 13;
numVertSegments = 1;
velocity = 2.0;
acceleration = -1.0;
lifetimeMS = 300;
height = 0.1;
verticalCurve = 0.5;

mapToTerrain = false;
renderBottom = false;
orientToNormal = true;
renderSquare = true;

texture[0] = "special/tracer00";
texture[1] = "special/tracercross";
texWrap = 3.0;

times[0] = 0.0;
times[1] = 0.5;
times[2] = 1.0;

colors[0] = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ "0.75";
colors[1] = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ "0.75";
colors[2] = 211.0/255.0 @ " " @ 215.0/255.0 @ " " @ 120.0/255.0 @ "0.75";
};


//--------------------------------------------------------------------------
// Heavy Chaingun Turret Image
//--------------------------------------------------------------------------

datablock TurretImageData(ChaingunBarrelLarge)
{
shapeFile = "turret_fusion_large.dts";
item = ChaingunBarrelLargePack;

projectile = ChaingunTurretBullet;
projectileType = TracerProjectile;
usesEnergy = true;
fireEnergy = 0;
minEnergy = 1;
emap = true;

projectileSpread = 3.0 / 1000.0; // The less spread, the more accurate.

muzzleFlash = ChaingunTurretMuzzleFlash;

// Turret parameters
activationMS = 300;
deactivateDelayMS = 600;
thinkTimeMS = 200;
degPerSecTheta = 580;
degPerSecPhi = 960;
attackRadius = 170;

// State transitions
stateName[0] = "Activate";
stateTransitionOnNotLoaded[0] = "Dead";
stateTransitionOnLoaded[0] = "ActivateReady";

stateName[1] = "ActivateReady";
stateSequence[1] = "Activate";
stateSound[1] = ChaingunSwitchSound;
stateTimeoutValue[1] = 1;
stateTransitionOnTimeout[1] = "Ready";
stateTransitionOnNotLoaded[1] = "Deactivate";
stateTransitionOnNoAmmo[1] = "NoAmmo";

stateName[2] = "Ready";
stateTransitionOnNotLoaded[2] = "Deactivate";
stateTransitionOnTriggerDown[2] = "Fire";
stateTransitionOnNoAmmo[2] = "NoAmmo";

// fire off about 2 quick shots
stateName[3] = "Fire";
stateFire[3] = true;
stateAllowImageChange[3] = false;
stateSequence[3] = "Fire";
stateSound[3] = ChaingunFireSound;
stateScript[3] = "onFire";
stateTimeoutValue[3] = 0.06; //0.3
stateTransitionOnTimeout[3] = "Fire";
stateTransitionOnTriggerUp[3] = "Ready";
// stateTransitionOnTriggerUp[3] = "Reload";
stateTransitionOnNoAmmo[3] = "NoAmmo";

stateName[8] = "Reload";
stateTimeoutValue[7] = 1.0;
stateAllowImageChange[8] = false;
stateSequence[8] = "Reload";
stateTransitionOnTimeout[8] = "Ready";
stateTransitionOnNotLoaded[8] = "Deactivate";
stateTransitionOnNoAmmo[8] = "NoAmmo";

stateName[9] = "Deactivate";
stateSequence[9] = "Activate";
stateDirection[9] = false;
stateTimeoutValue[9] = 1;
stateTransitionOnLoaded[9] = "ActivateReady";
stateTransitionOnTimeout[9] = "Dead";

stateName[10] = "Dead";
stateTransitionOnLoaded[10] = "ActivateReady";

stateName[11] = "NoAmmo";
stateTransitionOnAmmo[11] = "Reload";
stateSequence[11] = "NoAmmo";
};
