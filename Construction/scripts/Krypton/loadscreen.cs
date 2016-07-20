//------------------------------------------- 
// Mod information load screen - ZOD. z0ddm0d 
// and Modified later by the T2CC 
// Modified for Krypton Construction by Sloik
//------------------------------------------- 

package loadmodinfo 
{ 
function sendLoadInfoToClient( %client ) 
{ 
Parent::sendLoadInfoToClient(%client); 
// Change the schedule to load the messages faster or slower... 
schedule(8000, 0, "ModInfoLoad", %client); 
} 

function ModInfoLoad(%client) 
{ 
%count = ClientGroup.getCount(); 
for(%cl = 0; %cl < %count; %cl++) 
{ 
%client = ClientGroup.getObject( %cl ); 
if (!%client.isAIControlled()) 
sendModInfoToClient(%client); 
} 
} 

function sendModInfoToClient(%client) 
{ 
%on = "On"; 
%off = "Off"; 

messageClient( %client, 'MsgLoadInfo', "", $CurrentMission, $MissionDisplayName, $Host::GameName ); 

// Send mod details: 
%ModLine = "\n<color:dc2be2><font:arial italic:24>Krypton Construction Mod" @ 
"\n<color:792be2><font:Arial:16>Developed by Sloik" @ 
"\n<color:792be2><font:Arial:16>Based on Construction 0.70a\n"; 
messageClient( %client, 'MsgLoadQuoteLine', "", %ModLine ); 

// I made this one line because sending multiple lines causes flashing with scripts 
// that run on ModInfoLoad. There's a maxium # of characters that can be printed 
// with MsgLoadRulesLine. We're at that now. 
// 
// Send server information: 
%ServerText = "\nSERVER OPTIONS:" @ 
//"\n Time limit: " @ $Host::TimeLimit @  //There's usually no need to display the time limit.
"\n Max players: " @ $Host::MaxPlayers @ 
"\n Team damage: " @ ($Host::TeamDamageOn ? %on : %off) @ 
"\n Combat: " @ "Enabled" @ 
"\n" @ 
"\n <color:139a3d><font:Arial:16>Press F2 for additional functions and information";
//"\n CRC checking: " @ ($Host::CRCTextures ? %on : %off) @ 
//"\n Pure server: " @ ($Host::PureServer ? %on : %off) @ 
//"\n Refuse smurfs: " @ ($Host::NoSmurfs ? %on : %off) @ 
//"\n Next Mission: " @ findNextCycleMission(); 

messageClient( %client, 'MsgLoadRulesLine', "", %ServerText, false ); 
messageClient( %client, 'MsgLoadInfoDone' ); 


} 
}; 

activatepackage(loadmodinfo); 



function debriefLoad( %client )
{
%kryptonquotenumber = 11;
%kryptonquote[0] = "You can use permission spheres to prevent weapon fire near your buildings.";
%kryptonquote[1] = "You can change personal player preferences in the F2 menu.";
%kryptonquote[2] = "The /away command will let players know you're away when your name is mentioned.";
%kryptonquote[3] = "You can save buildings using the Advanced Save System in the F2 menu.";
%kryptonquote[4] = "You will be sent to jail if you kill a fellow player outside of a combat sphere.";
%kryptonquote[5] = "If you'd like to fight without being sent to jail, deploy a combat permission sphere under packs.";
%kryptonquote[6] = "Don't be a jerk.";
%kryptonquote[7] = "Theorem will warn you for cursing, typing in all capital letters, or repeating yourself. You will be kicked on the third warning.";
%kryptonquote[8] = "Don't ask for admin.";
%kryptonquote[9] = "Owner-only permission spheres will keep others from deploying near you. You can find the permission sphere under packs.";
%kryptonquote[10] = "You can give other players access cards to your level doors through the player list in the F2 menu.";
%kryptonquote[11] = "You can give other players your deployables through the player list in the F2 menu.";

if (isObject(Game))
      %game = Game.getId();
   else
      return;
messageClient(%client, 'MsgDebriefResult', "", '<just:center>Krypton Construction Mod');//Top of the debrief screen

//messageClient( %client, 'MsgDebriefAddLine', "", '<spush><color:dc2be2><font:arial italic:24>Krypton Construction Mod<spop>' );
messageClient( %client, 'MsgDebriefAddLine', "", '<just:center><color:ff00ba><font:Impact:18>Developed by Sloik' );
messageClient( %client, 'MsgDebriefAddLine', "", '<just:center><color:ff00ba><font:Impact:18>Based on Construction 0.70a' );
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '<color:005aff><font:Arial:32>Kr' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:005aff><font:Arial:19>Krypton Construction Mod' );
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '<color:00ffc6><font:Arial:32>Taking Suggestions!' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:00a37f><font:Arial:16>See a feature in some other mod that isn\'t here? Type /submit with your suggestion and it will be considered for Krypton 2. Your GUIDs are recorded with submission.' );
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '<color:aae8ff><font:Impact:20>Server Options:' );
messageClient( %client, 'MsgDebriefAddLine', "", "<color:00baff><font:Arial:18>Max players: " @ $Host::MaxPlayers );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", "<color:00baff><font:Arial:18>Combat: Enabled" );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '<color:ffffff><font:Impact:20>Credits:' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:c1c1c1><font:Impact:19>  Construction Development Team' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:adadad><font:Arial:16>     Mostlikely, JackTL, Construct, Tessio, Lucid, Badshot, Dynablade, Child_Killer' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:c1c1c1><font:Impact:19>  Metallic Development Team' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:adadad><font:Arial:16>     Captn Power, BlackFire, SUSaiyn, Kamikaze, Krash123, Dricon' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:c1c1c1><font:Impact:19>  Quantium Development Team' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:adadad><font:Arial:16>     Sun Quan.... etc?' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:c1c1c1><font:Impact:19>  {NOS} Clan' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:adadad><font:Arial:16>     Electricutioner, Naosyth, Ronnies, Mazo, etc.' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:c1c1c1><font:Impact:19>  Other Peoples' );
messageClient( %client, 'MsgDebriefAddLine', "", '<color:adadad><font:Arial:16>     Dark Dragon DX, Netherdark, Blnukem' );
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", "<color:aae8ff><font:Impact:20>Random Tip:" );
messageClient( %client, 'MsgDebriefAddLine', "", "<color:aae8ff><font:Impact:17>" @ %kryptonquote[getrandom(0,%kryptonquotenumber)] );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '' );//Newline
messageClient( %client, 'MsgDebriefAddLine', "", '<color:139a3d><font:Arial:18>Press F2 after loading for additional functions and information' );//Newline

messageClient( %client, 'MsgGameOver', "\c1The Map IS LOADING, despite the blank progress bar. Do not fret!" );
//         %game.sendDebriefing( %client );

}