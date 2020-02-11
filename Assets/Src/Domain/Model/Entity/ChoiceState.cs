using Assets.Src.Controller;
using Assets.Src.Domain.Factory;
using Assets.Src.Domain.Model.Abstract;
using Assets.Src.Domain.Model.Value;
using Assets.Src.Domain.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Src.Domain.Model.Entity
{
    public class ChoiceState : ViewStateKey, IDisposable
    {
        public ChoiceState(IList<string> choiceList, int? choiced)
        {
            this.choiceList = choiceList;
            this.choiced = choiced;

            choiceText = choiceList.ToChoiceText(choiced);
        }

        /// <summary>
        /// 選択肢リスト
        /// </summary>
        public IList<string> choiceList { get; }
        /// <summary>
        /// 現在選択中のインデックス
        /// nullであればキャンセル
        /// </summary>
        public int? choiced
        {
            get => _choiced;
            set {
                if(value is int valueInt && (choiceList?.Any() ?? false))
                {
                    while(valueInt < 0)
                        valueInt += choiceList.Count;
                    _choiced = valueInt % choiceList.Count;
                }
                else
                {
                    _choiced = null;
                }
            }
        }
        int? _choiced = 0;
        /// <summary>
        /// ↑キー押下状態継続時間
        /// </summary>
        public int keepUpTime { get; set; } = 0;
        /// <summary>
        /// ↓キー押下状態継続時間
        /// </summary>
        public int keepDownTime { get; set; } = 0;
        /// <summary>
        /// 選択処理完了フラグ
        /// </summary>
        public bool isFinished
        {
            get => _isFinished || choiceList is null || !choiceList.Any();
            set => _isFinished = value;
        }
        bool _isFinished = false;

        public ITextMeshKey choiceText { get; set; }

        public async void Dispose() => await this.End();
    }
}
