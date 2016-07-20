//--------------------------------------------------------------------------
//
// Chaingun barrel pack
//
//--------------------------------------------------------------------------

datablock ShapeBaseImageData(ChaingunBarrelPackImage)
{
mass = 15;

shapeFile = "pack_barrel_fusion.dts";
item = ChaingunBarrelPack;
mountPoint = 1;
offset = "0 0 0";
turretBarrel = "ChaingunBarrelLarge";

stateName[0] = "Idle";
stateTransitionOnTriggerDown[0] = "Activate";

stateName[1] = "Activate";
stateScript[1] = "onActivate";
stateTransitionOnTriggerUp[1] = "Deactivate";

stateName[2] = "Deactivate";
stateScript[2] = "onDeactivate";
stateTransitionOnTimeOut[2] = "Idle";

isLarge = true;
};

datablock ItemData(ChaingunBarrelPack)
{
className = Pack;
catagory = "Packs";
shapeFile = "pack_barrel_fusion.dts";
mass = 1;
elasticity = 0.2;
friction = 0.6;
pickupRadius = 2;
rotate = true;
image = "ChaingunBarrelPackImage";
pickUpName = "a heavy chaingun barrel pack";

computeCRC = true;

};

function ChaingunBarrelPackImage::onActivate(%data, %obj, %slot)
{
checkTurretMount(%data, %obj, %slot); // This cheks if there is a turret where the player is aiming.
}

function ChaingunBarrelPackImage::onDeactivate(%data, %obj, %slot)
{
%obj.setImageTrigger($BackpackSlot, false);
}

function ChaingunBarrelPack::onPickup(%this, %obj, %shape, %amount)
{
// created to prevent console errors
}
