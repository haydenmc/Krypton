//Particle Effects and crap like that
datablock ParticleData(WarpParticle)
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   
   lifetimeMS           = 3000;
   lifetimeVarianceMS   = 0;

   spinRandomMin = 30.0;
   spinRandomMax = 30.0;
   windcoefficient = 0;
   textureName          = "skins/jetflare03";

   colors[0]     = "0.3 0.3 1.0 0.1";
   colors[1]     = "0.3 0.3 1.0 1";
   colors[2]     = "1.0 0.3 0.3 1";
   colors[3]     = "1.0 0.3 0.3 0.1";

   sizes[0]      = 5;
   sizes[1]      = 6;
   sizes[2]      = 8;
   sizes[3]      = 15;

   times[0]      = 0.25;
   times[1]      = 0.5;
   times[2]      = 0.75;
   times[3]      = 1;

};


datablock ParticleEmitterData(WarpEmitter)
{
//   lifetimeMS        = 10;
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;

   ejectionVelocity = 0.01;
   velocityVariance = 0.0;
   ejectionoffset = 5;
   thetaMin         = 80.0;
   thetaMax         = 100.0;
	
   phiReferenceVel = "180";
   phiVariance = "5";
   orientParticles  = false;
   orientOnVelocity = true;

   particles = "WarpParticle";
};

datablock ShapeBaseImageData(WarpEmitMount)
{
   className = WeaponImage;
   shapeFile = "turret_Muzzlepoint.dts";
   offset = "0.0 4.0 0.0 ";
   rotation = "0 0 0 1";

   stateName[0]                = "Activate";
   stateEmitter[0]             = WarpEmitter;   // Particle Emitter
   stateEmitterNode[0]         = Muzzlepoint1;  // Just keep this the same
   stateEmitterTime[0]         = 99999;  // Time in seconds (forever?)
};


//Now for the solid code...

function toggleSuperWarp(%obj)
{
if (%obj.warping $= "" || %obj.warping == 0) {
%obj.player = %obj.getMountedObject(0);
%obj.client = %obj.player.client;
schedule(3000,0,SuperWarpLoop,%obj);
%obj.warping = 0.5;

   for(%i = 0; %i < %obj.getDatablock().numMountPoints; %i++)
      if (%obj.getMountNodeObject(%i)) {
         %passenger = %obj.getMountNodeObject(%i);
         commandToClient(%passenger.client, 'BottomPrint', "Initiating Slipstream Warp in 3 Seconds...", 2, 3);
      }

serverPlay3D(SatchelChargeActivateSound, %obj.getTransform());
} else if (%obj.warping == 1) {

   for(%i = 0; %i < %obj.getDatablock().numMountPoints; %i++)
      if (%obj.getMountNodeObject(%i)) {
         %passenger = %obj.getMountNodeObject(%i);
         commandToClient(%passenger.client, 'BottomPrint', "Slipstream Warp Disabling...", 2, 3);
      }

%obj.warping = 2;
}
}

function SuperWarpLoop(%obj)
{
%vec = vectorNormalize(%obj.getForwardVector());
if (%obj.warping == 0) 
return;

if (%obj.warping == 2) {
if (%obj.warpdistance $= "")
%obj.warpdistance = 150;

%obj.warpdistance -= 2;
%newposition = Vectoradd(%obj.getPosition(),VectorScale(%vec,%obj.warpdistance));
%obj.setPosition(%newposition);
if (%obj.warpdistance <= 2) {
%obj.unmountImage(7);
%obj.warping = 0;
%obj.warpdistance = "";
} else {
schedule(50,0,SuperWarpLoop,%obj);
}
return;
}
if (%obj.warping == 0.5)
%obj.mountImage(WarpEmitMount, 7);

%obj.warping = 1;
%newposition = Vectoradd(%obj.getPosition(),VectorScale(%vec,150));
%obj.setPosition(%newposition);
schedule(50,0,SuperWarpLoop,%obj);

   for(%i = 0; %i < %obj.getDatablock().numMountPoints; %i++)
      if (%obj.getMountNodeObject(%i)) {
         %passenger = %obj.getMountNodeObject(%i);
         %passenger.setWhiteOut(0.8);
      }

}
