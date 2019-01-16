using Assets.Src.Domain.Model.Abstract;
using System;
using UnityEngine;

namespace Assets.Src.Mock
{
    /// <summary>
    /// ただ値を持つだけのクラスのモック
    /// </summary>
    [Serializable]
    public class ClassMock : IDuplicatable<ClassMock>, IComparable<ClassMock>
    {
        [SerializeField]
        private string _text1 = "";
        public string text1 { get { return _text1; } set { _text1 = value; } }
        [SerializeField]
        private string _text2 = "";
        public string text2 { get { return _text2; } set { _text2 = value; } }
        [SerializeField]
        private int _number = 0;
        public int number { get { return _number; } set { _number = value; } }
        [SerializeField]
        private InnerClassMock _innerClass = new InnerClassMock();
        public InnerClassMock innerClass { get { return _innerClass; } set { _innerClass = value; } }

        [Serializable]
        public class InnerClassMock
        {
            [SerializeField]
            private string _text1 = "";
            public string text1 { get { return _text1; } set { _text1 = value; } }
            [SerializeField]
            private string _text2 = "";
            public string text2 { get { return _text2; } set { _text2 = value; } }
            [SerializeField]
            private int _number = 0;
            public int number { get { return _number; } set { _number = value; } }
        }

        /// <summary>
        /// シャローコピーメソッド
        /// </summary>
        /// <returns>コピーされたオブジェクト</returns>
        public ClassMock MemberwiseClonePublic() => (ClassMock)MemberwiseClone();

        public int CompareTo(ClassMock other) => number.CompareTo(other.number);
    }
}
