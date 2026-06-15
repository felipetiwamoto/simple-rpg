using SimpleRPG.Core.Entities;
using SimpleRPG.Core.Entities.Enemies;
using SimpleRPG.Core.Entities.Heroes;
using SimpleRPG.Core.Enums;
using SimpleRPG.Core.Fight;

// HEROES
Entity ethan = new EthanHero(99);
Entity vivian = new VivianHero(99);

ethan.AddCostume(new EthanCostume01(0));
ethan.AddCostume(new EthanCostume02(0));
ethan.AddCostume(new EthanCostume03(0));

EntityInBoard[] heroesInBoard = {
    new EntityInBoard(ethan, 0, 0),
    new EntityInBoard(vivian, 0, 1)
};
Board heroesBoard = new Board(4, 3, heroesInBoard);

// ENEMIES
Entity orc = new OrcEnemy(80, PropertyEnum.Water);
Entity dragon = new DragonEnemy(85, PropertyEnum.Water);

EntityInBoard[] enemiesInBoard = {
    new EntityInBoard(orc, 0, 0),
    new EntityInBoard(dragon, 0, 1)
};
Board enemiesBoard = new Board(4, 3, enemiesInBoard);

// BATTLE
Battle battle = new Battle(heroesBoard, enemiesBoard);
battle.Run();
