//Dayshift script... basically changes the sky every so often... like a real day, in game!
//Created for Krypton by Sloik. Hoorah!


//The following functions copied from Quantium
datablock PrecipitationData(MyReign)  // =)
{
   type = 0;
   soundProfile = "";
   materialList = "raindrops.dml";
   sizeX = 0.2;
   sizeY = 0.45;

   movingBoxPer = 0.35;
   divHeightVal = 1.5;
   sizeBigBox = 1;
   topBoxSpeed = 20;
   frontBoxSpeed = 30;
   topBoxDrawPer = 0.5;
   bottomDrawHeight = 40;
   skipIfPer = -0.3;
   bottomSpeedPer = 1.0;
   frontSpeedPer = 1.5;
   frontRadiusPer = 0.5;

};

function skyDusk()
{
MessageAll('Msg', "\c2And the day went to dusk....");
	Sky.delete();
 	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.5 0.2 0.00 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "SOM_TR2_Armageddon.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}
function skyCloudy()
{
//    MessageAll('Msg', "\c2And the day went cloudy....");
	Sky.delete();
 	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.3 0.3 0.3 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_badlands_cloudy.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}
function skyThunderStorm()
{
//MessageAll('Msg', "\c2And the thunder rolled in....");
	Sky.delete();
	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.2 0.2 0.2 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_badlands_cloudy.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
 	new Lightning(Lightning)
	{
		position = "0 0 0";
		rotation = "1 1 0 0";
		scale = "9999 9999 160";
		dataBlock = "DefaultStorm";
		strikesPerMinute = 30;
		strikeWidth = 1 + getrandom() * 3;
		chanceToHitTarget = 0.0;
		strikeRadius = 1000;
		boltStartRadius = 1000;
		color = "1 1 1";
		fadeColor = "1 1 1";
		useFog = "0";
	};
    new Precipitation(Precipitation)
		{
		position = "-225.463 143.423 202.782";
		rotation = "1 0 0 0";
		scale = "5 5 5";
		dataBlock = "MyReign";
		percentage = "1";
		color1 = "0.8 0.8 0.8 1";
		color2 = "0.6 0.6 0.6 1";
		color3 = "0.4 0.4 0.4 1";
		offsetSpeed = "0.25";
		minVelocity = "1";
		maxVelocity = "2";
		maxNumDrops = "2000";
		maxRadius = "100";
		};
}
function skyRain()
{
//    MessageAll('Msg', "\c2And the rain came....");
	Sky.delete();
//  	Precipitation.audioProfile.delete();
 	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.3 0.3 0.3 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_desert_brown.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
   new Precipitation(Precipitation)
		{
		position = "-225.463 143.423 202.782";
		rotation = "1 0 0 0";
		scale = "5 5 5";
		dataBlock = "MyReign";
		percentage = "1";
		color1 = "0.8 0.8 0.8 1";
		color2 = "0.6 0.6 0.6 1";
		color3 = "0.4 0.4 0.4 1";
		offsetSpeed = "0.25";
		minVelocity = "1";
		maxVelocity = "2";
		maxNumDrops = "2000";
		maxRadius = "100";
		};
}
function skyVeryDark()
{
//MessageAll('Msg', "\c2And night was pitch black....");
	Sky.delete();
  	//Sun.delete();
 	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "0";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.0 0.0 0.0 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_desert_starrynight.dml";
		windVelocity = "1 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}
function skyDaylight()
{
//    MessageAll('Msg', "\c2And daylight had come....");
	Sky.delete();
//  	Precipitation.audioProfile.delete();
 	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.5 0.5 0.5 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_lush_blue.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}
function skyNight()
{
//MessageAll('Msg', "\c2And night came.....");
	Sky.delete();
//  	Precipitation.audioProfile.delete();
 	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.0 0.0 0.00 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_lava_starrynight.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}

function skySunny()
{
//MessageAll('Msg', "\c2And the day was sunny....");
	Sky.delete();
 	Precipitation.delete();
 	Lightning.delete();
//   	Precipitation.audioProfile.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.650000 0.650000 0.500000 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "SOM_TR2_WinterBlue.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}

function skyMorning()
{
//MessageAll('Msg', "\c2And the morning awakes....");
	Sky.delete();
//  	Precipitation.audioProfile.delete();
 	Precipitation.delete();
 	Lightning.delete();
	new Sky(Sky)
		{
		position = "-1024 -1584 0";
		rotation = "1 0 0 0";
		scale = "1 1 1";
		cloudHeightPer[0] = "0.349971";
		cloudHeightPer[1] = "0.25";
		cloudHeightPer[2] = "0.199973";
		cloudSpeed1 = "0.0001";
		cloudSpeed2 = "0.0002";
		cloudSpeed3 = "0.0003";
		visibleDistance = "5000";
		useSkyTextures = "1";
		renderBottomTexture = "0";
		SkySolidColor = "0.000000 0.000000 0.000000 0.000000";
		fogDistance = "4500";
		fogColor = "0.6 0.6 0.7 1";
		fogVolume1 = "0 0 0";
		fogVolume2 = "0 0 0";
		fogVolume3 = "0 0 0";
		materialList = "sky_lush_blue.dml";
		windVelocity = "0 0 0";
		windEffectPrecipitation = "0";
		fogVolumeColor1 = "128.000000 128.000000 128.000000 1";
		fogVolumeColor2 = "128.000000 128.000000 128.000000 0.000000";
		fogVolumeColor3 = "128.000000 128.000000 128.000000 0.000000";
		cloudSpeed0 = "0.000000 0.000000";
	};
}


//End of Quantium Functions.

//Time for the actual dayshift.
$DayshiftDelay = 6; //6 minutes b/t changes.
//schedule(10000,0,skyMorning);
cancel($DayCycle);
//$DayCycle = schedule($DayshiftDelay*60*1000,0,dayshift,0); //Start the dayshift at morning.
//Dayshift cycles...
// 0 -- Morning
// 1 -- Daylight
// 2 -- Sunny
// 3 -- Cloudy
// 4 -- Rainy
// 5 -- Thunderstorm
// 6 -- Dusk
// 7 -- Night --- ZOMBIES!



//Don't use the following... it disconnects peoples.

function dayshift(%cycle) //Cycle being what we're at currently.
{
if (%cycle == 7) {
skyMorning(); //We always move from night straight to morning...
//And disable zombies.
EndZombies();
%current = 0; 
}
if (%cycle == 0) {
skyDaylight(); //We always move from morning straight to daylight...
%current = 1;
}
if (%cycle == 1) { //Move from daylight to a random weather change.
%wrnd = (getRandom(0,3) + 2); //2, 3, 4, or 5?
if (%wrnd == 2) {
skySunny(); //It's nice and sunny... we move straight to dusk from here.
%current = 2;
}
if (%wrnd == 3) {
skyCloudy(); //It's nice and cloudy... we move straight to dusk from here.
%current = 3;
}
if (%wrnd == 4) {
skyRain(); //It's nice and rainy... we move straight to dusk from here.
%current = 4;
}
if (%wrnd == 5) {
skyThunderstorm(); //It's nice and THUNDERSTORMY... we move straight to dusk from here.
%current = 5;
}
}
if (%cycle == 2 || %cycle == 3 || %cycle == 4 || %cycle == 5) {
skyDusk(); //We move straight to dusk from here.
%current = 6;
}
if (%cycle == 6) {
skyNight(); //From Dusk, we go to night...
//AND ENABLE ZOMBIEZ
$ZombiesEnabled = 1;
ZombieLoop();
%current = 7;
}
cancel($DayCycle); //Already a daycycle going?
$DayCycle = schedule($DayshiftDelay*60*1000,0,dayshift,%current);
}