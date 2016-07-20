//Player Specific deployable limitations. 

//Defintions for deployable "groups" or "types". Just increment by 1 to add more.
$DeployableGroup::LargeTurret = 0;
$DeployableGroup::PermissionSphere = 1;
$DeployableGroup::SmallTurret = 2;
$DeployableGroup::Energizers = 3;
$DeployableGroup::EnergizerLights = 4;

//Names for deployable groups.
$DeployableGroupName[$DeployableGroup::LargeTurret] = "Deployable Base Turret";
$DeployableGroupName[$DeployableGroup::PermissionSphere] = "Permission Sphere";
$DeployableGroupName[$DeployableGroup::SmallTurret] = "Small Turret";
$DeployableGroupName[$DeployableGroup::Energizers] = "Freaking Energizer";

//Limits for each group.
$PlayerDeployableMax[$DeployableGroup::LargeTurret] = 6;
$PlayerDeployableMax[$DeployableGroup::PermissionSphere] = 2;
$PlayerDeployableMax[$DeployableGroup::SmallTurret] = 12;
$PlayerDeployableMax[$DeployableGroup::Energizers] = 1;
$PlayerDeployableMax[$DeployableGroup::EnergizerLights] = 4;




//Define which deployables belong to each group...

//Base turrets...
$DeployableGroup[TurretDeployedBase] = $DeployableGroup::LargeTurret;

//Small turrets...
$DeployableGroup[TurretDeployedCeilingIndoor] = $DeployableGroup::SmallTurret; //Clamp turret
$DeployableGroup[TurretDeployedWallIndoor] = $DeployableGroup::SmallTurret; //Clamp turret
$DeployableGroup[TurretDeployedFloorIndoor] = $DeployableGroup::SmallTurret; //Clamp turret
$DeployableGroup[TurretDeployedOutdoor] = $DeployableGroup::SmallTurret; //Spike turret
$DeployableGroup[DiscTurretDeployed] = $DeployableGroup::SmallTurret; //Disc turret
$DeployableGroup[Mpm_Anti_TurretDeployed] = $DeployableGroup::SmallTurret; //Anti turret
$DeployableGroup[LaserDeployed] = $DeployableGroup::SmallTurret; //Laser turret

//Permission Spheres...
$DeployableGroup[DeployedOOSphere] = $DeployableGroup::PermissionSphere;
$DeployableGroup[DeployedPeaceSphere] = $DeployableGroup::PermissionSphere;
$DeployableGroup[DeployedCombatSphere] = $DeployableGroup::PermissionSphere;

//Energizers
$DeployableGroup[EnergizerLight] = $DeployableGroup::EnergizerLights;
$DeployableGroup[DeployedEnergizer] = $DeployableGroup::Energizers;
