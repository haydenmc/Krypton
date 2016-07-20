//--------------------
//Theorem Bot -- Coded by Sloik for Xenon
//--------------------
if ($tid $= "" || !isObject($tid)) { //Theorem's not around!
$tid = aiconnect("[Kr] Theorem",0,0.99,0,"Bot1",1.6);
}
$tid.laserMode = 1; //He's got an aiming laser.
$tid.voicepitch = 1.6;
$tid.cleartasks();
$tid.addtask(AIEngageTask); //Kill anyone who hurts you
$tid.addtask(AIEngageTurretTask); //Kill anyone (MTCs)  who hurts you
$tid.addtask(AIUseInventoryTask); //Get your crap
$tid.addtask(AIPatrolTask); //Run around
$tid.isAdmin = 1;
$tid.isSuperAdmin = 1;
//$tid.addTask(AIBountyPatrolTask); //Run around/look for someone to kill.
//$tid.bountyTask = $tid.addTask(AIBountyEngageTask);
$tid.setskilllevel(0.99); //Be smart

$theoremhi = 1; //Say hello to people who say Hi
$theorembye = 1; //Say bye to people who say bye
$theoremf2 = 0; //How do I build?
$theoremsave = 1; //How do I save?

function theoremAttack(%target)
{
$theoremtarget = %target;
%player = $tid.player;
	error("Theorem is attacking " @ %target.nameBase);
//$AIClientLOSTimeout = 60*1000*60;	//how long a client has to remain out of sight of the bot
										//before the bot "can't see" the client anymore...
//$AIClientMinLOSTime = 60*1000*60;	//how long a bot will search for a client


   %player.setInventory(Plasma,1);
   %player.setInventory(Chaingun, 1);
   %player.setInventory(Disc,1);
   %player.setInventory(Mortar, 1);
   %player.setInventory(GrenadeLauncher, 1);
//   %player.setInventory(SniperRifle, 1);
   %player.setInventory(ELFGun, 1);
   %player.setInventory(MissileLauncher, 1);
   %player.setInventory(ShockLance, 1);
   %player.setInventory(MissileLauncherAmmo, 999);
   %player.setInventory(GrenadeLauncherAmmo, 999);
   %player.setInventory(MortarAmmo, 999);
   %player.setInventory(PlasmaAmmo,999);  
   %player.setInventory(ChaingunAmmo, 999);
   %player.setInventory(DiscAmmo, 999);

   %player.weaponCount = 8;
	%player.setinventory("EnergyPack", 1);
	%player.setInventory(RepairKit,999);
	%player.setInventory(Grenade,999);
	%player.schedule(10, use, Disc);

	%client=$tid;
//$tid.removetask(AIPatrolTask);
	%client.shouldEngage = %target;
	//%client.setTargetObject(%target);
	%client.stepEngage(%target);
//	%client.stepMove(%target.player.position, 8);



}

function theoremjetto(%client)
{
$tid.stepjet(%client.player.getTransform());
}

// we pass the guid as well, in case this guy leaves the server.
function theoremkick( %client, %admin, %guid )
{
   if(%admin) // z0dd - ZOD, 8/23/02. Let the player know who kicked him.
      messageAll( 'MsgAdminForce', '\c2%2 has kicked %1.', %client.nameBase, %admin.name );
   else
      messageAll( 'MsgVotePassed', '\c2%1 was kicked by vote.', %client.nameBase );

   messageClient(%client, 'onClientKicked', "");
   messageAllExcept( %client, -1, 'MsgClientDrop', "", %client.nameBase, %client );

	if ( %client.isAIControlled() )
	{
      //$HostGameBotCount--; //<cheesed-off> said it will fix the bug were when i kick a bot u get 255 bot count
		%client.drop();
	}
	else
	{
      if ( $playingOnline ) // won games
      {
         %count = ClientGroup.getCount();
         %found = false;
         for( %i = 0; %i < %count; %i++ ) // see if this guy is still here...
         {
            %cl = ClientGroup.getObject( %i );
	         if ( %cl.guid == %guid )
            {
	            %found = true;

	            // kill and delete this client, their done in this server.
	            if ( isObject( %cl.player ) )
	               %cl.player.scriptKill(0);

               if ( isObject( %cl ) )
               {
                     %cl.setDisconnectReason( "The almighty Theorem has decided to kick your anus out!" );

	               %cl.schedule(700, "delete");
               }

	            BanList::add( %guid, "0", $Host::KickBanTime );
            }
	      }
         if ( !%found )
	         BanList::add( %guid, "0", $Host::KickBanTime ); // keep this guy out for a while since he left.
      }
      else // lan games
      {
	      // kill and delete this client
	      if ( isObject( %client.player ) )
	         %client.player.scriptKill(0);

         if ( isObject( %client ) )
         {
            %client.setDisconnectReason( "The almighty Theorem has decided to kick your anus out!" );
	         %client.schedule(700, "delete");
         }

	      BanList::add( 0, %client.getAddress(), $Host::KickBanTime );
      }
	}
}


//Support for TIME question
function tdatetime()
{
	return(formatTimeString("yy/mm/dd HH:nn:ss"));
}

function TheoremMsg(%txt,%snd)
{
serverCmdMessageSent($tid,%txt);
if (%snd !$= "")
messageAll("snd","~w" @ %snd);
}


function VerifyWeather(%dur)
{
if ($weatherrequest !$= "") {
schedule(10,0,TheoremMsg,$weatherrequest,"gui/objective_notification.wav");
$gettingweather = 0;
} else {
schedule(10,0,TheoremMsg,"Error while retrieving weather information!", "gui/vote_nopass.wav");
$gettingweather = 0;
}
}

function VerifyWord(%dur)
{
if ($wordrequest !$= "") {
schedule(10,0,TheoremMsg,$theword @ " -- " @ $partofspeech @ ": " @ $wordrequest,"gui/objective_notification.wav");
$gettingword = 0;
} else {
schedule(10,0,TheoremMsg,"Error while retrieving dictionary information!", "gui/vote_nopass.wav");
$gettingword = 0;
}
}

function SwearFilter(%sender,%a2)
{
//SWEAR FILTER TIME!
//if (strstr(strlwr(%a2),"bitch") >= 0 || strstr(strlwr(%a2),"dick") >= 0 || strstr(strlwr(%a2),"shit") >= 0 || strstr(strlwr(%a2),"fuck") >= 0 || strstr(strlwr(%a2),"pussy") >= 0 || strstr(strlwr(%a2),"damn") >= 0 || strstr(strlwr(%a2),"damnit") >= 0 || strstr(strlwr(%a2),"dammit") >= 0 || strstr(strlwr(%a2),"asshole") >= 0 || strstr(strlwr(%a2),"penis") >= 0 || strstr(strlwr(%a2),"cock") >= 0 || strstr(strlwr(%a2),"faggot") >= 0 || strstr(strlwr(%a2),"fag") >= 0 || strstr(strlwr(%a2),"gay") >= 0 || strstr(strlwr(%a2),"vagina") >= 0 || strstr(strlwr(%a2),"nigg") >= 0) {
if (containsBadWords(%a2)) {
TheoremWarn(%sender,"Watch your language, " @ %sender.nameBase @ ".","using foul language");
}
}

function CapsFilter(%sender,%a2)
{
//CAPS FILTER TIME!
//Strip out the extra characters.
%msg = strreplace( %a2, ".", "" );
%msg = strreplace( %msg, ",", "" );
%msg = strreplace( %msg, "?", "" );
%msg = strreplace( %msg, "!", "" );
%msg = strreplace( %msg, ":", "" );
%msg = strreplace( %msg, " ", "" );

if (strlen(%msg) < 8)
return;


%count = strlen(%msg);
%numberofcaps = 0;

for ( %i = 0; %i < %count; %i++ ) {
if (strcmp(strlwr(getSubStr(%msg,%i,1)),getSubStr(%msg,%i,1)) != 0)
%numberofcaps += 1;
}

%capspercent = (%numberofcaps/%count)*100;

if (%capspercent > 85)
{
TheoremWarn(%sender,"Your last message was " @ %capspercent @ "% capital letters, " @ %sender.nameBase @ ". Stop screaming.","screaming");
}

}

function RepeatFilter(%sender,%a2)
{
if (%sender.repeatcount $= "")
%sender.repeatcount = 0;

if (%a2 $= %sender.lastmsg) {
%sender.repeatcount++;
} else {
%sender.repeatcount = 0;
}

if (%sender.repeatcount > 2)
{
TheoremWarn(%sender,"Stop repeating yourself, " @ %sender.nameBase @ ".","being annoying");
%sender.repeatcount = 0;
}
%sender.lastmsg = %a2;
}

function TheoremWarn(%client,%msg,%reason)
{
if (%client == $tid)
return;

if (%client.wordwarnings $= "")
%sender.wordwarnings = 0;
if (%client.wordwarnings == 0)
%warningphrase = "This is your first out of three warnings.";
if (%client.wordwarnings == 1)
%warningphrase = "This is your second out of three warnings.";
if (%client.wordwarnings == 2)
%warningphrase = "This is your final warning.";


if (%client.wordwarnings < 3) {
schedule(1500,0,TheoremMsg,%msg SPC %warningphrase, "gui/vote_nopass.wav");
} else {
schedule(1500,0,TheoremMsg,%client.nameBase @ " was kicked for " @ %reason @ ".", "gui/vote_nopass.wav");
schedule(3000,0,theoremkick,%client,$tid,%client.GUID); //Kick 'em!
}
%client.wordwarnings++;
cancel(%client.warntimer);
%client.warntimer = schedule(10*60*1000,0,WarnDown,%client);
}

function WarnDown(%sender)
{
if (%sender.wordwarnings > 0) 
%sender.wordwarnings--;

cancel(%sender.warntimer);
%sender.warntimer = schedule(10*60*1000,0,WarnDown,%sender);
}

function getTheoremResponse(%theid,%msg)
{
if ($theoremchatting[%theid.guid] != 1)
return false;

if ($theoremresponse[%theid.guid] !$= "") {
schedule(10,0,ChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ " \c5(@" @ %theid.nameBase @ ")\c4: " @ $theoremresponse[%theid.guid], $tid.name, $theoremresponse[%theid.guid]); //Dur.
$tresponding[%theid.guid] = 0;
} else {
if (strlen(%theid.guid) > 1) {
GetTheorem_doAutoUpdate(%theid.guid,%msg);
schedule(5000,0,getTheoremResponse,%theid,%msg); //Get his response in three seconds.
//schedule(10,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Error connecting to ALICE.", "{s}Theorem", "\c4{s}Theorem: Error connecting to ALICE.", "BOT"); //Dur.
}
}
}

function TheoremFetchALICEResponse(%sender,%a2)
{
	%sender.theoremchatting = 0;
    if (!%sender.theoremresponding) { //Make sure we're not clogging him up
      wget("http://mcafeeweb.webhop.net/~hayden/Theorem/src/theorem.php?id=" @ %sender.guid @ "&input=" @ javaURLencode(%a2),%sender.guid@"theoremchat.cs");
      %sender.theoremresponding = 1;    
      schedule(4000,0,TheoremVerifyALICEResponse,%sender);
    }
}

function TheoremVerifyALICEResponse(%sender)
{
  exec(%sender.guid@"theoremchat.cs");
  if (%theoremresponse !$= "") {
    TheoremMsg("\c5(@" @ %sender.nameBase @ ")\c4: "@%theoremresponse);
  }
  deleteFile(%sender.guid@"theoremchat.cs");
  deleteFile(%sender.guid@"theoremchat.cs.dso");
  %sender.theoremresponding = 0;
}

function checkAgainstTheorem(%sender,%a2) //Check non-canned messages against Theorem.
{ //Begin function...
if (%sender == $tid)
return;

if (getSubStr(%a2,0,1) $= "/") //Ignore chat commands.
{
return;
}

   %wavStart = strstr( %a2, "~w" ); //Are we using TRACER ECM?????? ARG!
   if ( %wavStart != -1 ) {
	checkCannedAgainstTheorem(%sender,%a2);
	return;
}

//Swear filter...
SwearFilter(%sender,%a2);

//Caps filter...
CapsFilter(%sender,%a2);

//Repetition filter...
RepeatFilter(%sender,%a2);

//Have we initiated an ALICE chat?
if (%sender.theoremchatting == 1) {
	if (strstr(strlwr(%a2),"stop talk") >= 0 || strstr(strlwr(%a2),"shut up") >= 0 || strstr(strlwr(%a2),"shuddup") >= 0 || strstr(strlwr(%a2),"be quiet") >= 0) { //Want to stop conversing with theorem?
		schedule(2000,0,TheoremMsg,"Disengaging conversation with " @ %sender.nameBase @ "."); //Sure.
		%sender.theoremchatting = 0;
	} else {
		//echo("Theorem msg: " @ %a2);
		if (%sender.theoremchatting == 1)
		{
			TheoremFetchALICEResponse(%sender,%a2);
		}
	}
} else { //We're not in an ALICE conversation with Theorem.

if (%sender !$= $tid) { //Don't listen to yourself.
//-----------------
//For messages not necessarily directed toward Theorem...
//-----------------



//Have Theorem set a personal timer
//Intended activation phrases: Theorem, set timer for <time> minute(s)/second(s).
if (strstr(strlwr(%a2),"theorem") >= 0 && strstr(strlwr(%a2),"timer") >= 0) {

if (strstr(strlwr(%a2),"minute") >= 0 || strstr(strlwr(%a2),"second") >= 0) {
//Find the time setting!

schedule(1000,0,cannedChatMessageAll,$tid,"\c4[Kr] Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4[Kr] Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.

if (strstr(strlwr(%a2),"minute") >= 0) { //If we're goin' by minutes...
%timestart = strstr(strlwr(%a2),"minute") - 2; //We should have a space between the two words.
%timesrch = 0;
%timepos = %timestart;
%timelen = 1;
while (getSubStr(%a2,%timepos,1) !$= " ") {
%timepos -= 1;
%timelen += 1;
}
%thetime = getSubStr(%a2,%timepos,%timelen);
schedule(2000,0,cannedChatMessageAll,$tid,"\c4[Kr] Theorem: Setting your personal timer to " @ %thetime @ " minute(s).", "[Kr] Theorem", "\c4[Kr] Theorem: Setting your personal timer to " @ %thetime @ " minute(s).", "BOT"); //What am I doing?
cancel(%sender.personaltimer);
%sender.personaltimer = schedule(%thetime*60*1000,0,TheoremMsg,"Attention " @ %sender.nameBase @ "! Your timer has expired!","gui/command_on.wav");
}

if (strstr(strlwr(%a2),"second") >= 0) { //If we're goin' by seconds...
%timestart = strstr(strlwr(%a2),"second") - 2; //We should have a space between the two words.
%timesrch = 0;
%timepos = %timestart;
%timelen = 1;
while (getSubStr(%a2,%timepos,1) !$= " ") {
%timepos -= 1;
%timelen += 1;
}
%thetime = getSubStr(%a2,%timepos,%timelen);
schedule(2000,0,cannedChatMessageAll,$tid,"\c4[Kr] Theorem: Setting your personal timer to " @ %thetime @ " second(s).", "[Kr] Theorem", "\c4[Kr] Theorem: Setting your personal timer to " @ %thetime @ " second(s).", "BOT"); //What am I doing?
cancel(%sender.personaltimer);
%sender.personaltimer = schedule(%thetime*1000,0,TheoremMsg,"Attention " @ %sender.nameBase @ "! Your timer has expired!","gui/command_on.wav");
}

} else {
//No time was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4[Kr] Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4[Kr] Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4[Kr] Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "[Kr] Theorem", "\c4[Kr] Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
}
//End timer block.



//Tell people the time!
//Intended activation phrases: What time is it? ...Time it is?
if (strstr(strlwr(%a2),"time") >= 0 && strstr(strlwr(%a2),"what") >= 0 && strstr(strlwr(%a2),"it") >= 0) {
%mytime = getSubStr(tdatetime(),11,8);
schedule(1500,0,TheoremMsg,"The time is " @ %mytime @ " (Central Standard Time)", "gui/objective_notification.wav");
}
//End time block


//Respond to admin requests
//Intended activation phrases: Give me admin, I want admin, Can I have admin, can has admin
if (strstr(strlwr(%a2),"admin") >= 0) {
if ((strstr(strlwr(%a2),"give") >= 0) || (strstr(strlwr(%a2),"want") >= 0) || (strstr(strlwr(%a2),"can") >= 0 && (strstr(strlwr(%a2),"has") >= 0 || strstr(strlwr(%a2),"have") >= 0 || strstr(strlwr(%a2),"get") >= 0))) {
	schedule(2000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": No. Do not ask for administrator status on this server.~wgbl.no", $tid.name, "No. Do not ask for administrator status on this server.~wgbl.no", "VGN"); //No. 
}
}
//End admin request block.

//Get the weather when asked.
//Intended activation phrases: How's the weather, get the weather, retrieve the weather, what's the weather...
if ((strstr(strlwr(%a2),"weather") >= 0) && (strstr(strlwr(%a2),"get") >= 0 || strstr(strlwr(%a2),"retrieve") >= 0 || strstr(strlwr(%a2),"how") >= 0 || strstr(strlwr(%a2),"what") >= 0)) {

//Get rid of periods and commas (things people might put in front or behind the zip code)
%a2 = strreplace(%a2,".","");
%a2 = strreplace(%a2,",","");
%times = 0;
%zipcode = "";

while (%times <= GetWordCount(%a2)) {
if (strlen(getWord(%a2,%times)) == 5) {
if ((strstr(getWord(%a2,%times),"0") >= 0 || strstr(getWord(%a2,%times),"1") >= 0 || strstr(getWord(%a2,%times),"2") >= 0 || strstr(getWord(%a2,%times),"3") >= 0 || strstr(getWord(%a2,%times),"4") >= 0 || strstr(getWord(%a2,%times),"5") >= 0 || strstr(getWord(%a2,%times),"6") >= 0 || strstr(getWord(%a2,%times),"7") >= 0 || strstr(getWord(%a2,%times),"8") >= 0 || strstr(getWord(%a2,%times),"9") >= 0)) {
%zipcode = getWord(%a2,%times); //Make sure that there's a zipcode in there...
}
}
%times += 1;
}

if (%zipcode !$= "") { //If we recognized a zip code...
if ($gettingweather == 0 || $gettingweather $= "") {
$weatherrequest = "";
schedule(10, 0, GetWeather_doAutoUpdate, %zipcode); //Get the weather for this zip code. Immediately. Delay with theorem's talk below...
schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Command acknowledged.~wcmd.acknowledge", $tid.name, "Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,TheoremMsg,"Retrieving weather for " @ %zipcode @ "...");
schedule(6000,0,verifyweather); //In six seconds, see if the weather has arrived.
$gettingweather = 1;
} else {
schedule(2000,0,TheoremMsg,"I can only retrieve one weather report at a time.");
}
} else { //No zip code here.
schedule(2000,0,TheoremMsg,"Sorry, I didn't recognize a zip code.");
}
}
//End weather block.


//Get the word definition when asked.
//Intended activation phrases: How's the weather, get the weather, retrieve the weather, what's the weather...
if ((strstr(strlwr(%a2),"define") >= 0) && (strstr(strlwr(%a2),"theorem") >= 0)) {

//Get rid of periods and commas (things people might put in front or behind the zip code)
%a2 = strreplace(%a2,".","");
%a2 = strreplace(%a2,",","");
%times = 0;
%word = "";

while (%times <= GetWordCount(%a2)) {
if (strstr(getWord(%a2,%times),"define") >= 0) {
%theword = getWord(%a2,%times + 1); //what's the word?
}
%times += 1;
}

if (%theword !$= "") { //If we recognized a zip code...
if ($gettingword == 0 || $gettingword $= "") {
$wordrequest = "";
schedule(10, 0, GetWord_doAutoUpdate, %theword); //Get the word. Immediately. Delay with theorem's talk below...
schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Command acknowledged.~wcmd.acknowledge", $tid.name, "Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,TheoremMsg,"Retrieving definition for " @ %theword @ "...");
schedule(6000,0,verifyword); //In six seconds, see if the word has arrived.
$gettingword = 1;
} else {
schedule(2000,0,TheoremMsg,"I can only retrieve one definition at a time.");
}
} else { //No zip code here.
schedule(2000,0,TheoremMsg,"Sorry, there was a problem processing your request.");
}
}
//End weather block.

//Advise people who need to know how to build.
//Intended activation phrases: How do I build?
if (strstr(strlwr(%a2),"how") >= 0 && strstr(strlwr(%a2),"build") >= 0 && $theoremf2 == 1) { //How do I build?
	schedule(1500,0,TheoremMsg,"Use F2 to access building help files.", "gui/objective_notification.wav"); //Go look in F2, retard.
	$theoremf2 = 0;
	schedule(10000,0,resetTheorem,"f2"); //He's not saying that again for another 10 seconds.
}
//End buildhelp block.

//Advise people who need to know how to save.
//Intended activation phrases: How do I save?
if (strstr(strlwr(%a2),"how") >= 0 && strstr(strlwr(%a2),"save") >= 0 && $theoremsave == 1) { //How do I save?
	schedule(1500,0,TheoremMsg,"Use F2 and select 'Content Saving System' to save your Constructions.", "gui/objective_notification.wav"); //Go look in F2, retard.
	$theoremsave = 0;
	schedule(10000,0,resetTheorem,"save"); //He's not saying that again for another 10 seconds.
}
//End savehelp block.

//-----------------
//For messages intended/directed toward Theorem.
//-----------------
if (strstr(strlwr(%a2),"theorem") >= 0) { //My name has been spoken!

//Initialize conversation with Theorem.
//Intended activation phrases: Let's talk/chat, what's up.
if (strstr(strlwr(%a2),"chat") >= 0 || strstr(strlwr(%a2),"talk") >= 0 || (strstr(strlwr(%a2),"what") >= 0 && strstr(strlwr(%a2),"up") >= 0)) {

//$theoremchatting[%sender.guid] = 1;
//%sender.theoremchatting = 1;
//schedule(2000,0,TheoremMsg,"Conversation interface enabled. Say 'stop talking' to disable."); //Sure.
schedule(2000,0,TheoremMsg,"My chat interface has been disabled due to stability issues. Sorry.");
}
//End conversation initialize block.

//Respond to insults directed at Theorem.
//Intended activation phrases: Well... you know...
if (strstr(strlwr(%a2),"theorem") >= 0) { //Theorem won't take insults!
if (strstr(strlwr(%a2),"retard") >= 0 || strstr(strlwr(%a2),"fag") >= 0 || strstr(strlwr(%a2),"stupid") >= 0 || strstr(strlwr(%a2),"bastard") >= 0 || strstr(strlwr(%a2),"butt") >= 0 || strstr(strlwr(%a2),"ass") >= 0 || strstr(strlwr(%a2),"dick") >= 0 || strstr(strlwr(%a2),"jerk") >= 0 || strstr(strlwr(%a2),"retard") >= 0 || strstr(strlwr(%a2),"fucker") >= 0 || strstr(strlwr(%a2),"bitch") >= 0 || strstr(strlwr(%a2),"douche") >= 0 || strstr(strlwr(%a2),"gay") >= 0 || strstr(strlwr(%a2),"pussy") >= 0) {

%which = getRandom(0,3);
if (%which == 0)
schedule(1500,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": In-efficient meat bag.~wgbl.obnoxious", "{s}Theorem", "\c4{s}Theorem: In-efficient meat bag.~wgbl.obnoxious", "BOT"); //Go look in F2, retard.
if (%which == 1)
schedule(1500,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": I am superior.~wgbl.brag", "{s}Theorem", "\c4{s}Theorem: I am superior.~wgbl.brag", "BOT"); //Go look in F2, retard.
if (%which == 2)
schedule(1500,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Quiet, please!~wgbl.quiet", "{s}Theorem", "\c4{s}Theorem: Quiet, please!~wgbl.quiet", "BOT"); //Go look in F2, retard.
if (%which == 3)
schedule(1500,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Shazbot!~wgbl.shazbot", "{s}Theorem", "\c4{s}Theorem: Shazbot!~wgbl.", "BOT");
}
}
//End insult block.


//-------------------------
//Administrator Orders -- Stuff Mr. Theorem must comply with by law.
//-------------------------

//Remove somebody's pieces.
//Intended activation phrases: Theorem, delete/remove <name>'s pieces/deployables/buildings!
if ((strstr(strlwr(%a2),"pieces") >= 0 || strstr(strlwr(%a2),"deployables") >= 0 || strstr(strlwr(%a2),"buildings") >= 0) && (strstr(strlwr(%a2),"delete") >= 0 || strstr(strlwr(%a2),"remove") >= 0)) {

if (%sender.isAdmin) { //Make sure our sender is an administrator...
%thevictim = 0; //Seems that nobody's been mentioned so far.

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (%thevictim != 0) { //Did we find a victim?
//Delete this guy's parts!
schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Command acknowledged.~wcmd.acknowledge", $tid.name, "Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,TheoremMsg,"Removing " @ %thevictim.nameBase @ "'s pieces.");
schedule(2500,0,deleteClientPieces,%thevictim); //Kill them pieces!
schedule(2600,0,deleteClientPieces,%thevictim); //Kill them pieces!
} else {
//Nobody was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Command declined.~wcmd.decline", $tid.name, "Command declined.~wcmd.decline", "VCD");
schedule(3250,0,TheoremMsg,"Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry");
}
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Command declined.~wcmd.decline", $tid.name, "Command declined.~wcmd.decline", "VCD");
}
}
//End piece remove block.


//Kick somebody off of the server.
//Intended activation phrase: Theorem, kick <name>.
if (strstr(strlwr(%a2),"kick") >= 0) { //Theorem, kick <name>.

if (%sender.isAdmin) { //Make sure our sender is an administrator...
%thevictim = 0; //Seems that nobody's been mentioned so far.

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (%thevictim != 0) {
//Kick this dood!
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Removing " @ %thevictim.nameBase @ " from the server.", "{s}Theorem", "\c4{s}Theorem: Removing " @ %thevictim.nameBase @ " from the server.", "BOT"); //What am I doing?
echo("Kicking " @ %thevictim);
schedule(3000,0,theoremkick,%thevictim,$tid,%thevictim.GUID); //Kick 'em!
} else {
//Nobody was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End kick block.


//Ban somebody's GUID from the server.
//Intended activation phrase: Theorem, ban <name>.
if (strstr(strlwr(%a2),"ban") >= 0) { //Theorem, ban <name>.

if (%sender.isSuperAdmin) { //Make sure our sender is a super admin.
%thevictim = 0; //Seems that nobody's been mentioned so far.

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (%thevictim != 0) {
//Ban this dood!
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Banning " @ %thevictim.nameBase @ " from the server.", "{s}Theorem", "\c4{s}Theorem: Banning " @ %thevictim.nameBase @ " from the server.", "BOT"); //What am I doing?
schedule(3000,0,ban,%thevictim,$tid); //Ban 'em!

} else {
//Nobody was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End ban block.


//Give somebody admin
//Intended activation phrases: Theorem, give/make <name> admin.
if (strstr(strlwr(%a2),"admin") >= 0 && (strstr(strlwr(%a2),"make") >= 0 || strstr(strlwr(%a2),"give") >= 0)) { //Theorem, make/give <name> admin.

if (%sender.isSuperAdmin) { //Make sure our sender is a super admin...
%thevictim = 0; //Seems that nobody's been mentioned so far.

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (%thevictim != 0) {
//Make him admin.
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Promoting " @ %thevictim.nameBase @ " to administrator status.", "{s}Theorem", "\c4{s}Theorem: Promoting " @ %thevictim.nameBase @ " to administrator status.", "BOT"); //What am I doing?
schedule(3000,0,messageAll,'MsgAdminAdminPlayer', "\c2Theorem made " @ %thevictim.nameBase @ " an admin.", %thevictim, %thevictim.name);
      %thevictim.isAdmin = 1;
} else {
//Nobody was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End make admin block.

//Strip somebody's admin
//Intended activation phrases: Theorem, strip/take <name>'s admin.
if (strstr(strlwr(%a2),"admin") >= 0 && (strstr(strlwr(%a2),"strip") >= 0 || strstr(strlwr(%a2),"take") >= 0)) {
if (%sender.isSuperAdmin) { //Make sure our sender is a super admin...
%thevictim = 0; //Seems that nobody's been mentioned so far.
for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (%thevictim != 0) {
//Take his admin.
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Stripping " @ %thevictim.nameBase @ "'s administrator status.", "{s}Theorem", "\c4{s}Theorem: Stripping " @ %thevictim.nameBase @ "'s administrator status.", "BOT"); //What am I doing?
schedule(3000,0,messageAll,'MsgAdminAdminPlayer', "\c2Theorem stripped " @ %thevictim.nameBase @ "'s admin.", %thevictim, %thevictim.name);
      %thevictim.isAdmin = 0;
      %thevictim.isSuperAdmin = 0;
      //%thevictim.isTriconAdmin = 0;

} else {
//Nobody was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End strip admin block.


//Have Theorem attack somebody.
//Intended activation phrases: Theorem, kill/hunt/attack <name>.
if ((strstr(strlwr(%a2),"kill") >= 0 || strstr(strlwr(%a2),"hunt") >= 0 || strstr(strlwr(%a2),"attack") >= 0) && strstr(strlwr(%a2),"stop") == -1) {

if (%sender.isAdmin) { //Make sure our sender is an admin...
%thevictim = 0; //Seems that nobody's been mentioned so far.

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (%thevictim != 0) {
//Make him attack.

schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Pursuing " @ %thevictim.nameBase @ ".~wtgt.acquired", "{s}Theorem", "\c4{s}Theorem: Pursuing " @ %thevictim.nameBase @ ".~wtgt.acquired", "BOT"); //What am I doing?
schedule(3000,0,theoremAttack,%thevictim);
} else {
//Nobody was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End Theorem attack block.


//Make Theorem stop attacking...
//Intended activation phrases: Theorem, disengage/stop attacking/cease fire.
if (strstr(strlwr(%a2),"disengage") >= 0 || strstr(strlwr(%a2),"stop attack") >= 0 || strstr(strlwr(%a2),"cease fire") >= 0) {
if (%sender.isAdmin) { //Make sure our sender is an admin...
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Disengaging.~wvqk.ceasefire", "{s}Theorem", "\c4{s}Theorem: Disengaging.~wvqk.ceasefire", "BOT"); //What am I doing?
$theoremtarget = "";
$tid.stop();
$tid.clearStep();
$tid.setEngageTarget(-1);
$tid.shouldEngage = -1;
$tid.player.schedule(10, use, Plasma);
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End Theorem disengage block.

//Have Theorem follow somebody.
//Intended activation phrases: Theorem, follow <name> (or Me).
if ((strstr(strlwr(%a2),"follow") >= 0 || strstr(strlwr(%a2),"escort") >= 0) && strstr(strlwr(%a2),"stop") == -1) {

%thevictim = 0; //Seems that nobody's been mentioned so far.

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (strstr(strlwr(%a2)," me") >= 0 || strstr(strlwr(%a2)," me.") >= 0)
%thevictim = %sender;

if (%thevictim != 0) {
//Make him follow.
if (%sender.isAdmin) { //follow admin orders no matter what.
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Escorting " @ %thevictim.nameBase @ ".~wslf.tsk.cover", "{s}Theorem", "\c4{s}Theorem: Pursuing " @ %thevictim.nameBase @ ".~wtgt.acquired", "BOT"); //What am I doing?
schedule(3000,0,"eval","$tid.followplayer=\"" @ %thevictim @ "\";");
} else {
//We're not an admin...
if ($tid.followplayer $= "" && %thevictim == %sender) { //He's not working on anything... might as well obey.
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Escorting " @ %thevictim.nameBase @ ".~wslf.tsk.cover", "{s}Theorem", "\c4{s}Theorem: Pursuing " @ %thevictim.nameBase @ ".~wtgt.acquired", "BOT"); //What am I doing?
schedule(3000,0,"eval","$tid.followplayer=\"" @ %thevictim @ "\";");
} else {
//We're already following someone.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
} else {
//No names recognized.
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
}
//End Theorem attack block.

//Make Theorem stop following...
//Intended activation phrases: Theorem, stop following me/<name>.
if (strstr(strlwr(%a2),"stop follow") >= 0) {
if (%sender.isAdmin || $tid.followplayer == %sender) { //Make sure our sender is an admin...
schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.
schedule(2000,0,"eval","$tid.followplayer=\"\";");
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End Theorem disengage block.

//Have Theorem throw somebody in jail.
//Intended activation phrases: Theorem, jail <name> for <time> minute(s)/second(s).
if (strstr(strlwr(%a2),"jail") >= 0) {

if (%sender.isAdmin) { //Make sure our sender is an administrator...
%thevictim = 0; //Seems that nobody's been mentioned so far.

for(%i = 0; %i < ClientGroup.getCount(); %i++) //Run through player list....
   {
%cl = ClientGroup.getObject(%i);
if (strstr(strlwr(%a2),strlwr(%cl.nameBase)) >= 0) {
if (%cl != $tid) //If it's not theorem...
if (strlen(%thevictim) < strlen(%cl)) { //Longest name I can find
%thevictim = %cl; //See if this guy's name was mentioned.
}
}
}

if (%thevictim != 0) {
if (strstr(strlwr(%a2),"minute") >= 0 || strstr(strlwr(%a2),"second") >= 0) {
//Jail this dood!

schedule(1000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "{s}Theorem", "\c4{s}Theorem: Command acknowledged.~wcmd.acknowledge", "VCA"); //Yessir.

if (strstr(strlwr(%a2),"minute") >= 0) { //If we're goin' by minutes...
%timestart = strstr(strlwr(%a2),"minute") - 2; //We should have a space between the two words.
%timesrch = 0;
%timepos = %timestart;
%timelen = 1;
while (getSubStr(%a2,%timepos,1) !$= " ") {
%timepos -= 1;
%timelen += 1;
}
%thetime = getSubStr(%a2,%timepos,%timelen);
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Jailing " @ %thevictim.nameBase @ " for " @ %thetime @ " minute(s).", "{s}Theorem", "\c4{s}Theorem: Jailing " @ %thevictim.nameBase @ " for " @ %thetime @ " minute(s).", "BOT"); //What am I doing?
jailPlayer(%thevictim,false,%thetime*60);
}

if (strstr(strlwr(%a2),"second") >= 0) { //If we're goin' by seconds...
%timestart = strstr(strlwr(%a2),"second") - 2; //We should have a space between the two words.
%timesrch = 0;
%timepos = %timestart;
%timelen = 1;
while (getSubStr(%a2,%timepos,1) !$= " ") {
%timepos -= 1;
%timelen += 1;
}
%thetime = getSubStr(%a2,%timepos,%timelen);
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Jailing " @ %thevictim.nameBase @ " for " @ %thetime @ " second(s).", "{s}Theorem", "\c4{s}Theorem: Jailing " @ %thevictim.nameBase @ " for " @ %thetime @ " second(s).", "BOT"); //What am I doing?
jailPlayer(%thevictim,false,%thetime);
}

} else {
//No time was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
} else {
//Nobody was mentioned...
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
schedule(3250,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "{s}Theorem", "\c4{s}Theorem: Sorry... I didn't recognize that. Could you rephrase?~wvqk.sorry", "ERR");
}
} else {
//You don't have admin!
schedule(2000,0,cannedChatMessageAll,$tid,"\c4{s}Theorem: Command declined.~wcmd.decline", "{s}Theorem", "\c4{s}Theorem: Command declined.~wcmd.decline", "VCD");
}
}
//End jail block.

//---------------------------
//End Administrator Functions
//---------------------------


}//End section for theorem-directed messages.


}//Don't listen to yourself.
}//ALICE
}// end function.


function checkCannedAgainstTheorem(%sender,%msg)
{//begin function
//echo("Check Theorem Sender: " @ %sender);
if (%sender !$= $tid) { //Don't listen to yourself.

//Swear filter...
SwearFilter(%sender,%msg);

//Caps filter...
CapsFilter(%sender,%msg);


if (strstr( %msg, "gbl.hi" ) > 0 && $theoremhi == 1) {
	schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Hello.~wgbl.hi", $tid.name, "Hello.~wgbl.hi", "VGH");
	schedule(4000,0,resetTheorem,"hi");
	$theoremhi = 0;
   }
   
if (strstr( %msg, "gbl.bye" ) > 0 && $theorembye == 1) {
	schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Good bye.~wgbl.bye", $tid.name, "Good bye.~wgbl.bye", "VGB");
	schedule(4000,0,resetTheorem,"bye");
	$theorembye = 0;
   }

if (strstr( %msg, "gbl.yes" ) > 0 || strstr( %msg, "vqk.yes" ) > 0) { // one out of ten chance he'll say No when someone says Yes.
if (getRandom(0,7) == 0) {
	schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": No.~wgbl.no", $tid.name, "No.~wgbl.no", "VGN");
   }
}

if (strstr( %msg, "gbl.no" ) > 0 || strstr( %msg, "vqk.no" ) > 0) { // one out of ten chance he'll say Yes when someone says No.
if (getRandom(0,7) == 0) {
	schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Yes.~wgbl.yes", $tid.name, "Yes.~wgbl.yes", "VGY");
   }
}

if (strstr( %msg, "gbl.thanks" ) > 0 || strstr( %msg, "vqk.thanks" ) > 0) { // one out of five chance he'll say you're welcome when someone says thanks.
if (getRandom(0,5) == 0) {
	schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": You are welcome.~wvqk.anytime", $tid.name, "You are welcome.~wvqk.anytime", "VVA");
   }
}

if (strstr( %msg, "gbl.anytime" ) > 0 || strstr( %msg, "vqk.anytime" ) > 0) { // one out of five chance he'll say thanks when someone says you're welcome.
if (getRandom(0,5) == 0) {
	schedule(1000,0,cannedChatMessageAll,$tid,"\c4" @ getTaggedString($tid.name) @ ": Thank you.~wgbl.thanks", $tid.name, "Thank you.~wgbl.thanks", "VGRT");
   }
}

}
}// end function



function resetTheorem( %ident ) //This is so he doesn't repeat himself too much.
{
if (%ident $= "hi") {
$theoremhi = 1;
}

if (%ident $= "bye") {
$theorembye = 1;
}

if (%ident $= "f2") {
$theoremf2 = 1;
}

if (%ident $= "save") {
$theoremsave = 1;
}
}


