using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace EntityComponentSystem.Tests
{
    /// <summary>
    /// Тестовый пустой компонент, используется для тестов. Не удалять!
    /// </summary>
    public class TestComponent : Component
    {
        // Тут пусто - так надо!
    }


    [TestClass]
    public class GameObjectTests
    {
        [TestMethod]
        public void AddComponentReturnsInstanceOfCreatedComponent()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate();
                var position = obj.AddComponent<TestComponent>();

                if (position == null)
                {
                    Assert.Fail();
                    return;
                }

                Assert.IsTrue(position.GetType() == typeof(TestComponent));
            }
        }

        [TestMethod]
        public void GetComponentReturnsInstanceOfComponentByComponentGenericTypeIfComponenetExists()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate(
                    new Prefab(OnBeforeOnAwake: null, typeof(TestComponent))
                );

                var position = obj.GetComponent<TestComponent>();

                if (position == null)
                {
                    Assert.Fail();
                    return;
                }
                Assert.IsTrue(position.GetType() == typeof(TestComponent));
            }
        }
        [TestMethod]
        public void GetComponentReturnsNullByComponentGenericTypeIfComponenetNotExists()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate();
                var position = obj.GetComponent<TestComponent>();

                Assert.IsNull(position);
            }
        }
        [TestMethod]
        public void GetComponentReturnsInstanceOfComponentByComponentTypeIfComponenetExists()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate(
                    new Prefab(OnBeforeOnAwake: null, typeof(TestComponent))
                );
                var position = obj.GetComponent(typeof(TestComponent));

                if (position == null)
                {
                    Assert.Fail();
                    return;
                }
                Assert.IsTrue(position.GetType() == typeof(TestComponent));
            }
        }
        [TestMethod]
        public void GetComponentReturnsNullByComponentTypeIfComponenetNotExists()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate();
                var position = obj.GetComponent(typeof(TestComponent));

                Assert.IsNull(position);
            }
        }

        [TestMethod]
        public void GetOrAddComponentReturnInstanceOfComponentByComponentGenericTypeIfComponentNotExists()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate();
                var position = obj.GetOrAddComponent<TestComponent>();

                Assert.IsNotNull(position);
            }
        }
        [TestMethod]
        public void GetOrAddComponentReturnInstanceOfComponentByComponentGenericTypeIfComponentExists()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate(
                    new Prefab(OnBeforeOnAwake: null, typeof(TestComponent))
                );
                var position = obj.GetOrAddComponent<TestComponent>();

                Assert.IsNotNull(position);
            }
        }

        [TestMethod]
        public void InstantiateReturnsNotNullResult()
        {
            using (var scene = new Scene())
            {
                var scene_instantiated = scene.Instantiate();
                if (scene_instantiated == null)
                {
                    Assert.Fail();
                    return;
                }
                var gameobject_instatiated = scene_instantiated.Instantiate();
                Assert.IsNotNull(gameobject_instatiated);
            }
        }
        [TestMethod]
        public void DestroyShouldBeRemoveGameObjectFromScene()
        {
            using (var scene = new Scene())
            {
                var scene_instantiated = scene.Instantiate(
                    new Prefab(OnBeforeOnAwake: null, typeof(TestComponent))
                );
                scene_instantiated.Destroy();
                var gameObjectWherePositionComponenetIsExsits = scene.FindGameObjectsWithType<TestComponent>();
                Assert.IsTrue(gameObjectWherePositionComponenetIsExsits.Length == 0);
            }
        }

        [TestMethod]
        public void InstantiatedGameObjectIsNotActiveByDefault()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate();
                Assert.IsTrue(!obj.IsActive);
            }
        }
        [TestMethod]
        public void AddComponentReturnEnabledComponent()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate();
                var test = obj.AddComponent<TestComponent>();
                Assert.IsTrue(test.IsEnabled);
            }
        }
        [TestMethod]
        public void GameObjectInstantiatedByArrayOfComponentTypesHasAllEnabledComponents()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate(
                    new Prefab(OnBeforeOnAwake: null, typeof(TestComponent))
                );
                Assert.IsTrue(obj.GetComponent<TestComponent>().IsEnabled);
            }
        }
        [TestMethod]
        public void InstantiatedGameObjectHasScene()
        {
            using (var scene = new Scene())
            {
                var obj = scene.Instantiate();
                Assert.IsNotNull(obj.scene);
            }
        }
        [TestMethod]
        public void SceneTimeIsNotNull()
        {
            using (var scene = new Scene())
            {
                scene.Start();
                Assert.IsNotNull(scene.time);
            }
        }
    }
}
