//The Code Is Quite Messy.

//Datablocks
datablock StaticShapeData(DeployedEditorPack) : StaticShapeDamageProfile {
    className = "EditorPack";
    shapeFile = "stackable2s.dts";
    maxDamage = 2.0;
    destroyedLevel = 2.0;
    disabledLevel = 2.0;
    mass = 1.2;
    elasticity = 0.1;
    friction = 0.9;
    collideable = 1;
    pickupRadius = 1;
    sticky=false;

    explosion      = HandGrenadeExplosion;
    expDmgRadius = 1.0;
    expDamage = 0.1;
    expImpulse = 200.0;
    dynamicType = $TypeMasks::StaticShapeObjectType;
    deployedObject = true;
    cmdCategory = "DSupport";
    cmdIcon = CMDSensorIcon;
    cmdMiniIconName = "commander/MiniIcons/com_deploymotionsensor";

    targetNameTag = 'Editor';
    targetTypeTag = 'Pack';
    deployAmbientThread = true;
    debrisShapeName = "debris_generic_small.dts";
    debris = DeployableDebris;
    heatSignature = 0;
   	needsPower = true;
};

datablock ShapeBaseImageData(EditorPackDeployableImage) {
    mass = 10;
    emap = true;
    shapeFile = "stackable1s.dts";
    item = EditorPackDeployable;
    mountPoint = 1;
    offset = "0 0 0";
    deployed = DeployedEditorPack;
    heatSignature = 0;
    collideable = 1;
    stateName[0] = "Idle";
    stateTransitionOnTriggerDown[0] = "Activate";

    stateName[1] = "Activate";
    stateScript[1] = "onActivate";
    stateTransitionOnTriggerUp[1] = "Idle";

    isLarge = true;
    maxDepSlope = 360;
    deploySound = ItemPickupSound;

    minDeployDis = 0.5;
    maxDeployDis = 5.0;
};

datablock ItemData(EditorPackDeployable) {
    className = Pack;
    catagory = "Deployables";
    shapeFile = "stackable1s.dts";
    mass = 5.0;
    elasticity = 0.2;
    friction = 0.6;
    pickupRadius = 1;
    rotate = true;
    image = "EditorPackDeployableImage";
    pickUpName = "an editor pack";
    heatSignature = 0;
    emap = true;
};

//Code
$TeamDeployableMax[EditorPackDeployable]      = 9999; //Change To Your Desire

function EditorPackDeployableImage::onDeploy(%item, %plyr, %slot) {
    %className = "StaticShape";

    %playerVector = vectorNormalize(getWord(%plyr.getEyeVector(),1) SPC -1 * getWord(%plyr.getEyeVector(),0) SPC "0");
    %item.surfaceNrm2 = %playerVector;

    if (vAbs(floorVec(%item.surfaceNrm,100)) $= "0 0 1")
        %item.surfaceNrm2 = %playerVector;
    else
        %item.surfaceNrm2 = vectorNormalize(vectorCross(%item.surfaceNrm,"0 0 -1"));

    %rot = fullRot(%item.surfaceNrm,%item.surfaceNrm2);

    %deplObj = new (%className)() {
        dataBlock = %item.deployed;
        scale = "1 1 1";
    };

    // set orientation
    %deplObj.setTransform(%item.surfacePt SPC %rot);
    %deplObj.deploy();
    // set team, owner, and handle
    %deplObj.team = %plyr.client.Team;
    %deplObj.setOwner(%plyr);
    %deplObj.paded=1;
    //Msg Client
    messageclient(%plyr.client, 'MsgClient', "\c2Type /editor CMDs for a list of Editor Pack CMDs.");
    // set the sensor group if it needs one
    if (%deplObj.getTarget() != -1)
        setTargetSensorGroup(%deplObj.getTarget(), %plyr.client.team);

    // place the deployable in the MissionCleanup/Deployables group (AI reasons)
    addToDeployGroup(%deplObj);
    
    //
    %deplObj.powerFreq = %plyr.powerFreq;
   	checkPowerObject(%deplObj);

    //let the AI know as well...
    AIDeployObject(%plyr.client, %deplObj);

    // play the deploy sound
    serverPlay3D(%item.deploySound, %deplObj.getTransform());

    // increment the team count for this deployed object
    $TeamDeployedCount[%plyr.team, %item.item]++;

    addDSurface(%item.surface,%deplObj);

    %deplObj.playThread($PowerThread,"Power");
    %deplObj.playThread($AmbientThread,"ambient");

    // take the deployable off the player's back and out of inventory
    %plyr.unmountImage(%slot);
    %plyr.decInventory(%item.item, 1);
    //Do Some Stuff
    %deplobj.slotcount = 1; //For /editor addobj

    return %deplObj;
}

function EditorPack::onCollision(%data,%obj,%col) {
// created to prevent console errors
}

function EditorPackDeployable::onPickup(%this, %obj, %shape, %amount) {
// created to prevent console errors
}

function EditorPackDeployableImage::onMount(%data, %obj, %node) {
displayPowerFreq(%obj);
}

function EditorPackDeployableImage::onUnmount(%data, %obj, %node) {
// created to prevent console errors
}

function DeployedEditorPack::gainPower(%data, %obj)
{
PerformCloak(%obj);
PerformFade(%obj);
PerformScale(%obj);
PerformHide(%obj);
PerformName(%obj);
%obj.edited = false;
}

function DeployedEditorPack::losePower(%data, %obj)
{
EditorPerform(%obj);
%obj.edited = true;
}

function DeployedEditorPack::OnDestroyed(%data, %obj)
{
if (%obj.edited)
EditorPerform(%obj);
}

function EditorPerform(%obj)
{
PerformCloak(%obj);
PerformFade(%obj);
PerformScale(%obj);
PerformHide(%obj);
PerformName(%obj);
}


function DeployedEditorPack::onDestroyed(%this, %obj, %prevState) {
//    dismountPlayers();
    if (%obj.isRemoved)
        return;
    %obj.isRemoved = true;
    Parent::onDestroyed(%this, %obj, %prevState);
    $TeamDeployedCount[%obj.team, bogypackDeployable]--;
    remDSurface(%obj);
       %obj.schedule(500, "delete");
       fireBallExplode(%obj,1);
}

function ccEditor(%sender,%args)
{
%f = getword(%args,0);
%f = StrLwr(%f);

   %pos        = %sender.player.getMuzzlePoint($WeaponSlot);
   %vec        = %sender.player.getMuzzleVector($WeaponSlot);
   %targetpos  = vectoradd(%pos,vectorscale(%vec,100));
   %obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
   %obj        = getword(%obj,0);
   %className  = %classname;
   
   if (EvaluateFunction(%f) == false)
   {
   messageclient(%sender,'msgclient',"\c2Unknown command: \'"@%f@"\'.");
   return;
   }
   else if (%f $="help" || %f $="cmds")
   {
   messageclient(%sender,'msgclient',"\c2/editor select - Selects the editor you\'re looking at.");
   messageclient(%sender,'msgclient',"\c2/editor addobj - Adds the object you\'re looking at to the editor you\'ve selected.");
   messageclient(%sender,'msgclient',"\c2/editor cloak - Adds the object you\'re looking at to you\'r editor\'s cloak list.");
   messageclient(%sender,'msgclient',"\c2/editor fade - Adds the object you\'re looking at to you\'r editor\'s fade list.");
   messageclient(%sender,'msgclient',"\c2/editor name <New Name> - Adds the object you\'re looking at to you\'r editor\'s fade list.");
   messageclient(%sender,'msgclient',"\c2/editor scale <New Scale> - Adds the object you\'re looking at to you\'r editor\'s scale list.");
   messageclient(%sender,'msgclient',"\c2/editor hide - Adds the object you\'re looking at to you\'r editor\'s fade list..");
   return;
   }
   else if (%f $="")
   {
   messageclient(%sender,'msgclient',"\c2No command specified, please use \'/editor help\'.");
   return;
   }
   else if (%f $="delobj")
   {
   ProcessDelObj(%sender,%obj,%args);
   return;
   }
   else if (%f $="addobj")
   {
   ProcessAddObj(%sender,%obj);
   return;
   }
   else if (!%obj){
   messageclient(%sender,'msgclient',"\c2Unable to find an object.");
   return;
   }
   else if (%f $="getid" && %obj.owner $=%sender)
   {
   messageclient(%sender,'msgclient',"\c2The ID of this object is \'"@%obj@"\'.");
   return;
   }
   else if (%f $="fade")
   {
   ProcessFade(%sender,%obj,%args);
   return;
   }
   else if (%f $="cloak")
   {
   ProcessCloak(%sender,%obj,%args);
   return;
   }
   else if (%f $="scale")
   {
   ProcessScale(%sender,%obj,%args);
   return;
   }
   else if (%f $="name")
   {
   ProcessName(%sender,%obj,%args);
   return;
   }
   else if (%f $="hide")
   {
   ProcessHide(%sender,%obj,%args);
   return;
   }
   else
   {
   %dataBlock  = %obj.getDataBlock(); //To Prevent Errors
   if (!IsMatch(%obj.getdatablock().getname(),"DeployedEditorPack")){
   messageclient(%sender,'msgclient',"\c2Not an editor pack!");
   return;
   }
   ProcessEditorRequest(%sender,%obj,%f);
   }
   }
   
   function ProcessEditorRequest(%sender,%obj,%f)
   {
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2This editor pack is not yours!");
   return;
   }
   if (%f $="select" || %f $="selecteditor" || %f = "seledit")
   {
   %sender.currentedit = %obj;
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   messageclient(%sender,'msgclient',"\c2Current editor set.");
   return;
   }
   }
   
   function ProcessAddObj(%sender,%obj)
   {
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2This object is not yours!");
   return;
   }
   if (!IsObject(%Sender.CurrentEdit))
   {
   messageclient(%sender,'msgclient',"\c2No editor selected! You can use \'/editor select\' while pointing at an editor to select it.");
   return;
   }
   if (%obj.getdatablock().getname() $= "DeployedEditorPack")
   {
   messageclient(%sender,'msgclient',"\c2Unable to edit editor packs!");
   return;
   }
   %Slots = %sender.currentedit.slotcount;
   %edit = %sender.currentedit;
   for (%i=0;%i<%slots;%i++)
   {
   if (%edit.slot[%i] $="")
   {
   %edit.slot[%i] = %obj;
   %edit.slotcount++;
   messageclient(%sender,'msgclient',"\c2Object has been added to your current editor. ("@%i@")");
   %obj.slot = %i;
   %obj.editor = %edit;
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   return;
   }
   }
   }
   
   function ProcessDelObj(%sender,%obj,%args)
   {
   %edit = %sender.currentedit;
   
   if (!IsObject(%edit))
   {
   messageclient(%sender,'msgclient',"\c2No editor selected!");
   return;
   }
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2Not your object!");
   return;
   }
   if (%obj.slot $="A" || %obj.editor $="")
   {
   messageclient(%sender,'msgclient',"\c2This object is not bound to an editor!");
   return;
   }
   messageclient(%sender,'msgclient',"\c2Object deleted from your current editor.");
   %edit.slot[%obj.slot] = "";
   %obj.slot = "A";
   %obj.editor = "";
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   if (%edit.slotcount > 1)
   %edit.slotcount--;
   }
   
   function ProcessScale(%sender,%obj,%args)
   {
   %scale = getwords(%args,1);
   %edit = %sender.currentedit;

   if (!IsObject(%edit))
   {
   messageclient(%sender,'msgclient',"\c2No editor selected!");
   return;
   }
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2Not your object!");
   return;
   }
   if (%obj.getdatablock().getname() $= "DeployedEditorPack")
   {
   messageclient(%sender,'msgclient',"\c2Unable to scale editor packs!");
   return;
   }
   if (%obj.slot $="A" || %obj.editor $="")
   {
   %obj.slot = "A";
   messageclient(%sender,'msgclient',"\c2This object is not bound to an editor!");
   return;
   }
   if (%scale $="" && %obj.istscale == false)
   {
   messageclient(%sender,'msgclient',"\c2No scale specified.");
   return;
   }
   if (getword(%scale,0) > $Editor::MaxScale["X"] && %obj.istscale == false)
   {
   messageclient(%sender,'msgclient',"\c2\'"@%scale@"\' is invalid. The X axis is too big. Max: "@$Editor::MaxScale["X"]@"");
   return;
   }
   if (getword(%scale,1) > $Editor::MaxScale["Y"] && %obj.istscale == false)
   {
   messageclient(%sender,'msgclient',"\c2\'"@%scale@"\' is invalid. The Y axis is too big. Max: "@$Editor::MaxScale["Y"]@"");
   return;
   }
   if (getword(%scale,2) > $Editor::MaxScale["Z"] && %obj.istscale == false)
   {
   messageclient(%sender,'msgclient',"\c2\'"@%scale@"\' is invalid. The Z axis is too big. Max: "@$Editor::MaxScale["Z"]@"");
   return;
   }
   if (getword(%scale,0) < $Editor::MinScale["X"] && %obj.istscale == false)
   {
   messageclient(%sender,'msgclient',"\c2\'"@%scale@"\' is invalid. The X axis is too small. Min: "@$Editor::MinScale["X"]@"");
   return;
   }
   if (getword(%scale,1) < $Editor::MinScale["Y"] && %obj.istscale == false)
   {
   messageclient(%sender,'msgclient',"\c2\'"@%scale@"\' is invalid. The Y axis is too small. Min: "@$Editor::MinScale["Y"]@"");
   return;
   }
   if (getword(%scale,2) < $Editor::MinScale["Z"] && %obj.istscale == false)
   {
   messageclient(%sender,'msgclient',"\c2\'"@%scale@"\' is invalid. The Z axis is too small. Min: "@$Editor::MinScale["Z"]@"");
   return;
   }
   %scale = CheckScale(%scale);
   if (!%obj.istscale)
   {
   %obj.istscale = true;
   %obj.isescaled = false;
   messageclient(%sender,'msgclient',"\c2Object has been added to your currently selected editor\'s scale list. This object will be scaled to \'"@%scale@"\'.");
   %obj.escale = %scale;
   %obj.oldscale = %obj.getscale();
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   return;
   }
   else
   {
   %obj.istscale = false;
   %obj.isescaled = false;
   messageclient(%sender,'msgclient',"\c2Object has been removed from your currently selected editor\'s scale list. Original scale restored.");
   %obj.setscale(%obj.oldscale);
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   }
   }
   
   function ProcessFade(%sender,%obj,%args)
   {
   %edit = %sender.currentedit;
   if (!isObject(%edit))
   {
   messageclient(%sender,'msgclient',"\c2No editor selected!");
   return;
   }
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2This object is not yours!");
   return;
   }
   if (%obj.getdatablock().getname() $= "DeployedEditorPack")
   {
   messageclient(%sender,'msgclient',"\c2Unable to fade editor packs!");
   return;
   }
   if (%obj.slot $="A" || %obj.editor $="")
   {
   messageclient(%sender,'msgclient',"\c2This object is not bound to an editor!");
   return;
   }
   if (!%obj.istfade)
   {
   %obj.istfade = true;
   %obj.isefaded = false;
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj, false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   messageclient(%sender,'msgclient',"\c2Object has been added to your currently selected editor\'s fade list.");
   return;
   }
   else
   {
   %obj.istfade = false;
   %obj.isefaded = false;
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   messageclient(%sender,'msgclient',"\c2Object has been removed from your currently selected editor\'s fade list.");
   }
   }
   
   function ProcessHide(%sender,%obj,%args)
   {
   %edit = %sender.currentedit;
   
   if (!isObject(%edit))
   {
   messageclient(%sender,'msgclient',"\c2No editor selected!");
   return;
   }
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2This object is not yours!");
   return;
   }
   if (%obj.getdatablock().getname() $= "DeployedEditorPack")
   {
   messageclient(%sender,'msgclient',"\c2Unable to fade editor packs!");
   return;
   }
   if (%obj.slot $="A" || %obj.editor $="")
   {
   messageclient(%sender,'msgclient',"\c2This object is not bound to an editor!");
   return;
   }
   if (!%obj.isthide)
   {
   %obj.isthide = true;
   %obj.isehidden = false;
   messageclient(%sender,'msgclient',"\c2Object has been added to your editor\'s hide list.");
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   return;
   }
   else
   {
   %obj.isthide = false;
   %obj.isehidden = false;
   messageclient(%sender,'msgclient',"\c2Object has been removed from your current editor\'s hide list.");
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   }
   }
   
   function ProcessCloak(%sender,%obj,%args)
   {
   %edit = %sender.currentedit;
   if (!isObject(%edit))
   {
   messageclient(%sender,'msgclient',"\c2No editor selected!");
   return;
   }
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2This object is not yours!");
   return;
   }
   if (%obj.getdatablock().getname() $= "DeployedEditorPack")
   {
   messageclient(%sender,'msgclient',"\c2Unable to cloak editor packs!");
   return;
   }
   if (%obj.slot $="A" || %obj.editor $="")
   {
   messageclient(%sender,'msgclient',"\c2This object is not bound to an editor!");
   return;
   }
   if (!%obj.istcloak)
   {
   %obj.istcloak = true;
   %obj.isecloaked = false;
   messageclient(%sender,'msgclient',"\c2Object has been added to your current editor\'s cloak list.");
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   return;
   }
   else
   {
   %obj.istcloak = false;
   %obj.isecloaked = false;
   messageclient(%sender,'msgclient',"\c2Object has been removed from your current editor\'s cloak list.");
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   }
   }
   
   function ProcessName(%sender,%obj,%args)
   {
   %edit = %sender.currentedit;
   %name = getwords(%args,1);
   if (!isObject(%edit))
   {
   messageclient(%sender,'msgclient',"\c2No editor selected!");
   return;
   }
   if (%obj.owner!=%sender && %obj.owner !$=""){
   messageclient(%sender,'msgclient',"\c2This object is not yours!");
   return;
   }
   if (%obj.getdatablock().getname() $= "DeployedEditorPack")
   {
   messageclient(%sender,'msgclient',"\c2Unable to name editor packs!");
   return;
   }
   if (%obj.slot $="A" || %obj.editor $="")
   {
   messageclient(%sender,'msgclient',"\c2This object is not bound to an editor!");
   return;
   }
   if (!%obj.istname)
   {
   %obj.istname = true;
   %obj.isenamed = false;
   %obj.oldname = %obj.nametag;
   %obj.ename = %name;
   messageclient(%sender,'msgclient',"\c2Object has been added to your current editor\'s name list. This object will be named to: "@%name@"");
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   return;
   }
   else
   {
   %obj.istname = false;
   %obj.isename = false;
   %obj.nametag = %obj.oldname;
   setTargetName(%obj.target,addTaggedString("\c6"@%obj.oldname@""));
   %obj.oldname = "";
   messageclient(%sender,'msgclient',"\c2Object has been removed from your current editor\'s cloak list. Old name restored.");
   %obj.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   }
   }

   //Performance Functions
   function PerformCloak(%obj)
   {
   if (!IsObject(%obj))
   return;
   %c = %obj.slotcount;
   %c++;
   for (%i=0;%i<%c;%i++)
   {
   %object = %obj.slot[%i];
   if (!IsObject(%Object))
   return;
   if (!%object.istcloak) //IsTFade = Is To Fade
   %object.isecloaked = true; //To Make The System Think It's Already Cloaked
   if (%object.isecloaked)
   {
   %object.setcloaked(false);
   %object.isecloaked = false;
   }
   else
   {
   schedule(510,0,"SetCloaked",%object,true); //Somehow Fixed The Cloaking Problem
   %object.isecloaked = true;
   }
   }
   }
   
   function setcloaked(%obj,%bool)
   {
   %obj.setcloaked(%bool);
   }
   
   function PerformFade(%obj)
   {
   if (!IsObject(%obj))
   return;
   %c = %obj.slotcount;
   %c = %c++;
   for (%i=0;%i<%c;%i++)
   {
   %object = %obj.slot[%i];
   if (!IsObject(%Object))
   return;
   if (!%object.istfade) //IsTFade = Is To Fade
   %object.isefaded = true; //To Make The System Think It's Already Faded
   if (%object.isefaded)
   {
   %object.startfade(1,0,0);
   %object.isefaded= false;
   }
   else
   {
   %object.setcloaked(true);
   schedule(500,0,"SetCloaked",%object,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   schedule(510,0,"fade",%object);
   %object.isefaded = true;
   }
   }
   }
   
   function fade(%obj)
   {
   %obj.startfade(1,0,1);
   }
   
   function PerformHide(%obj)
   {
   if (!IsObject(%obj))
   return;
   for (%i=0;%i<%obj.slotcount;%i++)
   {
   %object = %obj.slot[%i];
   if (!IsObject(%Object))
   return;
   if (!%object.isthide) //IsTFade = Is To Fade
   %object.isehidden = true; //To Make The System Think It's Already Hidden
   if (%object.isehidden)
   {
   %object.hide(0);
   %object.isehidden= false;
   }
   else
   {
   %object.setcloaked(true);
   schedule(500,0,"SetCloaked",%obj.slot[%slot],false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   schedule(510,0,"Hide",%object);
   %object.isehidden = true;
   }
   }
   }
   
   function Hide(%obj)
   {
   %obj.hide(1);
   }
   
   function PerformScale(%obj)
   {
   if (!IsObject(%obj))
   return;
   %c = %obj.slotcount;
   %c = %c++;
   for (%i=0;%i<%c;%i++)
   {
   %object = %obj.slot[%i];
   if (!IsObject(%Object))
   return;
   if (!%object.istscale){ //IsTFade = Is To Fade
   %object.isescaled = true; //To Make The System Think It's Already Scaled
   %object.oldscale = %object.getscale();
   }
   if (%object.isescaled)
   {
   %object.setscale(%object.oldscale);
   %object.setcloaked(true);
   schedule(500,0,"SetCloaked",%object,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   %object.isescaled= false;
   }
   else
   {
   %object.setscale(%object.escale);
   %object.setcloaked(true);
   schedule(500,0,"SetCloaked",%object,false); //Couldn't Get It To Work With schedule(1000,0,"SetCloaked",%obj,true);
   %object.isescaled = true;
   }
   }
   }

   function PerformName(%obj)
   {
   if (!IsObject(%obj))
   return;
   %c = %obj.slotcount;
   %c = %c++;
   for (%i=0;%i<%c;%i++)
   {
   %object = %obj.slot[%i];
   if (!IsObject(%Object))
   return;
   if (!%object.istname){ //IsTFade = Is To Fade
   %object.isenamed = true; //To Make The System Think It's Already Scaled
   if (%object.nametag !$="")
   %object.oldname = %object.nametag;
   else
   %object.oldname = %object.getdatablock().targetNameTag;
   }
   if (%object.isenamed)
   {
   %object.nametag = %obj.oldname;
   setTargetName(%object.target,addTaggedString("\c6"@%object.oldname@""));
   %object.isenamed = false;
   }
   else
   {
   %object.nametag = %object.ename;
   setTargetName(%object.target,addTaggedString("\c6"@%object.ename@""));
   %object.isenamed = true;
   }
   }
   }

   //Support Functions
   function EvaluateFunction(%f) //To Eval The Command
   {
   if (%f $="addobj" || %f $= "selecteditor" || %f $="selecteditor" || %f $="seledit" || %f $="select" || %f $="delobj" || %f $="help" || %f $="cmds" || %f $="fade" || %f $="cloak" || %f $="scale" || %f $="getid" || %f $="hide" || %f $="name")
   return true;
   else
   return false;
   }
   
   function IsMatch(%string1,%string2)
   {
   %string1 = StrLwr(%string1);
   %string2 = StrLwr(%string2);
   
   if (%string1 $=%string2)
   return true;
   else
   return false;
   }
   
   function CheckScale(%scale) //Evals The Scale For Any Missing Args, If So, Puts A 1 In The Blank Slot
   {
   if (getword(%scale,0) $="")
   %scale = "1" SPC getword(%scale,1) SPC getword(%scale,2);
   if (getword(%scale,1) $="")
   %scale = getword(%scale,0) SPC "1" SPC getword(%scale,2);
   if (getword(%scale,2) $="")
   %scale = getword(%scale,0) SPC getword(%scale,1) SPC "1";
   return %scale;
   }

   //Variables
   $Editor::MaxScale["X"] = 20;
   $Editor::MaxScale["Y"] = 20;
   $Editor::MaxScale["Z"] = 20;
   $Editor::MinScale["X"] = -20;
   $Editor::MinScale["Y"] = -20;
   $Editor::MinScale["Z"] = -20;

