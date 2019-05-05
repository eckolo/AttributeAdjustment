using Assets.Src.Domain.Service;
using Assets.Src.Mock;
using NUnit.Framework;

namespace Assets.Editor.UnitTest.Domain.Service
{
    /// <summary>
    /// <see cref="DuplicateManager"/>のテスト
    /// </summary>
    public static class DuplicateManagerTest
    {
        static ClassMock origin = new ClassMock
        {
            text1 = "text1",
            text2 = "text2",
            number = 3,
            innerClass = new ClassMock.InnerClassMock
            {
                text1 = "text1-1",
                text2 = "text1-2",
                number = 12
            }
        };

        [Test]
        public static void DuplicateTest()
        {
            var duplicated = origin.Duplicate();

            duplicated.IsNotNull();
            duplicated.IsNotSameReferenceAs(origin);
            duplicated.text1.Is(origin.text1);
            duplicated.text2.Is(origin.text2);
            duplicated.number.Is(origin.number);

            duplicated.innerClass.IsNotNull();
            duplicated.innerClass.IsSameReferenceAs(origin.innerClass);
            duplicated.innerClass.text1.Is(origin.innerClass.text1);
            duplicated.innerClass.text2.Is(origin.innerClass.text2);
            duplicated.innerClass.number.Is(origin.innerClass.number);
        }

        [Test]
        public static void DuplicateFullTest()
        {
            var duplicated = origin.DuplicateFull();

            duplicated.IsNotNull();
            duplicated.IsNotSameReferenceAs(origin);
            duplicated.text1.Is(origin.text1);
            duplicated.text2.Is(origin.text2);
            duplicated.number.Is(origin.number);

            duplicated.innerClass.IsNotNull();
            duplicated.innerClass.IsNotSameReferenceAs(origin.innerClass);
            duplicated.innerClass.text1.Is(origin.innerClass.text1);
            duplicated.innerClass.text2.Is(origin.innerClass.text2);
            duplicated.innerClass.number.Is(origin.innerClass.number);
        }
    }
}
