using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using AsteroidsEngine;
using AsteroidsEngine.Components;
using EntityComponentSystem;
using System.Security;
using System.Linq;

namespace AsteroidsEngine.Tests
{
    [TestClass]
    public class AsteroidsEngineTests
    {
        [TestMethod]
        public void SetupShouldBeSetSettings()
        {
            using (var scene = new AsteroidsGameScene())
            {
                scene.Setup(new AsteroidsGameSettings()
                {
                    UFOSpeed = 0,
                });
                Assert.IsTrue(scene.settings.UFOSpeed == 0);
            }
        }
        [TestMethod]
        public void NewGameSpawnsPlayer()
        {
            using (var scene = new AsteroidsGameScene())
            {
                scene.Start();
                Assert.IsTrue(scene.Player != null);
            }
        }
        [TestMethod]
        public void NewGameSpawnsPlayerNormally()
        {
            using (var scene = new AsteroidsGameScene())
            {
                scene.Start();
                try
                {
                    var playerX = scene.Player.X;
                    var playerY = scene.Player.Y;
                }
                catch
                {
                    Assert.Fail();
                }
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void F()
        {
            using (var scene = new AsteroidsGameScene())
            {
                scene.Start();
                var gen = scene.FindComponentsByType<EnemyObjectsGenerator>();
                scene.Update(1);
                gen.First().SpawnSomeObject();
                scene.Update(1);
                Assert.IsTrue(true);
            }
        }
    }
}
