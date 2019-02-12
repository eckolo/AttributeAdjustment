using Assets.Src.Domain.Model.Entity;
using Assets.Src.Domain.Service;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Assets.Editor.UnitTest.Domain.Model.Entity
{
    public class TextSetTest
    {
        static readonly View view = View.CleateNew(nameof(TextSetTest));

        [Test]
        public static void SetterTest_color_正常系()
        {
            var texts = Enumerable.Range(1, 3)
                .Select(index => $"{nameof(SetterTest_color_正常系)}_{index}");
            var position = new Vector2(2, -4);
            var color = Color.green;

            var textSet = view.SetTextSet(texts, position);
            textSet.color = color;

            textSet.IsNotNull();
            textSet.texts.IsNotNull();
            var textList = textSet.texts.ToList();
            textList.IsNotNull();
            textList[0].color.Is(color);
            textList[1].color.Is(color);
            textList[2].color.Is(color);
        }
    }
}
