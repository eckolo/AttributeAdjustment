using Assets.Src.Domain.Model.Value;
using Assets.Src.Mock;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Editor.UnitTest.Domain.Model.Value
{
    public static partial class BattleStateTest
    {
        /// <summary>
        /// <see cref="BattleState.EveryActor"/>のテストクラス
        /// </summary>
        public static class EveryActorTest
        {
            [Test]
            public static void AddHandTipsTest_通常処理_元の手札無し()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var addList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate();

                var result = actorState.AddHandTips(addList).handTips;

                result.IsNotNull();
                result.Count().Is(addList.Count);
                result
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(3);
                result
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(2);
                result
                    .Where(elem => elem.energy == tip3.energy)
                    .Count(elem => elem.energyValue == tip3.energyValue)
                    .Is(1);
            }
            [Test]
            public static void AddHandTipsTest_通常処理_元の手札有り()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2 };
                var addList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.AddHandTips(addList).handTips;

                result.IsNotNull();
                result.Count().Is(tipList.Count + addList.Count);
                result
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(6);
                result
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(3);
                result
                    .Where(elem => elem.energy == tip3.energy)
                    .Count(elem => elem.energyValue == tip3.energyValue)
                    .Is(1);
            }
            [Test]
            public static void AddHandTipsTest_追加リストが空_元の手札無し()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var addList = new List<MotionTip>();
                var actorState = BattleStateMock.EveryActorMock.Generate();

                var result = actorState.AddHandTips(addList).handTips;

                result.IsNotNull();
                result.Any().IsFalse();
            }
            [Test]
            public static void AddHandTipsTest_追加リストが空_元の手札有り()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2 };
                var addList = new List<MotionTip>();
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.AddHandTips(addList).handTips;

                result.IsNotNull();
                result.Count().Is(tipList.Count);
                result
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(3);
                result
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(1);
            }
            [Test]
            public static void AddHandTipsTest_追加リストがNull_元の手札無し()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var actorState = BattleStateMock.EveryActorMock.Generate();

                var result = actorState.AddHandTips(null).handTips;

                result.IsNotNull();
                result.Any().IsFalse();
            }
            [Test]
            public static void AddHandTipsTest_追加リストがNull_元の手札有り()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.AddHandTips(null).handTips;

                result.IsNotNull();
                result.Count().Is(tipList.Count);
                result
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(3);
                result
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(1);
            }

            [Test]
            public static void PopHandTipsTest_通常処理_全て取り出し成功()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip2, tip3 };
                var popList = new List<MotionTip> { tip1, tip1, tip1, tip2 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Count().Is(popList.Count);
                result
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(3);
                result
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(1);

                actorState.handTips.IsNotNull();
                actorState.handTips.Count().Is(tipList.Count - popList.Count);
                actorState.handTips
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(2);
                actorState.handTips
                    .Where(elem => elem.energy == tip3.energy)
                    .Count(elem => elem.energyValue == tip3.energyValue)
                    .Is(1);
            }
            [Test]
            public static void PopHandTipsTest_通常処理_一部取り出し成功()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip2, tip2, tip2 };
                var popList = new List<MotionTip> { tip1, tip1, tip2, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Count().Is(2);
                result
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(1);
                result
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(1);

                actorState.handTips.IsNotNull();
                actorState.handTips.Count().Is(2);
                actorState.handTips
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(2);
            }
            [Test]
            public static void PopHandTipsTest_通常処理_全て取り出し失敗()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tip4 = MotionTipMock.Generate(Energy.FLAME, 60);
                var tipList = new List<MotionTip> { tip2, tip2, tip4 };
                var popList = new List<MotionTip> { tip1, tip1, tip1, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNotNull();
                actorState.handTips.Count().Is(tipList.Count);
                actorState.handTips
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(2);
                actorState.handTips
                    .Where(elem => elem.energy == tip4.energy)
                    .Count(elem => elem.energyValue == tip4.energyValue)
                    .Is(1);
            }
            [Test]
            public static void PopHandTipsTest_手札が空()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tip4 = MotionTipMock.Generate(Energy.FLAME, 60);
                var tipList = new List<MotionTip>();
                var popList = new List<MotionTip> { tip1, tip1, tip2, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNotNull();
                actorState.handTips.Any().IsFalse();
            }
            [Test]
            public static void PopHandTipsTest_手札がNull()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tip4 = MotionTipMock.Generate(Energy.FLAME, 60);
                var popList = new List<MotionTip> { tip1, tip1, tip2, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate(null);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNull();
            }
            [Test]
            public static void PopHandTipsTest_取り出しリストが空()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
                var popList = new List<MotionTip>();
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNotNull();
                actorState.handTips.Count().Is(tipList.Count);
                actorState.handTips
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(3);
                actorState.handTips
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(2);
                actorState.handTips
                    .Where(elem => elem.energy == tip3.energy)
                    .Count(elem => elem.energyValue == tip3.energyValue)
                    .Is(1);
            }
            [Test]
            public static void PopHandTipsTest_取り出しリストがNull()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(null);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNotNull();
                actorState.handTips.Count().Is(tipList.Count);
                actorState.handTips
                    .Where(elem => elem.energy == tip1.energy)
                    .Count(elem => elem.energyValue == tip1.energyValue)
                    .Is(3);
                actorState.handTips
                    .Where(elem => elem.energy == tip2.energy)
                    .Count(elem => elem.energyValue == tip2.energyValue)
                    .Is(2);
                actorState.handTips
                    .Where(elem => elem.energy == tip3.energy)
                    .Count(elem => elem.energyValue == tip3.energyValue)
                    .Is(1);
            }
            [Test]
            public static void PopHandTipsTest_手札が空_取り出しリストが空()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip>();
                var popList = new List<MotionTip>();
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNotNull();
                actorState.handTips.Any().IsFalse();
            }
            [Test]
            public static void PopHandTipsTest_手札が空_取り出しリストがNull()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip>();
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.PopHandTips(null);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNotNull();
                actorState.handTips.Any().IsFalse();
            }
            [Test]
            public static void PopHandTipsTest_手札がNull_取り出しリストが空()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var popList = new List<MotionTip>();
                var actorState = BattleStateMock.EveryActorMock.Generate(null);

                var result = actorState.PopHandTips(popList);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNull();
            }
            [Test]
            public static void PopHandTipsTest_手札がNull_取り出しリストがNull()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var actorState = BattleStateMock.EveryActorMock.Generate(null);

                var result = actorState.PopHandTips(null);

                result.IsNotNull();
                result.Any().IsFalse();

                actorState.handTips.IsNull();
            }

            [Test]
            public static void ClearHandTipsTest_通常処理()
            {
                var tip1 = MotionTipMock.Generate(Energy.DARKNESS, 10);
                var tip2 = MotionTipMock.Generate(Energy.LIFE, 40);
                var tip3 = MotionTipMock.Generate(Energy.WIND, 20);
                var tipList = new List<MotionTip> { tip1, tip1, tip1, tip2, tip2, tip2, tip3 };
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.ClearHandTips().handTips;

                result.IsNotNull();
                result.Any().IsFalse();
            }
            [Test]
            public static void ClearHandTipsTest_手札が元から空()
            {
                var tipList = new List<MotionTip>();
                var actorState = BattleStateMock.EveryActorMock.Generate(tipList);

                var result = actorState.ClearHandTips().handTips;

                result.IsNotNull();
                result.Any().IsFalse();
            }
            [Test]
            public static void ClearHandTipsTest_手札がNull()
            {
                var actorState = BattleStateMock.EveryActorMock.Generate(null);

                var result = actorState.ClearHandTips().handTips;

                result.IsNull();
            }
        }
    }
}
