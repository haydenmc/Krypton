function addbuddy(%client,%buddy) //Adds a buddy to %client's buddy list.
{
if (!isObject(%client) || !isObject(%buddy))
return false;

   %count = getFieldCount( %client.buddyguids );

   for ( %i = 0; %i < %count; %i++ )
   {
      %id = getField( %client.buddyguids, %i );
      if ( %id == %buddy.guid )
      {
         return false;  // They're already there!
      }
   }

   if ( %count == 0 ) {
      %client.buddyguids = %buddy.guid;
      %client.buddynames = %buddy.nameBase;
   } else {
      %client.buddyguids = %client.buddyguids TAB %buddy.guid;
      %client.buddynames = %client.buddynames TAB %buddy.nameBase;
   }

updateprefs(%client);
return true;
}

function delbuddy(%client,%buddy) //Removes a buddy from %client's buddy list.
{
if (!isObject(%client) || !isObject(%buddy))
return false;

%index = findWord(%client.buddyguids,%buddy.guid);

if (%index < 0) //Can't find 'em
return false;

%client.buddyguids = removeWord(%client.buddyguids, %index);
%client.buddynames = removeWord(%client.buddynames, %index);

updateprefs(%client);
return true;
}

function isbuddy(%client,%buddy)
{

   if ( !%totalRecords = getFieldCount( %client.buddyguids ) )
   {
      return false;
   }

   for(%i = 0; %i < %totalRecords; %i++)
   {
      %record = getField( getRecord( %client.buddyguids, 0 ), %i);
      if (%record == %buddy.guid)
         return true;
   }

   return false;
}

//Same thing below, except an ENEMY LIST -- Primarily for turret targeting systems
function addenemy(%client,%buddy) //Adds a buddy to %client's buddy list.
{
if (!isObject(%client) || !isObject(%buddy))
return false;


if (!isObject(%client) || !isObject(%buddy))
return false;

   %count = getFieldCount( %client.enemyguids );

   for ( %i = 0; %i < %count; %i++ )
   {
      %id = getField( %client.enemyguids, %i );
      if ( %id == %buddy.guid )
      {
         return false;  // They're already there!
      }
   }

   if ( %count == 0 ) {
      %client.enemyguids = %buddy.guid;
      %client.enemynames = %buddy.nameBase;
   } else {
      %client.enemyguids = %client.enemyguids TAB %buddy.guid;
      %client.enemynames = %client.enemynames TAB %buddy.nameBase;
   }

updateprefs(%client);
return true;
}

function delenemy(%client,%buddy) //Removes a buddy from %client's buddy list.
{
if (!isObject(%client) || !isObject(%buddy))
return false;

%index = findWord(%client.buddyguids,%buddy.guid);

if (%index < 0) //Can't find 'em
return false;

%client.enemyguids = removeWord(%client.enemyguids, %index);
%client.enemynames = removeWord(%client.enemynames, %index);

updateprefs(%client);
return true;
}

function isenemy(%client,%buddy)
{

   if ( !%totalRecords = getFieldCount( %client.enemyguids ) )
   {
      return false;
   }

   for(%i = 0; %i < %totalRecords; %i++)
   {
      %record = getField( getRecord( %client.enemyguids, 0 ), %i);
      if (%record == %buddy.guid)
         return true;
   }

   return false;
}

function ServerCmdAddToAdminList( %admin, %client )
{
   if ( !%admin.isSuperAdmin )
      return;

   %count = getFieldCount( $Host::AdminList );

   for ( %i = 0; %i < %count; %i++ )
   {
      %id = getField( $Host::AdminList, %i );
      if ( %id == %client.guid )
      {
         return;  // They're already there!
      }
   }

   if ( %count == 0 )
      $Host::AdminList = %client.guid;
   else
      $Host::AdminList = $Host::AdminList TAB %client.guid;
}