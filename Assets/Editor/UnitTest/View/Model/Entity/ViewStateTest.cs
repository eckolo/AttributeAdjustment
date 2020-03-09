using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock.Model.Entity;
using Assets.Src.Mock.Model.Value;
using Assets.Src.View.Model.Entity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Editor.UnitTest.View.Model.Entity
{
    /// <summary>
    /// <see cref="MonoBehaviourMock"/>のテスト
    /// </summary>
    public class ViewStateTest
    {
        static readonly IViewKey[] keys = new[]
        {
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(0) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(1) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(2) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(3) }),
            ViewStateKeyMock.Generate(new[] { ViewKeyMock.Generate(4) }),
        };
        static readonly ViewDeployment[] deploys = new[]
        {
            new ViewDeployment(new Vector2(0, 0)),
            new ViewDeployment(new Vector2(1, 1)),
            new ViewDeployment(new Vector2(2, 2)),
            new ViewDeployment(new Vector2(3, 3)),
            new ViewDeployment(new Vector2(4, 4)),
        };
        static readonly MonoBehaviourMock[] values = new[]
        {
            MonoBehaviourMock.Generate($"{nameof(ViewStateTest)}_0"),
            MonoBehaviourMock.Generate($"{nameof(ViewStateTest)}_1"),
            MonoBehaviourMock.Generate($"{nameof(ViewStateTest)}_2"),
            MonoBehaviourMock.Generate($"{nameof(ViewStateTest)}_3"),
        };

        [Test]
        public static void SaveTest_正常系_既存配置無し()
        {
            var addKey = keys[0];
            var addDeploy = deploys[0];
            var addValue = values[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    deploys[1],
                    new Dictionary<IViewKey, Queue<Component>>{
                        { keys[1], new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);

            var result = repository.Save(addDeploy, addKey, addValue);

            result.IsNotNull();
            result.IsSameReferenceAs(addValue);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(3);
            resultDeployMap.ContainsKey(deploys[1]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[1]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(keys[1]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[1]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(2);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
            resultDeployMap.ContainsKey(addDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[addDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(1);
                resultKeyMap.ContainsKey(addKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[addKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(addValue);
                }
            }
        }
        [Test]
        public static void SaveTest_正常系_既存配置有り_既存キー無し()
        {
            var addKey = keys[0];
            var addDeploy = deploys[0];
            var addValue = values[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    addDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { keys[1], new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);

            var result = repository.Save(addDeploy, addKey, addValue);

            result.IsNotNull();
            result.IsSameReferenceAs(addValue);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(addDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[addDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(3);
                resultKeyMap.ContainsKey(keys[1]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[1]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(2);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
                resultKeyMap.ContainsKey(addKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[addKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(addValue);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void SaveTest_正常系_既存配置有り_既存キー有り()
        {
            var addKey = keys[0];
            var addDeploy = deploys[0];
            var addValue = values[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    addDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { addKey, new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);

            var result = repository.Save(addDeploy, addKey, addValue);

            result.IsNotNull();
            result.IsSameReferenceAs(addValue);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(addDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[addDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(addKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[addKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(3);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                    resultQueue.ToArray()[2].Is(addValue);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void SaveTest_異常系_配置がNull()
        {
            var addKey = keys[0];
            var addDeploy = (ViewDeployment)null;
            var addValue = values[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    deploys[1],
                    new Dictionary<IViewKey, Queue<Component>>{
                        { keys[1], new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);

            Assert.Throws<ArgumentNullException>(() => repository.Save(addDeploy, addKey, addValue));
        }
        [Test]
        public static void SaveTest_異常系_キーがNull()
        {
            var addKey = (IViewKey)null;
            var addDeploy = deploys[0];
            var addValue = values[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    deploys[1],
                    new Dictionary<IViewKey, Queue<Component>>{
                        { keys[1], new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);

            Assert.Throws<ArgumentNullException>(() => repository.Save(addDeploy, addKey, addValue));
        }

        [Test]
        public static void SearchTest_正常系_該当配置有り_該当キー有り_キュー複数()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Search(searchDeploy, searchKey).Is(values[1]);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(2);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void SearchTest_正常系_該当配置有り_該当キー有り_キュー単数()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>(new[] { values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Search(searchDeploy, searchKey).Is(values[2]);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void SearchTest_正常系_該当配置有り_該当キー有り_キュー空()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>() },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Search(searchDeploy, searchKey).Is(default);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(0);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void SearchTest_正常系_該当配置有り_該当キー無し()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { keys[1], new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Search(searchDeploy, searchKey).Is(default);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(keys[1]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[1]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(2);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void SearchTest_正常系_該当配置無し()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    deploys[1],
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Search(searchDeploy, searchKey).Is(default);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(deploys[1]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[1]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(2);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }

        [Test]
        public static void PopTest_正常系_該当配置有り_該当キー有り_キュー複数()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Pop(searchDeploy, searchKey).Is(values[1]);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void PopTest_正常系_該当配置有り_該当キー有り_キュー単数()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>(new[] { values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Pop(searchDeploy, searchKey).Is(values[2]);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(0);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void PopTest_正常系_該当配置有り_該当キー有り_キュー空()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>() },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Pop(searchDeploy, searchKey).Is(default);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(0);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void PopTest_正常系_該当配置有り_該当キー無し()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    searchDeploy,
                    new Dictionary<IViewKey, Queue<Component>>{
                        { keys[1], new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Pop(searchDeploy, searchKey).Is(default);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(searchDeploy).IsTrue();
            {
                var resultKeyMap = resultDeployMap[searchDeploy];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(keys[1]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[1]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(2);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
        [Test]
        public static void PopTest_正常系_該当配置無し()
        {
            var searchDeploy = deploys[0];
            var searchKey = keys[0];
            var map = new Dictionary<ViewDeployment, Dictionary<IViewKey, Queue<Component>>>
            {
                {
                    deploys[1],
                    new Dictionary<IViewKey, Queue<Component>>{
                        { searchKey, new Queue<Component>(new[] { values[1], values[2] }) },
                        { keys[2], new Queue<Component>(new[] { values[3] }) },
                    }
                },
                {
                    deploys[2],
                    new Dictionary<IViewKey, Queue<Component>>()
                },
            };
            var repository = ViewStateMock.Generate(map);
            repository.Pop(searchDeploy, searchKey).Is(default);

            var resultDeployMap = repository.GetAllMap();
            resultDeployMap.IsNotNull();
            resultDeployMap.Count.Is(2);
            resultDeployMap.ContainsKey(deploys[1]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[1]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(2);
                resultKeyMap.ContainsKey(searchKey).IsTrue();
                {
                    var resultQueue = resultKeyMap[searchKey];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(2);
                    resultQueue.ToArray()[0].Is(values[1]);
                    resultQueue.ToArray()[1].Is(values[2]);
                }
                resultKeyMap.ContainsKey(keys[2]).IsTrue();
                {
                    var resultQueue = resultKeyMap[keys[2]];
                    resultQueue.IsNotNull();
                    resultQueue.Count.Is(1);
                    resultQueue.ToArray()[0].Is(values[3]);
                }
            }
            resultDeployMap.ContainsKey(deploys[2]).IsTrue();
            {
                var resultKeyMap = resultDeployMap[deploys[2]];
                resultKeyMap.IsNotNull();
                resultKeyMap.Count.Is(0);
            }
        }
    }
}
