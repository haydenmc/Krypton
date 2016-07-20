$MaxMessageWavLength = 5200;

function asay(%msg) //Publish a message as "console"
{
MessageAll("","\c4Console: " @ %msg);
echo("\c4Console: " @ %msg);
}

function addMessageCallback(%msgType, %func)
{
   for(%i = 0; (%afunc = $MSGCB[%msgType, %i]) !$= ""; %i++)
   {
      // only add each callback once
      if(%afunc $= %func)
         return;
   }
   $MSGCB[%msgType, %i] = %func;
}

function messagePump(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7 ,%a8, %a9, %a10)
{
   clientCmdServerMessage(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10);
}

function clientCmdServerMessage(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
   %tag = getWord(%msgType, 0);
   for(%i = 0; (%func = $MSGCB["", %i]) !$= ""; %i++)
      call(%func, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10);
   
   if(%tag !$= "")
      for(%i = 0; (%func = $MSGCB[%tag, %i]) !$= ""; %i++)
         call(%func, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10);
}

function defaultMessageCallback(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
   if ( %msgString $= "" )
      return;
      
   %message = detag( %msgString );
   // search for wav tag marker
   %wavStart = strstr( %message, "~w" );
   if ( %wavStart != -1 )
   {
      %wav = getSubStr( %message, %wavStart + 2, 1000 );
      %wavLengthMS = alxGetWaveLen( %wav );
      if ( %wavLengthMS <= $MaxMessageWavLength )
      {
         %handle = alxCreateSource( AudioChat, %wav );
         alxPlay( %handle );
      }
      else
         error( "WAV file \"" @ %wav @ "\" is too long! **" );

      %message = getSubStr( %message, 0, %wavStart );
      if ( %message !$= "" )
      	addMessageHudLine( %message );
   }
   else
	  addMessageHudLine( %message );
}

//--------------------------------------------------------------------------
function handleClientJoin(%msgType, %msgString, %clientName, %clientId, %targetId, %isAI, %isAdmin, %isSuperAdmin, %isSmurf, %guid)
{
   logEcho("got client join: " @ detag(%clientName) @ " : " @ %clientId);

	//create the player list group, and add it to the ClientConnectionGroup...
   if(!isObject("PlayerListGroup"))
	{
      %newGroup = new SimGroup("PlayerListGroup");
		ClientConnectionGroup.add(%newGroup);
	}

   %player = new ScriptObject() 
   {
      className = "PlayerRep";
      name = detag(%clientName);
      guid = %guid;
      clientId = %clientId;
      targetId = %targetId;
      teamId = 0; // start unassigned
      score = 0;
      ping = 0;
      packetLoss = 0;
      chatMuted = false;
      canListen = false;
      voiceEnabled = false;
      isListening = false;
      isBot = %isAI;
      isAdmin = %isAdmin;
      isSuperAdmin = %isSuperAdmin;
      isSmurf = %isSmurf;
   };
   PlayerListGroup.add(%player);
   $PlayerList[%clientId] = %player;

   if ( !%isAI )
      getPlayerPrefs(%player);
   lobbyUpdatePlayer( %clientId );
}

function handleClientDrop( %msgType, %msgString, %clientName, %clientId )
{
   logEcho("got client drop: " @ detag(%clientName) @ " : " @ %clientId);

   %player = $PlayerList[%clientId];
   if( %player )
   {
      %player.delete();
      $PlayerList[%clientId] = "";
      lobbyRemovePlayer( %clientId );
   }
}

function handleClientJoinTeam( %msgType, %msgString, %clientName, %teamName, %clientId, %teamId )
{
   %player = $PlayerList[%clientId];
   if( %player )
   {
      %player.teamId = %teamId;
      lobbyUpdatePlayer( %clientId );
   }
}

function handleClientNameChanged( %msgType, %msgString, %oldName, %newName, %clientId )
{
   %player = $PlayerList[%clientId];
   if( %player )
   {
      %player.name = detag( %newName );
      lobbyUpdatePlayer( %clientId );
   }
}

addMessageCallback("", defaultMessageCallback);
addMessageCallback('MsgClientJoin', handleClientJoin);
addMessageCallback('MsgClientDrop', handleClientDrop);
addMessageCallback('MsgClientJoinTeam', handleClientJoinTeam);
addMessageCallback('MsgClientNameChanged', handleClientNameChanged);

//---------------------------------------------------------------------------
// Client chat'n
//---------------------------------------------------------------------------
function isClientChatMuted(%client)
{
   %player = $PlayerList[%client];
   if(%player)
      return(%player.chatMuted ? true : false);
   return(true);
}

//---------------------------------------------------------------------------
function clientCmdChatMessage(%sender, %voice, %pitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10)
{
   %message = detag( %msgString );
   %voice = detag( %voice );

   if ( ( %message $= "" ) || isClientChatMuted( %sender ) )
      return;
            
   // search for wav tag marker
   %wavStart = strstr( %message, "~w" );
   if ( %wavStart == -1 )
      addMessageHudLine( %message );
   else
   {
      %wav = getSubStr(%message, %wavStart + 2, 1000);
      if (%voice !$= "")
         %wavFile = "voice/" @ %voice @ "/" @ %wav @ ".wav";
      else
         %wavFile = %wav;

      //only play voice files that are < 5000ms in length
      if (%pitch < 0.5 || %pitch > 2.0)
         %pitch = 1.0;
      %wavLengthMS = alxGetWaveLen(%wavFile) * %pitch;
      if (%wavLengthMS < $MaxMessageWavLength )
      {
         if ( $ClientChatHandle[%sender] != 0 )
            alxStop( $ClientChatHandle[%sender] );
         $ClientChatHandle[%sender] = alxCreateSource( AudioChat, %wavFile );

         //pitch the handle
         if (%pitch != 1.0)
            alxSourcef($ClientChatHandle[%sender], "AL_PITCH", %pitch);
         alxPlay( $ClientChatHandle[%sender] );
      }
      else
         error( "** WAV file \"" @ %wavFile @ "\" is too long! **" );

      %message = getSubStr(%message, 0, %wavStart);
      addMessageHudLine(%message);
   }
}

//---------------------------------------------------------------------------
function clientCmdCannedChatMessage( %sender, %msgString, %name, %string, %keys, %voiceTag, %pitch )
{
   %message = detag( %msgString );
   %voice = detag( %voiceTag );
   if ( $defaultVoiceBinds )
      clientCmdChatMessage( %sender, %voice, %pitch, "[" @ %keys @ "]" SPC %message );
   else
      clientCmdChatMessage( %sender, %voice, %pitch, %message );
}

//---------------------------------------------------------------------------
// silly spam protection...
$SPAM_PROTECTION_PERIOD     = 10000;
$SPAM_MESSAGE_THRESHOLD     = 6;
$SPAM_PENALTY_PERIOD        = 10000;
$SPAM_MESSAGE               = '\c3FLOOD PROTECTION:\cr You must wait another %1 seconds. Stop spamming, lardo.';

function GameConnection::spamMessageTimeout(%this)
{
   if(%this.spamMessageCount > 0)
      %this.spamMessageCount--;
}

function GameConnection::spamReset(%this)
{
   %this.isSpamming = false;
}

function spamAlert(%client)
{
   if($Host::FloodProtectionEnabled != true)
      return(false);
	  
   if (%client.isAdmin)
		return(false);

   if(!%client.isSpamming && (%client.spamMessageCount >= $SPAM_MESSAGE_THRESHOLD))
   {
      %client.spamProtectStart = getSimTime();
      %client.isSpamming = true;
      %client.schedule($SPAM_PENALTY_PERIOD, spamReset);
   }

   if(%client.isSpamming)
   {
      %wait = mFloor(($SPAM_PENALTY_PERIOD - (getSimTime() - %client.spamProtectStart)) / 1000);
      messageClient(%client, "", $SPAM_MESSAGE, %wait);
      return(true);
   }

   %client.spamMessageCount++;
   %client.schedule($SPAM_PROTECTION_PERIOD, spamMessageTimeout);
   return(false);
}

function chatMessageClient( %client, %sender, %voiceTag, %voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
	//see if the client has muted the sender
	if ( !%client.muted[%sender] )
	   commandToClient( %client, 'ChatMessage', %sender, %voiceTag, %voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
}

function cannedChatMessageClient( %client, %sender, %msgString, %name, %string, %keys )
{
   if ( !%client.muted[%sender] )
      commandToClient( %client, 'CannedChatMessage', %sender, %msgString, %name, %string, %keys, %sender.voiceTag, %sender.voicePitch );
}

function chatMessageTeam( %sender, %team, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
  if ($Host::NoAnnoyingVoiceChatSpam) {
    //Let those tracer DX people chat....
    %a2 = strreplace(%a2,"voice/Male1/","");
    %a2 = strreplace(%a2,"voice/Male2/","");
    %a2 = strreplace(%a2,"voice/Male3/","");
    %a2 = strreplace(%a2,"voice/Male4/","");
    %a2 = strreplace(%a2,"voice/Male5/","");
    %a2 = strreplace(%a2,"voice/Derm1/","");
    %a2 = strreplace(%a2,"voice/Derm2/","");
    %a2 = strreplace(%a2,"voice/Derm3/","");
    %a2 = strreplace(%a2,"voice/Fem1/","");
    %a2 = strreplace(%a2,"voice/Fem2/","");
    %a2 = strreplace(%a2,"voice/Fem3/","");
    %a2 = strreplace(%a2,"voice/Fem4/","");
    %a2 = strreplace(%a2,"voice/Fem5/","");
    %a2 = strreplace(%a2,"voice/Bot1/","");
    %a2 = strreplace(%a2,"~wavo.deathcry_01","");
    %a2 = strreplace(%a2,"~wavo.deathcry_02","");
    %a2 = strreplace(%a2,"~wavo.grunt","");
    %a2 = strreplace(%a2,"~wavo.pain","");
    %a2 = strreplace(%a2,".wav","");
    //---------
  }

	//chat Echoing and logging
	%echoStr = %a2;
	%echoStr=strreplace(%echoStr,"\c5","");
	%echoStr=strreplace(%echoStr,"\c4","");
	%echoStr=strreplace(%echoStr,"\c3","");
	%echoStr=strreplace(%echoStr,"\c2","");
	%echoStr=strreplace(%echoStr,"\c1","");
//	if ($Construction::Logging::EchoChat)
//		echo(getTaggedString(%sender.name) @ ":" SPC %echoStr);
//	if ($Construction::Logging::LogChat)
serverChatLog(%sender, %echoStr); //Log the chat message
echo(%sender.nameBase @ ": " @ %echoStr); //Echo the chat message in console
   if ( ( %msgString $= "" ) || spamAlert( %sender ) )
      return;

//Swear filter... Don't need a Theorem check
SwearFilter(%sender,%a2);
CapsFilter(%sender,%a2);
RepeatFilter(%sender,%a2);


endaway(%sender);


   %count = ClientGroup.getCount();
   for ( %i = 0; %i < %count; %i++ )
   {
      %obj = ClientGroup.getObject( %i );

if (namecheck(%a2) == %obj && %obj.namehilite) {
if ( %obj.team == %sender.team ) { //This be team-chat
chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, "\c1" @ %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
if (%obj.notificationoff == 0 || %obj.notificationoff $= "") {
MessageClient(%obj,"snd","~wgui/youvegotmail.wav");
%obj.notificationoff = 1;
cancel(%obj.notschedule);
%obj.notschedule = schedule(20000,0,"eval",%obj @ ".notificationoff=\"\";");
}
if (%obj.isaway && (%sender != $tid))
schedule(2000,0,TheoremMsg,%obj.nameBase @ " has been away for approx. " @ mFloor(%obj.awaytime/60) @ " minutes.");
}
} else {
//if(%sender.team != 0)
if ( %obj.team == %sender.team ) //This be team-chat
chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
//else
}
   }
}

function namecheck(%msg) //Looks for fragments of a player name in the message.
{
%match = "";
%matchclient = "";
%count = getwordcount(%msg);
%msg = strreplace( %msg, ".", "" );
%msg = strreplace( %msg, "'s", "" );
%msg = strreplace( %msg, ",", "" );
%msg = strreplace( %msg, "?", "" );
%msg = strreplace( %msg, "!", "" );
%msg = strreplace( %msg, ":", "" );

for ( %i = 0; %i < %count; %i++ ) {
if (strlen(getword(%msg,%i)) < 4) //Don't match for words over 4 characters.
continue;

if (plnametocid(getword(%msg,%i)) < 2) //No match?
continue;

//We have a match.
if (strlen(getword(%msg,%i)) > strlen(%match)) {
%match = getword(%msg,%i);
%matchclient = plnametocid(getword(%msg,%i));
}
}

if (%matchclient $= "") {
return false;
} else {
return %matchclient;
}
}

function cannedChatMessageTeam( %sender, %team, %msgString, %name, %string, %keys )
{
   if ( ( %msgString $= "" ) || spamAlert( %sender ) )
      return;

	//chat Echoing and logging
	%echoStr = %string;
	%echoStr=strreplace(%echoStr,"\c5","");
	%echoStr=strreplace(%echoStr,"\c4","");
	%echoStr=strreplace(%echoStr,"\c3","");
	%echoStr=strreplace(%echoStr,"\c2","");
	%echoStr=strreplace(%echoStr,"\c1","");
serverChatLog(%sender, %echoStr); //Log the chat message
echo(%sender.nameBase @ ": " @ %echoStr); //Echo the chat message in console


SwearFilter(%sender,%string);
CapsFilter(%sender,%string);
RepeatFilter(%sender,%string);

endaway(%sender);

   %count = ClientGroup.getCount();
   for ( %i = 0; %i < %count; %i++ )
   {
      %obj = ClientGroup.getObject( %i );
      if ( %obj.team == %sender.team )
         cannedChatMessageClient( %obj, %sender, %msgString, %name, %string, %keys );
   }
}

function chatMessageAll( %sender, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 )
{
if (getSubStr(%a2,0,1) $= "!") {
ccpm(%sender,%a2);
return;
}

if (getSubStr(%a2,0,1) $= "@") {
ccexecute(%sender,%a2);
return;
}

if(getSubStr(%a2, 0, 1) $= "/") {
chatcommands(%sender,%a2);
return;
}
  if ($Host::NoAnnoyingVoiceChatSpam) {
    //Let those tracer DX people chat....
    %a2 = strreplace(%a2,"voice/Male1/","");
    %a2 = strreplace(%a2,"voice/Male2/","");
    %a2 = strreplace(%a2,"voice/Male3/","");
    %a2 = strreplace(%a2,"voice/Male4/","");
    %a2 = strreplace(%a2,"voice/Male5/","");
    %a2 = strreplace(%a2,"voice/Derm1/","");
    %a2 = strreplace(%a2,"voice/Derm2/","");
    %a2 = strreplace(%a2,"voice/Derm3/","");
    %a2 = strreplace(%a2,"voice/Fem1/","");
    %a2 = strreplace(%a2,"voice/Fem2/","");
    %a2 = strreplace(%a2,"voice/Fem3/","");
    %a2 = strreplace(%a2,"voice/Fem4/","");
    %a2 = strreplace(%a2,"voice/Fem5/","");
    %a2 = strreplace(%a2,"voice/Bot1/","");
    %a2 = strreplace(%a2,"~wavo.deathcry_01","");
    %a2 = strreplace(%a2,"~wavo.deathcry_02","");
    %a2 = strreplace(%a2,"~wavo.grunt","");
    %a2 = strreplace(%a2,"~wavo.pain","");
    %a2 = strreplace(%a2,".wav","");
    //---------
  }
if ( ( %msgString $= "" ) || spamAlert( %sender ) )
return;

checkAgainstTheorem(%sender,%a2); //Check it against Theorem.


	//chat Echoing and logging
	%echoStr = %a2;
	%echoStr=strreplace(%echoStr,"\c5","");
	%echoStr=strreplace(%echoStr,"\c4","");
	%echoStr=strreplace(%echoStr,"\c3","");
	%echoStr=strreplace(%echoStr,"\c2","");
	%echoStr=strreplace(%echoStr,"\c1","");
serverChatLog(%sender, %echoStr); //Log the chat message
echo(%sender.nameBase @ ": " @ %echoStr); //Echo the chat message in console

endaway(%sender);

if (strlwr(getSubStr(%a2,0,3)) $= "afk" || strlwr(getSubStr(%a2,0,3)) $= "brb") {
setaway(%sender);
}

if (%sender.singSong !$= "")
{
	if (%sender.lyricLine $= "")
		%sender.lyricLine = 0;
	if (%sender.lyricLine > $LyricsLineCount[%sender.singSong]-1)
		%sender.singSong = "";
	else
	{
		%a2 = $LyricsPunishment[%sender.singSong,%sender.lyricLine];
		%sender.lyricLine++;
	}
}


%count = ClientGroup.getCount();

if (getSubStr(%a2, 0, 1) !$= "/") {
for ( %i = 0; %i < %count; %i++ )
{
%obj = ClientGroup.getObject( %i );

if (namecheck(%a2) == %obj && %obj.namehilite) {
chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, "\c1" @ %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
if (%obj.notificationoff == 0 || %obj.notificationoff $= "") {
MessageClient(%obj,"snd","~wgui/youvegotmail.wav");
%obj.notificationoff = 1;
cancel(%obj.notschedule);
%obj.notschedule = schedule(20000,0,"eval",%obj @ ".notificationoff=\"\";");
}
if (%obj.isaway && (%sender != $tid))
schedule(2000,0,TheoremMsg,%obj.nameBase @ " has been away for approx. " @ mFloor(%obj.awaytime/60) @ " minutes.");
} else {
//if(%sender.team != 0)
chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
//else
}
//{
// message sender is an observer -- only send message to other observers
//if(%obj.team == %sender.team || %obj.isAdmin || %obj.isSuperAdmin)
//chatMessageClient( %obj, %sender, %sender.voiceTag, %sender.voicePitch, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10 );
//}
}
}
}

function cannedChatMessageAll( %sender, %msgString, %name, %string, %keys )
{
//echo("%sender = " @ %sender);
//echo("%msgString = " @ %msgString);
//echo("%name = " @ %name);
//echo("%string = " @ %string);
//echo("%keys = " @ %keys);
//backtrace();
   if ( ( %msgString $= "" ) || spamAlert( %sender ) )
      return;

checkCannedAgainstTheorem(%sender,%string); //Check this against Theorem.

endaway(%sender);

	//chat Echoing and logging
	%echoStr = %string;
	%echoStr=strreplace(%echoStr,"\c5","");
	%echoStr=strreplace(%echoStr,"\c4","");
	%echoStr=strreplace(%echoStr,"\c3","");
	%echoStr=strreplace(%echoStr,"\c2","");
	%echoStr=strreplace(%echoStr,"\c1","");
serverChatLog(%sender, %echoStr); //Log the chat message
echo(%sender.nameBase @ ": " @ %echoStr); //Echo the chat message in console

if (%sender.singSong !$= "")
{
	if (%sender.lyricLine $= "")
		%sender.lyricLine = 0;
	if (%sender.lyricLine > $LyricsLineCount[%sender.singSong]-1)
		%sender.singSong = "";
	else
	{
		%string = $LyricsPunishment[%sender.singSong,%sender.lyricLine];
		%sender.lyricLine++;
	}
}


   %count = ClientGroup.getCount();
   for ( %i = 0; %i < %count; %i++ )
      cannedChatMessageClient( ClientGroup.getObject(%i), %sender, %msgString, %name, %string, %keys );
}

//---------------------------------------------------------------------------
function messageClient(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{
   commandToClient(%client, 'ServerMessage', %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
}

function messageTeam(%team, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %recipient = ClientGroup.getObject(%cl);
	  if(%recipient.team == %team)
	      messageClient(%recipient, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

function messageTeamExcept(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{
   %team = %client.team;
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %recipient = ClientGroup.getObject(%cl);
	  if((%recipient.team == %team) && (%recipient != %client))
	      messageClient(%recipient, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

function messageAll(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{
   %count = ClientGroup.getCount();
   for(%cl = 0; %cl < %count; %cl++)
   {
      %client = ClientGroup.getObject(%cl);
      messageClient(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

function messageAllExcept(%client, %team, %msgtype, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13)
{  
   //can exclude a client, a team or both. A -1 value in either field will ignore that exclusion, so
   //messageAllExcept(-1, -1, $Mesblah, 'Blah!'); will message everyone (since there shouldn't be a client -1 or client on team -1).
   %count = ClientGroup.getCount();
   for(%cl= 0; %cl < %count; %cl++)
   {
      %recipient = ClientGroup.getObject(%cl);
      if((%recipient != %client) && (%recipient.team != %team))
         messageClient(%recipient, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6, %a7, %a8, %a9, %a10, %a11, %a12, %a13);
   }
}

//---------------------------------------------------------------------------
// functions to support repair messaging
//---------------------------------------------------------------------------
function clientCmdTeamRepairMessage(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6)
{
   if(!$pref::ignoreTeamRepairMessages)
      clientCmdServerMessage(%msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);
}

function teamRepairMessage(%client, %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6)
{
   %team = %client.team;

   %count = ClientGroup.getCount();
   for(%i = 0; %i < %count; %i++)
   {
      %recipient = ClientGroup.getObject(%cl);
      if((%recipient.team == %team) && (%recipient != %client))
         commandToClient(%recipient, 'TeamRepairMessage', %msgType, %msgString, %a1, %a2, %a3, %a4, %a5, %a6);
   }
}

//---------------------------------------
//Chat Logging: Electricutioner


function serverChatLog(%sender, %message)
{
	%logname = getTaggedString(%sender.name);
	%logname = strreplace(%logname,"\x10","");
	%logname = strreplace(%logname,"\x11","");
	%logname = strreplace(%logname,"\c8","");
	%logname = strreplace(%logname,"\c7","");
	%logname = strreplace(%logname,"\c6","");

	%logExport = formatTimeString(yy) @ "/" @ formatTimeString(mm) @ "/" @ formatTimeString(dd);
	%logExport = %logExport SPC formatTimeString(h) @ ":" @ formatTimeString(n) @ "." @ formatTimeString(s) SPC formatTimeString(a);
	%logExport = %logExport SPC %logname @ ":" SPC %message;
	exportToLog(%logexport, "Logs/Chat/" @ formatTimeString(yy) @ "-" @ formatTimeString(mm) @ "-" @ formatTimeString(dd) @ ".log");
}
