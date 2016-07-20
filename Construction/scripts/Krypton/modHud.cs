function DefaultGame::InitModHud(%game, %client, %value)
{
   // Clear out any previous settings
   commandToClient(%client, 'InitializeModHud', "ModName");

   // Send the hud labels                 |  Hud Label  |  | Option label | | Setting label |
   commandToClient(%client, 'ModHudHead', "Piece Configuration",      "Option:",       "Setting:");

   %pack = %client.player.getMountedImage($BackpackSlot);

   // Send the Option list and settings per option    | Option |    | Setting |
   //commandToClient(%client, 'ModHudPopulate',          "Example1",   "Empty");
   //commandToClient(%client, 'ModHudPopulate',          "Example2",   "Setting1", "Setting2", "Setting3", "Setting4", "Setting5", "Setting6", "Setting7", "Setting8", "Setting9", "Setting10");
if (%pack.modhudenabled)
%pack.setModHud(%client);

   // Send the button labels and visual settings  |  Button  |  | Label |  | Visible |  | Active |
   commandToClient(%client,                       'ModHudBtn1',  "BUTTON1",      0,          0);
   commandToClient(%client,                       'ModHudBtn2',  "BUTTON2",      0,          0);
   commandToClient(%client,                       'ModHudBtn3',  "BUTTON3",      0,          0);
   commandToClient(%client,                       'ModHudBtn4',  "BUTTON4",      0,          0);

   

   // We're done!
   commandToClient(%client, 'ModHudDone');
}

function DefaultGame::UpdateModHudSet(%game, %client, %option, %value)
{
   // 1 = Example1
   // 2 = Example2

//   switch$ ( %option )
//   {
//      case 1:
//         %msg = '\c2Something set to: %2.';
//
//      case 2:
//         %msg = '\c2Something set to: %2.';
//
//      default:
//         %msg = '\c2Invalid setting.';
//   }
//   messageClient( %client, 'MsgModHud', %msg, %option, %value );
if (%client.modhuddone) {
%pack = %client.player.getMountedImage($BackpackSlot);
%pack.getModHud(%client,%option,%value);
}
}

function DefaultGame::ModButtonCmd(%game, %client, %button, %value)
{
   // 11 = Button 1
   // 12 = Button 2
   // 13 = Button 3
   // 14 = Button 4

   switch ( %button )
   {
      case 11:
         %msg = '\c2Something set to: %2.';

      case 12:
         %msg = '\c2Something set to: %2.';

      case 13:
         %msg = '\c2Something set to: %2.';

      case 14:
         %msg = '\c2Something set to: %2.';

      default:
         %msg = '\c2Invalid setting.';
   }
   messageClient( %client, 'MsgModHud', %msg, %button, %value );
}