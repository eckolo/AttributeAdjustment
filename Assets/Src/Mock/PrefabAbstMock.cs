﻿using Assets.Src.Domain.Service;
using Assets.Src.View.Model.Abstract;
using UnityEngine;

namespace Assets.Src.Mock
{
    public class PrefabAbstMock : PrefabAbst
    {
        public static PrefabAbstMock Generate(string name, MonoBehaviour parent, Vector2 position = default)
        {
            var mock = new GameObject($"{nameof(PrefabAbstMock)}_{name}", typeof(PrefabAbstMock))
                .GetComponent<PrefabAbstMock>()
                .SetParent(parent);
            mock.transform.localPosition = position;
            return mock;
        }
    }
}
