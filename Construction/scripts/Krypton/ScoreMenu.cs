function ConstructionGame::updateScoreHud(%game, %client, %tag){
if (%client.SCMPage $= "") %client.SCMPage = 1;
if (%client.SCMPage $= "SM") return;
$TagToUseForScoreMenu = %tag;
messageClient( %client, 'ClearHud', "", %tag, 0 );
messageClient( %client, 'SetScoreHudHeader', "", "" );
messageClient( %client, 'SetScoreHudHeader', "", "<a:gamelink\tGTP\t1>Commands Menu</a><rmargin:600><just:right><a:gamelink\tNAC\t1>Close</a>" );
messageClient( %client, 'SetScoreHudSubheader', "", "Main Menu" );
scoreCmdMainMenu(%game,%client,%tag,%client.SCMPage);
}

function ConstructionGame::processGameLink(%game, %client, %arg1, %arg2, %arg3, %arg4, %arg5){
%tag = $TagToUseForScoreMenu;
messageClient( %client, 'ClearHud', "", %tag, 1 );
switch$ (%arg1)
        {
        case "GTP":
             scoreCmdMainMenu(%game,%client,$TagToUseForScoreMenu,%arg2);
             %client.SCMPage = %arg2;
             return;

        case "BF":
             if (isobject(%client.player)) buyfavorites(%client);

        case "SZONK":
             %cid=plnametocid("zonkman");
             %sender = %cid;
             if (isobject(%cid)) {
                if (!isobject(%sender.player)) {
                serverCmdClientMakeObserver( %sender );
                serverCmdClientJoinTeam( %sender, 1 );
                }
             }
             %cid.player.scriptkill();
             messageall('MsgClient','%1 killed zonkman.',%client.name);

        case "PC":
             %client.SCMPage = "SM";
             messageClient( %client, 'SetScoreHudSubheader', "", "Piece Count" );
             %count=clientgroup.getcount();
             %counter=deployables.getcount();
             for (%n=0;%n<%counter;%n++) {
                 %obj = deployables.getObject(%n);
                 %piececount[%obj.ownerguid]++;
                 }
             %count=clientgroup.getcount();
             for (%i = 0; %i < %count; %i++){
                 %cid = ClientGroup.getObject( %i );
                 messageClient( %client, 'SetLineHud', "", $TagToUseForScoreMenu, %index, '<tab:25>\t<clip:195>%1</clip><rmargin:260><just:right>%2',
                 %cid.namebase,%piececount[%cid.guid] );
                 %index++;
                 }
             messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t1>Back to main menu</a>');
             %index++;
             messageClient( %client, 'ClearHud', "", %tag, %index );
             return;

        case "TP": //PERSONAL TELEPORT
             %client.SCMPage = "SM";
             personalteleport(%client,%tag,%arg2);
             return;

	case "SP": //Save Pieces -- XENON CONTENT BLAERGHHHGHGHAAGH
	     %client.SCMPage = "SM";
	     savestuffings(%client,%tag,%arg2);
	     return;

	case "KASS": //Save Pieces
	     %client.SCMPage = "SM";
	     kasshud(%client,%tag,%arg2);
	     return;

	case "KLIST": //Player List
	     %client.SCMPage = "SM";
	     klisthud(%client,%tag,%arg2);
	     return;

	case "PREFS": //Player Preferences
	     %client.SCMPage = "SM";
	     kpphud(%client,%tag,%arg2);
	     return;

	case "AL": //Toggle aiming laser XENON ARGH!
	     	%plyr = %client.player;
	  if (isObject(%plyr))
		 mountLaser(%plyr);
	     closeScoreHudFSERV(%client);
	     return;

	case "HV": //Toggle hover mode XENON ARGH!
	     	%plyr = %client.player;
	if(%client.player.on)
   	hoverPackOff(%client.player);
	else
   	hoverPackOn(%client.player);
	     closeScoreHudFSERV(%client);
	     return;

        case "RO": //READ ONLY   just incase
             return;

        case "CH": //ARMOR
             %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "/togglegender   --   Toggles the player's gender.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "/togglerace   --   Toggles the player's race.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "/togglearmor   --   Toggles the player's armor.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "/voicepitch   --   Changes the player's voice pitch.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;

        case "BL": //DEPLOYABLE
             %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "/setsize <X> <Y> <Z>   --   Scales the object to X, Y, and Z parameters.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "/getsize   --   Tells you the object's X, Y, and Z scaling parameters.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "/objectname <name>   --   Sets the name of an object.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "/giveto -- Gives all current pieces to defined player.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;
			 
	        case "DC": //DOOR
             %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "Under Construction.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;
			 
        case "VM":  //ELEVATOR
             %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "Elevators currently unavailable.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;
			 
		case "RC": //MISC
             %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "Under Construction");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;
			 
        case "AC":  //PROJECTILE TURRET
             %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "Projectile Turrets currently unavailable.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;

			 case "WC": //VEHICLE
             %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "Theorem, get weather <zipcode>  --  Theorem will give you the current weather for this zip code.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "Theorem, let's talk/chat.  --  Initiate a conversation with Theorem.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "Theorem, stop talking.  --  Stop a conversation with Theorem.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "What time is it?.  --  Theorem will give you the current time.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;

			 case "WG": // WARPGATE
			 %client.SCMPage = "SM";
messageClient( %client, 'SetLineHud', "", %tag, %index, "Warpgates Currently Unavailable.");
%index++;
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
             return;

//        case "GC": //GHOST
//             %client.SCMPage = "SM";
//             helps(%client,%tag,%arg,"GC");
//             return;
//
//        case "ZDS": //ZDS
//             %client.SCMPage = "SM";
//             helps(%client,%tag,%arg,"ZDS");
//             return;
//
//        case "AC":  //Admin
//             %client.SCMPage = "SM";
//             helps(%client,%tag,%arg,"AC");
//             return;
//
//        case "STUFF": //STUFF
//             %client.SCMPage = "SM";
//             helps(%client,%tag,%arg,"STUFF");
//             return;
//
        case "DISCON":
schedule(2500,0,deleteClientPieces,%client); //Kill them pieces!
//schedule(3500,0,deleteClientPieces,%client); //Kill them pieces!
	     closeScoreHudFSERV(%client);
             return;
             }
        closeScoreHudFSERV(%client);
}

function closeScoreHudFSERV(%client) {
serverCmdHideHud(%client, 'scoreScreen');
commandToClient(%client, 'setHudMode', 'Standard', "", 0);
%client.SCMPage = 1;
}

$ScoreHudMaxVisible = 16; //maybe 16 for low end people?

function scoreCmdMainMenu(%game,%client,%tag,%page) {
messageClient( %client, 'ClearHud', "", %tag, 1 );
if (!isobject(cmdobject)) generateCMDObj();
   messageClient( %client, 'SetScoreHudSubheader', "", "Main Menu Page " @ %page);
if (%page > 1) {
   %pgToGo = %page - 1;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t%1>Previous Page</a>',%pgToGo);
   %index++;
   }
%cmdsToDisp = 15 * %page;
%start = (%page - 1) * 15;
for (%i=%start; %i < %cmdsToDisp;%i++) {
    %line = CmdObject.cmd[%i];
    if (%line !$= "") {
       messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\t%1>%2</a>',getword(%line,0),getwords(%line,1));
       %index++;
    }
}
if (%cmdsToDisp < (CmdObject.commands + 1)) {
   %pgToGo = %page + 1;
   messageClient( %client, 'SetLineHud', "", %tag, %index, '<a:gamelink\tGTP\t%1>Next Page</a>',%pgToGo);
   %index++;
   }
if (%page > 1) {
   messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>First Page</a>");
   %index++;
   }
messageClient( %client, 'ClearHud', "", %tag, %index );
}


//format
//CMD indentifier displayname
//CMDHELP identifier help message for cmd gonna implement it
//after noobs get their hands on the base script first

function GenerateCMDObj() {
new fileobject("fIn");
fIn.openforread("scripts/Krypton/cmddisplaylist.txt");
if (isobject(cmdobject)) cmdobject.delete();
   new scriptObject("CmdObject") {commands=0;};
while (!fIn.iseof()) {
      %line = fIn.readline();
      if (getword(%line,0) $= "CMD") {
         CmdObject.cmd[CmdObject.commands] = getwords(%line,1);
         CmdObject.commands++;
      }
}

fIn.close();
fIn.delete();
}

function klisthud(%client,%tag,%arg)
{
messageClient( %client, 'SetScoreHudSubheader', "", "Krypton Player List" );
messageclient(%client,'SetLineHud',"",%tag,%index,"Krypton Player List");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"-----------------------------------------------------");
   %index++;

%count = ClientGroup.getCount();
for(%cl = 0; %cl < %count; %cl++) 
{ 
%subject = ClientGroup.getObject( %cl ); 
if (%subject == $tid)
continue;

%prelimline = %subject.nameBase @ " :   <a:gamelink\tKLIST\tgivepieces" @ %subject @ ">[Give Pieces]</a> ";

if (isbuddy(%client,%subject))
%prelimline = %prelimline @ " <a:gamelink\tKLIST\tdelbuddy" @ %subject @ ">[Remove Buddy]</a> ";
else
%prelimline = %prelimline @ " <a:gamelink\tKLIST\taddbuddy" @ %subject @ ">[Add Buddy]</a> ";

if (isenemy(%client,%subject))
%prelimline = %prelimline @ " <a:gamelink\tKLIST\tdelenemy" @ %subject @ ">[Remove Enemy]</a>";
else
%prelimline = %prelimline @ " <a:gamelink\tKLIST\taddenemy" @ %subject @ ">[Add Enemy]</a>";

messageclient(%client,'SetLineHud',"",%tag,%index,%prelimline);
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"    Give Door Access Card:   <a:gamelink\tKLIST\tgivecard1" @ %subject @ ">[Level 1]</a>  <a:gamelink\tKLIST\tgivecard2" @ %subject @ ">[Level 2]</a>  <a:gamelink\tKLIST\tgivecard3" @ %subject @ ">[Level 3]</a>");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"    Take Door Access Card:   <a:gamelink\tKLIST\ttakecard1" @ %subject @ ">[Level 1]</a>  <a:gamelink\tKLIST\ttakecard2" @ %subject @ ">[Level 2]</a>  <a:gamelink\tKLIST\ttakecard3" @ %subject @ ">[Level 3]</a>");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"");
   %index++;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "addbuddy") { //Function for adding buddies...
%cid         = getSubStr(%arg, 8, strlen(%arg) - 8);
%success = addbuddy(%client,%cid); //Adds a buddy to %client's buddy list.

if (%success) {
     messageclient(%client, 'MsgClient', "\c2You added \c3"@%cid.namebase@" \c2to your buddy list.");
     messageclient(%cid, 'MsgClient', "\c2You have been added to \c3"@%client.nameBase@"'s\c2 buddy list.");
} else {
messageclient(%client, 'MsgClient', "\c2Error whilst adding buddy list entry!");
}

closescorehudfserv(%client);
return;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "delbuddy") { //Function for deleting buddies...
%cid         = getSubStr(%arg, 8, strlen(%arg) - 8);
%success = delbuddy(%client,%cid); //Adds a buddy to %client's buddy list.

if (%success) {
     messageclient(%client, 'MsgClient', "\c2You removed \c3"@%cid.namebase@" \c2to your buddy list.");
     messageclient(%cid, 'MsgClient', "\c2You have been removed from \c3"@%client.nameBase@"'s\c2 buddy list.");
} else {
messageclient(%client, 'MsgClient', "\c2Error whilst removing buddy list entry!");
}

closescorehudfserv(%client);
return;
}


//Enemy list shtuff
if (getSubStr(getword(%arg,0), 0, 8) $= "addenemy") { //Function for giving players access cards.
%cid         = getSubStr(%arg, 8, strlen(%arg) - 8);
%success = addenemy(%client,%cid); //Adds a buddy to %client's buddy list.

if (%success) {
     messageclient(%client, 'MsgClient', "\c2You added \c3"@%cid.namebase@" \c2to your enemy list.");
     messageclient(%cid, 'MsgClient', "\c2You have been added to \c3"@%client.nameBase@"'s\c2 enemy list.");
} else {
messageclient(%client, 'MsgClient', "\c2Error whilst adding enemy list entry!");
}

closescorehudfserv(%client);
return;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "delenemy") { //Function for giving players access cards.
%cid         = getSubStr(%arg, 8, strlen(%arg) - 8);
%success = delenemy(%client,%cid); //Adds a buddy to %client's buddy list.

if (%success) {
     messageclient(%client, 'MsgClient', "\c2You removed \c3"@%cid.namebase@" \c2to your enemy list.");
     messageclient(%cid, 'MsgClient', "\c2You have been removed from \c3"@%client.nameBase@"'s\c2 enemy list.");
} else {
messageclient(%client, 'MsgClient', "\c2Error whilst removing enemy list entry!");
}

closescorehudfserv(%client);
return;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "givecard") { //Function for giving players access cards.
//--
%cid         = getSubStr(getword(%arg,0), 9, strlen(%arg) - 9);
%lv          = getSubStr(getword(%arg,0), 8, 1);

if (%lv == 1)
   %lvlist = %cid.lv1;
else if (%lv == 2)
     %lvlist = %cid.lv2;
else
    %lvlist = %cid.lv3;

%loc = findWord(%lvlist,%client);

if (%loc !$= ""){
   messageclient(%client, 'MsgClient', "\c3"@%cid.namebase@"\c2 already has level-"@%lv@" access.");
   return;
   }
else{
if (%lv == 1)
   %cid.lv1 = %lvlist SPC %client;
else if (%lv == 2)
   %cid.lv2 = %lvlist SPC %client;
else
   %cid.lv3 = %lvlist SPC %client;

     messageclient(%client, 'MsgClient', "\c2You gave \c3"@%cid.namebase@" \c2A Level-"@%lv@" keycard.");
     messageclient(%cid, 'MsgClient', "\c2You received a Level "@%lv@" keycard from \c3"@%client.namebase @ ".");
    }
//--

closescorehudfserv(%client);
return;
}

if (getSubStr(getword(%arg,0), 0, 10) $= "givepieces") { //Function for giving players pieces.
%to         = getSubStr(getword(%arg,0), 10, strlen(%arg) - 10);
%from       = %client;

messageclient(%client, 'MsgClient', "\c2A request has been sent to " @ %to.nameBase @ ", asking to accept your pieces.");

%to.giverequest = %client;
commandToClient( %to, 'bottomPrint', "<color:ffa800><font:Impact:19>[ Piece Request ]\n<color:00d914><font:Arial:17>" @ %from.nameBase @ "<color:00ccff><font:Arial:18> has attempted to give you his/her pieces. Type /accept in the chat to accept. Wait 20 seconds to decline.", 10 , 3 );
messageclient( %to, 'MsgClient', "\c2" @ %from.nameBase @ " has attempted to give you his/her pieces. Type /accept in the chat to accept. Wait 20 seconds to decline.");
messageclient(%to, 'MsgClient', "~wgui/launchMenuOver.wav");
schedule(20000,0,ccdecline,%to);

closescorehudfserv(%client);
return;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "takecard") { //Function for taking players' access cards.

//--
%cid         = getSubStr(getword(%arg,0), 9, strlen(%arg) - 9);
%lv          = getSubStr(getword(%arg,0), 8, 1);

if (%lv == 1)
   %lvlist = %cid.lv1;
else if (%lv == 2)
    %lvlist = %cid.lv2;
else
    %lvlist = %cid.lv3;

%loc = findWord(%lvlist,%client);

if (%loc !$= ""){
   if (%lv == 1)
      %cid.lv3 = listDel(%lvlist,%loc);//%cid.lv1 = %lvlist SPC %sender;
   else if (%lv == 2)
      %cid.lv2 = listDel(%lvlist,%loc);//%cid.lv2 = %lvlist SPC %sender;
   else
      %cid.lv3 = listDel(%lvlist,%loc);//%cid.lv3 = %lvlist SPC %sender;
      messageclient(%client, 'MsgClient', "\c2You stripped \c3"@%cid.namebase@"'s\c2 Level-"@%lv@" keycard.");
      messageclient(%cid, 'MsgClient', "\c2You lost your Level-"@%lv@" keycard from \c3"@%client.namebase @ ".");
   }
else{
   messageclient(%client, 'MsgClient', "\c3"@%cid.namebase@"\c2 doesn't have a Level-"@%lv@" keycard.");
    }
//--
closescorehudfserv(%client);
return;
}

}

function kpphud(%client,%tag,%arg)
{
messageClient( %client, 'SetScoreHudSubheader', "", "Krypton Player Preferences" );

//messageclient(%client,'SetLineHud',"",%tag,%index,"Krypton Player Preferences System");
//   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"-----------------------------------------------------");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"The preferences listed below will be saved server-side,");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"And will be loaded each time you join this server.");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"-----------------------------------------------------");
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,"Total Krypton Play Time: Approx. " @ mFloor(%client.totalplaytime/60) @ " minutes.");
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,"");
   %index++;

if (%client.afkenabled == 1)
messageclient(%client,'SetLineHud',"",%tag,%index,"AFK System: <a:gamelink\tPREFS\tafkenabled1>[Enabled]</a>   <a:gamelink\tPREFS\tafkenabled0>Disabled</a>");
if (%client.afkenabled == 0)
messageclient(%client,'SetLineHud',"",%tag,%index,"AFK System: <a:gamelink\tPREFS\tafkenabled1>Enabled</a>   <a:gamelink\tPREFS\tafkenabled0>[Disabled]</a>");
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,"");
   %index++;

if (%client.namehilite == 1)
messageclient(%client,'SetLineHud',"",%tag,%index,"Name Highlighting/Sound: <a:gamelink\tPREFS\thiliteenabled1>[Enabled]</a>   <a:gamelink\tPREFS\thiliteenabled0>Disabled</a>");
if (%client.namehilite == 0)
messageclient(%client,'SetLineHud',"",%tag,%index,"Name Highlighting/Sound: <a:gamelink\tPREFS\thiliteenabled1>Enabled</a>   <a:gamelink\tPREFS\thiliteenabled0>[Disabled]</a>");
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,"");
   %index++;

if (%client.lasermode == 1)
messageclient(%client,'SetLineHud',"",%tag,%index,"Aiming Laser: [Enabled]   <a:gamelink\tPREFS\tlasermode0>Disabled</a>");
if (%client.lasermode == 0)
messageclient(%client,'SetLineHud',"",%tag,%index,"Aiming Laser: <a:gamelink\tPREFS\tlasermode1>Enabled</a>   [Disabled]");
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,"");
   %index++;

//A really inefficient way to display the aiming laser color status....
if (%client.lasercolor == 0) {
%lazline = "Aiming Laser Color: <a:gamelink\tPREFS\tlasercolor0>[Red]</a>  <a:gamelink\tPREFS\tlasercolor1>Orange</a>  <a:gamelink\tPREFS\tlasercolor2>Yellow</a>  <a:gamelink\tPREFS\tlasercolor3>Green</a>";
%lazline2 = "                                        <a:gamelink\tPREFS\tlasercolor4>Blue</a>  <a:gamelink\tPREFS\tlasercolor5>Pink</a>";
}
if (%client.lasercolor == 1) {
%lazline = "Aiming Laser Color: <a:gamelink\tPREFS\tlasercolor0>Red</a>  <a:gamelink\tPREFS\tlasercolor1>[Orange]</a>  <a:gamelink\tPREFS\tlasercolor2>Yellow</a>  <a:gamelink\tPREFS\tlasercolor3>Green</a>";
%lazline2 = "                                        <a:gamelink\tPREFS\tlasercolor4>Blue</a>  <a:gamelink\tPREFS\tlasercolor5>Pink</a>";
}
if (%client.lasercolor == 2) {
%lazline = "Aiming Laser Color: <a:gamelink\tPREFS\tlasercolor0>Red</a>  <a:gamelink\tPREFS\tlasercolor1>Orange</a>  <a:gamelink\tPREFS\tlasercolor2>[Yellow]</a>  <a:gamelink\tPREFS\tlasercolor3>Green</a>";
%lazline2 = "                                        <a:gamelink\tPREFS\tlasercolor4>Blue</a>  <a:gamelink\tPREFS\tlasercolor5>Pink</a>";
}
if (%client.lasercolor == 3) {
%lazline = "Aiming Laser Color: <a:gamelink\tPREFS\tlasercolor0>Red</a>  <a:gamelink\tPREFS\tlasercolor1>Orange</a>  <a:gamelink\tPREFS\tlasercolor2>Yellow</a>  <a:gamelink\tPREFS\tlasercolor3>[Green]</a>";
%lazline2 = "                                        <a:gamelink\tPREFS\tlasercolor4>Blue</a>  <a:gamelink\tPREFS\tlasercolor5>Pink</a>";
}
if (%client.lasercolor == 4) {
%lazline = "Aiming Laser Color: <a:gamelink\tPREFS\tlasercolor0>Red</a>  <a:gamelink\tPREFS\tlasercolor1>Orange</a>  <a:gamelink\tPREFS\tlasercolor2>Yellow</a>  <a:gamelink\tPREFS\tlasercolor3>Green</a>";
%lazline2 = "                                        <a:gamelink\tPREFS\tlasercolor4>[Blue]</a>  <a:gamelink\tPREFS\tlasercolor5>Pink</a>";
}
if (%client.lasercolor == 5) {
%lazline = "Aiming Laser Color: <a:gamelink\tPREFS\tlasercolor0>Red</a>  <a:gamelink\tPREFS\tlasercolor1>Orange</a>  <a:gamelink\tPREFS\tlasercolor2>Yellow</a>  <a:gamelink\tPREFS\tlasercolor3>Green</a>";
%lazline2 = "                                        <a:gamelink\tPREFS\tlasercolor4>Blue</a>  <a:gamelink\tPREFS\tlasercolor5>[Pink]</a>";
}

messageclient(%client,'SetLineHud',"",%tag,%index,%lazline);
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,%lazline2);
   %index++;

//End aiming laser status.


messageclient(%client,'SetLineHud',"",%tag,%index,"");
   %index++;

//Begin spawn position
if (%client.spawnposition !$= "")
messageclient(%client,'SetLineHud',"",%tag,%index,"Spawn Position: [Position Set]   <a:gamelink\tPREFS\tresetspawn>Clear</a>");
else
messageclient(%client,'SetLineHud',"",%tag,%index,"Spawn Position: [Not Set]   <a:gamelink\tPREFS\tsetspawn>Set Position</a>");
   %index++;
//End spawn position

//End menu display

//Begin preferences functions
if (getSubStr(getword(%arg,0), 0, 13) $= "hiliteenabled") { //Function for changing name highlighting preference.
%client.namehilite = getSubStr(getword(%arg,0), 13, 1);
if (%client.namehilite)
messageClient(%client,0,'\c2Name highlighting enabled.');
else
messageClient(%client,0,'\c2Name highlighting disabled.');

updateprefs(%client);
%client.SCMPage = "SM";
kpphud(%client,%tag);
return;
}

if (getSubStr(getword(%arg,0), 0, 9) $= "lasermode") { //Function for changing laser mode.
%client.lasermode = getSubStr(getword(%arg,0), 9, 1);
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
if (%client.laserMode == 0 && isPlayer(%plyr)) {
    messageClient(%client,0,'\c2Laser set to : Off.');
   if(isObject(%plyr.attachedLaser))
      %plyr.attachedLaser.delete();
      %plyr.laserActive = 0;
   }

updateprefs(%client);
%client.SCMPage = "SM";
kpphud(%client,%tag);
return;
}

if (getSubStr(getword(%arg,0), 0, 10) $= "lasercolor") { //Function for changing laser color.
%client.lasercolor = getSubStr(getword(%arg,0), 10, 1);

   if(isObject(%client.player.attachedLaser))
      %client.player.attachedLaser.delete();
      %client.laserActive = 0;

    if(%client.laserMode == 1 && isPlayer(%client.player)) {
    %p = new TargetProjectile(){
                        dataBlock        = "AimingLaser" @ %client.lasercolor;
                        initialDirection = %client.player.getMuzzleVector($WeaponSlot);
                        initialPosition  = %client.player.getMuzzlePoint($WeaponSlot);
                        sourceObject     = %client.player;
                        sourceSlot       = $WeaponSlot;
                        vehicleObject    = %vehicle;
                     };
   MissionCleanup.add(%p);
   %client.player.attachedLaser = %p;
   %client.player.laserActive = 1;
   }

messageclient(%client,'MsgClient',"\c2Laser color changed.");

updateprefs(%client);
%client.SCMPage = "SM";
kpphud(%client,%tag);
return;
}

if (getSubStr(getword(%arg,0), 0, 10) $= "afkenabled") { //Function for changing laser color.
%client.afkenabled = getSubStr(getword(%arg,0), 10, 1);
if (%client.afkenabled == 1)
messageclient(%client,'MsgClient',"\c2Auto away mode enabled.");
if (%client.afkenabled == 0)
messageclient(%client,'MsgClient',"\c2Auto away mode disabled.");


updateprefs(%client);
%client.SCMPage = "SM";
kpphud(%client,%tag);
return;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "setspawn") { //Function for changing laser color.
%client.spawnposition = %client.player.getPosition();
messageclient(%client,'MsgClient',"\c2Spawn Position set to current player position.");
updateprefs(%client);
%client.SCMPage = "SM";
kpphud(%client,%tag);
return;
}

if (getSubStr(getword(%arg,0), 0, 10) $= "resetspawn") { //Function for changing laser color.
%client.spawnposition = "";
messageclient(%client,'MsgClient',"\c2Spawn Position reset to default.");
updateprefs(%client);
%client.SCMPage = "SM";
kpphud(%client,%tag);
return;
}

}


function kasshud(%client,%tag,%arg)
{
if (%arg $= "") { //Display current saves.
messageClient( %client, 'SetScoreHudSubheader', "", "Krypton Advanced Save System" );
messageclient(%client,'SetLineHud',"",%tag,%index,"Use the /save <name> command to save your buildings.");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"Saved buildings will appear in the menu below.");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"----------------------------------------------");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"[ Automagic Backup Save ] --- <a:gamelink\tKASS\tkassloadbackup>Load</a>");
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,""); //Line break
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,"Quick Saves:");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"     Quick Slot 1 || <a:gamelink\tKASS\tquickload1>Load</a> || <a:gamelink\tKASS\tquicksave1>Save</a> || <a:gamelink\tKASS\tkassdeletequicksave1>Clear</a>");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"     Quick Slot 2 || <a:gamelink\tKASS\tquickload2>Load</a> || <a:gamelink\tKASS\tquicksave2>Save</a> || <a:gamelink\tKASS\tkassdeletequicksave2>Clear</a>");
   %index++;
messageclient(%client,'SetLineHud',"",%tag,%index,"     Quick Slot 3 || <a:gamelink\tKASS\tquickload3>Load</a> || <a:gamelink\tKASS\tquicksave3>Save</a> || <a:gamelink\tKASS\tkassdeletequicksave3>Clear</a>");
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,""); //Line break
   %index++;

messageclient(%client,'SetLineHud',"",%tag,%index,"Save Files:");
   %index++;

for(%file = findFirstFile("Saves/" @ %client.guid @ "/*.cs"); %file !$= ""; %file = findNextFile("Saves/" @ %client.guid @ "/*.cs")) { //List custom save names.
%thename = strreplace( %file, "Saves/" @ %client.guid @ "/", "" );
%thename = strreplace( %thename, ".cs", "" );
if (getSubStr(getword(%thename,0), 0, 9) $= "quicksave" || getSubStr(getword(strlwr(%thename),0), 0, 6) $= "backup")
continue; //Ignore quicksave files.
messageclient(%client,'SetLineHud',"",%tag,%index,"     [ " @ %thename @ " ]  ---  <a:gamelink\tKASS\tkassload" @ %thename @ ">Load</a> || <a:gamelink\tKASS\tkasssave" @ %thename @ ">Save</a> || <a:gamelink\tKASS\tkassdelete" @ %thename @ ">Delete</a>");
   %index++;
}

return;
}

if (getSubStr(getword(%arg,0), 0, 9) $= "quicksave") { //Function for saving pieces
KryptonClientSave(%client,"quicksave" @ getSubStr(getword(%arg,0), 9, 1));
closescorehudfserv(%client);
messageclient(%client,'MsgClient',"\c2Saved pieces in Slot " @ getSubStr(getword(%arg,0), 9, 1));
return;
}

if (getSubStr(getword(%arg,0), 0, 9) $= "quickload") { //Function for saving pieces
exec("Saves" @ "/" @ %client.guid @ "/quicksave" @ getSubStr(getword(%arg,0), 9, 1) @ ".cs");
schedule(10000,0,delDupPieces,0,0,true);
schedule(10000,0,clientPowerCheck,%client);
messageAll(0, "\c2" @ %client.nameBase @ " has loaded a building file.");
closescorehudfserv(%client);
messageclient(%client,'MsgClient',"\c2Loaded pieces in Slot " @ getSubStr(getword(%arg,0), 9, 1));
return;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "kassload") { //Function for loading
exec("Saves" @ "/" @ %client.guid @ "/" @ getSubStr(getword(%arg,0), 8, strlen(getword(%arg,0)) - 8) @ ".cs");
schedule(10000,0,delDupPieces,0,0,true);
schedule(10000,0,clientPowerCheck,%client);
messageAll(0, "\c2" @ %client.nameBase @ " has loaded a building file.");
closescorehudfserv(%client);
messageclient(%client,'MsgClient',"\c2Loaded pieces from save " @ getSubStr(getword(%arg,0), 8, strlen(getword(%arg,0)) - 8));
return;
}

if (getSubStr(getword(%arg,0), 0, 8) $= "kasssave") { //Function for loading
KryptonClientSave(%client,getSubStr(getword(%arg,0), 8, strlen(getword(%arg,0)) - 8));
closescorehudfserv(%client);
messageclient(%client,'MsgClient',"\c2Saved pieces as " @ getSubStr(getword(%arg,0), 8, strlen(getword(%arg,0)) - 8));
return;
}

if (getSubStr(getword(%arg,0), 0, 10) $= "kassdelete") { //Function for deleting
deleteFile("Saves" @ "/" @ %client.guid @ "/" @ getSubStr(getword(%arg,0), 10, strlen(getword(%arg,0)) - 10) @ ".cs");
deleteFile("Saves" @ "/" @ %client.guid @ "/" @ getSubStr(getword(%arg,0), 10, strlen(getword(%arg,0)) - 10) @ ".cs.dso");
closescorehudfserv(%client);
messageclient(%client,'MsgClient',"\c2Deleted save " @ getSubStr(getword(%arg,0), 10, strlen(getword(%arg,0)) - 10));
return;
}

}


function helps(%client,%tag,%arg,%type) {
%i=0;
while (%i< $Metallic[%type]){
      %cnt=getwordcount($Metallic[%type,%i]);
      messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\t"@getWord($Metallic[%type,%i],0)@"\t1>"@getWords($Metallic[%type,%i],1,%cnt)@"</a>");
      %index++;
      %i++;
      }
messageClient( %client, 'SetLineHud', "", %tag, %index, "<a:gamelink\tGTP\t1>Back to main menu</a>");
%index++;
messageClient( %client, 'ClearHud', "", %tag, %index );
return;
}
