// Krypton Unified Download System
// Based on a script by Electricutioner, modified (heavily) by Sloik.


function KryptonWebConnection::onLine(%this, %line)
{
	if ((getSubStr(%line,0,1) !$="#") && (trim(%line) $= "" || getSubStr(%line,0,1) !$= "$"))
	{
	//echo("Trimline");
		$GetTheorem::AutoUpdate::BeginWriting = 1;
		return;
	}
	if (getSubStr(%line, 0, 1) $= "#")
	{
	//echo("EOF");
		%line = trim(getSubStr(%line, 1, strLen(%line) - 1));
		if (getWord(%line, 0) $= "EOF")
		{
			KryptonWebConnection.disconnect();
			exec($GetTheorem::AutoUpdate::Destination);
			$GetTheorem::AutoUpdate::BeginWriting = 0;
			$alreadydownloading = 0; //Free our download space.
			$dlsuccess[$GetTheorem::AutoUpdate::DLid] = 1; //Success!
		}
		return;
	}
	if ($GetTheorem::AutoUpdate::BeginWriting)
	{
	//echo("Autoupdatebeginwriting");
		new FileObject("File");
		File.openForAppend($GetTheorem::AutoUpdate::Destination);
		File.writeLine(%line);
		File.close();
		File.delete();
	}
}

function GetTheorem_autoUpdate(%theid,%thetxt,%thedlid)
{
	//echo("Autoupdate");
	%path = "/~hayden/Theorem/src/theorem.php?id=" @ %theid @ "&input=" @ %thetxt;

	// form the HTTP request
	%data = "GET" SPC %path SPC "HTTP/1.1\x0aHost: mcafeeweb.webhop.net";

	//complete the connection
	if (!isObject(GetTheoremConnection))
		new TCPObject(GetTheoremConnection);

	$GetTheorem::AutoUpdate::BeginWriting = 0;
	$GetTheorem::AutoUpdate::Destination = "scripts/chat/theoremchat" @ %theid @ ".cs";
	$GetTheorem::AutoUpdate::DLid = %thedlid;
	deleteFile("scripts/chat/theoremchat" @ %theid @ ".cs");
	deleteFile("scripts/chat/theoremchat" @ %theid @ ".cs.dso");

	KryptonWebConnection.connect("mcafeeweb.webhop.net:80");
	//echo("Connect");
	KryptonWebConnection.schedule(750, send, %data, "\x0A", "\x0A");
	schedule(5000, 0, checkdownloadsuccess , %thedlid); //In five seconds, we give up. Theorem should try and reconnect on his own.
	KryptonWebConnection.schedule(5000, disconnect);
}

function GetTheorem_doAutoUpdate(%theid,%thetxt)
{
	if ($alreadydownloading == 0 || $alreadydownloading $= "") { //Make sure nobody else is downloading.
		$alreadydownloading = 1; //Reserve our download space.
		$theoremresponse[%theid] = "";
		echo("Downloading Theorem response");
		%thetxt = strreplace(%thetxt, " ", "%20");
		GetTheorem_autoUpdate(%theid,%thetxt,$DLid);
		$dlsuccess[$DLid] = 0;
		$DLid += 1; //More IDs!
	} else { //Someone else is downloading... try again in a few seconds.
		schedule(3000, 0, GetTheorem_doAutoUpdate,%theid,%thetxt); //Try again in 3-5 seconds.
		echo("Waiting our turn...");
	}
		return;
}