$Host::NoAnnoyingVoiceChatSpam = 0; //Tracer DX, you suck.
setlogmode(1); //Console logging ON
$logechoenabled = 1; //Detailed.
$statstracking = 1; //Write stats to a log every so often...

function serverCmdcheckHtilt()
{
//Some weird client thing...
}

function serverCmdcheckendtilt()
{
//Some weird client thing...
}

function createtheatv(%client)
{
    %client.theatv = new HoverVehicle()
      {
         dataBlock  = AdminATV;
         teamBought = %client.team;
         team = %client.team;
         mountable = true;
         disableMove = false;
      };
      %client.theatv.setTransform(%client.player.getTransform());
    MissionCleanup.Add(%client.theatv);
}

function displaywepstat(%client,%wepname,%mode1,%mode2,%credits) //Just a simple way to display the weapon status.
{
if (%mode1 $= "")
%mode1 = "";
if (%mode2 $= "")
%mode2 = "";
if (%credits $= "")
%credits = "Krypton Construction";
if (%wepname $= "")
%wepname = "Unknown Weapon";
if (%mode1 !$= "")
bottomPrint(%client,"<font:Sui Generis:14>>>>" @ %wepname @ "<<<\n<font:Arial:14>Set to " @ %mode1 @ " --> " @ %mode2 @ "\n" @ %credits,3,3);
else
bottomPrint(%client,"<font:Sui Generis:14>>>>" @ %wepname @ "<<<\n<font:Arial:14>" @ %credits,3,3);
messageclient(%client,'MsgSPCurrentObjective1',"",%wepname @ " | " @ %mode1 @ " | " @ %mode2);
}

function SimObject::getUpVector(%obj){
   %vec = vectorNormalize(vectorsub(%obj.getEdge("0 0 1"),%obj.getEdge("0 0 -1")));
   return %vec;
}


function checkafk() {
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++) 
{ 
%client = ClientGroup.getObject( %cl ); 


//Away system stuff.
if (%client.afkenabled == 1) {

if (%client == $tid)
continue;

if (%client.isaway $= "")
%client.isaway = false;


if (%client.awaytime $= "")
%client.awaytime = 0;

//Are they inactive for more than 5 minutes?
%client.awaytime += 1;
if (%client.awaytime == 300 && !%client.isaway) {
%which = getRandom(0,3);
if (%which == 0)
schedule(2000,0,TheoremMsg,"You still with us, " @ %client.namebase @ "?");
if (%which == 1)
schedule(2000,0,TheoremMsg,"It seems as if " @ %client.namebase @ " has stepped away from the keyboard.");
if (%which == 2)
schedule(2000,0,TheoremMsg,"I'm marking you idle, " @ %client.namebase @ ".");
if (%which == 3)
schedule(2000,0,TheoremMsg,%client.namebase @ " has been marked as away.");

%client.isaway = true; //They're away.
}


if (!isObject(%client.player)) { //Not spawned? Add away time.
%client.awaytime += 1;
//continue;
} else { //They are spawned.

if (%client.lastpos $= "") //No position for comparison?
%client.lastpos = %client.player.getPosition(); //Grab one real quick.

//Compare their position to their last one.
%diffx = mabs(getword(%client.lastpos,0) - getword(%client.player.getPosition(),0));
%diffy = mabs(getword(%client.lastpos,1) - getword(%client.player.getPosition(),1));
%diffz = mabs(getword(%client.lastpos,2) - getword(%client.player.getPosition(),2));

if ((%diffx+%diffy+%diffz)/3 > 0.3) { //Have they moved?
endaway(%client); //They're here.
}
%client.lastpos = %client.player.getPosition(); //Update their current position for comparison.
}

} //End away system stuff.


//Total time spent calculation
if (%client.totalplaytime $= "")
%client.totalplaytime = 0;

%client.totalplaytime++; //add 1 more second to their total play time...

if (%client.totalplaytime > 172800 && !%client.viparmorenabled) { //Over 48 hours -- VIP Armor Enabled
TheoremMsg(%client.nameBase @ " has now played in Krypton for over 48 hours, and has gained VIP armor!", "gui/command_on.wav");
%client.viparmorenabled = 1;
updateprefs(%client);
}

}
$awaysched = schedule(1000,0,checkafk);
}

cancel($awaysched);
checkafk();

function setaway(%client)
{
if (%client.afkenabled == 0) {
schedule(1000,0,messageclient,%client,'msgclient',"\c2Your away system is disabled in player preferences; you will not be set as away.");
return;
}

%which = getRandom(0,1);
if (%which == 0)
schedule(2000,0,TheoremMsg,"I'm marking you as away, " @ %client.namebase @ ".");
if (%which == 1)
schedule(2000,0,TheoremMsg,%client.namebase @ " has been marked as away.");

%client.isaway = true; //They're away.
}

function endaway(%client)
{
if (%client.isaway) {
%which = getRandom(0,3);
if (%which == 0)
schedule(2000,0,TheoremMsg,"Welcome back, " @ %client.namebase @ ". You were away for approx. " @ mFloor(%client.awaytime/60) @ " minutes.");
if (%which == 1)
schedule(2000,0,TheoremMsg,%client.namebase @ " has returned. You were away for approx. " @ mFloor(%client.awaytime/60) @ " minutes.");
if (%which == 2)
schedule(2000,0,TheoremMsg,%client.namebase @ " is back in action. You were away for approx. " @ mFloor(%client.awaytime/60) @ " minutes.");
if (%which == 3)
schedule(2000,0,TheoremMsg,%client.namebase @ " has been marked as present. You were away for approx. " @ mFloor(%client.awaytime/60) @ " minutes.");
}
%client.isaway = false; //They've returned.
%client.awaytime = 0;
}



datablock TargetProjectileData(AimingLaser0)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "0.9 0.0 0.0";

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

datablock TargetProjectileData(AimingLaser1)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "1.0 0.5 0.0";

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

datablock TargetProjectileData(AimingLaser2)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "1.0 1.0 0.0";

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

datablock TargetProjectileData(AimingLaser3)
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

datablock TargetProjectileData(AimingLaser4)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "0.0 0.24 1.0";

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

datablock TargetProjectileData(AimingLaser5)
{
   directDamage        	= 0.0;
   hasDamageRadius     	= false;
   indirectDamage      	= 0.0;
   damageRadius        	= 0.0;
   velInheritFactor    	= 1.0;

   maxRifleRange       	= 1000;
   beamColor           	= "1.0 0.0 1.0";

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


function givestuff(%from,%to)
{
%group = nameToID("MissionCleanup/Deployables");
	%count = %group.getCount();
  for (%i=0;%i<%count;%i++) {
      %obj =  %group.getObject(%i);
         if (%obj.getOwner() == %from) {
		%obj.setOwner(%to.player);
		%obj.setOwnerClient(%to);
            }
      }
}


function makeblankfile(%file)
{
	new FileObject("SaveFile"); //create file object (player's save file)
	SaveFile.openForWrite(%file); //open it up, and create it if it isn't there
	SaveFile.writeLine("");
	SaveFile.close(); //close the file
	SaveFile.delete(); //delete the object (not the file)
}


function uberzHash(%str) {
%hash = 7;
%len = strlen(%str);
for (%i = 0; %i <%len; %i++){ %hash = mmod(%hash,131072); %hash *= strCmp(getSubStr(%str, %i, 1), ""); }
return mAbs(%hash % 1000000);
}




function updatestats() {
new FileObject("StatFile"); //Create stat file
StatFile.openForWrite("logs/currentstats.php");
StatFile.writeLine("<?php");
%playersline = "$players = \"";
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++) 
{ 
%subject = ClientGroup.getObject( %cl ); 
%playersline = %playersline @ %subject.nameBase @ "||";
}
%playersline = %playersline @ "\";";
StatFile.writeLine(%playersline);
StatFile.writeLine("$uptime = " @ getsimtime() @ ";");
StatFile.writeLine("?>");
StatFile.close();
StatFile.delete();

if ($statstracking)
schedule(5000,0,updatestats);
}

if ($statstracking)
schedule(5000,0,updatestats);


function limitloop() {
//Make sure we're not already over the vis limit. Check sphere count while we're at it.
%count=clientgroup.getcount();
for (%i = 0; %i < %count; %i++){
%client = clientgroup.getObject(%i);
%client.piececount = 0;
%client.spherecount = 0;
}

%group = nameToID("MissionCleanup/Deployables");
	%count = %group.getCount();

  for (%i=0;%i<%count;%i++) { //Reset all dep. specific counts to zero.
      %obj =  %group.getObject(%i);
         %owner = %obj.getOwner();
if ($DeployableGroup[%obj.getdatablock().getName()] !$= "")
	%owner.deployablecount[$DeployableGroup[%obj.getdatablock().getName()]] = "";
}

  for (%i=0;%i<%count;%i++) {
      %obj =  %group.getObject(%i);
         %owner = %obj.getOwner();

if ($DeployableGroup[%obj.getdatablock().getName()] !$= "")
	%owner.deployablecount[$DeployableGroup[%obj.getdatablock().getName()]]++;

	%maxamt = $PlayerDeployableMax[$DeployableGroup[%obj.getdatablock().getName()]];
	if (%maxamt !$= "" && (%owner.deployablecount[$DeployableGroup[%obj.getdatablock().getName()]] > %maxamt)) {
		%obj.getDataBlock().disassemble(%owner.player,%obj);
		messageclient(%owner, 'MsgClient', "\c2You have exceeded the " @ $DeployableGroupName[$DeployableGroup[%obj.getdatablock().getName()]] @ " limit of " @ %maxamt @ ". Deleting excess objects...");
	} else {
	%owner.piececount++;
	%piececount++;
	}
}

$PieceCount = %piececount;
cancel($limitloop);
$limitloop = schedule(5000,0,limitloop);
}
$limitloop = schedule(5000,0,limitloop);


function UpdatePlayerCount()
{

$ActivePlayerCount = 0;
%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++) 
{
%subject = ClientGroup.getObject( %cl );
if (!%subject.isAIControlled())
$ActivePlayerCount++;
}

echo("Currently Active Players: " @ $ActivePlayerCount);
}


function IPRenew() //The stability restart seems to make the server confused about its own IP. This should keep it in line...
{
//	if ($IPv4::InetAddress !$= "") //I don't care what it is now... we need to renew it!
//		return;

	if (isObject(IPv4Connection))
	{
		IPv4Connection.disconnect();
		IPv4Connection.delete();
	}
	new TCPObject(IPv4Connection);
	IPV4Connection.data = "GET " @ $IPv4::AutomationURL @ " HTTP/1.1\r\nHost: www.tribesnext.com\r\nUser-Agent: Tribes 2\r\nConnection: close\r\n\r\n";
	IPv4Connection.connect("www.tribesnext.com:80");

$Krypton::IPRenewSchedule.cancel();
$Krypton::IPRenewSchedule = schedule(1000*60*2,0,IPRenew); //Renew every 2 minutes.
}

//$Krypton::IPRenewSchedule = schedule(1000*60*2,0,IPRenew); //Renew every 2 minutes.



function serverCmdMetallic(%client,%metallic) {
	%plyr = %client.player;
	if (isObject(%plyr)) {
		switch$ (%metallic) {
			case "BuyFavs":
                schedule(10,0,"ccbf",%client);
            case "GetSize":
                schedule(10,0,"ccgetsize",%client);
            case "MoveDown":
                schedule(10,0,"ccmoveme",%client,"0 0 -1");
//            case "sizecopy":
//                schedule(10,0,"ccsetsize",%client,"copy",0);
//            case "sizeoriginal":
//                schedule(10,0,"ccsetsize",%client,"original",0);
//            case "sizeundo":
//                schedule(10,0,"ccsetsize",%client,"undo",0);
//            case "ActivateGate":
//                schedule(10,0,"ccActivate",%client);
            case "Help":
               schedule(10,0,"cchelp",%client);
		}
	}
}

function javaURLencode(%string) {
  %string = strreplace(%string,";","%3B");
  %string = strreplace(%string,"?","%3F");
  %string = strreplace(%string,"/","%2F");
  %string = strreplace(%string,":","%3A");
  %string = strreplace(%string,"#","%23");
  %string = strreplace(%string,"&","%24");
  %string = strreplace(%string,"=","%3D");
  %string = strreplace(%string,"+","%2B");
  %string = strreplace(%string,"$","%26");
  %string = strreplace(%string,",","%2C");
  %string = strreplace(%string," ","%20");
  %string = strreplace(%string,"%","%25");
  %string = strreplace(%string,"<","%3C");
  %string = strreplace(%string,">","%3E");
  %string = strreplace(%string,"~","%7E");
  %string = strreplace(%string,"'","%27");
  %string = strreplace(%string,"\"","%22");

  return %string;
}


function wget(%URL,%outputfile)
{
  makeblankfile(%outputfile); //Make sure torque can see the file.
  //%command = "start Z:\home\mcafeenet\ServerApps\Games\Tribes2\GameData\wget.exe --output-document=Construction/"@%outputfile@" "@%URL;
  %command = "wget --output-document=Construction/"@%outputfile@" "@%URL;
  //%command = "start notepad";
  rubyEval("system('"@%command@"');");
}

$LyricsNumSongs = 1;
$LyricsLineCount[0] = 63;
$LyricsPunishment[0] = "Mother We Just Can't Get Enough - New Radicals";
$LyricsPunishment[0,0] = "There's something about you";
$LyricsPunishment[0,1] = "Tears me inside out whenever you're around";
$LyricsPunishment[0,2] = "There's something about you";
$LyricsPunishment[0,3] = "Spin through my veins, then we hit the ground";
$LyricsPunishment[0,4] = "There's something about this rush";
$LyricsPunishment[0,5] = "Take it away";
$LyricsPunishment[0,6] = "It made me feel so good";
$LyricsPunishment[0,7] = "I got a feeling";
$LyricsPunishment[0,8] = "You got a feeling";
$LyricsPunishment[0,9] = "We got a feeling";
$LyricsPunishment[0,10] = "Like we could die";

$LyricsPunishment[0,11] = "Woooooooooooooah, woooah-ho";
$LyricsPunishment[0,12] = "We just can't get enough";
$LyricsPunishment[0,13] = "Just can't get enough";
$LyricsPunishment[0,14] = "Woooooooooooooah, woooah-ho";
$LyricsPunishment[0,15] = "We just gotta get it up";
$LyricsPunishment[0,16] = "Just gotta get it up";

$LyricsPunishment[0,17] = "There's something about you";
$LyricsPunishment[0,18] = "Tears me inside out whenever you're around";
$LyricsPunishment[0,19] = "There's something about you";
$LyricsPunishment[0,20] = "That makes me fly";
$LyricsPunishment[0,21] = "You're a heart attack";
$LyricsPunishment[0,22] = "Just the kind I like";
$LyricsPunishment[0,23] = "There's something about your kiss";
$LyricsPunishment[0,24] = "Haunting and strange";
$LyricsPunishment[0,25] = "That makes me feel so good";
$LyricsPunishment[0,26] = "I got a feeling";
$LyricsPunishment[0,27] = "You got a feeling";
$LyricsPunishment[0,28] = "We got a feeling";
$LyricsPunishment[0,29] = "Like we're alive";

$LyricsPunishment[0,30] = "Woooooooooooooah, woooah-ho";
$LyricsPunishment[0,31] = "We just can't get enough";
$LyricsPunishment[0,32] = "Just can't get enough";
$LyricsPunishment[0,33] = "Woooooooooooooah, woooah-ho";
$LyricsPunishment[0,34] = "We just gotta get it up";
$LyricsPunishment[0,35] = "Just gotta get it up";

$LyricsPunishment[0,36] = "This world ain't got too much time";
$LyricsPunishment[0,37] = "But baby I'm fine";
$LyricsPunishment[0,38] = "'Cause maybe you're mine";
$LyricsPunishment[0,39] = "We just can't get enough";
$LyricsPunishment[0,40] = "You better give up";
$LyricsPunishment[0,41] = "Come on, give up";
$LyricsPunishment[0,42] = "You better give up";
$LyricsPunishment[0,43] = "Give up your life";

$LyricsPunishment[0,44] = "It's you for me, and me for you";
$LyricsPunishment[0,45] = "You make my dreams come true";
$LyricsPunishment[0,46] = "Off the wall, I wanna say,";
$LyricsPunishment[0,47] = "I gotta be with you, now, baby";
$LyricsPunishment[0,48] = "You're on my mind all the time";
$LyricsPunishment[0,49] = "We found a million diamonds";
$LyricsPunishment[0,50] = "I rolled the dice, lost 'em all";
$LyricsPunishment[0,51] = "But baby I just don't mind";

$LyricsPunishment[0,52] = "Woooooooooooooah, woooah-ho";
$LyricsPunishment[0,53] = "We just can't get enough";
$LyricsPunishment[0,54] = "Just can't get enough";
$LyricsPunishment[0,55] = "Woooooooooooooah, woooah-ho";
$LyricsPunishment[0,56] = "We just gotta get it up";
$LyricsPunishment[0,57] = "Just gotta get it up";

$LyricsPunishment[0,58] = "This world ain't got too much time";
$LyricsPunishment[0,59] = "But baby I'm fine";
$LyricsPunishment[0,60] = "'Cause maybe you're mine";
$LyricsPunishment[0,61] = "We just can't get enough";
$LyricsPunishment[0,62] = "Woooooooooooooah, woooah-ho";

// VISUAL RICKROLL'D!!1
$LyricsNumSongs++;
$LyricsLineCount[1] = 54;
$LyricsPunishment[1] = "Never Gonna Give You Up - Rick Astley";

$LyricsPunishment[1, 0] = "We're no strangers to love";
$LyricsPunishment[1, 1] = "You know the rules and so do I";
$LyricsPunishment[1, 2] = "A full commitment's what I'm thinking of";
$LyricsPunishment[1, 3] = "You wouldn't get this from any other guy";
$LyricsPunishment[1, 4] = "I just wanna tell you how I'm feeling";
$LyricsPunishment[1, 5] = "Gotta make you understand";

$LyricsPunishment[1, 6] = "Never gonna give you up,";
$LyricsPunishment[1, 7] = "Never gonna let you down,";
$LyricsPunishment[1, 8] = "Never gonna run around and desert you,";
$LyricsPunishment[1, 9] = "Never gonna make you cry,";
$LyricsPunishment[1, 10] = "Never gonna say goodbye,";
$LyricsPunishment[1, 11] = "Never gonna tell a lie and hurt you";

$LyricsPunishment[1, 12] = "We've known each other for so long";
$LyricsPunishment[1, 13] = "Your heart's been aching but you're too shy to say it";
$LyricsPunishment[1, 14] = "Inside we both know what's been going on";
$LyricsPunishment[1, 15] = "We know the game and we're gonna play it";
$LyricsPunishment[1, 16] = "And if you ask me how I'm feeling";
$LyricsPunishment[1, 17] = "Don't tell me you're too blind to see";

$LyricsPunishment[1, 18] = "Never gonna give you up,";
$LyricsPunishment[1, 19] = "Never gonna let you down,";
$LyricsPunishment[1, 20] = "Never gonna run around and desert you,";
$LyricsPunishment[1, 21] = "Never gonna make you cry,";
$LyricsPunishment[1, 22] = "Never gonna say goodbye,";
$LyricsPunishment[1, 23] = "Never gonna tell a lie and hurt you";

$LyricsPunishment[1, 24] = "Never gonna give you up,";
$LyricsPunishment[1, 25] = "Never gonna let you down,";
$LyricsPunishment[1, 26] = "Never gonna run around and desert you,";
$LyricsPunishment[1, 27] = "Never gonna make you cry,";
$LyricsPunishment[1, 28] = "Never gonna say goodbye,";
$LyricsPunishment[1, 29] = "Never gonna tell a lie and hurt you";

$LyricsPunishment[1, 30] = "Ooooooooh give you up";
$LyricsPunishment[1, 31] = "Ooooooooh give you up";
$LyricsPunishment[1, 32] = "(Ooh) never gonna give, never gonna give";
$LyricsPunishment[1, 33] = "give you up";
$LyricsPunishment[1, 34] = "(Ooh) never gonna give, never gonna give";
$LyricsPunishment[1, 35] = "give you up";

$LyricsPunishment[1, 36] = "We've known each other for so long";
$LyricsPunishment[1, 37] = "Your heart's been aching but you're too shy to say it";
$LyricsPunishment[1, 38] = "Inside we both know what's been going on";
$LyricsPunishment[1, 39] = "We know the game and we're gonna play it";

$LyricsPunishment[1, 40] = "I just wanna tell you how I'm feeling";
$LyricsPunishment[1, 41] = "Gotta make you understand";

$LyricsPunishment[1, 42] = "Never gonna give you up,";
$LyricsPunishment[1, 43] = "Never gonna let you down,";
$LyricsPunishment[1, 44] = "Never gonna run around and desert you,";
$LyricsPunishment[1, 45] = "Never gonna make you cry,";
$LyricsPunishment[1, 46] = "Never gonna say goodbye,";
$LyricsPunishment[1, 47] = "Never gonna tell a lie and hurt you";

$LyricsPunishment[1, 48] = "Never gonna give you up,";
$LyricsPunishment[1, 49] = "Never gonna let you down,";
$LyricsPunishment[1, 50] = "Never gonna run around and desert you,";
$LyricsPunishment[1, 51] = "Never gonna make you cry,";
$LyricsPunishment[1, 52] = "Never gonna say goodbye,";
$LyricsPunishment[1, 53] = "Never gonna tell a lie and hurt you";
