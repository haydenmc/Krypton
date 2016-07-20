function setdefaultprefs(%client)
{
if (%client.lasercolor $= "")
%client.lasercolor = 3;
if (%client.laserMode $= "")
%client.laserMode = 0;
if (%client.afkenabled $= "")
%client.afkenabled = 1;
if (%client.namehilite $= "")
%client.namehilite = 1;

if (%client.buddyguids $= "")
%client.buddyguids = "";
if (%client.buddynames $= "")
%client.buddynames = "";

if (%client.enemyguids $= "")
%client.enemyguids = "";
if (%client.enemynames $= "")
%client.enemynames = "";

if (%client.spawnposition $= "")
%client.spawnposition = "";

if (%client.totalplaytime $= "")
%client.totalplaytime = 0;

if (%client.viparmorenabled $= "")
%client.viparmorenabled = 0;

}

function updateprefs(%client)
{
if (!isObject(%client) || %client < 1)
return false;

setdefaultprefs(%client);

new FileObject("PrefsFile"); //create file object (player's save file)
PrefsFile.openForWrite("playerprefs/" @ %client.guid @ ".cs"); //open it up, and create it if it isn't there
PrefsFile.writeLine("// KRYPTON MOD PLAYER PREFERENCES FILE");
PrefsFile.writeLine("// " @ %client.nameBase @ "'s Preferences.");

PrefsFile.writeLine("%client.lasercolor = \"" @ %client.lasercolor @ "\";");
PrefsFile.writeLine("%client.laserMode = \"" @ %client.laserMode @ "\";");
PrefsFile.writeLine("%client.AFKEnabled = \"" @ %client.AFKEnabled @ "\";");
PrefsFile.writeLine("%client.namehilite = \"" @ %client.namehilite @ "\";");

PrefsFile.writeLine("%client.buddyguids = \"" @ %client.buddyguids @ "\";");
PrefsFile.writeLine("%client.buddynames = \"" @ %client.buddynames @ "\";");

PrefsFile.writeLine("%client.enemyguids = \"" @ %client.enemyguids @ "\";");
PrefsFile.writeLine("%client.enemynames = \"" @ %client.enemynames @ "\";");

PrefsFile.writeLine("%client.spawnposition = \"" @ %client.spawnposition @ "\";");

PrefsFile.writeLine("%client.totalplaytime = \"" @ %client.totalplaytime @ "\";");

PrefsFile.writeLine("%client.viparmorenabled = \"" @ %client.viparmorenabled @ "\";");

PrefsFile.close(); //close the file
PrefsFile.delete(); //delete the object (not the file)

compile("playerprefs/" @ %client.guid @ ".cs"); //compile it. No old DSOs!
}

function loadprefs(%client)
{
exec("playerprefs/" @ %client.guid @ ".cs");
}