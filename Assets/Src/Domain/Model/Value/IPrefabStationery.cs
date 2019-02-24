using Assets.Src.Domain.Model.Abstract;
using UnityEngine;

namespace Assets.Src.Domain.Model.Value
{
    /// <summary>
    /// 画面表示されるオブジェクトの雛形
    /// </summary>
    public interface IPrefabStationery<TPrefab> where TPrefab : PrefabAbst
    {
        /// <summary>
        /// このオブジェクトに対応する画面表示パーツ
        /// </summary>
        TPrefab entity { get; }

        /// <summary>
        /// 画面表示パーツ初期化
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="localPosition"></param>
        /// <returns>生成された画面表示パーツ</returns>
        TPrefab InitializeEntity(Component parent, Vector2 localPosition);
    }
}
