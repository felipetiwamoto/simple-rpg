using SimpleRPG.Core.Entities;
using SimpleRPG.Core.Entities.Enemies;
using SimpleRPG.Core.Entities.Heroes;
using SimpleRPG.Core.Enums;
using SimpleRPG.Core.Fight;

// HEROES
Entity ethan = new EthanHero(99);
Entity vivian = new VivianHero(99);
Entity ethan2 = new EthanHero(99);

ethan.AddCostume(new EthanCostume01(5));
ethan.AddCostume(new EthanCostume02(4));
ethan.AddCostume(new EthanCostume03(3));

EntityInBoard[] heroesInBoard = {
    new EntityInBoard(ethan, 0, 0),
    new EntityInBoard(vivian, 0, 1),
    new EntityInBoard(ethan2, 0, 2),
};
Board heroesBoard = new Board(4, 3, heroesInBoard);

// ENEMIES
Entity orcWater = new OrcEnemy(80, PropertyEnum.Water);
Entity dragonWater = new DragonEnemy(85, PropertyEnum.Water);
Entity skeletonWater = new SkeletonEnemy(75, PropertyEnum.Water);

Entity orcNeutral = new OrcEnemy(80, PropertyEnum.Neutral);
Entity dragonNeutral = new DragonEnemy(85, PropertyEnum.Neutral);
Entity skeletonNeutral = new SkeletonEnemy(75, PropertyEnum.Neutral);

Entity orcFire = new OrcEnemy(80, PropertyEnum.Fire);
Entity dragonFire = new DragonEnemy(85, PropertyEnum.Fire);
Entity skeletonFire = new SkeletonEnemy(75, PropertyEnum.Fire);

EntityInBoard[] enemiesInBoard = {
    new EntityInBoard(orcWater, 0, 0),
    new EntityInBoard(orcNeutral, 1, 0),
    new EntityInBoard(orcFire, 2, 0),
    new EntityInBoard(dragonWater, 1, 1),
    new EntityInBoard(dragonNeutral, 2, 1),
    new EntityInBoard(dragonFire, 3, 1),
    new EntityInBoard(skeletonWater, 0, 2),
    new EntityInBoard(skeletonNeutral, 1, 2),
    new EntityInBoard(skeletonFire, 3, 2),
};
Board enemiesBoard = new Board(4, 3, enemiesInBoard);

// BATTLE
Battle battle = new Battle(heroesBoard, enemiesBoard);
battle.Run();
