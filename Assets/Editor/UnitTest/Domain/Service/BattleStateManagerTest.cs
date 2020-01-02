using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Repository;
using Assets.Src.Domain.Service;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="BattleStateManager"/>サービスのテスト
    /// </summary>
    public static class BattleStateManagerTest
    {
        [Test]
        public static void SetupDeckTest_通常処理_既存無し()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap);

            var result = state.SetupDeck();
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            {
                var resultViewActionList = result.viewActionList;
                resultViewActionList.IsNotNull();
                resultViewActionList.Count.Is(value1 + value2 + value3);
                resultViewActionList
                    .Where(elem => elem.actionType == ViewAction.Pattern.GENERATE)
                    .Where(elem => elem.actor.Equals(tip1))
                    .Where(elem => elem.target == default)
                    .Where(elem => elem.easing == default)
                    .Count().Is(value1);
                resultViewActionList
                    .Where(elem => elem.actionType == ViewAction.Pattern.GENERATE)
                    .Where(elem => elem.actor.Equals(tip2))
                    .Where(elem => elem.target == default)
                    .Where(elem => elem.easing == default)
                    .Count().Is(value2);
                resultViewActionList
                    .Where(elem => elem.actionType == ViewAction.Pattern.GENERATE)
                    .Where(elem => elem.actor.Equals(tip3))
                    .Where(elem => elem.target == default)
                    .Where(elem => elem.easing == default)
                    .Count().Is(value3);
            }
            {
                var resultDeckTips = result.deckTips;
                resultDeckTips.IsNotNull();
                resultDeckTips.Count.Is(value1 + value2 + value3);
                resultDeckTips
                    .Where(elem => elem.energy == tip1.energy)
                    .Where(elem => elem.energyValue == tip1.energyValue)
                    .Count().Is(value1);
                resultDeckTips
                    .Where(elem => elem.energy == tip2.energy)
                    .Where(elem => elem.energyValue == tip2.energyValue)
                    .Count().Is(value2);
                resultDeckTips
                    .Where(elem => elem.energy == tip3.energy)
                    .Where(elem => elem.energyValue == tip3.energyValue)
                    .Count().Is(value3);
            }
        }
        [Test]
        public static void SetupDeckTest_通常処理_既存有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap);
            state.deckTips.Enqueue(tip2);

            var result = state.SetupDeck();
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            {
                var resultViewActionList = result.viewActionList;
                resultViewActionList.IsNotNull();
                resultViewActionList.Count.Is(value1 + value2 + value3 + 1);
                {
                    var resultViewAction = resultViewActionList[0];
                    resultViewAction.actionType.Is(ViewAction.Pattern.DELETE);
                    resultViewAction.actor.Is(tip2);
                    resultViewAction.target.Is(default);
                    resultViewAction.easing.Is(default);
                }
                resultViewActionList
                    .Where(elem => elem.actionType == ViewAction.Pattern.GENERATE)
                    .Where(elem => elem.actor.Equals(tip1))
                    .Where(elem => elem.target == default)
                    .Where(elem => elem.easing == default)
                    .Count().Is(value1);
                resultViewActionList
                    .Where(elem => elem.actionType == ViewAction.Pattern.GENERATE)
                    .Where(elem => elem.actor.Equals(tip2))
                    .Where(elem => elem.target == default)
                    .Where(elem => elem.easing == default)
                    .Count().Is(value2);
                resultViewActionList
                    .Where(elem => elem.actionType == ViewAction.Pattern.GENERATE)
                    .Where(elem => elem.actor.Equals(tip3))
                    .Where(elem => elem.target == default)
                    .Where(elem => elem.easing == default)
                    .Count().Is(value3);
            }
            {
                var resultDeckTips = result.deckTips;
                resultDeckTips.IsNotNull();
                resultDeckTips.Count.Is(value1 + value2 + value3);
                resultDeckTips
                    .Where(elem => elem.energy == tip1.energy)
                    .Where(elem => elem.energyValue == tip1.energyValue)
                    .Count().Is(value1);
                resultDeckTips
                    .Where(elem => elem.energy == tip2.energy)
                    .Where(elem => elem.energyValue == tip2.energyValue)
                    .Count().Is(value2);
                resultDeckTips
                    .Where(elem => elem.energy == tip3.energy)
                    .Where(elem => elem.energyValue == tip3.energyValue)
                    .Count().Is(value3);
            }
        }
        [Test]
        public static void SetupDeckTest_元データが空()
        {
            var tipMap = new Dictionary<MotionTip, int> { };
            var state = BattleStateMock.Generate(tipMap);

            var result = state.SetupDeck();
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            {
                var resultViewActionList = result.viewActionList;
                resultViewActionList.IsNotNull();
                resultViewActionList.Count.Is(0);
            }
            {
                var resultDeckTips = result.deckTips;
                resultDeckTips.IsNotNull();
                resultDeckTips.Count.Is(0);
            }
        }
        [Test]
        public static void SetupDeckTest_元データがNull()
        {
            var state = BattleStateMock.Generate((Dictionary<MotionTip, int>)null);

            var result = state.SetupDeck();
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            {
                var resultViewActionList = result.viewActionList;
                resultViewActionList.IsNotNull();
                resultViewActionList.Count.Is(0);
            }
            {
                var resultDeckTips = result.deckTips;

                resultDeckTips.IsNotNull();
                resultDeckTips.Count.Is(0);
            }
        }

        [Test]
        public static void PopDeckTipsForcedTest_通常処理_山札数が取り出し数より大きい()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate();
            state.CleanupDeckTips(tipList);
            var popTipNumber = 5;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count - popTipNumber);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsForcedTest_通常処理_山札数が取り出し数と等しい()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip1, tip2, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap);
            state.CleanupDeckTips(tipList);
            var popTipNumber = tipList.Count;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(value1 + value2 + value3);
            state.deckTips
                .Where(elem => elem.energy == tip1.energy)
                .Count(elem => elem.energyValue == tip1.energyValue)
                .Is(value1);
            state.deckTips
                .Where(elem => elem.energy == tip2.energy)
                .Count(elem => elem.energyValue == tip2.energyValue)
                .Is(value2);
            state.deckTips
                .Where(elem => elem.energy == tip3.energy)
                .Count(elem => elem.energyValue == tip3.energyValue)
                .Is(value3);
        }
        [Test]
        public static void PopDeckTipsForcedTest_通常処理_山札数が取り出し数未満()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;

            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip1, tip2, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(tipMap);
            state.CleanupDeckTips(tipList);
            var popTipNumber = 10;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(popTipNumber);
            result.All(elem => tipList.Contains(elem)).IsTrue();

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count + value1 + value2 + value3 - popTipNumber);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsForcedTest_取り出し数が0()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate();
            state.CleanupDeckTips(tipList);
            var popTipNumber = 0;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(0);

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }
        [Test]
        public static void PopDeckTipsForcedTest_取り出し数が負の値()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
            var state = BattleStateMock.Generate();
            state.CleanupDeckTips(tipList);
            var popTipNumber = -5;

            var result = state.PopDeckTipsForced(popTipNumber);

            result.IsNotNull();
            result.Count().Is(0);

            state.deckTips.IsNotNull();
            state.deckTips.Count.Is(tipList.Count);
            state.deckTips.All(elem => tipList.Contains(elem)).IsTrue();
        }

        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipNew = MotionTipMock.Generate(Energy.BLOW, 30);
            var value1 = 1;
            var value2 = 5;
            var value3 = 2;
            var valueNew = 1;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(new Dictionary<MotionTip, int> { { tipNew, valueNew } });
            state.CleanupDeckTips(tipMap.Embody());

            var result = state.SetupBoard();
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            result.viewActionList.IsNotNull();
            result.viewActionList.Count.Is(Constants.Battle.DEFAULT_BOARD_TIP_NUMBERS);
            {
                var resultViewAction = result.viewActionList[0];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[1];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[2];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[3];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[4];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[5];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[6];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip3);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_BOARD_TIP_NUMBERS);
            result.boardTips.IsNotNull();
            result.boardTips.Count().Is(Constants.Battle.DEFAULT_BOARD_TIP_NUMBERS);
            result.boardTips.All(tip => tip is MotionTip).IsTrue();
            result.boardTips.ToArray()[0].Is(tip1);
            result.boardTips.ToArray()[1].Is(tip2);
            result.boardTips.ToArray()[2].Is(tip2);
            result.boardTips.ToArray()[3].Is(tip2);
            result.boardTips.ToArray()[4].Is(tip2);
            result.boardTips.ToArray()[5].Is(tip2);
            result.boardTips.ToArray()[6].Is(tip3);
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipNew = MotionTipMock.Generate(Energy.BLOW, 30);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;
            var valueNew = 1;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(new Dictionary<MotionTip, int> { { tipNew, valueNew } });
            state.CleanupDeckTips(tipMap.Embody());

            var result = state.SetupBoard();
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            result.viewActionList.IsNotNull();
            result.viewActionList.Count.Is(value1 + value2 + value3 + valueNew);
            {
                var resultViewAction = result.viewActionList[0];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tipNew);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[1];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[2];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[3];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[4];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[5];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip3);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[6];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip3);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(valueNew);
            result.boardTips.IsNotNull();
            result.boardTips.Count().Is(value1 + value2 + value3);
            result.boardTips.All(tip => tip is MotionTip).IsTrue();
            result.boardTips.ToArray()[0].Is(tip1);
            result.boardTips.ToArray()[1].Is(tip1);
            result.boardTips.ToArray()[2].Is(tip1);
            result.boardTips.ToArray()[3].Is(tip2);
            result.boardTips.ToArray()[4].Is(tip3);
            result.boardTips.ToArray()[5].Is(tip3);
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値_山札が空()
        {
            var state = BattleStateMock.Generate();
            var result = state.SetupBoard();
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            result.viewActionList.IsNotNull();
            result.viewActionList.Count.Is(0);
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(0);
            result.boardTips.IsNotNull();
            result.boardTips.Count().Is(0);
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数デフォルト値_戦闘状態がNull()
        {
            var result = BattleStateManager.SetupBoard(null);

            result.IsNull();
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipNew = MotionTipMock.Generate(Energy.BLOW, 30);
            var value1 = 2;
            var value2 = 1;
            var value3 = 6;
            var valueNew = 1;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(new Dictionary<MotionTip, int> { { tipNew, valueNew } });
            state.CleanupDeckTips(tipMap.Embody());
            var tipNumbers = 4;

            var result = state.SetupBoard(tipNumbers);
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            result.viewActionList.IsNotNull();
            result.viewActionList.Count.Is(tipNumbers);
            {
                var resultViewAction = result.viewActionList[0];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[1];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[2];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[3];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip3);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - tipNumbers);
            result.boardTips.IsNotNull();
            result.boardTips.Count().Is(tipNumbers);
            result.boardTips.All(tip => tip is MotionTip).IsTrue();
            result.boardTips.ToArray()[0].Is(tip1);
            result.boardTips.ToArray()[1].Is(tip1);
            result.boardTips.ToArray()[2].Is(tip2);
            result.boardTips.ToArray()[3].Is(tip3);
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var tipNew = MotionTipMock.Generate(Energy.BLOW, 30);
            var value1 = 3;
            var value2 = 1;
            var value3 = 2;
            var valueNew = 1;

            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var state = BattleStateMock.Generate(new Dictionary<MotionTip, int> { { tipNew, valueNew } });
            state.CleanupDeckTips(tipMap.Embody());
            var tipNumbers = 8;

            var result = state.SetupBoard(tipNumbers);
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            result.viewActionList.IsNotNull();
            result.viewActionList.Count.Is(value1 + value2 + value3 + valueNew);
            {
                var resultViewAction = result.viewActionList[0];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tipNew);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[1];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[2];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[3];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip1);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[4];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip2);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[5];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip3);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            {
                var resultViewAction = result.viewActionList[6];
                resultViewAction.IsNotNull();
                resultViewAction.actionType.Is(ViewAction.Pattern.GENERATE);
                resultViewAction.actor.Is(tip3);
                resultViewAction.target.Is(default);
                resultViewAction.easing.Is(default);
            }
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(valueNew);
            result.boardTips.IsNotNull();
            result.boardTips.Count().Is(value1 + value2 + value3);
            result.boardTips.All(tip => tip is MotionTip).IsTrue();
            result.boardTips.ToArray()[0].Is(tip1);
            result.boardTips.ToArray()[1].Is(tip1);
            result.boardTips.ToArray()[2].Is(tip1);
            result.boardTips.ToArray()[3].Is(tip2);
            result.boardTips.ToArray()[4].Is(tip3);
            result.boardTips.ToArray()[5].Is(tip3);
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定_山札が空()
        {
            var tipNumbers = 8;
            var state = BattleStateMock.Generate();
            var result = state.SetupBoard(tipNumbers);
            result.IsNotNull();
            result.IsSameReferenceAs(state);
            result.viewActionList.IsNotNull();
            result.viewActionList.Count.Is(0);
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(0);
            result.boardTips.IsNotNull();
            result.boardTips.Count().Is(0);
        }
        [Test]
        public static void SetupBoardTest_通常処理_補充枚数指定_戦闘状態がNull()
        {
            var tipNumbers = 8;

            var result = BattleStateManager.SetupBoard(null, tipNumbers);

            result.IsNull();
        }

        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数デフォルト値_元の手札は空()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 13;
            var value2 = 6;
            var value3 = 5;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();

            var result = state.SetupAllHandTips();

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Count().Is(actorList.Count);
            {
                var actorState = state.battleActors.ToList()[0];
                actorState.IsNotNull();
                actorState.state.handTips.IsNotNull();
                actorState.state.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
                actorState.state.handTips.All(tip => tipList.Contains(tip)).IsTrue();
            }
            {
                var actorState = state.battleActors.ToList()[0];
                actorState.IsNotNull();
                actorState.state.handTips.IsNotNull();
                actorState.state.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
                actorState.state.handTips.All(tip => tipList.Contains(tip)).IsTrue();
            }
            {
                var actorState = state.battleActors.ToList()[0];
                actorState.IsNotNull();
                actorState.state.handTips.IsNotNull();
                actorState.state.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
                actorState.state.handTips.All(tip => tipList.Contains(tip)).IsTrue();
            }
        }
        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数指定_元の手札は空()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 5;
            var value2 = 4;
            var value3 = 6;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 4;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = state.SetupAllHandTips(tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - tipNumbers * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数デフォルト値_元の手札有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 13;
            var value2 = 6;
            var value3 = 5;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupAllHandTips();

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数指定_元の手札有り()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 5;
            var value2 = 4;
            var value3 = 6;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 4;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupAllHandTips(tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is(value1 + value2 + value3 - tipNumbers * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数デフォルト値_元の手札は空_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = state.SetupAllHandTips();

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is((value1 + value2 + value3) * 6 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数指定_元の手札は空_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 12;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = state.SetupAllHandTips(tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is((value1 + value2 + value3) * 10 - tipNumbers * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数デフォルト値_元の手札有り_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupAllHandTips();

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is((value1 + value2 + value3) * 6 - Constants.Battle.DEFAULT_HAND_TIP_NUMBERS * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(Constants.Battle.DEFAULT_HAND_TIP_NUMBERS);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupAllHandTipsTest_通常処理_補充枚数指定_元の手札有り_山札が補充枚数以下()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 1;
            var value2 = 1;
            var value3 = 2;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            var tipNumbers = 12;

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state.AddHandTips(tipList);
            var result = state.SetupAllHandTips(tipNumbers);

            result.IsNotNull();
            result.deckTips.IsNotNull();
            result.deckTips.Count.Is((value1 + value2 + value3) * 10 - tipNumbers * state.battleActors.Count());
            result.battleActors.IsNotNull();
            result.battleActors.Contains(battleActor).IsTrue();

            actorState.IsNotNull();
            actorState.handTips.IsNotNull();
            actorState.handTips.Count().Is(tipNumbers);
            actorState.handTips.All(tip => tipList.Contains(tip)).IsTrue();
        }
        [Test]
        public static void SetupAllHandTipsTest_戦闘状態がNull()
        {
            var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
            var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
            var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
            var value1 = 3;
            var value2 = 2;
            var value3 = 4;
            var name1 = "name1";
            var name2 = "name2";
            var name3 = "name3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);

            var actorList = new List<BattleActorMock> { actor1, actor2, actor3 };
            var tipList = new List<MotionTip> { tip1, tip2, tip3 };
            var tipMap = new Dictionary<MotionTip, int> { { tip1, value1 }, { tip2, value2 }, { tip3, value3 } };
            var state = BattleStateMock.Generate(actorList, tipMap).SetupDeck();
            var battleActor = state.battleActors.First();

            var actorState = battleActor.state;
            var result = BattleStateManager.SetupAllHandTips(null);

            result.IsNull();
        }

        [Test]
        public static void UpdateEnergyTest_正常動作_行動力0()
        {
            var name1 = $"{nameof(UpdateEnergyTest_正常動作_行動力0)}_1";
            var name2 = $"{nameof(UpdateEnergyTest_正常動作_行動力0)}_2";
            var name3 = $"{nameof(UpdateEnergyTest_正常動作_行動力0)}_3";
            var actor1 = BattleActorMock.Generate(new Actor(name1)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 3)
            });
            var actor2 = BattleActorMock.Generate(new Actor(name2)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 12)
            });
            var actor3 = BattleActorMock.Generate(new Actor(name3)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 1)
            });

            var actorList = new[] { actor1, actor2, actor3 };
            var state = BattleStateMock.Generate(actorList);

            var result = state.UpdateEnergy();
            result.IsNotNull();

            var resultActors = result.battleActors;
            resultActors.IsNotNull();
            resultActors.Count().Is(actorList.Length);
            resultActors.Single(actor => actor.name == name1).energy.Is(Constants.Battle.ENERGY_NORM + 3);
            resultActors.Single(actor => actor.name == name2).energy.Is(Constants.Battle.ENERGY_NORM + 12);
            resultActors.Single(actor => actor.name == name3).energy.Is(Constants.Battle.ENERGY_NORM + 1);
        }
        [Test]
        public static void UpdateEnergyTest_正常動作_行動力有_正()
        {
            var name1 = $"{nameof(UpdateEnergyTest_正常動作_行動力有_正)}_1";
            var name2 = $"{nameof(UpdateEnergyTest_正常動作_行動力有_正)}_2";
            var name3 = $"{nameof(UpdateEnergyTest_正常動作_行動力有_正)}_3";
            var actor1 = BattleActorMock.Generate(new Actor(name1)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 3)
            });
            var actor2 = BattleActorMock.Generate(new Actor(name2)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 12)
            });
            var actor3 = BattleActorMock.Generate(new Actor(name3)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 1)
            });
            actor1.energy = 24;
            actor2.energy = 108;
            actor3.energy = 6;

            var actorList = new[] { actor1, actor2, actor3 };
            var state = BattleStateMock.Generate(actorList);

            var result = state.UpdateEnergy();
            result.IsNotNull();

            var resultActors = result.battleActors;
            resultActors.IsNotNull();
            resultActors.Count().Is(actorList.Length);
            resultActors.Single(actor => actor.name == name1).energy.Is(Constants.Battle.ENERGY_NORM + 24 - 6 + 3);
            resultActors.Single(actor => actor.name == name2).energy.Is(Constants.Battle.ENERGY_NORM + 108 - 6 + 12);
            resultActors.Single(actor => actor.name == name3).energy.Is(Constants.Battle.ENERGY_NORM + 6 - 6 + 1);
        }
        [Test]
        public static void UpdateEnergyTest_正常動作_行動力有_負()
        {
            var name1 = $"{nameof(UpdateEnergyTest_正常動作_行動力有_負)}_1";
            var name2 = $"{nameof(UpdateEnergyTest_正常動作_行動力有_負)}_2";
            var name3 = $"{nameof(UpdateEnergyTest_正常動作_行動力有_負)}_3";
            var actor1 = BattleActorMock.Generate(new Actor(name1)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 3)
            });
            var actor2 = BattleActorMock.Generate(new Actor(name2)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 12)
            });
            var actor3 = BattleActorMock.Generate(new Actor(name3)
            {
                parameter = new Actor.Parameter(0, 0, 0, speed: 1)
            });
            actor1.energy = -24;
            actor2.energy = 108;
            actor3.energy = 6;

            var actorList = new[] { actor1, actor2, actor3 };
            var state = BattleStateMock.Generate(actorList);

            var result = state.UpdateEnergy();
            result.IsNotNull();

            var resultActors = result.battleActors;
            resultActors.IsNotNull();
            resultActors.Count().Is(actorList.Length);
            resultActors.Single(actor => actor.name == name1).energy.Is(Constants.Battle.ENERGY_NORM + -24 + 24 + 3);
            resultActors.Single(actor => actor.name == name2).energy.Is(Constants.Battle.ENERGY_NORM + 108 + 24 + 12);
            resultActors.Single(actor => actor.name == name3).energy.Is(Constants.Battle.ENERGY_NORM + 6 + 24 + 1);
        }
        [Test]
        public static void UpdateEnergyTest_行動者無し()
        {
            var state = BattleStateMock.Generate(new BattleActor[] { });

            var result = state.UpdateEnergy();

            result.IsNotNull();
            result.battleActors.IsNotNull();
            result.battleActors.Count().Is(0);
        }
        [Test]
        public static void UpdateEnergyTest_行動者Null()
        {
            var state = BattleStateMock.Generate((List<BattleActor>)null);

            var result = state.UpdateEnergy();

            result.IsNotNull();
            result.battleActors.IsNull();
        }
        [Test]
        public static void UpdateEnergyTest_状態Null()
        {
            var result = BattleStateManager.UpdateEnergy(null);

            result.IsNull();
        }

        [Test]
        public static void SetNextActorTest_正常動作_行動者未設定()
        {
            var name1 = $"{nameof(SetNextActorTest_正常動作_行動者未設定)}_1";
            var name2 = $"{nameof(SetNextActorTest_正常動作_行動者未設定)}_2";
            var name3 = $"{nameof(SetNextActorTest_正常動作_行動者未設定)}_3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            actor1.energy = -24;
            actor2.energy = 108;
            actor3.energy = 6;

            var actorList = new[] { actor1, actor2, actor3 };
            var state = BattleStateMock.Generate(actorList);

            var result = state.SetNextActor();

            result.IsNotNull();
            result.thisTimeActor.IsSameReferenceAs(actor2);
        }
        [Test]
        public static void SetNextActorTest_正常動作_行動者設定済()
        {
            var name1 = $"{nameof(SetNextActorTest_正常動作_行動者設定済)}_1";
            var name2 = $"{nameof(SetNextActorTest_正常動作_行動者設定済)}_2";
            var name3 = $"{nameof(SetNextActorTest_正常動作_行動者設定済)}_3";
            var actor1 = BattleActorMock.Generate(name1);
            var actor2 = BattleActorMock.Generate(name2);
            var actor3 = BattleActorMock.Generate(name3);
            actor1.energy = -24;
            actor2.energy = 108;
            actor3.energy = 6;

            var actorList = new[] { actor1, actor2, actor3 };
            var state = BattleStateMock.Generate(actorList).SetThisTimeActor(actor3);

            var result = state.SetNextActor();

            result.IsNotNull();
            result.thisTimeActor.IsSameReferenceAs(actor2);
        }
        [Test]
        public static void SetNextActorTest_行動者無し()
        {
            var state = BattleStateMock.Generate(new BattleActor[] { });

            var result = state.SetNextActor();

            result.IsNotNull();
            result.thisTimeActor.IsNull();
        }
        [Test]
        public static void SetNextActorTest_行動者Null()
        {
            var state = BattleStateMock.Generate((List<BattleActor>)null);

            var result = state.SetNextActor();

            result.IsNotNull();
            result.thisTimeActor.IsNull();
        }
        [Test]
        public static void SetNextActorTest_状態Null()
        {
            var result = BattleStateManager.SetNextActor(null);

            result.IsNull();
        }
    }
}
