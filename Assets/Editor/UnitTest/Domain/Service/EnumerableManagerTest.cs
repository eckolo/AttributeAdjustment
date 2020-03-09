using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Service;
using Assets.Src.Mock.Model.Abstract;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="EnumerableManager">クラスのテスト
    /// </summary>
    public static class EnumerableManagerTest
    {
        /// <summary>
        /// <see cref="ToDictionaryTest"/>用クラス
        /// </summary>
        class KeyValueLikeTest : IKeyValueLike<string, float>
        {
            public KeyValueLikeTest(string key, float value)
            {
                this.key = key;
                this.value = value;
            }

            public string key { get; } = "";
            public float value { get; } = 0f;
        }

        [Test]
        public static void MaxKeysTest()
        {
            var list = new List<Vector2> {
                new Vector2(2, 2),
                new Vector2(6, -8),
                new Vector2(-6, 8),
                new Vector2(0, -1)
            };

            var result = list.MaxKeys(elem => elem.magnitude);
            result.Count().Is(2);

            var resultFirst = result.Single(elem => elem.x > 0);
            var resultSecond = result.Single(elem => elem.x < 0);
            resultFirst.x.Is(6);
            resultFirst.y.Is(-8);
            resultSecond.x.Is(-6);
            resultSecond.y.Is(8);
        }
        [Test]
        public static void MinKeysTest()
        {
            var list = new List<Vector2> {
                new Vector2(6, 6),
                new Vector2(3, -4),
                new Vector2(-3, 4),
                new Vector2(0, -7)
            };

            var result = list.MinKeys(elem => elem.magnitude);
            result.Count().Is(2);

            var resultFirst = result.Single(elem => elem.x > 0);
            var resultSecond = result.Single(elem => elem.x < 0);
            resultFirst.x.Is(3);
            resultFirst.y.Is(-4);
            resultSecond.x.Is(-3);
            resultSecond.y.Is(4);
        }
        [Test]
        public static void ToDictionaryTest()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var list1 = new List<KeyValueLikeTest> {
                new KeyValueLikeTest(key1, value1),
                new KeyValueLikeTest(key2, value2),
                new KeyValueLikeTest(key3, value3)
            };
            var list2 = new List<KeyValueLikeTest> {
                new KeyValueLikeTest(key1, value1),
                new KeyValueLikeTest(key1, value2),
            };

            var result1 = list1.ToDictionary();
            result1[key1].Is(value1);
            result1[key2].Is(value2);
            result1[key3].Is(value3);

            Assert.Throws<ArgumentException>(() => list2.ToDictionary());
        }
        [Test]
        public static void GetOrDefaultTest_リスト型_通常動作_デフォルト値未指定()
        {
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var list = new[] { value1, value2, value3 };

            list.GetOrDefault(-1).Is(default);
            list.GetOrDefault(0).Is(value1);
            list.GetOrDefault(1).Is(value2);
            list.GetOrDefault(2).Is(value3);
            list.GetOrDefault(3).Is(default);
        }
        [Test]
        public static void GetOrDefaultTest_リスト型_通常動作_デフォルト値指定()
        {
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var valueDefault = 7.4f;
            var list = new[] { value1, value2, value3 };

            list.GetOrDefault(-1, valueDefault).Is(valueDefault);
            list.GetOrDefault(0, valueDefault).Is(value1);
            list.GetOrDefault(1, valueDefault).Is(value2);
            list.GetOrDefault(2, valueDefault).Is(value3);
            list.GetOrDefault(3, valueDefault).Is(valueDefault);
        }
        [Test]
        public static void GetOrDefaultTest_リスト型_元の辞書型が空_デフォルト値未指定()
        {
            var list = new List<float>();

            list.GetOrDefault(-1).Is(default);
            list.GetOrDefault(0).Is(default);
            list.GetOrDefault(1).Is(default);
            list.GetOrDefault(2).Is(default);
            list.GetOrDefault(3).Is(default);
        }
        [Test]
        public static void GetOrDefaultTest_リスト型_元の辞書型が空_デフォルト値指定()
        {
            var valueDefault = 7.4f;
            var list = new List<float>();

            list.GetOrDefault(-1, valueDefault).Is(valueDefault);
            list.GetOrDefault(0, valueDefault).Is(valueDefault);
            list.GetOrDefault(1, valueDefault).Is(valueDefault);
            list.GetOrDefault(2, valueDefault).Is(valueDefault);
            list.GetOrDefault(3, valueDefault).Is(valueDefault);
        }
        [Test]
        public static void GetOrDefaultTest_リスト型_元の辞書型がNull_デフォルト値未指定()
        {
            var list = (List<float>)null;

            list.GetOrDefault(-1).Is(default);
            list.GetOrDefault(0).Is(default);
            list.GetOrDefault(1).Is(default);
            list.GetOrDefault(2).Is(default);
            list.GetOrDefault(3).Is(default);
        }
        [Test]
        public static void GetOrDefaultTest_リスト型_元の辞書型がNull_デフォルト値指定()
        {
            var valueDefault = 7.4f;
            var list = (List<float>)null;

            list.GetOrDefault(-1, valueDefault).Is(valueDefault);
            list.GetOrDefault(0, valueDefault).Is(valueDefault);
            list.GetOrDefault(1, valueDefault).Is(valueDefault);
            list.GetOrDefault(2, valueDefault).Is(valueDefault);
            list.GetOrDefault(3, valueDefault).Is(valueDefault);
        }
        [Test]
        public static void GetOrDefaultTest_辞書型_通常動作_デフォルト値未指定()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var dictionary = new Dictionary<string, float>
            {
                { key1, value1},
                { key2, value2},
                { key3, value3},
            };

            dictionary.GetOrDefault(key1).Is(value1);
            dictionary.GetOrDefault(key2).Is(value2);
            dictionary.GetOrDefault(key3).Is(value3);
            dictionary.GetOrDefault(key4).Is(default);
        }
        [Test]
        public static void GetOrDefaultTest_辞書型_通常動作_デフォルト値指定()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var valueDefault = 7.4f;
            var dictionary = new Dictionary<string, float>
            {
                { key1, value1},
                { key2, value2},
                { key3, value3},
            };

            dictionary.GetOrDefault(key1, valueDefault).Is(value1);
            dictionary.GetOrDefault(key2, valueDefault).Is(value2);
            dictionary.GetOrDefault(key3, valueDefault).Is(value3);
            dictionary.GetOrDefault(key4, valueDefault).Is(valueDefault);
        }
        [Test]
        public static void GetOrDefaultTest_辞書型_元の辞書型が空_デフォルト値未指定()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var dictionary = new Dictionary<string, float>();

            dictionary.GetOrDefault(key1).Is(default);
            dictionary.GetOrDefault(key2).Is(default);
            dictionary.GetOrDefault(key3).Is(default);
            dictionary.GetOrDefault(key4).Is(default);
        }
        [Test]
        public static void GetOrDefaultTest_辞書型_元の辞書型が空_デフォルト値指定()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var valueDefault = 7.4f;
            var dictionary = new Dictionary<string, float>();

            dictionary.GetOrDefault(key1, valueDefault).Is(valueDefault);
            dictionary.GetOrDefault(key2, valueDefault).Is(valueDefault);
            dictionary.GetOrDefault(key3, valueDefault).Is(valueDefault);
            dictionary.GetOrDefault(key4, valueDefault).Is(valueDefault);
        }
        [Test]
        public static void GetOrDefaultTest_辞書型_元の辞書型がNull_デフォルト値未指定()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var dictionary = (Dictionary<string, float>)null;

            dictionary.GetOrDefault(key1).Is(default);
            dictionary.GetOrDefault(key2).Is(default);
            dictionary.GetOrDefault(key3).Is(default);
            dictionary.GetOrDefault(key4).Is(default);
        }
        [Test]
        public static void GetOrDefaultTest_辞書型_元の辞書型がNull_デフォルト値指定()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var valueDefault = 7.4f;
            var dictionary = (Dictionary<string, float>)null;

            dictionary.GetOrDefault(key1, valueDefault).Is(valueDefault);
            dictionary.GetOrDefault(key2, valueDefault).Is(valueDefault);
            dictionary.GetOrDefault(key3, valueDefault).Is(valueDefault);
            dictionary.GetOrDefault(key4, valueDefault).Is(valueDefault);
        }
        [Test]
        public static void UpdateOrInsertTest_1要素_正常系()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var value4 = 7.4f;
            var dictionary = new Dictionary<string, float>
            {
                { key1, value1},
                { key2, value2},
                { key3, value3},
            };

            {
                var result = dictionary.UpdateOrInsert(key2, value4);
                result.Count.Is(3);
                result.ContainsKey(key1).IsTrue();
                result[key1].Is(value1);
                result.ContainsKey(key2).IsTrue();
                result[key2].Is(value4);
                result.ContainsKey(key3).IsTrue();
                result[key3].Is(value3);
            }
            {
                var result = dictionary.UpdateOrInsert(key4, value4);
                result.Count.Is(4);
                result.ContainsKey(key1).IsTrue();
                result[key1].Is(value1);
                result.ContainsKey(key2).IsTrue();
                result[key2].Is(value2);
                result.ContainsKey(key3).IsTrue();
                result[key3].Is(value3);
                result.ContainsKey(key4).IsTrue();
                result[key4].Is(value4);
            }
        }
        [Test]
        public static void UpdateOrInsertTest_1要素_Nullの値()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            float? value4 = null;
            var dictionary = new Dictionary<string, float?>
            {
                { key1, value1},
                { key2, value2},
                { key3, value3},
            };

            {
                var result = dictionary.UpdateOrInsert(key2, value4);
                result.Count.Is(3);
                result.ContainsKey(key1).IsTrue();
                result[key1].Is(value1);
                result.ContainsKey(key2).IsTrue();
                result[key2].Is(value4);
                result.ContainsKey(key3).IsTrue();
                result[key3].Is(value3);
            }
            {
                var result = dictionary.UpdateOrInsert(key4, value4);
                result.Count.Is(4);
                result.ContainsKey(key1).IsTrue();
                result[key1].Is(value1);
                result.ContainsKey(key2).IsTrue();
                result[key2].Is(value2);
                result.ContainsKey(key3).IsTrue();
                result[key3].Is(value3);
                result.ContainsKey(key4).IsTrue();
                result[key4].Is(value4);
            }
        }
        [Test]
        public static void UpdateOrInsertTest_1要素_Nullのキー()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            string key4 = null;
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var value4 = 7.4f;
            var dictionary = new Dictionary<string, float?>
            {
                { key1, value1},
                { key2, value2},
                { key3, value3},
            };

            Assert.Throws<ArgumentNullException>(() => dictionary.UpdateOrInsert(key4, value4));
        }
        [Test]
        public static void UpdateOrInsertTest_複数要素_正常系()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var value4 = 7.4f;
            var value5 = -72.65f;
            var dictionary = new Dictionary<string, float>
            {
                { key1, value1},
                { key2, value2},
                { key3, value3},
            };
            var update = new Dictionary<string, float>
            {
                { key4, value4},
                { key1, value5}
            };

            var result = dictionary.UpdateOrInsert(update);
            result.Count.Is(4);
            result.ContainsKey(key1).IsTrue();
            result[key1].Is(value5);
            result.ContainsKey(key2).IsTrue();
            result[key2].Is(value2);
            result.ContainsKey(key3).IsTrue();
            result[key3].Is(value3);
            result.ContainsKey(key4).IsTrue();
            result[key4].Is(value4);
        }
        [Test]
        public static void UpdateOrInsertTest_複数要素_Nullの値()
        {
            var key1 = "test1";
            var key2 = "test2";
            var key3 = "test3";
            var key4 = "test4";
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var dictionary = new Dictionary<string, float?>
            {
                { key1, value1},
                { key2, value2},
                { key3, value3},
            };
            var update = new Dictionary<string, float?>
            {
                { key4, null},
                { key1, null}
            };

            var result = dictionary.UpdateOrInsert(update);
            result.Count.Is(4);
            result.ContainsKey(key1).IsTrue();
            result[key1].Is(null);
            result.ContainsKey(key2).IsTrue();
            result[key2].Is(value2);
            result.ContainsKey(key3).IsTrue();
            result[key3].Is(value3);
            result.ContainsKey(key4).IsTrue();
            result[key4].Is(null);
        }
        [Test]
        public static void ContainsIndexTest()
        {
            var list = new List<Vector2> {
                new Vector2(2, 2),
                new Vector2(6, -8),
                new Vector2(-6, 8),
                new Vector2(0, -1)
            };

            list.ContainsIndex(-1).IsFalse();
            list.ContainsIndex(0).IsTrue();
            list.ContainsIndex(3).IsTrue();
            list.ContainsIndex(4).IsFalse();
        }

        [Test]
        public static void EmbodyTest_正常系()
        {
            var index1 = 0;
            var index2 = 1;
            var index3 = 2;
            var index4 = 3;
            var num1 = 1;
            var num2 = 0;
            var num3 = 3;
            var numNegative = -2;
            var class1 = new ClassMock() { number = index1 };
            var class2 = new ClassMock() { number = index2 };
            var class3 = new ClassMock() { number = index3 };
            var class4 = new ClassMock() { number = index4 };
            var map = new Dictionary<ClassMock, int> {
                { class1, num1 },
                { class2, num2 },
                { class3, num3 },
                { class4, numNegative },
            };

            var result = map.Embody()?.ToList();
            result.IsNotNull();
            result.Count.Is(num1 + num2 + num3);
            result.Count(elem => elem.number == index1).Is(num1);
            result.Count(elem => elem.number == index2).Is(num2);
            result.Count(elem => elem.number == index3).Is(num3);
            result.Count(elem => elem.number == index4).Is(0);
            result.Count(elem => elem.Equals(class1)).Is(0);
            result.Count(elem => elem.Equals(class2)).Is(0);
            result.Count(elem => elem.Equals(class3)).Is(0);
            result.Count(elem => elem.Equals(class4)).Is(0);
        }
        [Test]
        public static void EmbodyTest_正常系_元値が空()
        {
            var map = new Dictionary<ClassMock, int>();

            var result = map.Embody()?.ToList();
            result.IsNotNull();
            result.Count.Is(0);
        }
        [Test]
        public static void EmbodyTest_異常系_元値がNull()
        {
            var mapNull = (Dictionary<ClassMock, int>)null;

            Assert.Throws<ArgumentNullException>(() => mapNull.Embody()?.ToList());
        }

        [Test]
        public static void PickTest_正常系()
        {
            var vector1 = new Vector2(1, 2);
            var vector2 = new Vector2(2, -8);
            var vector3 = new Vector2(3, 8);
            var vector4 = new Vector2(4, -1);
            var list = new List<Vector2> { vector1, vector2, vector3, vector4 };
            var rate = new List<int> { 10, 20, 30, 40 };
            var norm1 = 0.DividedBy(100);
            var norm2 = 9.DividedBy(100);
            var norm3 = 10.DividedBy(100);
            var norm4 = 29.DividedBy(100);
            var norm5 = 30.DividedBy(100);
            var norm6 = 59.DividedBy(100);
            var norm7 = 60.DividedBy(100);
            var norm8 = 99.DividedBy(100);

            list.Pick(norm1, rate).Is(vector1);
            list.Pick(norm2, rate).Is(vector1);
            list.Pick(norm3, rate).Is(vector2);
            list.Pick(norm4, rate).Is(vector2);
            list.Pick(norm5, rate).Is(vector3);
            list.Pick(norm6, rate).Is(vector3);
            list.Pick(norm7, rate).Is(vector4);
            list.Pick(norm8, rate).Is(vector4);
        }
        [Test]
        public static void PickTest_基準値が閾値外()
        {
            var vector1 = new Vector2(1, 2);
            var vector2 = new Vector2(2, -8);
            var vector3 = new Vector2(3, 8);
            var vector4 = new Vector2(4, -1);
            var list = new List<Vector2> { vector1, vector2, vector3, vector4 };
            var rate = new List<int> { 10, 20, 30, 40 };
            var norm1 = -1.DividedBy(100);
            var norm2 = 100.DividedBy(100);

            list.Pick(norm1, rate).Is(default);
            list.Pick(norm2, rate).Is(default);
        }
        [Test]
        public static void PickTest_確率分布が負の値()
        {
            var vector1 = new Vector2(1, 2);
            var vector2 = new Vector2(2, -8);
            var vector3 = new Vector2(3, 8);
            var vector4 = new Vector2(4, -1);
            var list = new List<Vector2> { vector1, vector2, vector3, vector4 };
            var rate = new List<int> { 10, -20, 30, 40 };
            var norm1 = 0.DividedBy(80);
            var norm2 = 9.DividedBy(80);
            var norm3 = 10.DividedBy(80);
            var norm4 = 39.DividedBy(80);
            var norm5 = 40.DividedBy(80);
            var norm6 = 79.DividedBy(80);

            list.Pick(norm1, rate).Is(vector1);
            list.Pick(norm2, rate).Is(vector1);
            list.Pick(norm3, rate).Is(vector3);
            list.Pick(norm4, rate).Is(vector3);
            list.Pick(norm5, rate).Is(vector4);
            list.Pick(norm6, rate).Is(vector4);
        }
        [Test]
        public static void PickTest_基準値が閾値外かつ確率分布が負の値()
        {
            var vector1 = new Vector2(1, 2);
            var vector2 = new Vector2(2, -8);
            var vector3 = new Vector2(3, 8);
            var vector4 = new Vector2(4, -1);
            var list = new List<Vector2> { vector1, vector2, vector3, vector4 };
            var rate = new List<int> { 10, -20, 30, 40 };
            var norm1 = -1.DividedBy(80);
            var norm2 = 80.DividedBy(80);

            list.Pick(norm1, rate).Is(default);
            list.Pick(norm2, rate).Is(default);
        }

        [Test]
        public static void ShuffleTest_通常動作()
        {
            var list = Enumerable.Range(0, 100).ToList();

            var result = list.Shuffle().ToList();

            result.IsNotSameReferenceAs(list);
            result.All(elem => list.Any(_elem => _elem == elem)).IsTrue();
            list.All(elem => result.Any(_elem => _elem == elem)).IsTrue();
        }

        [Test]
        public static void AnyNotNullTest_要素複数()
        {
            var value1 = 0.3f;
            var value2 = 1.2f;
            var value3 = 5;
            var list = new[] { value1, value2, value3 };

            list.AnyNotNull().IsTrue();
        }
        [Test]
        public static void AnyNotNullTest_要素単数()
        {
            var value1 = 0.3f;
            var list = new[] { value1 };

            list.AnyNotNull().IsTrue();
        }
        [Test]
        public static void AnyNotNullTest_要素無し()
        {
            var list = new float[] { };

            list.AnyNotNull().IsFalse();
        }
        [Test]
        public static void AnyNotNullTest_Null()
        {
            var list = (IEnumerable<float>)null;

            list.AnyNotNull().IsFalse();
        }
    }
}
