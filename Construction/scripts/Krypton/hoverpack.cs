//Taken from Quantium for use in Krypton Construct.


// orgianly BadShots but converted to with a key

datablock StaticShapeData(PlayerPlatform) : StaticShapeDamageProfile
{
   shapeFile = "Pmiscf.dts";
   isInvincible = true;
};

function CreateHoverPackData(%obj)
{
   %obj.platform = new StaticShape()
   {
      dataBlock = PlayerPlatform;
      Position = "0 0 -1000";
      Scale = "0.3 0.3 0.1";
   };
   %obj.platform.startfade(200,0,true);
   %obj.platform.setTransform("0 0 -1000 0 0 0 1");
   %obj.platform.team = 0;
   if(%obj.platform.getTarget() != -1)
      setTargetSensorGroup(%obj.platform.getTarget(), %obj.team);
   MissionCleanup.add(%obj.platform);

 }
function KillHoverPackData(%obj)
{
   %obj.setMoveState(false);
   if (isObject(%obj.platform))
      %obj.platform.schedule(100, "delete");
}
function hoverPackOn(%obj)
{
   if(%obj.client.player.isFrozen)
      return;
   if (%obj.on != 0)
     return;
   //%obj.setMoveState(true);
   %obj.client.player.setVelocity("0 0 1");
CreateHoverPackData(%obj);
   %obj.platform.setPosition(VectorAdd(%obj.client.player.getPosition(),"0 0 -2"));

   %obj.client.player.applyImpulse(%obj.client.player.getPosition(),VectorScale("0 0 1",2000));
schedule(32,0,updateHover,%obj);
     %obj.on = 1;
}

function updateHover(%obj)
{
if (isObject(%obj.client.player)) {
%px = getWord(%obj.client.player.getPosition(),0);
%py = getWord(%obj.client.player.getPosition(),1);
%pz = getWord(%obj.platform.getPosition(),2);
%playaz = getWord(%obj.client.player.getPosition(),2);
if (mAbs(%pz - %playaz) > 5) {
%obj.platform.setPosition(%px SPC %py SPC (%playaz-1.5));
} else {
%obj.platform.setPosition(%px SPC %py SPC %pz);
}
%lateralSpeed = VectorLen(getWords(%obj.client.player.getVelocity(),0,1) SPC 0);
%thesize = (%lateralSpeed/(1000/32))*5;
if (%thesize < 0.7)
%thesize = 0.7;
%obj.platform.setRealSize(%thesize SPC %thesize SPC 0.1);
//divide by cycles per second and multiply by two.
//Thanks Electricutioner!!
if (%obj.on == 1)
schedule(32,0,updateHover,%obj);
} else {
hoverPackOff(%obj);
}
}


function hoverPackOff(%obj)
{
   if(%obj.client.player.isFrozen)
      return;

   %obj.on = "";
     %obj.setMoveState(false);
   if (isObject(%obj.platform))
      %obj.platform.setTransform("0 0 -1000 0 0 0 1");
   KillHoverPackData(%obj);
}