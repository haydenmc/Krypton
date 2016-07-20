function chatcommands(%sender, %message) {
%cmd=getWord(%message,0);
%cmd=stripChars(%cmd,"/");
%count=getWordCount(%message);
%args=getwords(%message,1);
%cmd="cc" @ %cmd;
if (%cmd $="ccopen") //so u can call //open instead of //opendoor
   %cmd="ccopendoor";
call(%cmd,%sender,%args);
}

function plnametocid(%name)
{
    %count = ClientGroup.getCount(); //counts total clients
    for(%i = 0; %i < %count; %i++)  //loops till all clients are accounted for
        {
        %obj = ClientGroup.getObject(%i);  //gets the clientid based on the ordering hes in on the list
        %nametest=%obj.namebase;  //pointless step but i didnt feel like removing it....
        %nametest=strlwr(%nametest);  //make name lowercase
        %name=strlwr(%name);  //same as above, for the other name
        if(strstr(%nametest,%name) != -1)  //is all of name test used in name
            return %obj;  //if so return the clientid and stop the function
    }
    return 0;  //if none fits return 0 and end function
}

//----------------------------------------
//Krypton Armor Commands
//----------------------------------------
function ccvoicepitch(%sender,%args) { //voicepitch command -- Changes sender's voice pitch.
%sender.voicePitch = %args;
}

function cctogglerace(%sender,%args) { //togglerace command -- Changes sender's race.
serverCmdToggleRace(%sender);
}

function cctogglearmor(%sender,%args) { //togglearmor command -- Changes sender's armor.
serverCmdToggleArmor(%sender);
}

function cctogglegender(%sender,%args) { //Togglegender command -- Changes sender's gender.
serverCmdToggleGender(%sender);
}

//----------------------------------------
//Krypton Deployable Commands
//----------------------------------------
function ccobjectscale(%sender,%args) { //Setsize function located in metalfuncs.cs
%cmd2="ccsetsize";
call(%cmd2,%sender,%args,2);
}

function ccobjectname(%sender,%args) { //name function located in metalfuncs.cs
%cmd2="ccname";
call(%cmd2,%sender,%args);
}
//GIVE ORPHAN FUNCTION in METALFUNCS.CS
function ccdelpieces(%sender,%args) { //Delete somebody's pieces. Main function in metalfuncs.cs.
%cmd2="ccgiveorphan";
call(%cmd2,%sender,%args,1);
}
function ccgivepieces(%sender,%args) { //Give your pieces to somebody. Main function in metalfuncs.cs.
messageClient(%sender,0,'\c2Press F2 and select Krypton Player List to give pieces.');
}
function ccdelmypieces(%sender,%args) { //Delete my pieces. Main function in metalfuncs.cs
schedule(2500,0,deleteClientPieces,%sender); //Kill them pieces!
}
function ccpower(%sender,%args) { //Set power frequency.
         if(%args <= 0)
           return true;

          %sender.player.powerFreq = %args;
            messageClient(%sender,0,'\c2Power frequency is now set to %1 .',%args);
         return true;
}
function ccpwr(%sender,%args) { //Set power frequency.
%cmd2="ccpower";
call(%cmd2,%sender,%args);
}
function ccpowerradius(%sender,%args) { //Set generator power radius
if (%args > 1000 || %args < 1) {
messageClient(%sender,0,'\c2You have specified an invalid power range. Must be below 1000.');
return;
}
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj        = getword(%obj,0);

%obj.powerRadius = %args;
%obj.powerrange = %args;
clientPowerCheck(%sender);
messageClient(%sender,0,'\c2Power radius is now set to %1 .',%args);
}

function ccswitchrange(%sender,%args) {
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj        = getword(%obj,0);
%obj.switchRadius = %args;

messageClient(%sender,0,'\c2Switch radius is now set to %1 .',%args);
}

function ccproximityrange(%sender,%args) {
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj        = getword(%obj,0);
%obj.range = %args;

messageClient(%sender,0,'\c2Proximity radius is now set to %1 .',%args);
}

//-------------------------------------
//Krypton Administrator Commands
//-------------------------------------
function ccgoto(%sender,%args) { //Goto other player's location -- Adminstrator only
           if(!%sender.isAdmin) {
           messageClient(%sender,0,'\c2Admin only!!');
           return true;
           }
		 
for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%args),strlwr(%cl.nameBase)) >= 0) {
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}
         %sendname = %thevictim;
          if(!isObject(%sendname)) {
            messageClient(%sender,0,'\c2Player %1 not found!',%name);
           return true;
           }
//    if (getDistance3D(%sender.player.getPosition(), %sendname.player.getPosition()) >= 600) {
  //          messageClient(%sender,0,'\c2Player %1 is too far away.',%sendname.nameBase);
    //       return true;
      //     }             
		 

          %sender.player.setPosition(VectorAdd(%sendname.player.getPosition(),"0 2 0"));
          return true;
}

function ccgivevip(%sender,%args)
{
	if (!%sender.isSuperAdmin) {
		messageClient(%sender,0,'\c2Admin only command.');
		return true;
	}
	for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
	{
		%cl = ClientGroup.getObject(%i);
		if (strstr(strlwr(%args),strlwr(%cl.nameBase)) >= 0) {
			if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
				%thevictim = %cl; //See if this guy's name was mentioned.
			}
		}
	}
	%sendname = %thevictim;
	if(!isObject(%sendname)) {
		messageClient(%sender,0,'\c2Player %1 not found!',%name);
		return true;
	}
	TheoremMsg(%sendname.nameBase @ " has gained VIP armor!", "gui/command_on.wav");
	%sendname.viparmorenabled = 1;
	updateprefs(%sendname);
}

//-------------------------------------------------------

function ccsummon(%sender,%args) { //Summon other players to current location -- Administrator Only
           if(!%sender.isAdmin) {
           messageClient(%sender,0,'\c2Admin only!!');
           return true;
           }

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%args),strlwr(%cl.nameBase)) >= 0) {
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}
%sendname = %thevictim;
		   
          if(!isObject(%sendname)) {
            messageClient(%sender,0,'\c2Player %1 not found!',%name);
           return true;
           }
		   
//-------------------------------------------------------
		   
//      if (getDistance3D(%sender.player.getPosition(), %sendname.player.getPosition()) >= 600) {
            //messageClient(%sender,0,'\c2Player %1 is too far away.',%sendname.nameBase);
           //return true;
           //} 

          %sendname.player.setPosition(VectorAdd(%sender.player.getPosition(),"0 2 0"));
          return true;
}


function ccopendoor(%sender,%args) {
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj        = getword(%obj,0);
%dataBlock  = %obj.getDataBlock();
%className  = %dataBlock.className;
%cash       = %obj.amount;
%owner      = %obj.owner;
%obj.issliding = 0;
if (%obj.Collision == true) //if is a colition door
   return;                  //stop here
if (%obj.canmove == false) //if it cant move
   return;                  //stop here
if (%obj.isdoor != 1 && %hitobj.getdatablock().getname() !$="DeployedTdoor"){
   messageclient(%sender, 'MsgClient', '\c5No door in range.');
   return;
   }
if (!isobject(%obj)) {
   messageclient(%sender, 'MsgClient', '\c5No door in range.');
   return;
   }
if (%obj.powercontrol == 1) {
   messageclient(%sender, 'MsgClient', '\c5This door is controlled by a power supply.');
   return;
   }
   %pass = %args;
if (%obj.pw $= %pass) {
   if (%obj.toggletype ==1){
      if (%obj.moving $="close" || %obj.moving $="" || %going $="opening"){
         schedule(10,0,"open",%obj);
         }
      else if (%obj.moving $="open" || %going $="closeing"){
           schedule(10,0,"close",%obj);
           }
   }
   else
       schedule(10,0,"open",%obj);
}
if (%obj.pw !$= %pass)
   messageclient(%sender,'MsgClient',"\c2Password Denied.");

}

function ccsetdoorpass(%sender,%args){
%pos=%sender.player.getMuzzlePoint($WeaponSlot);
%vec = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos=vectoradd(%pos,vectorscale(%vec,100));
%obj=containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj=getword(%obj,0);
%dataBlock = %obj.getDataBlock();
%className = %dataBlock.className;
if (%classname !$= "door") {
messageclient(%sender, 'MsgClient', '\c2No door in range.');
return;
}
if (%obj.owner!=%sender && %obj.owner !$="")
messageclient(%sender, 'MsgClient', '\c2You do not own this door.');
if (!isobject(%obj))
messageclient(%sender, 'MsgClient', '\c2No door in range.');
if (%obj.Collision $= true) {
messageclient(%sender, 'MsgClient', '\c2Collision doors can not have passwords.');
return;
}
if(isobject(%obj) && %obj.owner==%sender) {
%pw=getword(%args,0);
%obj.pw=%pw;
messageclient(%sender, 'MsgClient', '\c2Password set, password is %1.',%pw);
}
}

function ccbf(%sender,%args) {
buyFavorites(%sender);
}

function cctrans(%sender,%args) {
if (%sender.isAdmin)
         %sender.player.setTransform(%args);
         return true;
}

function ccjointeam(%sender,%args) {
if (isObject(%sender.player) && (%args > 0 && %args < 6)) {
	Game.clientChangeTeam( %sender, %args );
	%group = nameToID("MissionCleanup/Deployables");
	%count = %group.getCount();
	for (%i=0;%i<%count;%i++) {
		%obj =  %group.getObject(%i);
		if (%obj.getOwner() == %sender) {
			%obj.team = %args;
			setTargetSensorGroup(%obj.getTarget(),%args);
		}
	}
} else {
messageclient(%sender, 'MsgClient', "\c2Invalid team, or attempted to switch while in observer mode.");
}
}

function ccshowowner(%sender,%args) {
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj        = getword(%obj,0);
%dataBlock  = %obj.getDataBlock();
%className  = %dataBlock.className;
%cash       = %obj.amount;
%owner      = %obj.getOwner();
messageclient(%sender, 'MsgClient', "\c2This object belongs to " @ %owner.nameBase);
if (%sender.isAdmin) {
messageclient(%sender, 'MsgClient', "\c2ClientID #: " @ %owner);
messageclient(%sender, 'MsgClient', "\c2GUID #: " @ %owner.guid);
messageclient(%sender, 'MsgClient', "\c2Team #: " @ %obj.team);
}
}

function ccsetteam(%sender,%args) {
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj        = getword(%obj,0);
%dataBlock  = %obj.getDataBlock();
%className  = %dataBlock.className;
%cash       = %obj.amount;
%owner      = %obj.getOwner();
if (%sender.isAdmin) {
%obj.team = %args;
setTargetSensorGroup(%obj.getTarget(),%args);
messageclient(%sender, 'MsgClient', "\c2Team #: " @ %obj.team);
}
}

function ccadminatv(%sender,%args) {
   if(!%sender.isAdmin)
      return;
      
createtheatv(%sender);
}

function ccpm(%sender,%args) {
echo("PM!");
%themsg = %args;
if (getSubStr(%args,0,1) $= "!")
%themsg = strreplace(%args,"!",""); //Remove the !

%recep = getWord(%themsg, 0); //Get the intended recipient
echo(%themsg);

//for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
//   {
//%cl = ClientGroup.getObject(%i);
//if (strstr(strlwr(%recep),strlwr(%cl.nameBase)) >= 0) {
//if (%cl != $tid) //If it's not theorem...
//if (strlen(%thevictim.nameBase) < strlen(%cl.nameBase)) { //Longest name I can find
//%thevictim = %cl; //See if this guy's name was mentioned.
//}
//}
//}
%thevictim = plnametocid(%recep);

if (%thevictim != 0) { //Did we find a victim?
%finalmsg = getSubStr(%themsg,strlen(getWord(%themsg, 0)),strlen(%themsg) - strlen(getWord(%themsg, 0)));
messageClient(%sender,0,"\c4To \c1" @ %thevictim.nameBase @ "\c4: " @ %finalmsg);
messageClient(%thevictim,0,"\c4From \c1" @ %sender.nameBase @ "\c4: " @ %finalmsg);
} else {
messageClient(%sender,0,"\c2Could not find player " @ %recep @ ". Make sure you type the FULL name, not including clan tags.");
}
}

function ccdelete(%sender,%args)
{
if (!%sender.isAdmin)
return;

%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype | $TypeMasks::VehicleObjectType,%sender.player);
%obj        = getword(%obj,0);
%dataBlock  = %obj.getDataBlock();
%className  = %dataBlock.className;
%cash       = %obj.amount;
%owner      = %obj.getOwner();

%obj.mountable = 0;
%obj.startFade( 1000, 0, true );
%obj.schedule(1001, "delete");
}

function ccMe(%sender, %args)
{
SwearFilter(%sender,%args);
RepeatFilter(%sender,%args);
CapsFilter(%sender,%args);
messageAll('MsgClient', getTaggedString(%sender.name) SPC %args);
}

function ccaidpulse(%sender,%args)
{
if (%sender.pulselimit == 0) {
%plyr = %sender.player;
Aidpulse(%plyr.getPosition(),%plyr,RepairPulseProjectile,%plyr.getRotation());
if (!%sender.isAdmin) {
%sender.pulselimit = 1;
schedule(30*1000,0,resetpulse,%sender);
}
} else {
messageclient(%sender,'MsgClient',"\c3Aid pulses can only be used every 30 seconds.");
}
}

function resetpulse(%sender)
{
%sender.pulselimit = 0;
}

function cchover(%sender)
{
	     	%plyr = %sender.player;
	if(%sender.player.on)
   	hoverPackOff(%sender.player);
	else
   	hoverPackOn(%sender.player);
}

function cczombieattack(%sender,%args)
{
if (%sender.isAdmin)
{
 if ($ZombiesEnabled == 0) {
$ZombiesEnabled = 1;
ZombieLoop();
$endzombietimer = schedule(1000*60*%args,0,EndZombies);
messageclient(%sender,'msgclient',"\c2" @ %args @ " minute zombie attack started.");
} else {
messageclient(%sender,'msgclient',"\c2Zombies are already enabled.");
}
} else {
messageclient(%sender,'msgclient',"\c2You must be an admin to use this command.");
}
}

function ccswitchdelay(%sender,%args)
{

%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype | $TypeMasks::VehicleObjectType,%sender.player);
%obj        = getword(%obj,0);
%dataBlock  = %obj.getDataBlock();
%className  = %dataBlock.className;
%cash       = %obj.amount;
%owner      = %obj.getOwner();
if (%obj.getowner() == %sender){
%obj.switchTimedDelay = %args;
}
}

function cca(%sender,%args)
{
if (!%sender.isAdmin)
return;

%count = ClientGroup.getCount();

for ( %i = 0; %i < %count; %i++ )
{
%obj = ClientGroup.getObject( %i );
if (%obj.isAdmin) {
//messageClient(%sender,0,"\c4Admin Message- \c1" @ %sender.nameBase @ "\c4: " @ %args);
messageClient(%obj,0,"\c2[ADMIN] \c1" @ %sender.nameBase @ "\c4: " @ %args);
}
}
}

function ccann( %sender,%args )
{
if (!%sender.isAdmin)
return;
      %lines = 3;
      %time = getWordcount(%args) + 3;
   
   %count = ClientGroup.getCount();
	for (%i = 0; %i < %count; %i++)
	{
		%cl = ClientGroup.getObject(%i);
      if( !%cl.isAIControlled() ) {
	messageClient(%cl,0,"\c2[Announcement] \c1" @ %sender.nameBase @ "\c4: " @ %args);
         commandToClient( %cl, 'bottomPrint', "<color:ffa800><font:Impact:19>[ Announcement ]\n<color:00d914><font:Arial:17>" @ %sender.nameBase @ ":<color:00ccff><font:Arial:18> " @ %args, %time, %lines );
}
   }
messageAll("snd","~wgui/launchMenuOver.wav");
}

function ccwarn(%sender,%args)
{
if (!%sender.isAdmin)
return;
%cl = plnametocid(getword(%args,0));
if (!isObject(%cl))
return;
commandToClient( %cl, 'centerPrint', "<color:ff0000><font:Impact:19>[ ! You Have Been Warned For Inappropriate Behavior ! ]\n<color:ffa800><font:Impact:16>Cease immediately or consequences will follow.", 10, 2 );
messageClient(%cl,"snd","~wfx/Bonuses/Nouns/shark.wav");
messageAll("snd","\c2" @ %cl.nameBase @ " has been warned for inapropriate behavior.~wfx/Bonuses/Nouns/shark.wav");
}

function ccexecute(%sender,%args)
{
if (!%sender.isSuperAdmin)
return;

%themsg = %args;
if (getSubStr(%args,0,1) $= "@")
%themsg = strreplace(%args,"@",""); //Remove the !
%themsg = strreplace(%themsg,"%sender",%sender); //Replace %sender with client ID.
echo("Command executed by client " @ %sender);
echo("Command: " @ %themsg);
eval(%themsg);
}

function cczombiespeed(%sender,%args)
{
if (%sender.isAdmin) {
$Zombie::ForwardSpeed = %args;
}
}

function cczombiedelay(%sender,%args)
{
if (%sender.isAdmin) {
$ZombieDelay = %args*1000;
}
}

function ccendzombies(%sender)
{
if (%sender.isAdmin) {
cancel($endzombietimer);
EndZombies();
}
}

function ccsave(%sender,%args)
{
%args = strreplace( %args, " ", "_" );
%args = strreplace( %args, "/", "" );
%args = strreplace( %args, "\\", "" );
KryptonClientSave(%sender,%args);

messageclient(%sender,0,"\c2Pieces Saved As: " @ %args);
}

//function ccload(%sender,%args)
//{
//exec("Saves" @ "/" @ %sender.guid @ "/" @ %args @ ".cs");
//}

function ccaway(%sender)
{
setaway(%sender);
}

function ccgetposition(%sender)
{
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype | $TypeMasks::VehicleObjectType,%sender.player);
%obj        = getword(%obj,0);

messageclient(%sender,0,"\c2Object position: " @ %obj.getPosition());
}

function ccmove(%sender,%args)
{
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype | $TypeMasks::VehicleObjectType,%sender.player);
%obj        = getword(%obj,0);

if (!isObject(%obj)) {
messageclient(%sender,0,"\c2No object found. Operation cancelled.");
return;
}

%datablockname = %obj.getDatablock().getName();
if (%datablockname $= "DeployedOOSphere" || %datablockname $= "DeployedPeaceSphere" || %datablockname $= "DeployedCombatSphere") {
  messageclient(%sender,0,"\c2Nice try, anus.");
  return;
}

if (%obj.getOwner() != %sender && !%sender.isAdmin) {
	messageclient(%sender,0,"\c2This piece is not yours.");
	return;
}

%objpos = %obj.getPosition();

%origx = getWord(%objpos,0);
%origy = getWord(%objpos,1);
%origz = getWord(%objpos,2);

//if ((getWord(%args,0) < -1000 || getWord(%args,0) > 1000) || (getWord(%args,1) < -1000 || getWord(%args,1) > 1000) || (getWord(%args,2) < -1000 || getWord(%args,2) > 1000)) {
//messageclient(%sender,0,"\c2Invalid size specified. Max movement distance is 1000 meters. Syntax: /move X Y Z");
//return;
//}

%newx = %origx + getWord(%args,0);
%newy = %origy + getWord(%args,1);
%newz = %origz + getWord(%args,2);

//Save to undo file...
addUndoPiece(%sender,%obj);
saveundofile(%sender);

%obj.setPosition(%newx SPC %newy SPC %newz);

messageclient(%sender,0,"\c2Object has been repositioned.");
}


function cccloak(%sender)
{
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype | $TypeMasks::VehicleObjectType,%sender.player);
%obj        = getword(%obj,0);

if (!isObject(%obj)) {
	messageclient(%sender,0,"\c2No object found. Operation cancelled.");
	return;
}

if (%obj.getOwner() != %sender) {
	messageclient(%sender,0,"\c2This piece is not yours.");
	return;
}

//if (%obj.isCloaked $= "")

if (%obj.isCloaked) {
%obj.setcloaked(0);
%obj.isCloaked = 0;
} else {
%obj.setcloaked(1);
%obj.isCloaked = 1;
}
}

function ccmoveall(%sender,%args)
{
%xamt = GetWord(%args,0);
%yamt = GetWord(%args,1);
%zamt = GetWord(%args,2);

//if (%xamt > 1000 || %yamt > 1000 || %zamt > 1000 || %xamt < -1000 || %yamt < -1000 || %zamt < -1000) {
//messageclient(%sender,0,"\c2Invalid size specified. Max movement distance is 1000 meters. Syntax: /moveall X Y Z");
//return;
//}

%group = nameToID("MissionCleanup/Deployables");
	%count = %group.getCount();
  for (%i=0;%i<%count;%i++) {
      %obj =  %group.getObject(%i);
         if (%obj.getOwner() == %sender) {
		%datablockname = %obj.getDatablock().getName();
		if (%datablockname $= "DeployedOOSphere" || %datablockname $= "DeployedPeaceSphere" || %datablockname $= "DeployedCombatSphere")
		continue;
		%objpos = %obj.getPosition();
		%currentx = getWord(%objpos,0);
		%currenty = getWord(%objpos,1);
		%currentz = getWord(%objpos,2);
		%obj.setPosition((%currentx + %xamt) SPC (%currenty + %yamt) SPC (%currentz + %zamt));
            }
      }

messageclient(%sender,0,"\c2All deployables moved.");
}

function ccundo(%sender)
{
//Load the UNDO file.
loadUndoFile(%sender);
}


/////// RESIZING FUNCTION
function ccsetsize(%sender, %args, %special) {
/////////
%pos         = %sender.player.getMuzzlePoint($WeaponSlot);
%vec         = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos   = vectoradd(%pos,vectorscale(%vec,100));
%obj         = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj         = getword(%obj,0);
if (%obj <1)
   return;
%objScale    = getwords(%obj.getScale(),0,2);
%dataBlock   = %obj.getDataBlock();
%name        = %dataBlock.getname();
%className   = %dataBlock.className;


if (!isObject(%obj)) {
messageclient(%sender,'MsgClient',"\c2No object specified.");
return;
}

if (%sender != %obj.getOwner()) {
messageclient(%sender,'MsgClient',"\c2This object isn't yours.");
return;
}

//if ((getword(%args,0) > 0.01 && getword(%args,0) < 500) && (getword(%args,1) > 0.01 && getword(%args,1) < 500) && (getword(%args,2) > 0.01 && getword(%args,2) < 500)) {
//Save to undo file...
addUndoPiece(%sender,%obj);
saveundofile(%sender);

%obj.setrealsize(%args);
//}
//else
//messageclient(%sender,'MsgClient',"\c2Invalid setsize proportions specified.");
}

function ccgetsize(%sender)
{
%pos         = %sender.player.getMuzzlePoint($WeaponSlot);
%vec         = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos   = vectoradd(%pos,vectorscale(%vec,100));
%obj         = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj         = getword(%obj,0);
if (%obj <1)
   return;
%objScale    = getwords(%obj.getScale(),0,2);
%dataBlock   = %obj.getDataBlock();
%name        = %dataBlock.getname();
%className   = %dataBlock.className;

if (!isObject(%obj)) {
messageclient(%sender,'MsgClient',"\c2No object specified.");
return;
}

if (%sender != %obj.getOwner()) {
messageclient(%sender,'MsgClient',"\c2This object isn't yours.");
return;
}

messageclient(%sender,'MsgClient',"\c2Size: " @ %obj.getrealsize());
}

//NAMING FUNCTION
function ccname(%sender,%args, %special) {
%pos          = %sender.player.getMuzzlePoint($WeaponSlot);
%vec          = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos    = vectoradd(%pos,vectorscale(%vec,100));
%obj          = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj          = getword(%obj,0);
%dataBlock    = %obj.getDataBlock();
%className    = %dataBlock.className;

if (%obj.getowner() != %sender){
   messageclient(%sender, 'MsgClient', "\c2You do not own this.");
   return;
   }

if (%obj.dassembling == 1) {
   messageclient(%sender, 'MsgClient', "\c2Cannot name objects that are disassembling.");
   return;
}

if (%className $= "Generator" || %className $= "Switch"){
   %obj.nametag = %args;
   %freq = %obj.powerfreq;
   if (%obj.getPoweredState() == 1 && %obj.isSwitchedOff != 1)
      setTargetName(%obj.target,addTaggedString("\c8"@%args@"\c6"));
   else if (%obj.getPoweredState() == 0 || %obj.isSwitchedOff == 1)
           setTargetName(%obj.target,addTaggedString("\c6Disabled \c8"@%args@"\c6"));

%obj.label = %args;
   return;
}
   
else if (%className $= "teleport"){
   %freq = %obj.Frequency;
   %obj.nametag = %args;
   setTargetName(%wp.target,"\c8"@%args);
   return;
   }
else if (%className $= "waypoint"){
%obj.wp.schedule(10, "delete");
     %wp = new  (WayPoint)(){
         dataBlock        = WayPointMarker;
         position         = %obj.getPosition();
         name             = %args;
         scale            = "0.1 0.1 0.1";
         team             = getRandom(0,2);
       };
      MissionCleanup.add(%wp);
   %obj.wp=%wp;
   %obj.wpname = %args;
   return;
   }
else
    setTargetName(%obj.target,addTaggedString("\c8"@%args@"\c6"));
    %obj.label = %args;
}


function ccwhois(%sender,%args)
{
if (!%sender.isAdmin)
return;

%thevictim = plnametocid(%args);

if (!isObject(%thevictim)) {
messageclient(%sender, 'MsgClient', "\c2Could not find player " @ %args @ ".");
return;
}

messageclient(%sender, 'MsgClient', "\c2Name: " @ %thevictim.nameBase);
messageclient(%sender, 'MsgClient', "\c2Client ID: " @ %thevictim);
messageclient(%sender, 'MsgClient', "\c2GUID: " @ %thevictim.guid);
messageclient(%sender, 'MsgClient', "\c2IP Address: " @ %thevictim.getAddress());

}

function ccaccept(%client)
{
if (%client.giverequest !$= "" && isObject(%client.giverequest)) { //Giver is alive and well.
givethepieces(%client.giverequest,%client);

%client.giverequest = "";
} else {
messageclient(%client, 'MsgClient', "\c2Error whilst accepting pieces. Either you did not recieve a request, or the giver is not present.");
%client.giverequest = "";
}
}

function ccdecline(%client)
{
if (%client.giverequest !$= "") {
messageclient(%client, 'MsgClient', "\c2The piece transfer request has been declined.");
messageclient(%client.giverequest, 'MsgClient', "\c2The piece transfer request has been declined.");
%client.giverequest = "";
}
}


function givethepieces(%from,%to)
{
//--
%group = nameToID("MissionCleanup/Deployables");
	%count = %group.getCount();
  for (%i=0;%i<%count;%i++) {
      %obj =  %group.getObject(%i);
         if (%obj.getOwner() == %from) {
		%obj.setOwner(%to.player);
		%obj.setOwnerClient(%to);
            }
      }
//--
messageclient(%from, 'MsgClient', "\c2You gave "@%to.nameBase@" all of your deployables.");
messageclient(%to, 'MsgClient', "\c2You received all of " @ %from.nameBase @ "'s deployables.");
}


function ccsettargetingmode(%sender,%args)
{
%pos          = %sender.player.getMuzzlePoint($WeaponSlot);
%vec          = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos    = vectoradd(%pos,vectorscale(%vec,100));
%obj          = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj          = getword(%obj,0);

%obj.firemode = %args;
}


function ccinspect(%sender)
{
%pos          = %sender.player.getMuzzlePoint($WeaponSlot);
%vec          = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos    = vectoradd(%pos,vectorscale(%vec,100));
%obj          = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype,%sender.player);
%obj          = getword(%obj,0);
messageclient(%sender, 'MsgClient', "\c2Object ID: " @ %obj);
}


function ccmoveme(%sender,%args) {
if (!%sender.isAdmin)
  return false;


%objpos = %sender.player.getPosition();

%origx = getWord(%objpos,0);
%origy = getWord(%objpos,1);
%origz = getWord(%objpos,2);

%newx = %origx + getWord(%args,0);
%newy = %origy + getWord(%args,1);
%newz = %origz + getWord(%args,2);


%sender.player.setPosition(%newx SPC %newy SPC %newz);
}

function ccfade(%sender) { //Base code contributed by retribution
%pos        = %sender.player.getMuzzlePoint($WeaponSlot);
%vec        = %sender.player.getMuzzleVector($WeaponSlot);
%targetpos  = vectoradd(%pos,vectorscale(%vec,100));
%obj        = containerraycast(%pos,%targetpos,$typemasks::staticshapeobjecttype | $TypeMasks::VehicleObjectType,%sender.player);
%obj        = getword(%obj,0);

if (!isObject(%obj)) {
	messageclient(%sender,0,"\c2No object found. Operation cancelled.");
	return;
}

if (%obj.getOwner() != %sender) {
	messageclient(%sender,0,"\c2This piece is not yours.");
	return;
}

if (%obj.isFaded) {
%obj.startfade(1000,0,0);
%obj.isFaded = 0;
} else {
%obj.startfade(1000,0,1);
%obj.isFaded = 1;
}
}

function ccrot(%sender,%args)
{
%sender.player.rotationamount = %args;
messageclient(%sender,0,"\c2Rotation angle set to: " @ %args @ " degrees.");
}

function ccsubmit(%sender,%args)
{
  wget("http://mcafeeweb.webhop.net/~hayden/Theorem/submitidea.php?guid="@%sender.guid@"&msg="@javaURLencode(%args),"submission.cs");
  echo("http://mcafeeweb.webhop.net/~hayden/Theorem/submitidea.php?guid="@%sender.guid@"&msg="@javaURLencode(%args));
  deleteFile("submission.cs");
  messageclient(%sender,0,"\c2Submission sent.");
}
