using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    public partial class Configs
    {
        /// <summary>
        /// キーコンフィグ対応用可変ボタンコード
        /// </summary>
        public class Buttom
        {
            public Buttom()
            {
            }
            public Buttom(
                KeyCode key1, KeyCode key2, KeyCode key3,
                KeyCode sub, KeyCode sink, KeyCode menu,
                KeyCode up, KeyCode down, KeyCode left, KeyCode right,
                KeyCode subUp, KeyCode subDown, KeyCode subLeft, KeyCode subRight)
            {
                _key1 = key1;
                _key2 = key2;
                _key3 = key3;
                _sub = sub;
                _sink = sink;
                _menu = menu;
                _up = up;
                _down = down;
                _left = left;
                _right = right;
                _subUp = subUp;
                _subDown = subDown;
                _subLeft = subLeft;
                _subRight = subRight;
            }

            /// <summary>
            /// ボタン1
            /// </summary>
            [SerializeField]
            KeyCode _key1 = KeyCode.Z;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode key1 => _key1;
            /// <summary>
            /// ボタン2
            /// </summary>
            [SerializeField]
            KeyCode _key2 = KeyCode.X;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode key2 => _key2;
            /// <summary>
            /// ボタン3
            /// </summary>
            [SerializeField]
            KeyCode _key3 = KeyCode.C;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode key3 => _key3;
            /// <summary>
            /// サブボタン
            /// </summary>
            [SerializeField]
            KeyCode _sub = KeyCode.LeftShift;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode sub => _sub;
            /// <summary>
            /// 特殊動作ボタン
            /// </summary>
            [SerializeField]
            KeyCode _sink = KeyCode.Space;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode sink => _sink;
            /// <summary>
            /// ポーズボタン
            /// </summary>
            [SerializeField]
            KeyCode _menu = KeyCode.Escape;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode menu => _menu;
            /// <summary>
            /// ↑ボタン
            /// </summary>
            [SerializeField]
            KeyCode _up = KeyCode.UpArrow;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode up => _up;
            /// <summary>
            /// ↓ボタン
            /// </summary>
            [SerializeField]
            KeyCode _down = KeyCode.DownArrow;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode down => _down;
            /// <summary>
            /// ←ボタン
            /// </summary>
            [SerializeField]
            KeyCode _left = KeyCode.LeftArrow;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode left => _left;
            /// <summary>
            /// →ボタン
            /// </summary>
            [SerializeField]
            KeyCode _right = KeyCode.RightArrow;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode right => _right;
            /// <summary>
            /// サブ↑ボタン
            /// </summary>
            [SerializeField]
            KeyCode _subUp = KeyCode.W;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode subUp => _subUp;
            /// <summary>
            /// サブ↓ボタン
            /// </summary>
            [SerializeField]
            KeyCode _subDown = KeyCode.S;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode subDown => _subDown;
            /// <summary>
            /// サブ←ボタン
            /// </summary>
            [SerializeField]
            KeyCode _subLeft = KeyCode.A;
            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode subLeft => _subLeft;
            /// <summary>
            /// サブ→ボタン
            /// </summary>
            [SerializeField]
            KeyCode _subRight = KeyCode.D;

            /// <summary>
            /// ボタン1
            /// </summary>
            public KeyCode subRight => _subRight;

            /// <summary>
            /// 決定キーセット
            /// </summary>
            public List<KeyCode> decide => new List<KeyCode>
            {
                key1,
                KeyCode.Return,
                KeyCode.Space
            };
            /// <summary>
            /// キャンセルキーセット
            /// </summary>
            public List<KeyCode> cancel => new List<KeyCode>
            {
                key2,
                menu
            };
            /// <summary>
            /// ↑キーセット
            /// </summary>
            public List<KeyCode> ups => new List<KeyCode>
            {
                up,
                subUp
            };
            /// <summary>
            /// ↓キーセット
            /// </summary>
            public List<KeyCode> downs => new List<KeyCode>
            {
                down,
                subDown
            };
            /// <summary>
            /// 上下キーセット
            /// </summary>
            public List<KeyCode> vertical => ups.Concat(downs).ToList();
            /// <summary>
            /// ←キーセット
            /// </summary>
            public List<KeyCode> lefts => new List<KeyCode>
            {
                left,
                subLeft
            };
            /// <summary>
            /// →キーセット
            /// </summary>
            public List<KeyCode> rights => new List<KeyCode>
            {
                right,
                subRight
            };
            /// <summary>
            /// 左右キーセット
            /// </summary>
            public List<KeyCode> horizontal => lefts.Concat(rights).ToList();
        }
    }
}
