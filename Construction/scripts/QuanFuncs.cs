//These functions brought to you by Quantium X

function mountLaser(%client)
{
	%plyr = %client.client.player;
    %client = %client.client;
    if(%client.laserMode != 1)
      %client.laserMode++;
    else
     %client.laserMode = 0;
     
    valLaser(%client);
}
function valLaser(%client) {
    %plyr = %client.player;
    if(%client.laserMode == 1 && isPlayer(%plyr)) {
    messageClient(%client,0,'\c2Laser set to : Full-On.');
    %p = new TargetProjectile(){
                        dataBlock        = "AimingLaser" @ %client.lasercolor;
                        initialDirection = %plyr.getMuzzleVector($WeaponSlot);
                        initialPosition  = %plyr.getMuzzlePoint($WeaponSlot);
                        sourceObject     = %plyr;
                        sourceSlot       = $WeaponSlot;
                        vehicleObject    = %vehicle;
                     };
   MissionCleanup.add(%p);
   %plyr.attachedLaser = %p;
   %plyr.laserActive = 1;
   }
   else if(%client.laserMode == 2) {
    messageClient(%client,0,'\c2Laser set to : Auto.');
      if(isObject(%plyr.attachedLaser))
      %plyr.attachedLaser.delete();
      %plyr.laserActive = 0;
     return;
   }
   else {
    messageClient(%client,0,'\c2Laser set to : Off.');
   if(isObject(%plyr.attachedLaser))
      %plyr.attachedLaser.delete();
      %plyr.laserActive = 0;
   }
}


datablock TargetProjectileData(DeathBeam)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "0.0 0.9 0.0";

   startBeamWidth			= 0.04;
   pulseBeamWidth 	   = 0.04;
   beamFlareAngle 	   = 3.0;
   minFlareSize        	= 0.0;
   maxFlareSize        	= 200.0;
   pulseSpeed          	= 6.0;
   pulseLength         	= 0.150;

   textureName[0]      	= "special/nonlingradient";
   textureName[1]      	= "special/flare";
   textureName[2]      	= "special/pulse";
   textureName[3]      	= "skins/glow_red";
   beacon               = true;
};