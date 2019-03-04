﻿using Assets.Src.Domain.Model.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Service
{
    /// <summary> 
    /// <see cref="PrefabAbst"/>の表示操作に関するクラス
    /// </summary>
    public static class PrefabViewer
    {
        public static async Task<List<Vector2>> Move<TPrefab>(
            this TPrefab prefab,
            Vector2 targetPosition,
            int timeRequired)
            where TPrefab : PrefabAbst
        {
            var moveDiff = targetPosition - prefab.targetPosition;
            var timeRequiredPositive = timeRequired.LimitLower(1);
            prefab.targetPosition = targetPosition;

            var moveDiffs = Enumerable.Repeat(moveDiff / timeRequiredPositive, timeRequiredPositive - 1);
            var lastDiff = moveDiff - moveDiffs.Aggregate((vector1, vector2) => vector1 + vector2);

            var movingList = moveDiffs.Concat(new[] { lastDiff }).ToList();

            foreach(var diff in movingList)
            {
                prefab.position += diff;
                await Wait.Until(1);
            }

            return movingList;
        }
    }
}
