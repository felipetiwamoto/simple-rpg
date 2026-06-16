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
Board heroesBoard = new Board(5, 5, heroesInBoard);

// ENEMIES
Entity orc = new OrcEnemy(80, PropertyEnum.Water);
Entity dragon = new DragonEnemy(85, PropertyEnum.Water);

EntityInBoard[] enemiesInBoard = {
    new EntityInBoard(orc, 0, 0),
    new EntityInBoard(dragon, 0, 1)
};
Board enemiesBoard = new Board(5, 5, enemiesInBoard);

// BATTLE
Battle battle = new Battle(heroesBoard, enemiesBoard);
battle.Run();
