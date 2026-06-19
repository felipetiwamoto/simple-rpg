using SimpleRPG.Core.Entities;
using SimpleRPG.Core.Entities.Enemies;
using SimpleRPG.Core.Entities.Heroes;
using SimpleRPG.Core.Enums;
using SimpleRPG.Core.Fight;

// HEROES
Entity ethan = new EthanHero(99);
Entity vivian = new VivianHero(99);
Entity maria = new MariaHero(99);

//ethan.AddCostume(new EthanCostume01(5));
ethan.AddCostume(new EthanCostume02(4));
ethan.AddCostume(new EthanCostume03(3));

vivian.AddCostume(new VivianCostume01(5));
//vivian.AddCostume(new VivianCostume02(5));
vivian.AddCostume(new VivianCostume03(5));

maria.AddCostume(new MariaCostume01(5));
maria.AddCostume(new MariaCostume02(5));
//maria.AddCostume(new MariaCostume03(5));

EntityInBoard[] heroesInBoard = {
    new EntityInBoard(ethan, 0, 0, 3),
    new EntityInBoard(vivian, 0, 1, 1),
    new EntityInBoard(maria, 0, 2, 2),
};

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
    //new EntityInBoard(orcFire, 2, 0, 1),
    //new EntityInBoard(orcNeutral, 1, 0, 2),
    //new EntityInBoard(orcWater, 0, 0, 3),
    new EntityInBoard(dragonWater, 1, 1, 1),
    new EntityInBoard(dragonFire, 3, 1, 2),
    new EntityInBoard(dragonNeutral, 2, 1, 3),
    new EntityInBoard(skeletonNeutral, 0, 1, 4),
    new EntityInBoard(skeletonWater, 0, 2, 5),
    new EntityInBoard(skeletonFire, 3, 2, 6),
};

// BATTLE
int maxSp = 20;
Board heroesBoard = new Board(4, 3, heroesInBoard, maxSp);
Board enemiesBoard = new Board(4, 3, enemiesInBoard, maxSp);
Battle battle = new Battle(heroesBoard, enemiesBoard, maxSp);
battle.Run();
