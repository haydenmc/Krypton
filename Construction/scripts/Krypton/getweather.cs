// Weather Getter For Theorem, using accuweather.com RSS feeds.
// Originally written by Electricutioner, modified by Sloik.
// 2/11/2008

// Structural Infinity enabled clients are able to see over 1024
// objects. Don't sweat the details, little monkey.


function GetWeatherConnection::onLine(%this, %line)
{
	if (trim(%line) $= "")
	{
		$GetWeather::AutoUpdate::BeginWriting = 1;
		return;
	}
	if (getSubStr(%line, 0, 1) $= "#")
	{
		%line = trim(getSubStr(%line, 1, strLen(%line) - 1));
		if (getWord(%line, 0) $= "EOF")
		{
			GetWeatherConnection.disconnect();
			exec($GetWeather::AutoUpdate::Destination);
			$GetWeather::AutoUpdate::BeginWriting = 0;
			$alreadydownloading = 0; //Free our download space.
			$dlsuccess[$GetWeather::AutoUpdate::DLid] = 1; //Success!
		}
		return;
	}
	if ($GetWeather::AutoUpdate::BeginWriting)
	{
		new FileObject("File");
		File.openForAppend($GetWeather::AutoUpdate::Destination);
		File.writeLine(%line);
		File.close();
		File.delete();
	}
}

function GetWeather_autoUpdate(%thezip,%theid)
{
	%path = "/~hayden/getweather.php?zip=" @ %thezip;

	// form the HTTP request
	%data = "GET" SPC %path SPC "HTTP/1.1\x0aHost: mcafeeweb.webhop.net";

	//complete the connection
	if (!isObject(GetWeatherConnection))
		new TCPObject(GetWeatherConnection);

	$GetWeather::AutoUpdate::BeginWriting = 0;
	$GetWeather::AutoUpdate::Destination = "scripts/Krypton/currentweather.cs";
	$GetWeather::AutoUpdate::DLid = %theid;
	deleteFile("scripts/Krypton/currentweather.cs");
	deleteFile("scripts/Krypton/currentweather.cs.dso");

	GetWeatherConnection.connect("mcafeeweb.webhop.net:80");
	GetWeatherConnection.schedule(500, send, %data, "\x0A", "\x0A");
	schedule(5000, 0, checkdownloadsuccess , %theid); //In five seconds, we give up. Theorem should try and reconnect on his own.
	GetWeatherConnection.schedule(5000, disconnect);
//echo("HUR");
}

function checkdownloadsuccess(%theid,%thezip)
{
echo("Checking...");
if ($dlsuccess[%theid] == 0) { //This is taking far too long.
echo("Download failed.");
$alreadydownloading = 0; //Don't want to change this variable unless we're sure our download failed.
}
}

function GetWeather_doAutoUpdate(%zipcode)
{
	if ($alreadydownloading == 0 || $alreadydownloading $= "") { //Make sure nobody else is downloading.
		$alreadydownloading = 1; //Reserve our download space.
		//$weatherrequest = "";
		//echo("Getting weather information...");
		$GetWeather::AutoUpdate::UpdateFile = %zipcode;
		GetWeather_autoUpdate(%zipcode,$DLid);
		$dlsuccess[$DLid] = 0;
		$DLid += 1; //More IDs!
	} else {
		schedule(3000, 0, GetWeather_doAutoUpdate,%zipcode,$DLid); //Try again in 3 seconds.
		echo("Waiting our turn...");
	}
		return;
}




//------------Dictionary

function GetWordConnection::onLine(%this, %line)
{
	if (trim(%line) $= "")
	{
		$GetWord::AutoUpdate::BeginWriting = 1;
		return;
	}
	if (getSubStr(%line, 0, 1) $= "#")
	{
		%line = trim(getSubStr(%line, 1, strLen(%line) - 1));
		if (getWord(%line, 0) $= "EOF")
		{
			GetWordConnection.disconnect();
			exec($GetWord::AutoUpdate::Destination);
			$GetWord::AutoUpdate::BeginWriting = 0;
			$alreadydownloading = 0; //Free our download space.
			$dlsuccess[$GetWord::AutoUpdate::DLid] = 1; //Success!
		}
		return;
	}
	if ($GetWord::AutoUpdate::BeginWriting)
	{
		new FileObject("File");
		File.openForAppend($GetWord::AutoUpdate::Destination);
		File.writeLine(%line);
		File.close();
		File.delete();
	}
}

function GetWord_autoUpdate(%theword,%theid)
{
	%path = "/~hayden/getdefinition.php?word=" @ %theword;

	// form the HTTP request
	%data = "GET" SPC %path SPC "HTTP/1.1\x0aHost: mcafeeweb.webhop.net";

	//complete the connection
	if (!isObject(GetWordConnection))
		new TCPObject(GetWordConnection);

	$GetWord::AutoUpdate::BeginWriting = 0;
	$GetWord::AutoUpdate::Destination = "scripts/Krypton/currentword.cs";
	$GetWord::AutoUpdate::DLid = %theid;
	deleteFile("scripts/Krypton/currentword.cs");
	deleteFile("scripts/Krypton/currentword.cs.dso");

	GetWordConnection.connect("mcafeeweb.webhop.net:80");
	GetWordConnection.schedule(500, send, %data, "\x0A", "\x0A");
	schedule(5000, 0, checkdownloadsuccess , %theid); //In five seconds, we give up. Theorem should try and reconnect on his own.
	GetWordConnection.schedule(5000, disconnect);
//echo("HUR");
}

function GetWord_doAutoUpdate(%word)
{
	if ($alreadydownloading == 0 || $alreadydownloading $= "") { //Make sure nobody else is downloading.
		$alreadydownloading = 1; //Reserve our download space.
		echo("Getting word information for " @ %word);
		$GetWord::AutoUpdate::UpdateFile = %word;
		GetWord_autoUpdate(%word,$DLid);
		$dlsuccess[$DLid] = 0;
		$DLid += 1; //More IDs!
	} else {
		schedule(3000, 0, GetWord_doAutoUpdate,%word); //Try again in 3 seconds.
		echo("Waiting our turn...");
	}
		return;
}